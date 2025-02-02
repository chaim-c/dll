using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000084 RID: 132
	public class ScriptBehavior : AgentBehavior
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00022F53 File Offset: 0x00021153
		public ScriptBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00022F5C File Offset: 0x0002115C
		public static void AddUsableMachineTarget(Agent ownerAgent, UsableMachine targetUsableMachine)
		{
			DailyBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			ScriptBehavior scriptBehavior = behaviorGroup.GetBehavior<ScriptBehavior>() ?? behaviorGroup.AddBehavior<ScriptBehavior>();
			bool flag = behaviorGroup.ScriptedBehavior != scriptBehavior;
			scriptBehavior._targetUsableMachine = targetUsableMachine;
			scriptBehavior._state = ScriptBehavior.State.GoToUsableMachine;
			scriptBehavior._sentToTarget = false;
			if (flag)
			{
				behaviorGroup.SetScriptedBehavior<ScriptBehavior>();
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00022FB4 File Offset: 0x000211B4
		public static void AddAgentTarget(Agent ownerAgent, Agent targetAgent)
		{
			DailyBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			ScriptBehavior scriptBehavior = behaviorGroup.GetBehavior<ScriptBehavior>() ?? behaviorGroup.AddBehavior<ScriptBehavior>();
			bool flag = behaviorGroup.ScriptedBehavior != scriptBehavior;
			scriptBehavior._targetAgent = targetAgent;
			scriptBehavior._state = ScriptBehavior.State.GoToAgent;
			scriptBehavior._sentToTarget = false;
			if (flag)
			{
				behaviorGroup.SetScriptedBehavior<ScriptBehavior>();
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002300C File Offset: 0x0002120C
		public static void AddWorldFrameTarget(Agent ownerAgent, WorldFrame targetWorldFrame)
		{
			DailyBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			ScriptBehavior scriptBehavior = behaviorGroup.GetBehavior<ScriptBehavior>() ?? behaviorGroup.AddBehavior<ScriptBehavior>();
			bool flag = behaviorGroup.ScriptedBehavior != scriptBehavior;
			scriptBehavior._targetFrame = targetWorldFrame;
			scriptBehavior._state = ScriptBehavior.State.GoToTargetFrame;
			scriptBehavior._sentToTarget = false;
			if (flag)
			{
				behaviorGroup.SetScriptedBehavior<ScriptBehavior>();
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00023064 File Offset: 0x00021264
		public static void AddTargetWithDelegate(Agent ownerAgent, ScriptBehavior.SelectTargetDelegate selectTargetDelegate, ScriptBehavior.OnTargetReachedDelegate onTargetReachedDelegate)
		{
			DailyBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			ScriptBehavior scriptBehavior = behaviorGroup.GetBehavior<ScriptBehavior>() ?? behaviorGroup.AddBehavior<ScriptBehavior>();
			bool flag = behaviorGroup.ScriptedBehavior != scriptBehavior;
			scriptBehavior._selectTargetDelegate = selectTargetDelegate;
			scriptBehavior._onTargetReachedDelegate = onTargetReachedDelegate;
			scriptBehavior._state = ScriptBehavior.State.NoTarget;
			scriptBehavior._sentToTarget = false;
			if (flag)
			{
				behaviorGroup.SetScriptedBehavior<ScriptBehavior>();
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000230C3 File Offset: 0x000212C3
		public bool IsNearTarget(Agent targetAgent)
		{
			return this._targetAgent == targetAgent && (this._state == ScriptBehavior.State.NearAgent || this._state == ScriptBehavior.State.NearStationaryTarget);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000230E4 File Offset: 0x000212E4
		public override void Tick(float dt, bool isSimulation)
		{
			if (this._state == ScriptBehavior.State.NoTarget)
			{
				if (this._selectTargetDelegate == null)
				{
					if (this.BehaviorGroup.ScriptedBehavior == this)
					{
						this.BehaviorGroup.DisableScriptedBehavior();
					}
					return;
				}
				this.SearchForNewTarget();
			}
			switch (this._state)
			{
			case ScriptBehavior.State.GoToUsableMachine:
				if (!this._sentToTarget)
				{
					base.Navigator.SetTarget(this._targetUsableMachine, false);
					this._sentToTarget = true;
					return;
				}
				if (base.OwnerAgent.IsUsingGameObject && base.OwnerAgent.Position.DistanceSquared(this._targetUsableMachine.GameEntity.GetGlobalFrame().origin) < 1f)
				{
					if (this.CheckForSearchNewTarget(ScriptBehavior.State.NearStationaryTarget))
					{
						base.OwnerAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						return;
					}
					this.RemoveTargets();
					return;
				}
				break;
			case ScriptBehavior.State.GoToAgent:
			{
				float interactionDistanceToUsable = base.OwnerAgent.GetInteractionDistanceToUsable(this._targetAgent);
				if (base.OwnerAgent.Position.DistanceSquared(this._targetAgent.Position) >= interactionDistanceToUsable * interactionDistanceToUsable)
				{
					AgentNavigator navigator = base.Navigator;
					WorldPosition worldPosition = this._targetAgent.GetWorldPosition();
					MatrixFrame frame = this._targetAgent.Frame;
					navigator.SetTargetFrame(worldPosition, frame.rotation.f.AsVec2.RotationInRadians, 1f, 1f, Agent.AIScriptedFrameFlags.None, false);
					return;
				}
				if (!this.CheckForSearchNewTarget(ScriptBehavior.State.NearAgent))
				{
					AgentNavigator navigator2 = base.Navigator;
					WorldPosition worldPosition2 = base.OwnerAgent.GetWorldPosition();
					MatrixFrame frame = base.OwnerAgent.Frame;
					navigator2.SetTargetFrame(worldPosition2, frame.rotation.f.AsVec2.RotationInRadians, 1f, 1f, Agent.AIScriptedFrameFlags.None, false);
					this.RemoveTargets();
					return;
				}
				break;
			}
			case ScriptBehavior.State.GoToTargetFrame:
				if (!this._sentToTarget)
				{
					base.Navigator.SetTargetFrame(this._targetFrame.Origin, this._targetFrame.Rotation.f.AsVec2.RotationInRadians, 1f, 1f, Agent.AIScriptedFrameFlags.DoNotRun, false);
					this._sentToTarget = true;
					return;
				}
				if (base.Navigator.IsTargetReached() && !this.CheckForSearchNewTarget(ScriptBehavior.State.NearStationaryTarget))
				{
					this.RemoveTargets();
					return;
				}
				break;
			case ScriptBehavior.State.NearAgent:
			{
				if (base.OwnerAgent.Position.DistanceSquared(this._targetAgent.Position) >= 1f)
				{
					this._state = ScriptBehavior.State.GoToAgent;
					return;
				}
				AgentNavigator navigator3 = base.Navigator;
				WorldPosition worldPosition3 = base.OwnerAgent.GetWorldPosition();
				MatrixFrame frame = base.OwnerAgent.Frame;
				navigator3.SetTargetFrame(worldPosition3, frame.rotation.f.AsVec2.RotationInRadians, 1f, 1f, Agent.AIScriptedFrameFlags.None, false);
				this.RemoveTargets();
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00023390 File Offset: 0x00021590
		private bool CheckForSearchNewTarget(ScriptBehavior.State endState)
		{
			bool flag = false;
			if (this._onTargetReachedDelegate != null)
			{
				flag = this._onTargetReachedDelegate(base.OwnerAgent, ref this._targetAgent, ref this._targetUsableMachine, ref this._targetFrame);
			}
			if (flag)
			{
				this.SearchForNewTarget();
			}
			else
			{
				this._state = endState;
			}
			return flag;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000233E0 File Offset: 0x000215E0
		private void SearchForNewTarget()
		{
			Agent agent = null;
			UsableMachine usableMachine = null;
			WorldFrame invalid = WorldFrame.Invalid;
			if (this._selectTargetDelegate(base.OwnerAgent, ref agent, ref usableMachine, ref invalid))
			{
				if (agent != null)
				{
					this._targetAgent = agent;
					this._state = ScriptBehavior.State.GoToAgent;
					return;
				}
				if (usableMachine != null)
				{
					this._targetUsableMachine = usableMachine;
					this._state = ScriptBehavior.State.GoToUsableMachine;
					return;
				}
				this._targetFrame = invalid;
				this._state = ScriptBehavior.State.GoToTargetFrame;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00023442 File Offset: 0x00021642
		public override float GetAvailability(bool isSimulation)
		{
			return (float)((this._state == ScriptBehavior.State.NoTarget) ? 0 : 1);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00023451 File Offset: 0x00021651
		protected override void OnDeactivate()
		{
			base.Navigator.ClearTarget();
			this.RemoveTargets();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00023464 File Offset: 0x00021664
		private void RemoveTargets()
		{
			this._targetUsableMachine = null;
			this._targetAgent = null;
			this._targetFrame = WorldFrame.Invalid;
			this._state = ScriptBehavior.State.NoTarget;
			this._selectTargetDelegate = null;
			this._onTargetReachedDelegate = null;
			this._sentToTarget = false;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002349B File Offset: 0x0002169B
		public override string GetDebugInfo()
		{
			return "Scripted";
		}

		// Token: 0x04000275 RID: 629
		private UsableMachine _targetUsableMachine;

		// Token: 0x04000276 RID: 630
		private Agent _targetAgent;

		// Token: 0x04000277 RID: 631
		private WorldFrame _targetFrame;

		// Token: 0x04000278 RID: 632
		private ScriptBehavior.State _state;

		// Token: 0x04000279 RID: 633
		private bool _sentToTarget;

		// Token: 0x0400027A RID: 634
		private ScriptBehavior.SelectTargetDelegate _selectTargetDelegate;

		// Token: 0x0400027B RID: 635
		private ScriptBehavior.OnTargetReachedDelegate _onTargetReachedDelegate;

		// Token: 0x02000155 RID: 341
		// (Invoke) Token: 0x06000C55 RID: 3157
		public delegate bool SelectTargetDelegate(Agent agent, ref Agent targetAgent, ref UsableMachine targetUsableMachine, ref WorldFrame targetFrame);

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06000C59 RID: 3161
		public delegate bool OnTargetReachedDelegate(Agent agent, ref Agent targetAgent, ref UsableMachine targetUsableMachine, ref WorldFrame targetFrame);

		// Token: 0x02000157 RID: 343
		private enum State
		{
			// Token: 0x040005C7 RID: 1479
			NoTarget,
			// Token: 0x040005C8 RID: 1480
			GoToUsableMachine,
			// Token: 0x040005C9 RID: 1481
			GoToAgent,
			// Token: 0x040005CA RID: 1482
			GoToTargetFrame,
			// Token: 0x040005CB RID: 1483
			NearAgent,
			// Token: 0x040005CC RID: 1484
			NearStationaryTarget
		}
	}
}
