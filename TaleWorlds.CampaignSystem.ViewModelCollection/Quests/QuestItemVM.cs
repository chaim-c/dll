using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x0200001F RID: 31
	public class QuestItemVM : ViewModel
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000101A8 File Offset: 0x0000E3A8
		public QuestBase Quest { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public IssueBase Issue { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public JournalLogEntry QuestLogEntry { get; }

		// Token: 0x060001BE RID: 446 RVA: 0x000101C0 File Offset: 0x0000E3C0
		public QuestItemVM(JournalLogEntry questLogEntry, Action<QuestItemVM> onSelection, QuestsVM.QuestCompletionType completion)
		{
			this._onSelection = onSelection;
			this.QuestLogEntry = questLogEntry;
			this.Stages = new MBBindingList<QuestStageVM>();
			this._completionType = completion;
			this.IsCompleted = (this._completionType > QuestsVM.QuestCompletionType.Active);
			this.IsCompletedSuccessfully = (this._completionType == QuestsVM.QuestCompletionType.Successful);
			this.CompletionTypeAsInt = (int)this._completionType;
			bool isRemainingDaysHidden;
			if (!this.IsCompleted)
			{
				QuestBase quest = this.Quest;
				CampaignTime? campaignTime = (quest != null) ? new CampaignTime?(quest.QuestDueTime) : null;
				CampaignTime never = CampaignTime.Never;
				isRemainingDaysHidden = (campaignTime != null && (campaignTime == null || campaignTime.GetValueOrDefault() == never));
			}
			else
			{
				isRemainingDaysHidden = true;
			}
			this.IsRemainingDaysHidden = isRemainingDaysHidden;
			this.IsQuestGiverHeroHidden = false;
			this.IsMainQuest = questLogEntry.IsSpecial;
			foreach (JournalLog log in questLogEntry.GetEntries())
			{
				this.PopulateQuestLog(log, false);
			}
			this.Name = questLogEntry.Title.ToString();
			this.QuestGiverHero = new HeroVM(questLogEntry.RelatedHero, false);
			this.IsTracked = false;
			this.IsTrackable = false;
			this.RefreshValues();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00010308 File Offset: 0x0000E508
		public QuestItemVM(QuestBase quest, Action<QuestItemVM> onSelection)
		{
			this.Quest = quest;
			this._onSelection = onSelection;
			this.Stages = new MBBindingList<QuestStageVM>();
			this.CompletionTypeAsInt = 0;
			this.IsRemainingDaysHidden = (!this.Quest.IsOngoing || this.Quest.IsRemainingTimeHidden);
			this.IsQuestGiverHeroHidden = (this.Quest.QuestGiver == null);
			MBReadOnlyList<JournalLog> journalEntries = this.Quest.JournalEntries;
			for (int i = 0; i < journalEntries.Count; i++)
			{
				bool isLastStage = i == journalEntries.Count - 1;
				JournalLog log = journalEntries[i];
				this.PopulateQuestLog(log, isLastStage);
			}
			this.IsMainQuest = quest.IsSpecialQuest;
			if (!this.IsQuestGiverHeroHidden)
			{
				this.QuestGiverHero = new HeroVM(this.Quest.QuestGiver, false);
			}
			this.UpdateIsUpdated();
			this.IsTrackable = !this.Quest.IsFinalized;
			this.IsTracked = this.Quest.IsTrackEnabled;
			this.RefreshValues();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010408 File Offset: 0x0000E608
		public QuestItemVM(IssueBase issue, Action<QuestItemVM> onSelection)
		{
			this.Issue = issue;
			this._onSelection = onSelection;
			this.Stages = new MBBindingList<QuestStageVM>();
			this.IsCompleted = false;
			this.CompletionTypeAsInt = 0;
			this.IsRemainingDaysHidden = this.Issue.IsOngoingWithoutQuest;
			this.IsQuestGiverHeroHidden = false;
			this.UpdateRemainingTime(this.Issue.IssueDueTime);
			foreach (JournalLog log in issue.JournalEntries)
			{
				this.PopulateQuestLog(log, false);
			}
			this.Name = issue.Title.ToString();
			this.QuestGiverHero = new HeroVM(issue.IssueOwner, false);
			this.UpdateIsUpdated();
			this.IsTrackable = false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000104E4 File Offset: 0x0000E6E4
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Quest != null)
			{
				this.Name = this.Quest.Title.ToString();
				this.UpdateRemainingTime(this.Quest.QuestDueTime);
			}
			else if (this.Issue != null)
			{
				this.Name = this.Issue.Title.ToString();
				this.UpdateRemainingTime(this.Issue.IssueDueTime);
			}
			else if (this.QuestLogEntry != null)
			{
				this.Name = this.QuestLogEntry.Title.ToString();
			}
			HeroVM questGiverHero = this.QuestGiverHero;
			if (questGiverHero != null)
			{
				questGiverHero.RefreshValues();
			}
			this.Stages.ApplyActionOnAllItems(delegate(QuestStageVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private void UpdateRemainingTime(CampaignTime dueTime)
		{
			if (this.IsRemainingDaysHidden)
			{
				this.RemainingDays = 0;
			}
			else
			{
				this.RemainingDays = (int)(dueTime - CampaignTime.Now).ToDays;
			}
			GameTexts.SetVariable("DAY_IS_PLURAL", (this.RemainingDays > 1) ? 1 : 0);
			GameTexts.SetVariable("DAY", this.RemainingDays);
			if (dueTime.ToHours - CampaignTime.Now.ToHours < 24.0)
			{
				this.RemainingDaysText = GameTexts.FindText("str_less_than_a_day", null).ToString();
				this.RemainingDaysTextCombined = GameTexts.FindText("str_less_than_a_day", null).ToString();
				return;
			}
			this.RemainingDaysText = GameTexts.FindText("str_DAY_days_capital", null).ToString();
			this.RemainingDaysTextCombined = GameTexts.FindText("str_DAY_days", null).ToString();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001068C File Offset: 0x0000E88C
		private void PopulateQuestLog(JournalLog log, bool isLastStage)
		{
			string dateString = log.GetTimeText().ToString();
			if (log.Type != LogType.Text && log.Type != LogType.None)
			{
				int num = MathF.Max(log.Range, 0);
				int num2 = (log.Type == LogType.TwoWayContinuous) ? log.CurrentProgress : MathF.Max(log.CurrentProgress, 0);
				TextObject textObject = new TextObject("{=Pdo7PpS3}{TASK_NAME} {CURRENT_PROGRESS}/{TARGET_PROGRESS}", null);
				textObject.SetTextVariable("TASK_NAME", log.TaskName);
				textObject.SetTextVariable("CURRENT_PROGRESS", num2);
				textObject.SetTextVariable("TARGET_PROGRESS", num);
				QuestStageTaskVM stageTask = new QuestStageTaskVM(textObject, num2, num, log.Type);
				this.Stages.Add(new QuestStageVM(log, dateString, isLastStage, new Action(this.UpdateIsUpdated), stageTask));
				return;
			}
			this.Stages.Add(new QuestStageVM(log, log.LogText.ToString(), dateString, isLastStage, new Action(this.UpdateIsUpdated)));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00010777 File Offset: 0x0000E977
		public void UpdateIsUpdated()
		{
			this.IsUpdated = this.Stages.Any((QuestStageVM s) => s.IsNew);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000107A9 File Offset: 0x0000E9A9
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000107B7 File Offset: 0x0000E9B7
		public void ExecuteToggleQuestTrack()
		{
			if (this.Quest != null)
			{
				this.Quest.ToggleTrackedObjects();
				this.IsTracked = this.Quest.IsTrackEnabled;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000107DD File Offset: 0x0000E9DD
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000107E5 File Offset: 0x0000E9E5
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00010808 File Offset: 0x0000EA08
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00010810 File Offset: 0x0000EA10
		[DataSourceProperty]
		public int CompletionTypeAsInt
		{
			get
			{
				return this._completionTypeAsInt;
			}
			set
			{
				if (value != this._completionTypeAsInt)
				{
					this._completionTypeAsInt = value;
					base.OnPropertyChangedWithValue(value, "CompletionTypeAsInt");
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0001082E File Offset: 0x0000EA2E
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00010836 File Offset: 0x0000EA36
		[DataSourceProperty]
		public bool IsMainQuest
		{
			get
			{
				return this._isMainQuest;
			}
			set
			{
				if (value != this._isMainQuest)
				{
					this._isMainQuest = value;
					base.OnPropertyChangedWithValue(value, "IsMainQuest");
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00010854 File Offset: 0x0000EA54
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0001085C File Offset: 0x0000EA5C
		[DataSourceProperty]
		public bool IsCompletedSuccessfully
		{
			get
			{
				return this._isCompletedSuccessfully;
			}
			set
			{
				if (value != this._isCompletedSuccessfully)
				{
					this._isCompletedSuccessfully = value;
					base.OnPropertyChangedWithValue(value, "IsCompletedSuccessfully");
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0001087A File Offset: 0x0000EA7A
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00010882 File Offset: 0x0000EA82
		[DataSourceProperty]
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
			set
			{
				if (value != this._isCompleted)
				{
					this._isCompleted = value;
					base.OnPropertyChangedWithValue(value, "IsCompleted");
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000108A0 File Offset: 0x0000EAA0
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000108A8 File Offset: 0x0000EAA8
		[DataSourceProperty]
		public bool IsUpdated
		{
			get
			{
				return this._isUpdated;
			}
			set
			{
				if (value != this._isUpdated)
				{
					this._isUpdated = value;
					base.OnPropertyChangedWithValue(value, "IsUpdated");
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000108C6 File Offset: 0x0000EAC6
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000108CE File Offset: 0x0000EACE
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000108EC File Offset: 0x0000EAEC
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000108F4 File Offset: 0x0000EAF4
		[DataSourceProperty]
		public bool IsRemainingDaysHidden
		{
			get
			{
				return this._isRemainingDaysHidden;
			}
			set
			{
				if (value != this._isRemainingDaysHidden)
				{
					this._isRemainingDaysHidden = value;
					base.OnPropertyChangedWithValue(value, "IsRemainingDaysHidden");
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00010912 File Offset: 0x0000EB12
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0001091A File Offset: 0x0000EB1A
		[DataSourceProperty]
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (value != this._isTracked)
				{
					this._isTracked = value;
					base.OnPropertyChangedWithValue(value, "IsTracked");
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00010938 File Offset: 0x0000EB38
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00010940 File Offset: 0x0000EB40
		[DataSourceProperty]
		public bool IsTrackable
		{
			get
			{
				return this._isTrackable;
			}
			set
			{
				if (value != this._isTrackable)
				{
					this._isTrackable = value;
					base.OnPropertyChangedWithValue(value, "IsTrackable");
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0001095E File Offset: 0x0000EB5E
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00010966 File Offset: 0x0000EB66
		[DataSourceProperty]
		public string RemainingDaysText
		{
			get
			{
				return this._remainingDaysText;
			}
			set
			{
				if (value != this._remainingDaysText)
				{
					this._remainingDaysText = value;
					base.OnPropertyChangedWithValue<string>(value, "RemainingDaysText");
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00010989 File Offset: 0x0000EB89
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00010991 File Offset: 0x0000EB91
		[DataSourceProperty]
		public string RemainingDaysTextCombined
		{
			get
			{
				return this._remainingDaysTextCombined;
			}
			set
			{
				if (value != this._remainingDaysTextCombined)
				{
					this._remainingDaysTextCombined = value;
					base.OnPropertyChangedWithValue<string>(value, "RemainingDaysTextCombined");
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000109B4 File Offset: 0x0000EBB4
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000109BC File Offset: 0x0000EBBC
		[DataSourceProperty]
		public int RemainingDays
		{
			get
			{
				return this._remainingDays;
			}
			set
			{
				if (value != this._remainingDays)
				{
					this._remainingDays = value;
					base.OnPropertyChangedWithValue(value, "RemainingDays");
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000109DA File Offset: 0x0000EBDA
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000109E2 File Offset: 0x0000EBE2
		[DataSourceProperty]
		public HeroVM QuestGiverHero
		{
			get
			{
				return this._questGiverHero;
			}
			set
			{
				if (value != this._questGiverHero)
				{
					this._questGiverHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "QuestGiverHero");
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00010A00 File Offset: 0x0000EC00
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00010A08 File Offset: 0x0000EC08
		[DataSourceProperty]
		public bool IsQuestGiverHeroHidden
		{
			get
			{
				return this._isQuestGiverHeroHidden;
			}
			set
			{
				if (value != this._isQuestGiverHeroHidden)
				{
					this._isQuestGiverHeroHidden = value;
					base.OnPropertyChangedWithValue(value, "IsQuestGiverHeroHidden");
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00010A26 File Offset: 0x0000EC26
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00010A2E File Offset: 0x0000EC2E
		[DataSourceProperty]
		public MBBindingList<QuestStageVM> Stages
		{
			get
			{
				return this._stages;
			}
			set
			{
				if (value != this._stages)
				{
					this._stages = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestStageVM>>(value, "Stages");
				}
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly Action<QuestItemVM> _onSelection;

		// Token: 0x040000D0 RID: 208
		private QuestsVM.QuestCompletionType _completionType;

		// Token: 0x040000D1 RID: 209
		private string _name;

		// Token: 0x040000D2 RID: 210
		private string _remainingDaysText;

		// Token: 0x040000D3 RID: 211
		private string _remainingDaysTextCombined;

		// Token: 0x040000D4 RID: 212
		private int _remainingDays;

		// Token: 0x040000D5 RID: 213
		private int _completionTypeAsInt;

		// Token: 0x040000D6 RID: 214
		private bool _isRemainingDaysHidden;

		// Token: 0x040000D7 RID: 215
		private bool _isUpdated;

		// Token: 0x040000D8 RID: 216
		private bool _isSelected;

		// Token: 0x040000D9 RID: 217
		private bool _isCompleted;

		// Token: 0x040000DA RID: 218
		private bool _isCompletedSuccessfully;

		// Token: 0x040000DB RID: 219
		private bool _isTracked;

		// Token: 0x040000DC RID: 220
		private bool _isTrackable;

		// Token: 0x040000DD RID: 221
		private bool _isMainQuest;

		// Token: 0x040000DE RID: 222
		private HeroVM _questGiverHero;

		// Token: 0x040000DF RID: 223
		private bool _isQuestGiverHeroHidden;

		// Token: 0x040000E0 RID: 224
		private MBBindingList<QuestStageVM> _stages;
	}
}
