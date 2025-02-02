using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.DividableTasks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200015B RID: 347
	public abstract class RangedSiegeWeaponAi : UsableMachineAIBase
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x0003789C File Offset: 0x00035A9C
		public RangedSiegeWeaponAi(RangedSiegeWeapon rangedSiegeWeapon) : base(rangedSiegeWeapon)
		{
			this._threatSeeker = new RangedSiegeWeaponAi.ThreatSeeker(rangedSiegeWeapon);
			((RangedSiegeWeapon)this.UsableMachine).OnReloadDone += this.FindNextTarget;
			this._delayTimer = this._delayDuration;
			this._targetEvaluationTimer = new Timer(Mission.Current.CurrentTime, 0.5f, true);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003790C File Offset: 0x00035B0C
		protected override void OnTick(Agent agentToCompareTo, Formation formationToCompareTo, Team potentialUsersTeam, float dt)
		{
			base.OnTick(agentToCompareTo, formationToCompareTo, potentialUsersTeam, dt);
			if (this.UsableMachine.PilotAgent != null && this.UsableMachine.PilotAgent.IsAIControlled)
			{
				RangedSiegeWeapon rangedSiegeWeapon = this.UsableMachine as RangedSiegeWeapon;
				if (rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.WaitingAfterShooting && rangedSiegeWeapon.PilotAgent != null && rangedSiegeWeapon.PilotAgent.IsAIControlled)
				{
					rangedSiegeWeapon.AiRequestsManualReload();
				}
				if (this._threatSeeker.UpdateThreatSeekerTask() && dt > 0f && this._target == null && rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.Idle)
				{
					if (this._delayTimer <= 0f)
					{
						this.FindNextTarget();
					}
					this._delayTimer -= dt;
				}
				if (this._target != null)
				{
					if (this._target.Agent != null && !this._target.Agent.IsActive())
					{
						this._target = null;
						return;
					}
					if (rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.Idle && rangedSiegeWeapon.UserCountNotInStruckAction > 0)
					{
						if (DebugSiegeBehavior.ToggleTargetDebug)
						{
							Agent pilotAgent = this.UsableMachine.PilotAgent;
						}
						if (this._targetEvaluationTimer.Check(Mission.Current.CurrentTime) && !((RangedSiegeWeapon)this.UsableMachine).CanShootAtBox(this._target.BoundingBoxMin, this._target.BoundingBoxMax, 5U))
						{
							this._cannotShootCounter++;
						}
						if (this._cannotShootCounter < 4)
						{
							if (rangedSiegeWeapon.AimAtThreat(this._target) && rangedSiegeWeapon.PilotAgent != null)
							{
								this._delayTimer -= dt;
								if (this._delayTimer <= 0f)
								{
									rangedSiegeWeapon.AiRequestsShoot();
									this._target = null;
									this.SetTargetingTimer();
									this._cannotShootCounter = 0;
									this._targetEvaluationTimer.Reset(Mission.Current.CurrentTime);
								}
							}
						}
						else
						{
							this._target = null;
							this.SetTargetingTimer();
							this._cannotShootCounter = 0;
						}
					}
					else
					{
						this._targetEvaluationTimer.Reset(Mission.Current.CurrentTime);
					}
				}
			}
			this.AfterTick(agentToCompareTo, formationToCompareTo, potentialUsersTeam, dt);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00037B10 File Offset: 0x00035D10
		private void SetTargetFromThreatSeeker()
		{
			this._target = this._threatSeeker.PrepareTargetFromTask();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00037B23 File Offset: 0x00035D23
		public void FindNextTarget()
		{
			if (this.UsableMachine.PilotAgent != null && this.UsableMachine.PilotAgent.IsAIControlled)
			{
				this._threatSeeker.PrepareThreatSeekerTask(new Action(this.SetTargetFromThreatSeeker));
				this.SetTargetingTimer();
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00037B64 File Offset: 0x00035D64
		private void AfterTick(Agent agentToCompareTo, Formation formationToCompareTo, Team potentialUsersTeam, float dt)
		{
			if ((dt <= 0f || (agentToCompareTo != null && this.UsableMachine.PilotAgent != agentToCompareTo) || (formationToCompareTo != null && (this.UsableMachine.PilotAgent == null || !this.UsableMachine.PilotAgent.IsAIControlled || this.UsableMachine.PilotAgent.Formation != formationToCompareTo))) && this.UsableMachine.PilotAgent == null)
			{
				this._threatSeeker.Release();
				this._target = null;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00037BDF File Offset: 0x00035DDF
		private void SetTargetingTimer()
		{
			this._delayTimer = this._delayDuration + MBRandom.RandomFloat * 0.5f;
		}

		// Token: 0x04000462 RID: 1122
		private const float TargetEvaluationDelay = 0.5f;

		// Token: 0x04000463 RID: 1123
		private const int MaxTargetEvaluationCount = 4;

		// Token: 0x04000464 RID: 1124
		private readonly RangedSiegeWeaponAi.ThreatSeeker _threatSeeker;

		// Token: 0x04000465 RID: 1125
		private Threat _target;

		// Token: 0x04000466 RID: 1126
		private float _delayTimer;

		// Token: 0x04000467 RID: 1127
		private float _delayDuration = 1f;

		// Token: 0x04000468 RID: 1128
		private int _cannotShootCounter;

		// Token: 0x04000469 RID: 1129
		private readonly Timer _targetEvaluationTimer;

		// Token: 0x02000457 RID: 1111
		public class ThreatSeeker
		{
			// Token: 0x06003507 RID: 13575 RVA: 0x000D8EA8 File Offset: 0x000D70A8
			public ThreatSeeker(RangedSiegeWeapon weapon)
			{
				this.Weapon = weapon;
				this.WeaponPositions = new List<Vec3>
				{
					this.Weapon.GameEntity.GlobalPosition
				};
				this._targetAgent = null;
				IEnumerable<UsableMachine> source = Mission.Current.ActiveMissionObjects.FindAllWithType<UsableMachine>();
				this._potentialTargetUsableMachines = (from um in source.WhereQ(delegate(UsableMachine um)
				{
					ITargetable targetable;
					return um.GameEntity.HasScriptOfType<DestructableComponent>() && (targetable = (um as ITargetable)) != null && targetable.GetSide() != this.Weapon.Side && targetable.GetTargetEntity() != null;
				})
				select um as ITargetable).ToList<ITargetable>();
				this._referencePositions = source.OfType<ICastleKeyPosition>().ToList<ICastleKeyPosition>();
				this._getMostDangerousThreat = new FindMostDangerousThreat(null);
			}

			// Token: 0x06003508 RID: 13576 RVA: 0x000D8F58 File Offset: 0x000D7158
			public Threat PrepareTargetFromTask()
			{
				Agent agent2;
				this._currentThreat = this._getMostDangerousThreat.GetResult(out agent2);
				if (this._currentThreat != null && this._currentThreat.WeaponEntity == null)
				{
					this._currentThreat.Agent = this._targetAgent;
					if (this._targetAgent == null || !this._targetAgent.IsActive() || this._targetAgent.Formation != this._currentThreat.Formation || !this.Weapon.CanShootAtAgent(this._targetAgent))
					{
						this._targetAgent = agent2;
						float selectedAgentScore = float.MaxValue;
						Agent selectedAgent = this._targetAgent;
						Action<Agent> action = delegate(Agent agent)
						{
							float num = agent.Position.DistanceSquared(this.Weapon.GameEntity.GlobalPosition) * (MBRandom.RandomFloat * 0.2f + 0.8f);
							if (agent == this._targetAgent)
							{
								num *= 0.5f;
							}
							if (selectedAgentScore > num && this.Weapon.CanShootAtAgent(agent))
							{
								selectedAgent = agent;
								selectedAgentScore = num;
							}
						};
						if (agent2.Detachment == null)
						{
							this._currentThreat.Formation.ApplyActionOnEachAttachedUnit(action);
						}
						else
						{
							this._currentThreat.Formation.ApplyActionOnEachDetachedUnit(action);
						}
						this._targetAgent = (selectedAgent ?? this._currentThreat.Formation.GetUnitWithIndex(MBRandom.RandomInt(this._currentThreat.Formation.CountOfUnits)));
						this._currentThreat.Agent = this._targetAgent;
					}
				}
				if (this._currentThreat != null && this._currentThreat.WeaponEntity == null && this._currentThreat.Agent == null)
				{
					this._currentThreat = null;
				}
				return this._currentThreat;
			}

			// Token: 0x06003509 RID: 13577 RVA: 0x000D90C0 File Offset: 0x000D72C0
			public bool UpdateThreatSeekerTask()
			{
				Agent targetAgent = this._targetAgent;
				if (targetAgent != null && !targetAgent.IsActive())
				{
					this._targetAgent = null;
				}
				return this._getMostDangerousThreat.Update();
			}

			// Token: 0x0600350A RID: 13578 RVA: 0x000D90EB File Offset: 0x000D72EB
			public void PrepareThreatSeekerTask(Action lastAction)
			{
				this._getMostDangerousThreat.Prepare(this.GetAllThreats(), this.Weapon);
				this._getMostDangerousThreat.SetLastAction(lastAction);
			}

			// Token: 0x0600350B RID: 13579 RVA: 0x000D9110 File Offset: 0x000D7310
			public void Release()
			{
				this._targetAgent = null;
				this._currentThreat = null;
			}

			// Token: 0x0600350C RID: 13580 RVA: 0x000D9120 File Offset: 0x000D7320
			public List<Threat> GetAllThreats()
			{
				List<Threat> list = new List<Threat>();
				for (int i = this._potentialTargetUsableMachines.Count - 1; i >= 0; i--)
				{
					ITargetable targetable = this._potentialTargetUsableMachines[i];
					UsableMachine usableMachine;
					if ((usableMachine = (targetable as UsableMachine)) != null && (usableMachine.IsDestroyed || usableMachine.IsDeactivated || usableMachine.GameEntity == null))
					{
						this._potentialTargetUsableMachines.RemoveAt(i);
					}
					else
					{
						Threat item = new Threat
						{
							WeaponEntity = targetable,
							ThreatValue = this.Weapon.ProcessTargetValue(targetable.GetTargetValue(this.WeaponPositions), targetable.GetTargetFlags())
						};
						list.Add(item);
					}
				}
				foreach (Team team in Mission.Current.Teams)
				{
					if (team.Side.GetOppositeSide() == this.Weapon.Side)
					{
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0)
							{
								float targetValueOfFormation = RangedSiegeWeaponAi.ThreatSeeker.GetTargetValueOfFormation(formation, this._referencePositions);
								if (targetValueOfFormation != -1f)
								{
									list.Add(new Threat
									{
										Formation = formation,
										ThreatValue = this.Weapon.ProcessTargetValue(targetValueOfFormation, RangedSiegeWeaponAi.ThreatSeeker.GetTargetFlagsOfFormation())
									});
								}
							}
						}
					}
				}
				return list;
			}

			// Token: 0x0600350D RID: 13581 RVA: 0x000D92C0 File Offset: 0x000D74C0
			private static float GetTargetValueOfFormation(Formation formation, IEnumerable<ICastleKeyPosition> referencePositions)
			{
				if (formation.QuerySystem.LocalEnemyPower / formation.QuerySystem.LocalAllyPower > 0.5f)
				{
					return -1f;
				}
				float num = (float)formation.CountOfUnits * 3f;
				if (TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.4f))
				{
					num *= 3f;
				}
				num *= RangedSiegeWeaponAi.ThreatSeeker.GetPositionMultiplierOfFormation(formation, referencePositions);
				float num2 = MBMath.ClampFloat(formation.QuerySystem.LocalAllyPower / (formation.QuerySystem.LocalEnemyPower + 0.01f), 0f, 5f) / 5f;
				return num * num2;
			}

			// Token: 0x0600350E RID: 13582 RVA: 0x000D9357 File Offset: 0x000D7557
			public static TargetFlags GetTargetFlagsOfFormation()
			{
				return TargetFlags.None | TargetFlags.IsMoving | TargetFlags.IsFlammable | TargetFlags.IsAttacker;
			}

			// Token: 0x0600350F RID: 13583 RVA: 0x000D9364 File Offset: 0x000D7564
			private static float GetPositionMultiplierOfFormation(Formation formation, IEnumerable<ICastleKeyPosition> referencePositions)
			{
				ICastleKeyPosition castleKeyPosition;
				float minimumDistanceBetweenPositions = RangedSiegeWeaponAi.ThreatSeeker.GetMinimumDistanceBetweenPositions(formation.GetMedianAgent(false, false, formation.GetAveragePositionOfUnits(false, false)).Position, referencePositions, out castleKeyPosition);
				bool flag = castleKeyPosition.AttackerSiegeWeapon != null && castleKeyPosition.AttackerSiegeWeapon.HasCompletedAction();
				float num;
				if (formation.PhysicalClass.IsRanged())
				{
					if (minimumDistanceBetweenPositions < 20f)
					{
						num = 1f;
					}
					else if (minimumDistanceBetweenPositions < 35f)
					{
						num = 0.8f;
					}
					else
					{
						num = 0.6f;
					}
					return num + (flag ? 0.2f : 0f);
				}
				if (minimumDistanceBetweenPositions < 15f)
				{
					num = 0.2f;
				}
				else if (minimumDistanceBetweenPositions < 40f)
				{
					num = 0.15f;
				}
				else
				{
					num = 0.12f;
				}
				return num * (flag ? 7.5f : 1f);
			}

			// Token: 0x06003510 RID: 13584 RVA: 0x000D9424 File Offset: 0x000D7624
			private static float GetMinimumDistanceBetweenPositions(Vec3 position, IEnumerable<ICastleKeyPosition> referencePositions, out ICastleKeyPosition closestCastlePosition)
			{
				if (referencePositions != null && referencePositions.Count<ICastleKeyPosition>() != 0)
				{
					closestCastlePosition = referencePositions.MinBy((ICastleKeyPosition rp) => rp.GetPosition().DistanceSquared(position));
					return MathF.Sqrt(closestCastlePosition.GetPosition().DistanceSquared(position));
				}
				closestCastlePosition = null;
				return -1f;
			}

			// Token: 0x06003511 RID: 13585 RVA: 0x000D9480 File Offset: 0x000D7680
			public static Threat GetMaxThreat(List<ICastleKeyPosition> castleKeyPositions)
			{
				List<ITargetable> list = new List<ITargetable>();
				List<Threat> list2 = new List<Threat>();
				using (IEnumerator<GameEntity> enumerator = (from amo in Mission.Current.ActiveMissionObjects
				select amo.GameEntity).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ITargetable item;
						if ((item = (enumerator.Current.GetFirstScriptOfType<UsableMachine>() as ITargetable)) != null)
						{
							list.Add(item);
						}
					}
				}
				list.RemoveAll((ITargetable um) => um.GetSide() == BattleSideEnum.Defender);
				list2.AddRange(list.Select(delegate(ITargetable um)
				{
					Threat threat = new Threat();
					threat.WeaponEntity = um;
					threat.ThreatValue = um.GetTargetValue((from c in castleKeyPositions
					select c.GetPosition()).ToList<Vec3>());
					return threat;
				}));
				return list2.MaxBy((Threat t) => t.ThreatValue);
			}

			// Token: 0x0400191B RID: 6427
			private FindMostDangerousThreat _getMostDangerousThreat;

			// Token: 0x0400191C RID: 6428
			private const float SingleUnitThreatValue = 3f;

			// Token: 0x0400191D RID: 6429
			private const float InsideWallsThreatMultiplier = 3f;

			// Token: 0x0400191E RID: 6430
			private Threat _currentThreat;

			// Token: 0x0400191F RID: 6431
			private Agent _targetAgent;

			// Token: 0x04001920 RID: 6432
			public RangedSiegeWeapon Weapon;

			// Token: 0x04001921 RID: 6433
			public List<Vec3> WeaponPositions;

			// Token: 0x04001922 RID: 6434
			private readonly List<ITargetable> _potentialTargetUsableMachines;

			// Token: 0x04001923 RID: 6435
			private readonly List<ICastleKeyPosition> _referencePositions;
		}
	}
}
