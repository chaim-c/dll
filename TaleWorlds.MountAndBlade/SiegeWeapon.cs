using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000340 RID: 832
	public abstract class SiegeWeapon : UsableMachine, ITargetable
	{
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000B8069 File Offset: 0x000B6269
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x000B8071 File Offset: 0x000B6271
		[EditorVisibleScriptComponentVariable(false)]
		public bool ForcedUse { get; private set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000B807C File Offset: 0x000B627C
		public bool IsUsed
		{
			get
			{
				using (List<Formation>.Enumerator enumerator = base.UserFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Team.Side == this.Side)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000B80E0 File Offset: 0x000B62E0
		public void SetForcedUse(bool value)
		{
			this.ForcedUse = value;
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000B80E9 File Offset: 0x000B62E9
		public virtual BattleSideEnum Side
		{
			get
			{
				return BattleSideEnum.Attacker;
			}
		}

		// Token: 0x06002D7F RID: 11647
		public abstract SiegeEngineType GetSiegeEngineType();

		// Token: 0x06002D80 RID: 11648 RVA: 0x000B80EC File Offset: 0x000B62EC
		protected virtual bool CalculateIsSufficientlyManned(BattleSideEnum battleSide)
		{
			if (this.GetDetachmentWeightAux(battleSide) < 1f)
			{
				return true;
			}
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.Side == this.Side)
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation.CountOfUnits > 0 && base.IsUsedByFormation(formation) && (formation.Arrangement.UnitCount > 1 || (formation.Arrangement.UnitCount > 0 && !formation.HasPlayerControlledTroop)))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000B81DC File Offset: 0x000B63DC
		private bool HasNewMovingAgents()
		{
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.HasAIMovingTo && standingPoint.PreviousUserAgent != standingPoint.MovingAgent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000B8248 File Offset: 0x000B6448
		protected internal override void OnInit()
		{
			base.OnInit();
			this.ForcedUse = true;
			this._potentialUsingFormations = new List<Formation>();
			this._forcedUseFormations = new List<Formation>();
			base.GameEntity.SetAnimationSoundActivation(true);
			this._removeOnDeployEntities = Mission.Current.Scene.FindEntitiesWithTag(this.RemoveOnDeployTag).ToList<GameEntity>();
			this._addOnDeployEntities = Mission.Current.Scene.FindEntitiesWithTag(this.AddOnDeployTag).ToList<GameEntity>();
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (!(standingPoint is StandingPointWithWeaponRequirement))
				{
					standingPoint.AutoEquipWeaponsOnUseStopped = true;
				}
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
			GameEntity gameEntity = base.GameEntity.CollectChildrenEntitiesWithTag("targeting_entity").FirstOrDefault<GameEntity>();
			this._targetingPositionOffset = ((gameEntity != null) ? new Vec3?(gameEntity.GlobalPosition) : null) - (base.GameEntity.PhysicsGlobalBoxMax + base.GameEntity.PhysicsGlobalBoxMin) * 0.5f;
			GameEntity gameEntity2 = base.GameEntity.CollectChildrenEntitiesWithTag("targeting_entity").FirstOrDefault<GameEntity>();
			this._targetingPositionOffset = ((gameEntity2 != null) ? new Vec3?(gameEntity2.GlobalPosition) : null) - (base.GameEntity.PhysicsGlobalBoxMax + base.GameEntity.PhysicsGlobalBoxMin) * 0.5f;
			this.EnemyRangeToStopUsing = 5f;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000B8430 File Offset: 0x000B6630
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents() && !GameNetwork.IsClientOrReplay)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000B8458 File Offset: 0x000B6658
		private void TickAux(bool isParallel)
		{
			if (!GameNetwork.IsClientOrReplay && base.GameEntity.IsVisibleIncludeParents())
			{
				if (this.IsDisabledForBattleSide(this.Side))
				{
					using (List<StandingPoint>.Enumerator enumerator = base.StandingPoints.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StandingPoint standingPoint = enumerator.Current;
							Agent userAgent = standingPoint.UserAgent;
							if (userAgent != null && !userAgent.IsPlayerControlled && userAgent.Formation != null && userAgent.Formation.Team.Side == this.Side)
							{
								if (isParallel)
								{
									this._needsSingleThreadTickOnce = true;
								}
								else
								{
									userAgent.Formation.StopUsingMachine(this, false);
									this._forcedUseFormations.Remove(userAgent.Formation);
									this._isValidated = false;
								}
							}
						}
						return;
					}
				}
				if (this.ForcedUse)
				{
					bool flag = false;
					foreach (Team team in Mission.Current.Teams)
					{
						if (team.Side == this.Side)
						{
							if (!this.CalculateIsSufficientlyManned(team.Side))
							{
								foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
								{
									if (formation.CountOfUnits > 0 && formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat && (formation.Arrangement.UnitCount > 1 || (formation.Arrangement.UnitCount > 0 && !formation.HasPlayerControlledTroop)) && !formation.Detachments.Contains(this))
									{
										if (isParallel)
										{
											this._needsSingleThreadTickOnce = true;
										}
										else
										{
											this._potentialUsingFormations.Add(formation);
										}
									}
								}
								this._areMovingAgentsProcessed = false;
							}
							else if (this.HasNewMovingAgents())
							{
								if (!this._areMovingAgentsProcessed)
								{
									float num = float.MaxValue;
									Formation formation2 = null;
									foreach (Formation formation3 in team.FormationsIncludingSpecialAndEmpty)
									{
										if (formation3.CountOfUnits > 0 && formation3.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat && (formation3.Arrangement.UnitCount > 1 || (formation3.Arrangement.UnitCount > 0 && !formation3.HasPlayerControlledTroop)))
										{
											WorldPosition medianPosition = formation3.QuerySystem.MedianPosition;
											Vec3 vec = base.GameEntity.GlobalPosition;
											float num2 = medianPosition.DistanceSquaredWithLimit(vec, 10000f);
											if (num2 < num)
											{
												num = num2;
												formation2 = formation3;
											}
										}
									}
									if (formation2 != null && !base.IsUsedByFormation(formation2))
									{
										if (isParallel)
										{
											this._needsSingleThreadTickOnce = true;
										}
										else
										{
											this._potentialUsingFormations.Clear();
											this._potentialUsingFormations.Add(formation2);
											flag = true;
											this._areMovingAgentsProcessed = true;
										}
									}
									else
									{
										this._areMovingAgentsProcessed = true;
									}
								}
							}
							else
							{
								this._areMovingAgentsProcessed = false;
							}
							if (flag)
							{
								this._potentialUsingFormations[0].StartUsingMachine(this, !this._potentialUsingFormations[0].IsAIControlled);
								this._forcedUseFormations.Add(this._potentialUsingFormations[0]);
								this._potentialUsingFormations.Clear();
								this._isValidated = false;
								flag = false;
							}
							else if (this._potentialUsingFormations.Count > 0)
							{
								float num3 = float.MaxValue;
								Formation formation4 = null;
								foreach (Formation formation5 in this._potentialUsingFormations)
								{
									Vec2 averagePosition = formation5.QuerySystem.AveragePosition;
									Vec3 vec = base.GameEntity.GlobalPosition;
									float num4 = averagePosition.DistanceSquared(vec.AsVec2);
									if (num4 < num3)
									{
										num3 = num4;
										formation4 = formation5;
									}
								}
								int count = base.StandingPoints.Count;
								int num5 = 0;
								Formation formation6 = null;
								Vec2 vec2 = Vec2.Zero;
								for (int i = 0; i < count; i++)
								{
									Agent previousUserAgent = base.StandingPoints[i].PreviousUserAgent;
									if (previousUserAgent != null)
									{
										if (!previousUserAgent.IsActive() || previousUserAgent.Formation == null || (formation6 != null && previousUserAgent.Formation != formation6))
										{
											num5 = -1;
											break;
										}
										num5++;
										Vec2 v = vec2;
										Vec3 vec = previousUserAgent.Position;
										vec2 = v + vec.AsVec2;
										formation6 = previousUserAgent.Formation;
									}
								}
								Formation formation7 = formation4;
								if (num5 > 0 && this._potentialUsingFormations.Contains(formation6))
								{
									vec2 *= 1f / (float)num5;
									Vec3 vec = base.GameEntity.GlobalPosition;
									if (vec2.DistanceSquared(vec.AsVec2) < num3)
									{
										formation7 = formation6;
									}
								}
								formation7.StartUsingMachine(this, !formation7.IsAIControlled);
								this._forcedUseFormations.Add(formation7);
								this._potentialUsingFormations.Clear();
								this._isValidated = false;
							}
							else if (!this._isValidated)
							{
								if (!this.HasToBeDefendedByUser(team.Side) && this.GetDetachmentWeightAux(team.Side) == -3.4028235E+38f)
								{
									for (int j = this._forcedUseFormations.Count - 1; j >= 0; j--)
									{
										Formation formation8 = this._forcedUseFormations[j];
										if (formation8.Team.Side == this.Side && !this.IsAnyUserBelongsToFormation(formation8))
										{
											if (isParallel)
											{
												if (base.IsUsedByFormation(formation8))
												{
													this._needsSingleThreadTickOnce = true;
													break;
												}
												this._forcedUseFormations.Remove(formation8);
											}
											else
											{
												if (base.IsUsedByFormation(formation8))
												{
													formation8.StopUsingMachine(this, !formation8.IsAIControlled);
												}
												this._forcedUseFormations.Remove(formation8);
											}
										}
									}
									if (isParallel && this._needsSingleThreadTickOnce)
									{
										break;
									}
								}
								if (!isParallel)
								{
									this._isValidated = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000B8AAC File Offset: 0x000B6CAC
		protected virtual bool IsAnyUserBelongsToFormation(Formation formation)
		{
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.UserAgent != null && standingPoint.UserAgent.Formation == formation)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000B8B18 File Offset: 0x000B6D18
		protected internal override void OnTickParallel(float dt)
		{
			this.TickAux(true);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000B8B21 File Offset: 0x000B6D21
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				this.TickAux(false);
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000B8B40 File Offset: 0x000B6D40
		public void TickAuxForInit()
		{
			this.TickAux(false);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000B8B4C File Offset: 0x000B6D4C
		protected internal virtual void OnDeploymentStateChanged(bool isDeployed)
		{
			foreach (GameEntity gameEntity in this._removeOnDeployEntities)
			{
				gameEntity.SetVisibilityExcludeParents(!isDeployed);
				StrategicArea firstScriptOfType = gameEntity.GetFirstScriptOfType<StrategicArea>();
				if (firstScriptOfType != null)
				{
					firstScriptOfType.OnParentGameEntityVisibilityChanged(!isDeployed);
				}
				else
				{
					foreach (StrategicArea strategicArea in from c in gameEntity.GetChildren()
					where c.HasScriptOfType<StrategicArea>()
					select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea.OnParentGameEntityVisibilityChanged(!isDeployed);
					}
				}
			}
			foreach (GameEntity gameEntity2 in this._addOnDeployEntities)
			{
				gameEntity2.SetVisibilityExcludeParents(isDeployed);
				MissionObject firstScriptOfType2 = gameEntity2.GetFirstScriptOfType<MissionObject>();
				if (firstScriptOfType2 != null)
				{
					firstScriptOfType2.SetAbilityOfFaces(isDeployed);
				}
				StrategicArea firstScriptOfType3 = gameEntity2.GetFirstScriptOfType<StrategicArea>();
				if (firstScriptOfType3 != null)
				{
					firstScriptOfType3.OnParentGameEntityVisibilityChanged(isDeployed);
				}
				else
				{
					foreach (StrategicArea strategicArea2 in from c in gameEntity2.GetChildren()
					where c.HasScriptOfType<StrategicArea>()
					select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea2.OnParentGameEntityVisibilityChanged(isDeployed);
					}
				}
			}
			if (this._addOnDeployEntities.Count > 0 || this._removeOnDeployEntities.Count > 0)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					standingPoint.RefreshGameEntityWithWorldPosition();
				}
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000B8D98 File Offset: 0x000B6F98
		public override bool HasWaitFrame
		{
			get
			{
				return base.HasWaitFrame && (!(this is IPrimarySiegeWeapon) || !(this as IPrimarySiegeWeapon).HasCompletedAction());
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000B8DBC File Offset: 0x000B6FBC
		public override bool IsDeactivated
		{
			get
			{
				return base.IsDisabled || base.GameEntity == null || !base.GameEntity.IsVisibleIncludeParents() || base.IsDeactivated;
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000B8DE9 File Offset: 0x000B6FE9
		public override bool ShouldAutoLeaveDetachmentWhenDisabled(BattleSideEnum sideEnum)
		{
			return this.AutoAttachUserToFormation(sideEnum);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000B8DF2 File Offset: 0x000B6FF2
		public override bool AutoAttachUserToFormation(BattleSideEnum sideEnum)
		{
			return base.Ai.HasActionCompleted || !base.IsDisabledDueToEnemyInRange(sideEnum);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000B8E0D File Offset: 0x000B700D
		public override bool HasToBeDefendedByUser(BattleSideEnum sideEnum)
		{
			return !base.Ai.HasActionCompleted && base.IsDisabledDueToEnemyInRange(sideEnum);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000B8E28 File Offset: 0x000B7028
		protected float GetUserMultiplierOfWeapon()
		{
			int userCountIncludingInStruckAction = base.UserCountIncludingInStruckAction;
			if (userCountIncludingInStruckAction == 0)
			{
				return 0.1f;
			}
			return 0.7f + 0.3f * (float)userCountIncludingInStruckAction / (float)this.MaxUserCount;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000B8E5B File Offset: 0x000B705B
		protected virtual float GetDistanceMultiplierOfWeapon(Vec3 weaponPos)
		{
			if (this.GetMinimumDistanceBetweenPositions(weaponPos) > 20f)
			{
				return 0.4f;
			}
			Debug.FailedAssert("Invalid weapon type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SiegeWeapon.cs", "GetDistanceMultiplierOfWeapon", 549);
			return 1f;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000B8E90 File Offset: 0x000B7090
		protected virtual float GetMinimumDistanceBetweenPositions(Vec3 position)
		{
			return base.GameEntity.GlobalPosition.DistanceSquared(position);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000B8EB4 File Offset: 0x000B70B4
		protected float GetHitPointMultiplierOfWeapon()
		{
			if (base.DestructionComponent != null)
			{
				return MathF.Max(1f, 2f - MathF.Log10(base.DestructionComponent.HitPoint / base.DestructionComponent.MaxHitPoint * 10f + 1f));
			}
			return 1f;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000B8F07 File Offset: 0x000B7107
		public GameEntity GetTargetEntity()
		{
			return base.GameEntity;
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000B8F0F File Offset: 0x000B710F
		public Vec3 GetTargetingOffset()
		{
			if (this._targetingPositionOffset != null)
			{
				return this._targetingPositionOffset.Value;
			}
			return Vec3.Zero;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000B8F2F File Offset: 0x000B712F
		public BattleSideEnum GetSide()
		{
			return this.Side;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000B8F37 File Offset: 0x000B7137
		public GameEntity Entity()
		{
			return base.GameEntity;
		}

		// Token: 0x06002D97 RID: 11671
		public abstract TargetFlags GetTargetFlags();

		// Token: 0x06002D98 RID: 11672
		public abstract float GetTargetValue(List<Vec3> weaponPos);

		// Token: 0x040012EA RID: 4842
		private const string TargetingEntityTag = "targeting_entity";

		// Token: 0x040012EB RID: 4843
		[EditableScriptComponentVariable(true)]
		internal string RemoveOnDeployTag = "";

		// Token: 0x040012EC RID: 4844
		[EditableScriptComponentVariable(true)]
		internal string AddOnDeployTag = "";

		// Token: 0x040012ED RID: 4845
		private List<GameEntity> _addOnDeployEntities;

		// Token: 0x040012EF RID: 4847
		protected bool _spawnedFromSpawner;

		// Token: 0x040012F0 RID: 4848
		private List<GameEntity> _removeOnDeployEntities;

		// Token: 0x040012F1 RID: 4849
		private List<Formation> _potentialUsingFormations;

		// Token: 0x040012F2 RID: 4850
		private List<Formation> _forcedUseFormations;

		// Token: 0x040012F3 RID: 4851
		private bool _needsSingleThreadTickOnce;

		// Token: 0x040012F4 RID: 4852
		private bool _areMovingAgentsProcessed;

		// Token: 0x040012F5 RID: 4853
		private bool _isValidated;

		// Token: 0x040012F6 RID: 4854
		private Vec3? _targetingPositionOffset;
	}
}
