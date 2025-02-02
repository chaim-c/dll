using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000162 RID: 354
	public class StrategicArea : MissionObject, IDetachment
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00038AB7 File Offset: 0x00036CB7
		public bool IsLoose
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00038ABA File Offset: 0x00036CBA
		public MBReadOnlyList<Formation> UserFormations
		{
			get
			{
				return this._userFormations;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00038AC2 File Offset: 0x00036CC2
		public float DistanceToCheck
		{
			get
			{
				return this._distanceToCheck;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00038ACA File Offset: 0x00036CCA
		public bool IgnoreHeight
		{
			get
			{
				return this._ignoreHeight;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00038AD2 File Offset: 0x00036CD2
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00038ADC File Offset: 0x00036CDC
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					List<Team> list = (from t in Mission.Current.Teams
					where this.IsUsableBy(t.Side)
					select t).ToList<Team>();
					this._isActive = value;
					foreach (Team team in list)
					{
						if (team.TeamAI != null)
						{
							if (this._isActive)
							{
								team.TeamAI.AddStrategicArea(this);
							}
							else
							{
								team.TeamAI.RemoveStrategicArea(this);
							}
						}
					}
				}
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00038B7C File Offset: 0x00036D7C
		protected internal override void OnInit()
		{
			base.OnInit();
			this._agents = new List<Agent>();
			this._userFormations = new MBList<Formation>();
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			this._frame = new WorldFrame(globalFrame.rotation, new WorldPosition(base.Scene, UIntPtr.Zero, globalFrame.origin, false));
			this._frame.Rotation.Orthonormalize();
			this._unitSpacing = ArrangementOrder.GetUnitSpacingOf(ArrangementOrder.ArrangementOrderEnum.Line);
			this._capacity = this.CalculateCapacity();
			this._simulationFormations = new Dictionary<Formation, Formation>();
			this._isActive = true;
			for (int i = 0; i < 5; i++)
			{
				this._strategicAreaSidesScoreTally[i] = new StrategicArea.StrategicAreaMutableTuple(0, 0);
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00038C2E File Offset: 0x00036E2E
		private int CalculateCapacity()
		{
			return MathF.Max(1, MathF.Ceiling(MathF.Max(1f, this._width) * MathF.Max(1f, this._depth)));
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00038C5C File Offset: 0x00036E5C
		public Vec3 GetGroundPosition()
		{
			return this._frame.Origin.GetGroundVec3();
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00038C70 File Offset: 0x00036E70
		public void DetermineAssociatedDestructibleComponents(IEnumerable<DestructableComponent> destructibleComponents)
		{
			this._nearbyDestructibleObjects = new List<DestructableComponent>();
			foreach (DestructableComponent destructableComponent in destructibleComponents)
			{
				destructableComponent.GameEntity.GetGlobalFrame();
				Vec3 v;
				Vec3 v2;
				destructableComponent.GameEntity.GetPhysicsMinMax(true, out v, out v2, false);
				if (((v2 + v) * 0.5f).DistanceSquared(base.GameEntity.GlobalPosition) <= 9f)
				{
					this._nearbyDestructibleObjects.Add(destructableComponent);
				}
			}
			foreach (DestructableComponent destructableComponent2 in this._nearbyDestructibleObjects)
			{
				destructableComponent2.OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnCoveringDestructibleObjectDestroyed);
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00038D60 File Offset: 0x00036F60
		public void OnParentGameEntityVisibilityChanged(bool isVisible)
		{
			this.IsActive = isVisible;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00038D69 File Offset: 0x00036F69
		private void OnCoveringDestructibleObjectDestroyed(DestructableComponent destroyedComponent, Agent destroyerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			this.IsActive = false;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00038D74 File Offset: 0x00036F74
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			foreach (DestructableComponent destructableComponent in this._nearbyDestructibleObjects)
			{
				destructableComponent.OnDestroyed -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnCoveringDestructibleObjectDestroyed);
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00038DD8 File Offset: 0x00036FD8
		public void InitializeAutogenerated(float width, int capacity, BattleSideEnum side)
		{
			this._width = width;
			this._capacity = capacity;
			this._side = side;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00038DF0 File Offset: 0x00036FF0
		public void AddAgent(Agent agent, int slotIndex)
		{
			this._agents.Add(agent);
			if (this._capacity == 1 && this._centerPosition == null)
			{
				this._centerPosition = new WorldPosition?(this._frame.Origin);
				Mat3 identity = Mat3.Identity;
				identity.f = base.GameEntity.GetGlobalFrame().rotation.f;
				identity.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				this._cachedWorldFrame = new WorldFrame(identity, this._centerPosition.Value);
			}
			agent.SetPreciseRangedAimingEnabled(true);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00038E7C File Offset: 0x0003707C
		public void AddAgentAtSlotIndex(Agent agent, int slotIndex)
		{
			this.AddAgent(agent, slotIndex);
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.DetachUnit(agent, true);
			}
			agent.Detachment = this;
			agent.DetachmentWeight = 1f;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00038EAB File Offset: 0x000370AB
		void IDetachment.FormationStartUsing(Formation formation)
		{
			this._userFormations.Add(formation);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00038EB9 File Offset: 0x000370B9
		void IDetachment.FormationStopUsing(Formation formation)
		{
			this._userFormations.Remove(formation);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00038EC8 File Offset: 0x000370C8
		public bool IsUsedByFormation(Formation formation)
		{
			return this._userFormations.Contains(formation);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00038ED6 File Offset: 0x000370D6
		Agent IDetachment.GetMovingAgentAtSlotIndex(int slotIndex)
		{
			return null;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00038EDC File Offset: 0x000370DC
		void IDetachment.GetSlotIndexWeightTuples(List<ValueTuple<int, float>> slotIndexWeightTuples)
		{
			for (int i = this._agents.Count; i < this._capacity; i++)
			{
				slotIndexWeightTuples.Add(new ValueTuple<int, float>(i, StrategicArea.CalculateWeight(this._capacity, i)));
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00038F1C File Offset: 0x0003711C
		bool IDetachment.IsSlotAtIndexAvailableForAgent(int slotIndex, Agent agent)
		{
			return agent.CanBeAssignedForScriptedMovement() && slotIndex < this._capacity && slotIndex >= this._agents.Count && this.IsAgentEligible(agent) && !this.IsAgentOnInconvenientNavmesh(agent);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00038F54 File Offset: 0x00037154
		private bool IsAgentOnInconvenientNavmesh(Agent agent)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (agent.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				if (teamAISiegeComponent is TeamAISiegeAttacker && currentNavigationFaceId % 10 == 1)
				{
					return true;
				}
				if (teamAISiegeComponent is TeamAISiegeDefender && currentNavigationFaceId % 10 != 1)
				{
					return true;
				}
				foreach (int num in teamAISiegeComponent.DifficultNavmeshIDs)
				{
					if (currentNavigationFaceId == num)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00038FFC File Offset: 0x000371FC
		public bool IsAgentEligible(Agent agent)
		{
			return agent.IsRangedCached;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00039004 File Offset: 0x00037204
		void IDetachment.UnmarkDetachment()
		{
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00039006 File Offset: 0x00037206
		bool IDetachment.IsDetachmentRecentlyEvaluated()
		{
			return false;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00039009 File Offset: 0x00037209
		void IDetachment.MarkSlotAtIndex(int slotIndex)
		{
			Debug.FailedAssert("This should never have been called because this detachment does not seek to replace moving agents.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\StrategicArea.cs", "MarkSlotAtIndex", 323);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00039024 File Offset: 0x00037224
		bool IDetachment.IsAgentUsingOrInterested(Agent agent)
		{
			return this._agents.Contains(agent);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00039034 File Offset: 0x00037234
		void IDetachment.OnFormationLeave(Formation formation)
		{
			for (int i = this._agents.Count - 1; i >= 0; i--)
			{
				Agent agent = this._agents[i];
				if (agent.Formation == formation && !agent.IsPlayerControlled)
				{
					((IDetachment)this).RemoveAgent(agent);
					formation.AttachUnit(agent);
				}
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00039085 File Offset: 0x00037285
		public bool IsStandingPointAvailableForAgent(Agent agent)
		{
			return this._agents.Count < this._capacity;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003909C File Offset: 0x0003729C
		public List<float> GetTemplateCostsOfAgent(Agent candidate, List<float> oldValue)
		{
			WorldPosition worldPosition = candidate.GetWorldPosition();
			float num = candidate.Mission.Scene.DoesPathExistBetweenPositions(worldPosition, this._frame.Origin) ? worldPosition.GetNavMeshVec3().DistanceSquared(this._frame.Origin.GetNavMeshVec3()) : float.MaxValue;
			num *= MissionGameModels.Current.AgentStatCalculateModel.GetDetachmentCostMultiplierOfAgent(candidate, this);
			List<float> list = oldValue ?? new List<float>(this._capacity);
			list.Clear();
			for (int i = 0; i < this._capacity; i++)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003913D File Offset: 0x0003733D
		float IDetachment.GetExactCostOfAgentAtSlot(Agent candidate, int slotIndex)
		{
			Debug.FailedAssert("This should never have been called because this detachment does not seek to replace moving agents.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\StrategicArea.cs", "GetExactCostOfAgentAtSlot", 373);
			return 0f;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00039160 File Offset: 0x00037360
		public float GetTemplateWeightOfAgent(Agent candidate)
		{
			WorldPosition worldPosition = candidate.GetWorldPosition();
			WorldPosition origin = this._frame.Origin;
			if (!candidate.Mission.Scene.DoesPathExistBetweenPositions(worldPosition, origin))
			{
				return float.MaxValue;
			}
			return worldPosition.GetNavMeshVec3().DistanceSquared(origin.GetNavMeshVec3());
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000391B0 File Offset: 0x000373B0
		public float? GetWeightOfAgentAtNextSlot(List<Agent> newAgents, out Agent match)
		{
			float? weightOfNextSlot = this.GetWeightOfNextSlot(newAgents[0].Team.Side);
			if (this._agents.Count < this._capacity)
			{
				Vec3 position = base.GameEntity.GlobalPosition;
				match = newAgents.MinBy((Agent a) => a.Position.DistanceSquared(position));
				return weightOfNextSlot;
			}
			match = null;
			return null;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00039224 File Offset: 0x00037424
		public float? GetWeightOfAgentAtNextSlot(List<ValueTuple<Agent, float>> agentTemplateScores, out Agent match)
		{
			float? weight = this.GetWeightOfNextSlot(agentTemplateScores[0].Item1.Team.Side);
			if (this._agents.Count < this._capacity)
			{
				IEnumerable<ValueTuple<Agent, float>> source = agentTemplateScores.Where(delegate(ValueTuple<Agent, float> a)
				{
					Agent item = a.Item1;
					if (item.IsDetachedFromFormation)
					{
						float detachmentWeight = item.DetachmentWeight;
						float? num = weight * 0.4f;
						return detachmentWeight < num.GetValueOrDefault() & num != null;
					}
					return true;
				});
				if (source.Any<ValueTuple<Agent, float>>())
				{
					match = source.MinBy((ValueTuple<Agent, float> a) => a.Item2).Item1;
					return weight;
				}
			}
			match = null;
			return null;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000392C8 File Offset: 0x000374C8
		public float? GetWeightOfAgentAtOccupiedSlot(Agent detachedAgent, List<Agent> newAgents, out Agent match)
		{
			float weightOfOccupiedSlot = this.GetWeightOfOccupiedSlot(detachedAgent);
			Vec3 position = base.GameEntity.GlobalPosition;
			match = newAgents.MinBy((Agent a) => a.Position.DistanceSquared(position));
			return new float?(weightOfOccupiedSlot * 0.5f);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00039312 File Offset: 0x00037512
		public void RemoveAgent(Agent agent)
		{
			this._agents.Remove(agent);
			agent.SetPreciseRangedAimingEnabled(false);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00039328 File Offset: 0x00037528
		public int GetNumberOfUsableSlots()
		{
			return this._capacity;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00039330 File Offset: 0x00037530
		private Formation GetSimulationFormation(Formation formation)
		{
			if (!this._simulationFormations.ContainsKey(formation))
			{
				this._simulationFormations[formation] = new Formation(null, -1);
			}
			return this._simulationFormations[formation];
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00039360 File Offset: 0x00037560
		protected internal override bool OnCheckForProblems()
		{
			bool result = base.OnCheckForProblems();
			if (base.GameEntity.IsVisibleIncludeParents() && this.CalculateCapacity() == 1)
			{
				MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
				WorldFrame worldFrame = new WorldFrame(globalFrame.rotation, new WorldPosition(base.Scene, globalFrame.origin));
				if (worldFrame.Origin.GetNavMesh() == UIntPtr.Zero)
				{
					uint upgradeLevelMaskCumulative = (uint)base.GameEntity.GetUpgradeLevelMaskCumulative();
					int upgradeLevelCount = base.Scene.GetUpgradeLevelCount();
					string text = "";
					for (int i = 0; i < upgradeLevelCount; i++)
					{
						if (((ulong)upgradeLevelMaskCumulative & (ulong)(1L << (i & 31))) != 0UL)
						{
							text = text + base.Scene.GetUpgradeLevelNameOfIndex(i) + ",";
						}
					}
					MBEditor.AddEntityWarning(base.GameEntity, string.Concat(new object[]
					{
						"Strategic archer position at position at X=",
						globalFrame.origin.X,
						" Y=",
						globalFrame.origin.Y,
						" Z=",
						globalFrame.origin.Z,
						"doesn't yield a viable frame. It may be in the air, underground or off the navmesh, please check. Scene: ",
						base.Scene.GetName(),
						"Upgrade Mask: ",
						upgradeLevelMaskCumulative,
						", Upgrade Level Names: ",
						text
					}));
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000394D4 File Offset: 0x000376D4
		public WorldFrame? GetAgentFrame(Agent agent)
		{
			if (this._capacity > 1)
			{
				int num = this._agents.IndexOf(agent);
				Formation formation = agent.Formation;
				Formation simulationFormation = this.GetSimulationFormation(formation);
				Formation formation2 = formation;
				Formation simulationFormation2 = simulationFormation;
				int unitIndex = num;
				Vec2 vec = this._frame.Rotation.f.AsVec2;
				vec = vec.Normalized();
				WorldPosition? worldPosition;
				Vec2? vec2;
				formation2.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation2, unitIndex, this._frame.Origin, vec, this._width, this._unitSpacing, this._agents.Count, out worldPosition, out vec2);
				if (worldPosition != null)
				{
					return new WorldFrame?(new WorldFrame(this._frame.Rotation, worldPosition.Value));
				}
				if (this._centerPosition == null)
				{
					MBDebug.ShowWarning(string.Concat(new object[]
					{
						"Strategic archer position at position at X=",
						this._frame.Origin.GetGroundVec3().x,
						" Y=",
						this._frame.Origin.GetGroundVec3().y,
						" Z=",
						this._frame.Origin.GetGroundVec3().z,
						"doesn't yield a viable frame. It may be in the air, underground or off the navmesh, please check. Scene: ",
						base.Scene.GetName()
					}));
				}
				return new WorldFrame?(agent.GetWorldFrame());
			}
			else
			{
				float totalMissionTime = MBCommon.GetTotalMissionTime();
				StrategicArea.ShimmyDirection shimmyDirection = this._shimmyDirection;
				int num2 = 0;
				StrategicArea.StrategicAreaMutableTuple[] strategicAreaSidesScoreTally = this._strategicAreaSidesScoreTally;
				for (int i = 0; i < strategicAreaSidesScoreTally.Length; i++)
				{
					if (strategicAreaSidesScoreTally[i] != null)
					{
						num2++;
					}
				}
				bool flag = num2 > 1;
				if (flag && this._lastShootTime < agent.LastRangedAttackTime)
				{
					this._lastShootTime = agent.LastRangedAttackTime;
					StrategicArea.StrategicAreaMutableTuple strategicAreaMutableTuple = this._strategicAreaSidesScoreTally[(int)this._shimmyDirection];
					if (strategicAreaMutableTuple != null)
					{
						strategicAreaMutableTuple.RangedHitScoredCount++;
					}
					else
					{
						this._strategicAreaSidesScoreTally[(int)this._shimmyDirection] = new StrategicArea.StrategicAreaMutableTuple(0, 1);
					}
				}
				bool flag2 = false;
				if (flag && this._lastShimmyTime < agent.LastRangedHitTime)
				{
					StrategicArea.StrategicAreaMutableTuple strategicAreaMutableTuple2 = this._strategicAreaSidesScoreTally[(int)this._shimmyDirection];
					if (strategicAreaMutableTuple2 != null)
					{
						strategicAreaMutableTuple2.RangedHitReceivedCount++;
					}
					else
					{
						this._strategicAreaSidesScoreTally[(int)this._shimmyDirection] = new StrategicArea.StrategicAreaMutableTuple(1, 0);
					}
					flag2 = true;
				}
				bool flag3 = false;
				if (flag && !flag2 && totalMissionTime - MathF.Max(agent.LastRangedAttackTime, this._lastShimmyTime) > 8f)
				{
					StrategicArea.StrategicAreaMutableTuple strategicAreaMutableTuple3 = this._strategicAreaSidesScoreTally[(int)this._shimmyDirection];
					if (strategicAreaMutableTuple3 != null)
					{
						strategicAreaMutableTuple3.RangedHitScoredCount--;
					}
					else
					{
						this._strategicAreaSidesScoreTally[(int)this._shimmyDirection] = new StrategicArea.StrategicAreaMutableTuple(0, -1);
					}
					flag3 = true;
				}
				if (flag2 || flag3)
				{
					int num3 = int.MinValue;
					int num4 = 0;
					for (int j = 0; j < 5; j++)
					{
						if (j != (int)this._shimmyDirection && this._strategicAreaSidesScoreTally[j] != null)
						{
							int num5 = this._strategicAreaSidesScoreTally[j].RangedHitScoredCount - this._strategicAreaSidesScoreTally[j].RangedHitReceivedCount;
							if (num5 > num3)
							{
								num3 = num5;
								num4 = 1;
							}
							else if (num5 == num3)
							{
								num4++;
							}
						}
					}
					int num6 = MBRandom.RandomInt(num4 - 1);
					for (int k = 0; k < 5; k++)
					{
						if (k != (int)this._shimmyDirection && this._strategicAreaSidesScoreTally[k] != null && this._strategicAreaSidesScoreTally[k].RangedHitScoredCount - this._strategicAreaSidesScoreTally[k].RangedHitReceivedCount == num3 && --num6 < 0)
						{
							shimmyDirection = (StrategicArea.ShimmyDirection)k;
						}
					}
					this._doesFrameNeedUpdate = true;
				}
				if (!this._doesFrameNeedUpdate)
				{
					return new WorldFrame?(this._cachedWorldFrame);
				}
				if (this._centerPosition != null)
				{
					WorldPosition value = this._centerPosition.Value;
					Vec2 vec = this._frame.Rotation.f.AsVec2;
					Vec2 vec3 = vec.Normalized();
					Vec2 v;
					switch (shimmyDirection)
					{
					case StrategicArea.ShimmyDirection.Center:
						v = Vec2.Zero;
						break;
					case StrategicArea.ShimmyDirection.Left:
						v = vec3.RightVec();
						break;
					case StrategicArea.ShimmyDirection.Forward:
						v = vec3;
						break;
					case StrategicArea.ShimmyDirection.Right:
						v = vec3.LeftVec();
						break;
					case StrategicArea.ShimmyDirection.Back:
						v = -vec3;
						break;
					default:
						v = Vec2.Zero;
						break;
					}
					WorldPosition worldPosition2 = value;
					int num7 = 8;
					bool flag4 = false;
					while (num7-- > 0)
					{
						value.SetVec2(worldPosition2.AsVec2 + (0.6f + 0.05f * (float)num7) * v);
						if (value.GetNavMesh() != UIntPtr.Zero)
						{
							flag4 = true;
							break;
						}
					}
					this._doesFrameNeedUpdate = false;
					if (!flag4)
					{
						this._strategicAreaSidesScoreTally[(int)shimmyDirection] = null;
					}
					else
					{
						this._shimmyDirection = shimmyDirection;
						this._lastShimmyTime = totalMissionTime;
						Mat3 identity = Mat3.Identity;
						identity.f = new Vec3(vec3, 0f, -1f);
						identity.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
						this._cachedWorldFrame = new WorldFrame(identity, value);
					}
					return new WorldFrame?(this._cachedWorldFrame);
				}
				MBDebug.ShowWarning(string.Concat(new object[]
				{
					"Strategic archer position at position at X=",
					this._frame.Origin.GetGroundVec3().x,
					" Y=",
					this._frame.Origin.GetGroundVec3().y,
					" Z=",
					this._frame.Origin.GetGroundVec3().z,
					"doesn't yield a viable frame. It may be in the air, underground or off the navmesh, please check. Scene: ",
					base.Scene.GetName()
				}));
				return new WorldFrame?(agent.GetWorldFrame());
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00039A4F File Offset: 0x00037C4F
		private static float CalculateWeight(int capacity, int index)
		{
			return (float)(capacity - index) * 1f / (float)capacity * 0.5f;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00039A64 File Offset: 0x00037C64
		public float? GetWeightOfNextSlot(BattleSideEnum side)
		{
			if (this._agents.Count < this._capacity)
			{
				return new float?(StrategicArea.CalculateWeight(this._capacity, this._agents.Count));
			}
			return null;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00039AA9 File Offset: 0x00037CA9
		public float GetWeightOfOccupiedSlot(Agent agent)
		{
			return StrategicArea.CalculateWeight(this._capacity, this._agents.IndexOf(agent));
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00039AC2 File Offset: 0x00037CC2
		public bool IsUsableBy(BattleSideEnum side)
		{
			return this._side == side || this._side == BattleSideEnum.None;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00039ADB File Offset: 0x00037CDB
		float IDetachment.GetDetachmentWeight(BattleSideEnum side)
		{
			if (this._agents.Count < this._capacity)
			{
				return (float)(this._capacity - this._agents.Count) * 1f / (float)this._capacity;
			}
			return float.MinValue;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00039B17 File Offset: 0x00037D17
		void IDetachment.ResetEvaluation()
		{
			this._isEvaluated = false;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00039B20 File Offset: 0x00037D20
		bool IDetachment.IsEvaluated()
		{
			return this._isEvaluated;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00039B28 File Offset: 0x00037D28
		void IDetachment.SetAsEvaluated()
		{
			this._isEvaluated = true;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00039B31 File Offset: 0x00037D31
		float IDetachment.GetDetachmentWeightFromCache()
		{
			return this._cachedDetachmentWeight;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00039B39 File Offset: 0x00037D39
		float IDetachment.ComputeAndCacheDetachmentWeight(BattleSideEnum side)
		{
			this._cachedDetachmentWeight = ((IDetachment)this).GetDetachmentWeight(side);
			return this._cachedDetachmentWeight;
		}

		// Token: 0x0400047B RID: 1147
		private List<Agent> _agents;

		// Token: 0x0400047C RID: 1148
		private WorldFrame _frame;

		// Token: 0x0400047D RID: 1149
		[EditableScriptComponentVariable(true)]
		private float _width;

		// Token: 0x0400047E RID: 1150
		private int _unitSpacing;

		// Token: 0x0400047F RID: 1151
		private int _capacity;

		// Token: 0x04000480 RID: 1152
		private MBList<Formation> _userFormations;

		// Token: 0x04000481 RID: 1153
		private Dictionary<Formation, Formation> _simulationFormations;

		// Token: 0x04000482 RID: 1154
		[EditableScriptComponentVariable(true)]
		private BattleSideEnum _side;

		// Token: 0x04000483 RID: 1155
		[EditableScriptComponentVariable(true)]
		private float _depth = 1f;

		// Token: 0x04000484 RID: 1156
		[EditableScriptComponentVariable(true)]
		private float _distanceToCheck = 10f;

		// Token: 0x04000485 RID: 1157
		[EditableScriptComponentVariable(true)]
		private bool _ignoreHeight = true;

		// Token: 0x04000486 RID: 1158
		private List<DestructableComponent> _nearbyDestructibleObjects = new List<DestructableComponent>();

		// Token: 0x04000487 RID: 1159
		private bool _isActive;

		// Token: 0x04000488 RID: 1160
		private float _lastShimmyTime;

		// Token: 0x04000489 RID: 1161
		private float _lastShootTime;

		// Token: 0x0400048A RID: 1162
		private StrategicArea.ShimmyDirection _shimmyDirection;

		// Token: 0x0400048B RID: 1163
		private bool _doesFrameNeedUpdate = true;

		// Token: 0x0400048C RID: 1164
		private readonly StrategicArea.StrategicAreaMutableTuple[] _strategicAreaSidesScoreTally = new StrategicArea.StrategicAreaMutableTuple[5];

		// Token: 0x0400048D RID: 1165
		private WorldPosition? _centerPosition;

		// Token: 0x0400048E RID: 1166
		private WorldFrame _cachedWorldFrame;

		// Token: 0x0400048F RID: 1167
		private bool _isEvaluated;

		// Token: 0x04000490 RID: 1168
		private float _cachedDetachmentWeight;

		// Token: 0x0200045A RID: 1114
		private class StrategicAreaMutableTuple
		{
			// Token: 0x0600351C RID: 13596 RVA: 0x000D96B4 File Offset: 0x000D78B4
			public StrategicAreaMutableTuple(int rangedHitReceivedCount, int rangedHitScoredCount)
			{
				this.RangedHitReceivedCount = rangedHitReceivedCount;
				this.RangedHitScoredCount = rangedHitScoredCount;
			}

			// Token: 0x0400192B RID: 6443
			public int RangedHitReceivedCount;

			// Token: 0x0400192C RID: 6444
			public int RangedHitScoredCount;
		}

		// Token: 0x0200045B RID: 1115
		private enum ShimmyDirection
		{
			// Token: 0x0400192E RID: 6446
			Center,
			// Token: 0x0400192F RID: 6447
			Left,
			// Token: 0x04001930 RID: 6448
			Forward,
			// Token: 0x04001931 RID: 6449
			Right,
			// Token: 0x04001932 RID: 6450
			Back,
			// Token: 0x04001933 RID: 6451
			NumDirections
		}
	}
}
