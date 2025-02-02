using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Objects;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000080 RID: 128
	public class FollowAgentBehavior : AgentBehavior
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00021D06 File Offset: 0x0001FF06
		public FollowAgentBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._selectedAgent = null;
			this._deactivatedAgent = null;
			this._myLastStateWasRunning = false;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00021D24 File Offset: 0x0001FF24
		public void SetTargetAgent(Agent agent)
		{
			this._selectedAgent = agent;
			this._state = FollowAgentBehavior.State.Idle;
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("navigation_mesh_deactivator");
			if (gameEntity != null)
			{
				int disableFaceWithId = gameEntity.GetFirstScriptOfType<NavigationMeshDeactivator>().DisableFaceWithId;
				if (disableFaceWithId != -1)
				{
					base.OwnerAgent.SetAgentExcludeStateForFaceGroupId(disableFaceWithId, false);
				}
			}
			this.TryMoveStateTransition(true);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00021D82 File Offset: 0x0001FF82
		public override void Tick(float dt, bool isSimulation)
		{
			if (this._selectedAgent != null)
			{
				this.ControlMovement();
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00021D94 File Offset: 0x0001FF94
		private void ControlMovement()
		{
			if (base.Navigator.TargetPosition.IsValid && base.Navigator.IsTargetReached())
			{
				base.OwnerAgent.DisableScriptedMovement();
				base.OwnerAgent.SetMaximumSpeedLimit(-1f, false);
				if (this._state == FollowAgentBehavior.State.OnMove)
				{
					this._idleDistance = base.OwnerAgent.Position.AsVec2.Distance(this._selectedAgent.Position.AsVec2);
				}
				this._state = FollowAgentBehavior.State.Idle;
			}
			int nearbyEnemyAgentCount = base.Mission.GetNearbyEnemyAgentCount(base.OwnerAgent.Team, base.OwnerAgent.Position.AsVec2, 5f);
			if (this._state != FollowAgentBehavior.State.Fight && nearbyEnemyAgentCount > 0)
			{
				base.OwnerAgent.SetWatchState(Agent.WatchState.Alarmed);
				base.OwnerAgent.ResetLookAgent();
				base.Navigator.ClearTarget();
				base.OwnerAgent.DisableScriptedMovement();
				this._state = FollowAgentBehavior.State.Fight;
				Debug.Print("[Follow agent behavior] Fight!", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			switch (this._state)
			{
			case FollowAgentBehavior.State.Idle:
				this.TryMoveStateTransition(false);
				return;
			case FollowAgentBehavior.State.OnMove:
				this.MoveToFollowingAgent(false);
				break;
			case FollowAgentBehavior.State.Fight:
				if (nearbyEnemyAgentCount == 0)
				{
					base.OwnerAgent.SetWatchState(Agent.WatchState.Patrolling);
					base.OwnerAgent.SetLookAgent(this._selectedAgent);
					this._state = FollowAgentBehavior.State.Idle;
					Debug.Print("[Follow agent behavior] Stop fighting!", 0, Debug.DebugColor.White, 17592186044416UL);
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00021F14 File Offset: 0x00020114
		private void TryMoveStateTransition(bool forceMove)
		{
			if (this._selectedAgent != null && base.OwnerAgent.Position.AsVec2.Distance(this._selectedAgent.Position.AsVec2) > 4f + this._idleDistance)
			{
				this._state = FollowAgentBehavior.State.OnMove;
				this.MoveToFollowingAgent(forceMove);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00021F74 File Offset: 0x00020174
		private void MoveToFollowingAgent(bool forcedMove)
		{
			Vec2 asVec = this._selectedAgent.Velocity.AsVec2;
			if (this._updatePositionThisFrame || forcedMove || asVec.IsNonZero())
			{
				this._updatePositionThisFrame = false;
				WorldPosition worldPosition = this._selectedAgent.GetWorldPosition();
				Vec2 vec = asVec.IsNonZero() ? asVec.Normalized() : this._selectedAgent.GetMovementDirection();
				Vec2 vec2 = vec.LeftVec();
				Vec2 va = this._selectedAgent.Position.AsVec2 - base.OwnerAgent.Position.AsVec2;
				float lengthSquared = va.LengthSquared;
				int num = (Vec2.DotProduct(va, vec2) > 0f) ? 1 : -1;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				foreach (Agent agent in base.Mission.Agents)
				{
					CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
					if (((component != null) ? component.AgentNavigator : null) != null)
					{
						DailyBehaviorGroup behaviorGroup = component.AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
						FollowAgentBehavior followAgentBehavior = (behaviorGroup != null) ? behaviorGroup.GetBehavior<FollowAgentBehavior>() : null;
						if (followAgentBehavior != null && followAgentBehavior._selectedAgent != null && followAgentBehavior._selectedAgent == this._selectedAgent)
						{
							Vec2 va2 = this._selectedAgent.Position.AsVec2 - agent.Position.AsVec2;
							int num6 = (Vec2.DotProduct(va2, vec2) > 0f) ? 1 : -1;
							if (va2.LengthSquared < lengthSquared)
							{
								if (num6 == num)
								{
									if (agent.HasMount)
									{
										num3++;
									}
									else
									{
										num2++;
									}
								}
								if (Vec2.DotProduct(va2, vec) > 0.3f)
								{
									if (agent.HasMount)
									{
										num5++;
									}
									else
									{
										num4++;
									}
								}
							}
						}
					}
				}
				float num7 = this._selectedAgent.HasMount ? 1.25f : 0.6f;
				float num8 = base.OwnerAgent.HasMount ? 1.25f : 0.6f;
				float num9 = this._selectedAgent.HasMount ? 1.5f : 1f;
				float num10 = base.OwnerAgent.HasMount ? 1.5f : 1f;
				Vec2 v = vec * (2f + 0.5f * (num8 + num7) + (float)num2 * 0.6f + (float)num3 * 1.25f);
				Vec2 v2 = (float)num * vec2 * (0.5f * (num10 + num9) + (float)num2 * 1f + (float)num3 * 1.5f);
				Vec2 vec3 = this._selectedAgent.Position.AsVec2 - v - v2;
				bool flag = false;
				AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, vec3, 0.5f, false);
				while (proximityMapSearchStruct.LastFoundAgent != null)
				{
					Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
					if (lastFoundAgent.Index != base.OwnerAgent.Index && lastFoundAgent.Index != this._selectedAgent.Index)
					{
						flag = true;
						break;
					}
					AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
				}
				float num11 = base.OwnerAgent.HasMount ? 2.2f : 1.2f;
				if (!flag)
				{
					WorldPosition worldPosition2 = worldPosition;
					worldPosition2 = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, worldPosition2.GetGroundVec3(), false);
					worldPosition2.SetVec2(vec3);
					if (worldPosition2.GetNavMesh() != UIntPtr.Zero && base.Mission.Scene.IsLineToPointClear(ref worldPosition2, ref worldPosition, base.OwnerAgent.Monster.BodyCapsuleRadius))
					{
						WorldPosition pos = worldPosition2;
						pos.SetVec2(pos.AsVec2 + vec * 1.5f);
						if (pos.GetNavMesh() != UIntPtr.Zero && base.Mission.Scene.IsLineToPointClear(ref pos, ref worldPosition2, base.OwnerAgent.Monster.BodyCapsuleRadius))
						{
							this.SetMovePos(pos, this._selectedAgent.MovementDirectionAsAngle, num11, Agent.AIScriptedFrameFlags.NoAttack);
						}
						else
						{
							this.SetMovePos(worldPosition2, this._selectedAgent.MovementDirectionAsAngle, num11, Agent.AIScriptedFrameFlags.NoAttack);
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					float rangeThreshold = num11 + (float)num4 * 0.6f + (float)num5 * 1.25f;
					this.SetMovePos(worldPosition, this._selectedAgent.MovementDirectionAsAngle, rangeThreshold, Agent.AIScriptedFrameFlags.NoAttack);
				}
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00022418 File Offset: 0x00020618
		private void SetMovePos(WorldPosition pos, float rotationInRadians, float rangeThreshold, Agent.AIScriptedFrameFlags flags)
		{
			bool flag = base.Mission.Mode == MissionMode.Stealth;
			if (base.Navigator.CharacterHasVisiblePrefabs)
			{
				this._myLastStateWasRunning = false;
			}
			else
			{
				if (flag && this._selectedAgent.CrouchMode)
				{
					flags |= Agent.AIScriptedFrameFlags.Crouch;
				}
				if (flag && this._selectedAgent.WalkMode)
				{
					base.OwnerAgent.SetMaximumSpeedLimit(this._selectedAgent.CrouchMode ? this._selectedAgent.Monster.CrouchWalkingSpeedLimit : this._selectedAgent.Monster.WalkingSpeedLimit, false);
					this._myLastStateWasRunning = false;
				}
				else
				{
					float num = base.OwnerAgent.Position.AsVec2.Distance(pos.AsVec2);
					if (num - rangeThreshold <= 0.5f * (this._myLastStateWasRunning ? 1f : 1.2f) && this._selectedAgent.Velocity.AsVec2.Length <= base.OwnerAgent.Monster.WalkingSpeedLimit * (this._myLastStateWasRunning ? 1f : 1.2f))
					{
						this._myLastStateWasRunning = false;
					}
					else
					{
						base.OwnerAgent.SetMaximumSpeedLimit(num - rangeThreshold + this._selectedAgent.Velocity.AsVec2.Length, false);
						this._myLastStateWasRunning = true;
					}
				}
			}
			if (!this._myLastStateWasRunning)
			{
				flags |= Agent.AIScriptedFrameFlags.DoNotRun;
			}
			base.Navigator.SetTargetFrame(pos, rotationInRadians, rangeThreshold, -10f, flags, flag);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000225A2 File Offset: 0x000207A2
		public override void OnAgentRemoved(Agent agent)
		{
			if (agent == this._selectedAgent)
			{
				base.OwnerAgent.ResetLookAgent();
				this._selectedAgent = null;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000225BF File Offset: 0x000207BF
		protected override void OnActivate()
		{
			if (this._deactivatedAgent != null)
			{
				this.SetTargetAgent(this._deactivatedAgent);
				this._deactivatedAgent = null;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000225DC File Offset: 0x000207DC
		protected override void OnDeactivate()
		{
			this._state = FollowAgentBehavior.State.Idle;
			this._deactivatedAgent = this._selectedAgent;
			this._selectedAgent = null;
			base.OwnerAgent.DisableScriptedMovement();
			base.OwnerAgent.ResetLookAgent();
			base.Navigator.ClearTarget();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002261C File Offset: 0x0002081C
		public override string GetDebugInfo()
		{
			return string.Concat(new object[]
			{
				"Follow ",
				this._selectedAgent.Name,
				" (id:",
				this._selectedAgent.Index,
				")"
			});
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002266D File Offset: 0x0002086D
		public override float GetAvailability(bool isSimulation)
		{
			return (float)((this._selectedAgent == null) ? 0 : 100);
		}

		// Token: 0x0400025A RID: 602
		private const float _moveReactionProximityThreshold = 4f;

		// Token: 0x0400025B RID: 603
		private const float _longitudinalClearanceOffset = 2f;

		// Token: 0x0400025C RID: 604
		private const float _onFootMoveProximityThreshold = 1.2f;

		// Token: 0x0400025D RID: 605
		private const float _mountedMoveProximityThreshold = 2.2f;

		// Token: 0x0400025E RID: 606
		private const float _onFootAgentLongitudinalOffset = 0.6f;

		// Token: 0x0400025F RID: 607
		private const float _onFootAgentLateralOffset = 1f;

		// Token: 0x04000260 RID: 608
		private const float _mountedAgentLongitudinalOffset = 1.25f;

		// Token: 0x04000261 RID: 609
		private const float _mountedAgentLateralOffset = 1.5f;

		// Token: 0x04000262 RID: 610
		private float _idleDistance;

		// Token: 0x04000263 RID: 611
		private Agent _selectedAgent;

		// Token: 0x04000264 RID: 612
		private FollowAgentBehavior.State _state;

		// Token: 0x04000265 RID: 613
		private Agent _deactivatedAgent;

		// Token: 0x04000266 RID: 614
		private bool _myLastStateWasRunning;

		// Token: 0x04000267 RID: 615
		private bool _updatePositionThisFrame;

		// Token: 0x02000153 RID: 339
		private enum State
		{
			// Token: 0x040005C0 RID: 1472
			Idle,
			// Token: 0x040005C1 RID: 1473
			OnMove,
			// Token: 0x040005C2 RID: 1474
			Fight
		}
	}
}
