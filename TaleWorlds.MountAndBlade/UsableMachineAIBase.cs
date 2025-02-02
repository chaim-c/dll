using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200015F RID: 351
	public abstract class UsableMachineAIBase
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00037C63 File Offset: 0x00035E63
		protected UsableMachineAIBase(UsableMachine usableMachine)
		{
			this.UsableMachine = usableMachine;
			this._lastActiveWaitStandingPoint = this.UsableMachine.WaitEntity;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00037C83 File Offset: 0x00035E83
		public virtual bool HasActionCompleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00037C86 File Offset: 0x00035E86
		protected internal virtual Agent.AIScriptedFrameFlags GetScriptedFrameFlags(Agent agent)
		{
			return Agent.AIScriptedFrameFlags.None;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00037C89 File Offset: 0x00035E89
		public void Tick(Agent agentToCompareTo, Formation formationToCompareTo, Team potentialUsersTeam, float dt)
		{
			this.OnTick(agentToCompareTo, formationToCompareTo, potentialUsersTeam, dt);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00037C98 File Offset: 0x00035E98
		protected virtual void OnTick(Agent agentToCompareTo, Formation formationToCompareTo, Team potentialUsersTeam, float dt)
		{
			foreach (StandingPoint standingPoint in this.UsableMachine.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if ((agentToCompareTo == null || userAgent == agentToCompareTo) && (formationToCompareTo == null || (userAgent != null && userAgent.IsAIControlled && userAgent.Formation == formationToCompareTo)) && (this.HasActionCompleted || (potentialUsersTeam != null && this.UsableMachine.IsDisabledForBattleSideAI(potentialUsersTeam.Side)) || userAgent.IsRunningAway))
				{
					this.HandleAgentStopUsingStandingPoint(userAgent, standingPoint);
				}
				if (standingPoint.HasAIMovingTo)
				{
					Agent movingAgent = standingPoint.MovingAgent;
					if ((agentToCompareTo == null || movingAgent == agentToCompareTo) && (formationToCompareTo == null || (movingAgent != null && movingAgent.IsAIControlled && movingAgent.Formation == formationToCompareTo)))
					{
						if (this.HasActionCompleted || (potentialUsersTeam != null && this.UsableMachine.IsDisabledForBattleSideAI(potentialUsersTeam.Side)) || movingAgent.IsRunningAway)
						{
							this.HandleAgentStopUsingStandingPoint(movingAgent, standingPoint);
						}
						else
						{
							if (standingPoint.HasAlternative() && this.UsableMachine.IsInRangeToCheckAlternativePoints(movingAgent))
							{
								StandingPoint bestPointAlternativeTo = this.UsableMachine.GetBestPointAlternativeTo(standingPoint, movingAgent);
								if (bestPointAlternativeTo != standingPoint)
								{
									standingPoint.OnMoveToStopped(movingAgent);
									movingAgent.AIMoveToGameObjectEnable(bestPointAlternativeTo, this.UsableMachine, this.GetScriptedFrameFlags(movingAgent));
									if (standingPoint == this.UsableMachine.CurrentlyUsedAmmoPickUpPoint)
									{
										this.UsableMachine.CurrentlyUsedAmmoPickUpPoint = bestPointAlternativeTo;
										continue;
									}
									continue;
								}
							}
							if (standingPoint.HasUserPositionsChanged(movingAgent))
							{
								WorldFrame userFrameForAgent = standingPoint.GetUserFrameForAgent(movingAgent);
								movingAgent.SetScriptedPositionAndDirection(ref userFrameForAgent.Origin, userFrameForAgent.Rotation.f.AsVec2.RotationInRadians, false, this.GetScriptedFrameFlags(movingAgent));
							}
							if (!standingPoint.IsDisabled && !standingPoint.HasUser && !movingAgent.IsPaused && movingAgent.CanReachAndUseObject(standingPoint, standingPoint.GetUserFrameForAgent(movingAgent).Origin.GetGroundVec3().DistanceSquared(movingAgent.Position)))
							{
								movingAgent.UseGameObject(standingPoint, -1);
								movingAgent.SetScriptedFlags(movingAgent.GetScriptedFlags() & ~standingPoint.DisableScriptedFrameFlags);
							}
						}
					}
				}
				for (int i = standingPoint.GetDefendingAgentCount() - 1; i >= 0; i--)
				{
					Agent agent = standingPoint.DefendingAgents[i];
					if ((agentToCompareTo == null || agent == agentToCompareTo) && (formationToCompareTo == null || (agent != null && agent.IsAIControlled && agent.Formation == formationToCompareTo)) && (this.HasActionCompleted || (potentialUsersTeam != null && !this.UsableMachine.IsDisabledForBattleSideAI(potentialUsersTeam.Side)) || agent.IsRunningAway))
					{
						this.HandleAgentStopUsingStandingPoint(agent, standingPoint);
					}
				}
			}
			if (this._lastActiveWaitStandingPoint != this.UsableMachine.WaitEntity)
			{
				foreach (Formation formation in from f in potentialUsersTeam.FormationsIncludingSpecialAndEmpty
				where f.CountOfUnits > 0 && this.UsableMachine.IsUsedByFormation(f) && f.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.FollowEntity && f.GetReadonlyMovementOrderReference().TargetEntity == this._lastActiveWaitStandingPoint
				select f)
				{
					if (this is SiegeTowerAI)
					{
						formation.SetMovementOrder(this.NextOrder);
					}
					else
					{
						formation.SetMovementOrder(MovementOrder.MovementOrderFollowEntity(this.UsableMachine.WaitEntity));
					}
				}
				this._lastActiveWaitStandingPoint = this.UsableMachine.WaitEntity;
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00037FF0 File Offset: 0x000361F0
		[Conditional("DEBUG")]
		private void TickForDebug()
		{
			if (Input.DebugInput.IsHotKeyDown("UsableMachineAiBaseHotkeyShowMachineUsers"))
			{
				foreach (StandingPoint standingPoint in this.UsableMachine.StandingPoints)
				{
					bool hasAIMovingTo = standingPoint.HasAIMovingTo;
					Agent userAgent = standingPoint.UserAgent;
				}
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00038060 File Offset: 0x00036260
		public static Agent GetSuitableAgentForStandingPoint(UsableMachine usableMachine, StandingPoint standingPoint, IEnumerable<Agent> agents, List<Agent> usedAgents)
		{
			if (usableMachine.AmmoPickUpPoints.Contains(standingPoint) && usableMachine.StandingPoints.Any((StandingPoint standingPoint2) => (standingPoint2.IsDeactivated || standingPoint2.HasUser || standingPoint2.HasAIMovingTo) && !standingPoint2.GameEntity.HasTag(usableMachine.AmmoPickUpTag) && standingPoint2 is StandingPointWithWeaponRequirement))
			{
				return null;
			}
			IEnumerable<Agent> source = from a in agents
			where !usedAgents.Contains(a) && a.IsAIControlled && a.IsActive() && !a.IsRunningAway && !a.InteractingWithAnyGameObject() && !standingPoint.IsDisabledForAgent(a) && (a.Formation == null || !a.IsDetachedFromFormation)
			select a;
			if (!source.Any<Agent>())
			{
				return null;
			}
			return source.MaxBy((Agent a) => standingPoint.GetUsageScoreForAgent(a));
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000380F0 File Offset: 0x000362F0
		public static Agent GetSuitableAgentForStandingPoint(UsableMachine usableMachine, StandingPoint standingPoint, List<ValueTuple<Agent, float>> agents, List<Agent> usedAgents, float weight)
		{
			if (usableMachine.IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(standingPoint))
			{
				return null;
			}
			Agent result = null;
			float num = float.MinValue;
			foreach (ValueTuple<Agent, float> valueTuple in agents)
			{
				Agent item = valueTuple.Item1;
				if (!usedAgents.Contains(item) && item.IsAIControlled && item.IsActive() && !item.IsRunningAway && !item.InteractingWithAnyGameObject() && !standingPoint.IsDisabledForAgent(item) && (item.Formation == null || !item.IsDetachedFromFormation || item.DetachmentWeight * 0.4f > weight))
				{
					float usageScoreForAgent = standingPoint.GetUsageScoreForAgent(item);
					if (num < usageScoreForAgent)
					{
						num = usageScoreForAgent;
						result = item;
					}
				}
			}
			return result;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000381B8 File Offset: 0x000363B8
		protected virtual MovementOrder NextOrder
		{
			get
			{
				return MovementOrder.MovementOrderStop;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000381C0 File Offset: 0x000363C0
		public virtual void TeleportUserAgentsToMachine(List<Agent> agentList)
		{
			int num = 0;
			bool flag;
			do
			{
				num++;
				flag = false;
				foreach (Agent agent in agentList)
				{
					if (agent.IsAIControlled && agent.AIMoveToGameObjectIsEnabled())
					{
						flag = true;
						WorldFrame userFrameForAgent = this.UsableMachine.GetTargetStandingPointOfAIAgent(agent).GetUserFrameForAgent(agent);
						Vec2 vec = userFrameForAgent.Rotation.f.AsVec2.Normalized();
						if ((agent.Position.AsVec2 - userFrameForAgent.Origin.AsVec2).LengthSquared > 0.0001f || (agent.GetMovementDirection() - vec).LengthSquared > 0.0001f)
						{
							agent.TeleportToPosition(userFrameForAgent.Origin.GetGroundVec3());
							agent.SetMovementDirection(vec);
							if (GameNetwork.IsServerOrRecorder)
							{
								GameNetwork.BeginBroadcastModuleEvent();
								GameNetwork.WriteMessage(new AgentTeleportToFrame(agent.Index, userFrameForAgent.Origin.GetGroundVec3(), vec));
								GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
							}
						}
					}
				}
			}
			while (flag && num < 10);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00038304 File Offset: 0x00036504
		public void StopUsingStandingPoint(StandingPoint standingPoint)
		{
			Agent agent = standingPoint.HasUser ? standingPoint.UserAgent : (standingPoint.HasAIMovingTo ? standingPoint.MovingAgent : null);
			this.HandleAgentStopUsingStandingPoint(agent, standingPoint);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003833C File Offset: 0x0003653C
		protected virtual void HandleAgentStopUsingStandingPoint(Agent agent, StandingPoint standingPoint)
		{
			Agent.StopUsingGameObjectFlags stopUsingGameObjectFlags = Agent.StopUsingGameObjectFlags.None;
			if (agent.Team == null || agent.IsRunningAway)
			{
				stopUsingGameObjectFlags |= Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject;
			}
			else
			{
				if (this.UsableMachine.AutoAttachUserToFormation(agent.Team.Side))
				{
					stopUsingGameObjectFlags |= Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject;
				}
				if (this.UsableMachine.HasToBeDefendedByUser(agent.Team.Side))
				{
					stopUsingGameObjectFlags |= Agent.StopUsingGameObjectFlags.DefendAfterStoppingUsingGameObject;
				}
			}
			agent.StopUsingGameObjectMT(true, stopUsingGameObjectFlags);
		}

		// Token: 0x0400046A RID: 1130
		protected readonly UsableMachine UsableMachine;

		// Token: 0x0400046B RID: 1131
		private GameEntity _lastActiveWaitStandingPoint;
	}
}
