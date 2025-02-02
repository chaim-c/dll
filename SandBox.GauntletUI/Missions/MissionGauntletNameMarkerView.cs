using System;
using System.Collections.Generic;
using SandBox.View.Missions;
using SandBox.ViewModelCollection;
using SandBox.ViewModelCollection.Missions.NameMarker;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x02000018 RID: 24
	[OverrideView(typeof(MissionNameMarkerUIHandler))]
	public class MissionGauntletNameMarkerView : MissionView
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000087DA File Offset: 0x000069DA
		public MissionGauntletNameMarkerView()
		{
			this._additionalTargetAgents = new Dictionary<Agent, SandBoxUIHelper.IssueQuestFlags>();
			this._additionalGenericTargets = new Dictionary<string, ValueTuple<Vec3, string, string>>();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000087F8 File Offset: 0x000069F8
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._dataSource = new MissionNameMarkerVM(base.Mission, base.MissionScreen.CombatCamera, this._additionalTargetAgents, this._additionalGenericTargets);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("NameMarker", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			CampaignEvents.ConversationEnded.AddNonSerializedListener(this, new Action<IEnumerable<CharacterObject>>(this.OnConversationEnd));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008884 File Offset: 0x00006A84
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._additionalTargetAgents.Clear();
			CampaignEvents.ConversationEnded.ClearListeners(this);
			InformationManager.ClearAllMessages();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000088DC File Offset: 0x00006ADC
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.Input.IsGameKeyDown(5))
			{
				this._dataSource.IsEnabled = true;
			}
			else
			{
				this._dataSource.IsEnabled = false;
			}
			this._dataSource.Tick(dt);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008919 File Offset: 0x00006B19
		public override void OnAgentBuild(Agent affectedAgent, Banner banner)
		{
			base.OnAgentBuild(affectedAgent, banner);
			MissionNameMarkerVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnAgentBuild(affectedAgent);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008934 File Offset: 0x00006B34
		public override void OnAgentDeleted(Agent affectedAgent)
		{
			this._dataSource.OnAgentDeleted(affectedAgent);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008942 File Offset: 0x00006B42
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			this._dataSource.OnAgentRemoved(affectedAgent);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008950 File Offset: 0x00006B50
		private void OnConversationEnd(IEnumerable<CharacterObject> conversationCharacters)
		{
			this._dataSource.OnConversationEnd();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000895D File Offset: 0x00006B5D
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000897A File Offset: 0x00006B7A
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008998 File Offset: 0x00006B98
		public void UpdateAgentTargetQuestStatus(Agent agent, SandBoxUIHelper.IssueQuestFlags issueQuestFlags)
		{
			if (agent != null)
			{
				MissionNameMarkerVM dataSource = this._dataSource;
				if (dataSource != null && dataSource.IsTargetsAdded)
				{
					this._dataSource.UpdateAdditionalTargetAgentQuestStatus(agent, issueQuestFlags);
					return;
				}
				SandBoxUIHelper.IssueQuestFlags issueQuestFlags2;
				if (this._additionalTargetAgents.TryGetValue(agent, out issueQuestFlags2))
				{
					this._additionalTargetAgents[agent] = issueQuestFlags;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000089E7 File Offset: 0x00006BE7
		public void AddGenericMarker(string markerIdentifier, Vec3 globalPosition, string name, string iconType)
		{
			MissionNameMarkerVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsTargetsAdded)
			{
				this._dataSource.AddGenericMarker(markerIdentifier, globalPosition, name, iconType);
				return;
			}
			this._additionalGenericTargets.Add(markerIdentifier, new ValueTuple<Vec3, string, string>(globalPosition, name, iconType));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008A23 File Offset: 0x00006C23
		public void RemoveGenericMarker(string markerIdentifier)
		{
			MissionNameMarkerVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsTargetsAdded)
			{
				this._dataSource.RemoveGenericMarker(markerIdentifier);
				return;
			}
			this._additionalGenericTargets.Remove(markerIdentifier);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008A54 File Offset: 0x00006C54
		public void AddAgentTarget(Agent agent, SandBoxUIHelper.IssueQuestFlags issueQuestFlags)
		{
			if (agent != null)
			{
				MissionNameMarkerVM dataSource = this._dataSource;
				if (dataSource != null && dataSource.IsTargetsAdded)
				{
					this._dataSource.AddAgentTarget(agent, true);
					this._dataSource.UpdateAdditionalTargetAgentQuestStatus(agent, issueQuestFlags);
					return;
				}
				SandBoxUIHelper.IssueQuestFlags issueQuestFlags2;
				if (!this._additionalTargetAgents.TryGetValue(agent, out issueQuestFlags2))
				{
					this._additionalTargetAgents.Add(agent, issueQuestFlags);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public void RemoveAgentTarget(Agent agent)
		{
			if (agent != null)
			{
				MissionNameMarkerVM dataSource = this._dataSource;
				if (dataSource != null && dataSource.IsTargetsAdded)
				{
					this._dataSource.RemoveAgentTarget(agent);
					return;
				}
				SandBoxUIHelper.IssueQuestFlags issueQuestFlags;
				if (this._additionalTargetAgents.TryGetValue(agent, out issueQuestFlags))
				{
					this._additionalTargetAgents.Remove(agent);
				}
			}
		}

		// Token: 0x0400006B RID: 107
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400006C RID: 108
		private MissionNameMarkerVM _dataSource;

		// Token: 0x0400006D RID: 109
		private readonly Dictionary<Agent, SandBoxUIHelper.IssueQuestFlags> _additionalTargetAgents;

		// Token: 0x0400006E RID: 110
		private Dictionary<string, ValueTuple<Vec3, string, string>> _additionalGenericTargets;
	}
}
