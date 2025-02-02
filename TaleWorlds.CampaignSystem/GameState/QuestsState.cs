using System;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000342 RID: 834
	public class QuestsState : GameState
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000C260C File Offset: 0x000C080C
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000C2614 File Offset: 0x000C0814
		public IssueBase InitialSelectedIssue { get; private set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000C261D File Offset: 0x000C081D
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x000C2625 File Offset: 0x000C0825
		public QuestBase InitialSelectedQuest { get; private set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000C262E File Offset: 0x000C082E
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x000C2636 File Offset: 0x000C0836
		public JournalLogEntry InitialSelectedLog { get; private set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000C263F File Offset: 0x000C083F
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000C2642 File Offset: 0x000C0842
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000C264A File Offset: 0x000C084A
		public IQuestsStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000C2653 File Offset: 0x000C0853
		public QuestsState()
		{
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000C265B File Offset: 0x000C085B
		public QuestsState(IssueBase initialSelectedIssue)
		{
			this.InitialSelectedIssue = initialSelectedIssue;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000C266A File Offset: 0x000C086A
		public QuestsState(QuestBase initialSelectedQuest)
		{
			this.InitialSelectedQuest = initialSelectedQuest;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000C2679 File Offset: 0x000C0879
		public QuestsState(JournalLogEntry initialSelectedLog)
		{
			this.InitialSelectedLog = initialSelectedLog;
		}

		// Token: 0x04000E03 RID: 3587
		private IQuestsStateHandler _handler;
	}
}
