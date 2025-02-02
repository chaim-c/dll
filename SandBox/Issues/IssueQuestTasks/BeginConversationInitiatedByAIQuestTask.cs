using System;
using SandBox.Conversation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Issues.IssueQuestTasks
{
	// Token: 0x02000090 RID: 144
	public class BeginConversationInitiatedByAIQuestTask : QuestTaskBase
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00025332 File Offset: 0x00023532
		public BeginConversationInitiatedByAIQuestTask(Agent agent, Action onSucceededAction, Action onFailedAction, Action onCanceledAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, onFailedAction, onCanceledAction)
		{
			this._conversationAgent = agent;
			base.IsLogged = false;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002534E File Offset: 0x0002354E
		public void MissionTick(float dt)
		{
			if (Mission.Current.MainAgent == null || this._conversationAgent == null)
			{
				return;
			}
			if (!this._conversationOpened && Mission.Current.Mode != MissionMode.Conversation)
			{
				this.OpenConversation(this._conversationAgent);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00025386 File Offset: 0x00023586
		private void OpenConversation(Agent agent)
		{
			ConversationMission.StartConversationWithAgent(agent);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002538E File Offset: 0x0002358E
		protected override void OnFinished()
		{
			this._conversationAgent = null;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00025397 File Offset: 0x00023597
		public override void SetReferences()
		{
			CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.MissionTick));
		}

		// Token: 0x040002A0 RID: 672
		private bool _conversationOpened;

		// Token: 0x040002A1 RID: 673
		private Agent _conversationAgent;
	}
}
