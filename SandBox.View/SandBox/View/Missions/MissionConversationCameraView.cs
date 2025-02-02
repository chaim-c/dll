using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x02000015 RID: 21
	public class MissionConversationCameraView : MissionView
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00005BE2 File Offset: 0x00003DE2
		public override void AfterStart()
		{
			this._missionMainAgentController = base.Mission.GetMissionBehavior<MissionMainAgentController>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public override bool UpdateOverridenCamera(float dt)
		{
			MissionMode mode = base.Mission.Mode;
			if ((mode == MissionMode.Conversation || mode == MissionMode.Barter) && !base.MissionScreen.IsCheatGhostMode)
			{
				this.UpdateAgentLooksForConversation();
			}
			else if (this._missionMainAgentController.CustomLookDir.IsNonZero)
			{
				this._missionMainAgentController.CustomLookDir = Vec3.Zero;
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005C54 File Offset: 0x00003E54
		private void UpdateAgentLooksForConversation()
		{
			Agent agent = null;
			ConversationManager conversationManager = Campaign.Current.ConversationManager;
			if (conversationManager.ConversationAgents != null && conversationManager.ConversationAgents.Count > 0)
			{
				this._speakerAgent = (Agent)conversationManager.SpeakerAgent;
				this._listenerAgent = (Agent)conversationManager.ListenerAgent;
				agent = Agent.Main.GetLookAgent();
				if (this._speakerAgent == null)
				{
					return;
				}
				foreach (IAgent agent2 in conversationManager.ConversationAgents)
				{
					if (agent2 != this._speakerAgent)
					{
						this.MakeAgentLookToSpeaker((Agent)agent2);
					}
				}
				this.MakeSpeakerLookToListener();
			}
			this.SetFocusedObjectForCameraFocus();
			if (Agent.Main.GetLookAgent() != agent && this._speakerAgent != null)
			{
				this.SpeakerAgentIsChanged();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005D34 File Offset: 0x00003F34
		private void SpeakerAgentIsChanged()
		{
			Mission.Current.ConversationCharacterChanged();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005D40 File Offset: 0x00003F40
		private void SetFocusedObjectForCameraFocus()
		{
			if (this._speakerAgent == Agent.Main)
			{
				this._missionMainAgentController.InteractionComponent.SetCurrentFocusedObject(this._listenerAgent, null, true);
				this._missionMainAgentController.CustomLookDir = (this._listenerAgent.Position - Agent.Main.Position).NormalizedCopy();
				Agent.Main.SetLookAgent(this._listenerAgent);
				return;
			}
			this._missionMainAgentController.InteractionComponent.SetCurrentFocusedObject(this._speakerAgent, null, true);
			this._missionMainAgentController.CustomLookDir = (this._speakerAgent.Position - Agent.Main.Position).NormalizedCopy();
			Agent.Main.SetLookAgent(this._speakerAgent);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005E08 File Offset: 0x00004008
		private void MakeAgentLookToSpeaker(Agent agent)
		{
			Vec3 position = agent.Position;
			Vec3 position2 = this._speakerAgent.Position;
			position.z = agent.AgentVisuals.GetGlobalStableEyePoint(true).z;
			position2.z = this._speakerAgent.AgentVisuals.GetGlobalStableEyePoint(true).z;
			agent.SetLookToPointOfInterest(this._speakerAgent.AgentVisuals.GetGlobalStableEyePoint(true));
			agent.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			agent.LookDirection = (position2 - position).NormalizedCopy();
			agent.SetLookAgent(this._speakerAgent);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005EA8 File Offset: 0x000040A8
		private void MakeSpeakerLookToListener()
		{
			Vec3 position = this._speakerAgent.Position;
			Vec3 position2 = this._listenerAgent.Position;
			position.z = this._speakerAgent.AgentVisuals.GetGlobalStableEyePoint(true).z;
			position2.z = this._listenerAgent.AgentVisuals.GetGlobalStableEyePoint(true).z;
			this._speakerAgent.SetLookToPointOfInterest(this._listenerAgent.AgentVisuals.GetGlobalStableEyePoint(true));
			this._speakerAgent.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			this._speakerAgent.LookDirection = (position2 - position).NormalizedCopy();
			this._speakerAgent.SetLookAgent(this._listenerAgent);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005F64 File Offset: 0x00004164
		private void SetConversationLook(Agent agent1, Agent agent2)
		{
			Vec3 position = agent2.Position;
			Vec3 position2 = agent1.Position;
			agent2.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			agent1.AgentVisuals.GetSkeleton().ForceUpdateBoneFrames();
			position.z = agent2.AgentVisuals.GetGlobalStableEyePoint(true).z;
			position2.z = agent1.AgentVisuals.GetGlobalStableEyePoint(true).z;
			agent1.SetLookToPointOfInterest(agent2.AgentVisuals.GetGlobalStableEyePoint(true));
			agent2.SetLookToPointOfInterest(agent1.AgentVisuals.GetGlobalStableEyePoint(true));
			agent1.LookDirection = (position2 - position).NormalizedCopy();
			agent2.LookDirection = (position - position2).NormalizedCopy();
			agent2.SetLookAgent(agent1);
			agent1.SetLookAgent(agent2);
		}

		// Token: 0x0400003B RID: 59
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x0400003C RID: 60
		private Agent _speakerAgent;

		// Token: 0x0400003D RID: 61
		private Agent _listenerAgent;
	}
}
