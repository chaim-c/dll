﻿using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003A8 RID: 936
	public class JournalLogsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003831 RID: 14385 RVA: 0x000FDD28 File Offset: 0x000FBF28
		public override void RegisterEvents()
		{
			CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
			CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
			CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
			CampaignEvents.IssueLogAddedEvent.AddNonSerializedListener(this, new Action<IssueBase, bool>(this.OnIssueLogAdded));
			CampaignEvents.QuestLogAddedEvent.AddNonSerializedListener(this, new Action<QuestBase, bool>(this.OnQuestLogAdded));
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000FDDA8 File Offset: 0x000FBFA8
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000FDDAC File Offset: 0x000FBFAC
		private void OnIssueLogAdded(IssueBase issue, bool hideInformation)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(issue);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(issue);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(issue), IssueBase.IssueUpdateDetails.None);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000FDDE0 File Offset: 0x000FBFE0
		private void OnQuestLogAdded(QuestBase quest, bool hideInformation)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), IssueBase.IssueUpdateDetails.None);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000FDE14 File Offset: 0x000FC014
		private void OnQuestStarted(QuestBase quest)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), IssueBase.IssueUpdateDetails.None);
			LogEntry.AddLogEntry(new IssueQuestStartLogEntry(journalLogEntry.RelatedHero));
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000FDE58 File Offset: 0x000FC058
		private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), detail);
			LogEntry.AddLogEntry(new IssueQuestLogEntry(journalLogEntry.RelatedHero, journalLogEntry.Antagonist, detail));
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000FDEA4 File Offset: 0x000FC0A4
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver)
		{
			if (issueSolver == Hero.MainHero)
			{
				JournalLogEntry journalLogEntry = this.GetRelatedLog(issue);
				if (journalLogEntry == null)
				{
					journalLogEntry = this.CreateRelatedLog(issue);
					LogEntry.AddLogEntry(journalLogEntry);
				}
				journalLogEntry.Update(this.GetEntries(issue), details);
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000FDEE0 File Offset: 0x000FC0E0
		private JournalLogEntry CreateRelatedLog(IssueBase issue)
		{
			if (issue.IssueQuest != null)
			{
				return new JournalLogEntry(issue.IssueQuest.Title, issue.IssueQuest.QuestGiver, null, issue.IssueQuest.IsSpecialQuest, new MBObjectBase[]
				{
					issue,
					issue.IssueQuest
				});
			}
			return new JournalLogEntry(issue.Title, issue.IssueOwner, issue.CounterOfferHero, false, new MBObjectBase[]
			{
				issue
			});
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000FDF54 File Offset: 0x000FC154
		private JournalLogEntry CreateRelatedLog(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.CreateRelatedLog(issueOfQuest);
			}
			return new JournalLogEntry(quest.Title, quest.QuestGiver, null, quest.IsSpecialQuest, new MBObjectBase[]
			{
				quest
			});
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000FDF98 File Offset: 0x000FC198
		private JournalLogEntry GetRelatedLog(IssueBase issue)
		{
			return Campaign.Current.LogEntryHistory.FindLastGameActionLog<JournalLogEntry>((JournalLogEntry x) => x.IsRelatedTo(issue));
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000FDFD0 File Offset: 0x000FC1D0
		private JournalLogEntry GetRelatedLog(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.GetRelatedLog(issueOfQuest);
			}
			return Campaign.Current.LogEntryHistory.FindLastGameActionLog<JournalLogEntry>((JournalLogEntry x) => x.IsRelatedTo(quest));
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000FE01C File Offset: 0x000FC21C
		private MBReadOnlyList<JournalLog> GetEntries(IssueBase issue)
		{
			if (issue.IssueQuest == null)
			{
				return issue.JournalEntries;
			}
			MBList<JournalLog> mblist = issue.JournalEntries.ToMBList<JournalLog>();
			JournalLog journalLog = issue.IssueQuest.JournalEntries.FirstOrDefault<JournalLog>();
			if (journalLog != null)
			{
				int i;
				for (i = 0; i < mblist.Count; i++)
				{
					if (mblist[i].LogTime > journalLog.LogTime)
					{
						i--;
						break;
					}
				}
				mblist.InsertRange(i, issue.IssueQuest.JournalEntries);
			}
			return mblist;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000FE09C File Offset: 0x000FC29C
		private MBReadOnlyList<JournalLog> GetEntries(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.GetEntries(issueOfQuest);
			}
			return quest.JournalEntries;
		}
	}
}
