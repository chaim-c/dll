using System;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000048 RID: 72
	public class QuestNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public QuestNotificationItemVM(QuestBase quest, InformationData data, Action<QuestBase> onQuestNotificationInspect, Action<MapNotificationItemBaseVM> onRemove) : base(data)
		{
			this._quest = quest;
			this._onQuestNotificationInspect = onQuestNotificationInspect;
			this._onInspect = (this._onInspectAction = delegate()
			{
				this._onQuestNotificationInspect(this._quest);
			});
			base.NotificationIdentifier = "quest";
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public QuestNotificationItemVM(IssueBase issue, InformationData data, Action<IssueBase> onIssueNotificationInspect, Action<MapNotificationItemBaseVM> onRemove) : base(data)
		{
			this._issue = issue;
			this._onIssueNotificationInspect = onIssueNotificationInspect;
			this._onInspect = (this._onInspectAction = delegate()
			{
				this._onIssueNotificationInspect(this._issue);
			});
			base.NotificationIdentifier = "quest";
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001C738 File Offset: 0x0001A938
		public override void ManualRefreshRelevantStatus()
		{
			base.ManualRefreshRelevantStatus();
		}

		// Token: 0x0400026C RID: 620
		private QuestBase _quest;

		// Token: 0x0400026D RID: 621
		private IssueBase _issue;

		// Token: 0x0400026E RID: 622
		private Action<QuestBase> _onQuestNotificationInspect;

		// Token: 0x0400026F RID: 623
		private Action<IssueBase> _onIssueNotificationInspect;

		// Token: 0x04000270 RID: 624
		protected Action _onInspectAction;
	}
}
