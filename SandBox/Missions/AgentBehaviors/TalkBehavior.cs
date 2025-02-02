using System;
using SandBox.Conversation.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000086 RID: 134
	public class TalkBehavior : AgentBehavior
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00023578 File Offset: 0x00021778
		public TalkBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._startConversation = true;
			this._doNotMove = true;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00023590 File Offset: 0x00021790
		public override void Tick(float dt, bool isSimulation)
		{
			if (!this._startConversation || base.Mission.MainAgent == null || !base.Mission.MainAgent.IsActive() || base.Mission.Mode == MissionMode.Conversation || base.Mission.Mode == MissionMode.Battle || base.Mission.Mode == MissionMode.Barter)
			{
				return;
			}
			float interactionDistanceToUsable = base.OwnerAgent.GetInteractionDistanceToUsable(base.Mission.MainAgent);
			if (base.OwnerAgent.Position.DistanceSquared(base.Mission.MainAgent.Position) < (interactionDistanceToUsable + 3f) * (interactionDistanceToUsable + 3f) && base.Navigator.CanSeeAgent(base.Mission.MainAgent))
			{
				AgentNavigator navigator = base.Navigator;
				WorldPosition worldPosition = base.OwnerAgent.GetWorldPosition();
				MatrixFrame frame = base.OwnerAgent.Frame;
				navigator.SetTargetFrame(worldPosition, frame.rotation.f.AsVec2.RotationInRadians, 1f, -10f, Agent.AIScriptedFrameFlags.DoNotRun, false);
				MissionConversationLogic missionBehavior = base.Mission.GetMissionBehavior<MissionConversationLogic>();
				if (missionBehavior != null && missionBehavior.IsReadyForConversation)
				{
					missionBehavior.OnAgentInteraction(base.Mission.MainAgent, base.OwnerAgent);
					this._startConversation = false;
					return;
				}
			}
			else if (!this._doNotMove)
			{
				AgentNavigator navigator2 = base.Navigator;
				WorldPosition worldPosition2 = Agent.Main.GetWorldPosition();
				MatrixFrame frame = Agent.Main.Frame;
				navigator2.SetTargetFrame(worldPosition2, frame.rotation.f.AsVec2.RotationInRadians, 1f, -10f, Agent.AIScriptedFrameFlags.DoNotRun, false);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00023724 File Offset: 0x00021924
		public override float GetAvailability(bool isSimulation)
		{
			if (isSimulation)
			{
				return 0f;
			}
			if (this._startConversation && base.Mission.MainAgent != null && base.Mission.MainAgent.IsActive())
			{
				float num = base.OwnerAgent.GetInteractionDistanceToUsable(base.Mission.MainAgent) + 3f;
				if (base.OwnerAgent.Position.DistanceSquared(base.Mission.MainAgent.Position) < num * num && base.Mission.Mode != MissionMode.Conversation && !base.Mission.MainAgent.IsEnemyOf(base.OwnerAgent))
				{
					return 1f;
				}
			}
			return 0f;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000237DD File Offset: 0x000219DD
		public override string GetDebugInfo()
		{
			return "Talk";
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000237E4 File Offset: 0x000219E4
		protected override void OnDeactivate()
		{
			base.Navigator.ClearTarget();
			this.Disable();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000237F7 File Offset: 0x000219F7
		public void Disable()
		{
			this._startConversation = false;
			this._doNotMove = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00023807 File Offset: 0x00021A07
		public void Enable(bool doNotMove)
		{
			this._startConversation = true;
			this._doNotMove = doNotMove;
		}

		// Token: 0x0400027F RID: 639
		private bool _doNotMove;

		// Token: 0x04000280 RID: 640
		private bool _startConversation;
	}
}
