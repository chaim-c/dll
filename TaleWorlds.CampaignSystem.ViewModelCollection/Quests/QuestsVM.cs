using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x02000023 RID: 35
	public class QuestsVM : ViewModel
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00010F10 File Offset: 0x0000F110
		public QuestsVM(Action closeQuestsScreen)
		{
			this._closeQuestsScreen = closeQuestsScreen;
			this.ActiveQuestsList = new MBBindingList<QuestItemVM>();
			this.OldQuestsList = new MBBindingList<QuestItemVM>();
			this.CurrentQuestStages = new MBBindingList<QuestStageVM>();
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			QuestBase questSelection = this._viewDataTracker.GetQuestSelection();
			foreach (QuestBase questBase in from Q in Campaign.Current.QuestManager.Quests
			where Q.IsOngoing
			select Q)
			{
				QuestItemVM questItemVM = new QuestItemVM(questBase, new Action<QuestItemVM>(this.SetSelectedItem));
				if (questSelection != null && questBase == questSelection)
				{
					this.SetSelectedItem(questItemVM);
				}
				this.ActiveQuestsList.Add(questItemVM);
			}
			foreach (KeyValuePair<Hero, IssueBase> keyValuePair in from i in Campaign.Current.IssueManager.Issues
			where i.Value.IsSolvingWithAlternative
			select i)
			{
				QuestItemVM item = new QuestItemVM(keyValuePair.Value, new Action<QuestItemVM>(this.SetSelectedItem));
				this.ActiveQuestsList.Add(item);
			}
			foreach (JournalLogEntry journalLogEntry in Campaign.Current.LogEntryHistory.GetGameActionLogs<JournalLogEntry>((JournalLogEntry JournalLogEntry) => true))
			{
				if (journalLogEntry.IsEnded())
				{
					QuestItemVM item2 = new QuestItemVM(journalLogEntry, new Action<QuestItemVM>(this.SetSelectedItem), journalLogEntry.IsEndedUnsuccessfully() ? QuestsVM.QuestCompletionType.UnSuccessful : QuestsVM.QuestCompletionType.Successful);
					this.OldQuestsList.Add(item2);
				}
			}
			Comparer<QuestItemVM> comparer = Comparer<QuestItemVM>.Create((QuestItemVM q1, QuestItemVM q2) => q1.IsMainQuest.CompareTo(q2.IsMainQuest));
			this.ActiveQuestsList.Sort(comparer);
			if (!this.OldQuestsList.Any((QuestItemVM q) => q.IsSelected))
			{
				if (!this.ActiveQuestsList.Any((QuestItemVM q) => q.IsSelected))
				{
					if (this.ActiveQuestsList.Count > 0)
					{
						this.SetSelectedItem(this.ActiveQuestsList.FirstOrDefault<QuestItemVM>());
					}
					else if (this.OldQuestsList.Count > 0)
					{
						this.SetSelectedItem(this.OldQuestsList.FirstOrDefault<QuestItemVM>());
					}
				}
			}
			this.IsThereAnyQuest = (MathF.Max(this.ActiveQuestsList.Count, this.OldQuestsList.Count) > 0);
			List<TextObject> list = new List<TextObject>
			{
				new TextObject("{=7l0LGKRk}Date Started", null),
				new TextObject("{=Y8EcVL1c}Last Updated", null),
				new TextObject("{=BEXTcJaS}Time Due", null)
			};
			this.ActiveQuestsSortController = new QuestItemSortControllerVM(ref this._activeQuestsList);
			this.OldQuestsSortController = new QuestItemSortControllerVM(ref this._oldQuestsList);
			this.SortSelector = new SelectorVM<SelectorItemVM>(list, this._viewDataTracker.GetQuestSortTypeSelection(), new Action<SelectorVM<SelectorItemVM>>(this.OnSortOptionChanged));
			this.RefreshValues();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001129C File Offset: 0x0000F49C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.QuestGiverText = GameTexts.FindText("str_quest_given_by", null).ToString();
			this.TimeRemainingLbl = GameTexts.FindText("str_time_remaining", null).ToString();
			this.QuestTitleText = GameTexts.FindText("str_quests", null).ToString();
			this.NoActiveQuestText = GameTexts.FindText("str_no_active_quest", null).ToString();
			this.SortQuestsText = GameTexts.FindText("str_sort_quests", null).ToString();
			this.OldQuestsHint = new HintViewModel(GameTexts.FindText("str_old_quests_explanation", null), null);
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			GameTexts.SetVariable("RANK", GameTexts.FindText("str_active_quests", null));
			GameTexts.SetVariable("NUMBER", this.ActiveQuestsList.Count);
			this.ActiveQuestsText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			GameTexts.SetVariable("RANK", GameTexts.FindText("str_old_quests", null));
			GameTexts.SetVariable("NUMBER", this.OldQuestsList.Count);
			this.OldQuestsText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			this.CurrentQuestStages.ApplyActionOnAllItems(delegate(QuestStageVM x)
			{
				x.RefreshValues();
			});
			this.ActiveQuestsList.ApplyActionOnAllItems(delegate(QuestItemVM x)
			{
				x.RefreshValues();
			});
			this.OldQuestsList.ApplyActionOnAllItems(delegate(QuestItemVM x)
			{
				x.RefreshValues();
			});
			QuestItemVM selectedQuest = this.SelectedQuest;
			if (selectedQuest != null)
			{
				selectedQuest.RefreshValues();
			}
			HeroVM currentQuestGiverHero = this.CurrentQuestGiverHero;
			if (currentQuestGiverHero == null)
			{
				return;
			}
			currentQuestGiverHero.RefreshValues();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001146C File Offset: 0x0000F66C
		private void SetSelectedItem(QuestItemVM quest)
		{
			if (this._selectedQuest != quest)
			{
				this.CurrentQuestStages.Clear();
				if (this._selectedQuest != null)
				{
					this._selectedQuest.IsSelected = false;
				}
				if (quest != null)
				{
					quest.IsSelected = true;
				}
				this.SelectedQuest = quest;
				if (this._selectedQuest != null)
				{
					this.CurrentQuestGiverHero = this._selectedQuest.QuestGiverHero;
					this.CurrentQuestTitle = this._selectedQuest.Name;
					this.IsCurrentQuestGiverHeroHidden = this._selectedQuest.IsQuestGiverHeroHidden;
					using (IEnumerator<QuestStageVM> enumerator = this._selectedQuest.Stages.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							QuestStageVM item = enumerator.Current;
							this.CurrentQuestStages.Add(item);
						}
						goto IL_D0;
					}
				}
				this.CurrentQuestGiverHero = new HeroVM(null, false);
				this.CurrentQuestTitle = "";
				this.IsCurrentQuestGiverHeroHidden = true;
			}
			IL_D0:
			this._viewDataTracker.SetQuestSelection(quest.Quest);
			this.TimeRemainingHint = new HintViewModel(new TextObject("{=2nN1QuxZ}This quest will be failed unless completed in this time.", null), null);
			foreach (QuestStageVM questStageVM in this._selectedQuest.Stages)
			{
				this._viewDataTracker.OnQuestLogExamined(questStageVM.Log);
				questStageVM.UpdateIsNew();
				this._selectedQuest.UpdateIsUpdated();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public void ExecuteOpenQuestGiverEncyclopedia()
		{
			HeroVM currentQuestGiverHero = this.CurrentQuestGiverHero;
			if (currentQuestGiverHero == null)
			{
				return;
			}
			currentQuestGiverHero.ExecuteLink();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public void ExecuteClose()
		{
			this._closeQuestsScreen();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00011600 File Offset: 0x0000F800
		public void SetSelectedIssue(IssueBase issue)
		{
			foreach (QuestItemVM questItemVM in this.ActiveQuestsList)
			{
				if (questItemVM.Issue == issue)
				{
					this.SetSelectedItem(questItemVM);
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00011658 File Offset: 0x0000F858
		public void SetSelectedQuest(QuestBase quest)
		{
			foreach (QuestItemVM questItemVM in this.ActiveQuestsList)
			{
				if (questItemVM.Quest == quest)
				{
					this.SetSelectedItem(questItemVM);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000116B0 File Offset: 0x0000F8B0
		public void SetSelectedLog(JournalLogEntry log)
		{
			foreach (QuestItemVM questItemVM in this.OldQuestsList)
			{
				if (questItemVM.QuestLogEntry == log)
				{
					this.SetSelectedItem(questItemVM);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00011708 File Offset: 0x0000F908
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.OnFinalize();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00011720 File Offset: 0x0000F920
		private void OnSortOptionChanged(SelectorVM<SelectorItemVM> sortSelector)
		{
			this._viewDataTracker.SetQuestSortTypeSelection(sortSelector.SelectedIndex);
			this.ActiveQuestsSortController.SortByOption((QuestItemSortControllerVM.QuestItemSortOption)sortSelector.SelectedIndex);
			this.OldQuestsSortController.SortByOption((QuestItemSortControllerVM.QuestItemSortOption)sortSelector.SelectedIndex);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00011755 File Offset: 0x0000F955
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00011764 File Offset: 0x0000F964
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0001176C File Offset: 0x0000F96C
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0001178A File Offset: 0x0000F98A
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00011792 File Offset: 0x0000F992
		[DataSourceProperty]
		public QuestItemVM SelectedQuest
		{
			get
			{
				return this._selectedQuest;
			}
			set
			{
				if (value != this._selectedQuest)
				{
					this._selectedQuest = value;
					base.OnPropertyChangedWithValue<QuestItemVM>(value, "SelectedQuest");
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000117B0 File Offset: 0x0000F9B0
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000117B8 File Offset: 0x0000F9B8
		[DataSourceProperty]
		public MBBindingList<QuestItemVM> ActiveQuestsList
		{
			get
			{
				return this._activeQuestsList;
			}
			set
			{
				if (value != this._activeQuestsList)
				{
					this._activeQuestsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestItemVM>>(value, "ActiveQuestsList");
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000117D6 File Offset: 0x0000F9D6
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000117DE File Offset: 0x0000F9DE
		[DataSourceProperty]
		public MBBindingList<QuestItemVM> OldQuestsList
		{
			get
			{
				return this._oldQuestsList;
			}
			set
			{
				if (value != this._oldQuestsList)
				{
					this._oldQuestsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestItemVM>>(value, "OldQuestsList");
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000117FC File Offset: 0x0000F9FC
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00011804 File Offset: 0x0000FA04
		[DataSourceProperty]
		public HeroVM CurrentQuestGiverHero
		{
			get
			{
				return this._currentQuestGiverHero;
			}
			set
			{
				if (value != this._currentQuestGiverHero)
				{
					this._currentQuestGiverHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "CurrentQuestGiverHero");
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00011822 File Offset: 0x0000FA22
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0001182A File Offset: 0x0000FA2A
		[DataSourceProperty]
		public string TimeRemainingLbl
		{
			get
			{
				return this._timeRemainingLbl;
			}
			set
			{
				if (value != this._timeRemainingLbl)
				{
					this._timeRemainingLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "TimeRemainingLbl");
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0001184D File Offset: 0x0000FA4D
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00011855 File Offset: 0x0000FA55
		[DataSourceProperty]
		public bool IsThereAnyQuest
		{
			get
			{
				return this._isThereAnyQuest;
			}
			set
			{
				if (value != this._isThereAnyQuest)
				{
					this._isThereAnyQuest = value;
					base.OnPropertyChangedWithValue(value, "IsThereAnyQuest");
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00011873 File Offset: 0x0000FA73
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0001187B File Offset: 0x0000FA7B
		[DataSourceProperty]
		public string NoActiveQuestText
		{
			get
			{
				return this._noActiveQuestText;
			}
			set
			{
				if (value != this._noActiveQuestText)
				{
					this._noActiveQuestText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoActiveQuestText");
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0001189E File Offset: 0x0000FA9E
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000118A6 File Offset: 0x0000FAA6
		[DataSourceProperty]
		public string SortQuestsText
		{
			get
			{
				return this._sortQuestsText;
			}
			set
			{
				if (value != this._sortQuestsText)
				{
					this._sortQuestsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortQuestsText");
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000118C9 File Offset: 0x0000FAC9
		// (set) Token: 0x06000231 RID: 561 RVA: 0x000118D1 File Offset: 0x0000FAD1
		[DataSourceProperty]
		public string QuestGiverText
		{
			get
			{
				return this._questGiverText;
			}
			set
			{
				if (value != this._questGiverText)
				{
					this._questGiverText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuestGiverText");
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000118F4 File Offset: 0x0000FAF4
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000118FC File Offset: 0x0000FAFC
		[DataSourceProperty]
		public string QuestTitleText
		{
			get
			{
				return this._questTitleText;
			}
			set
			{
				if (value != this._questTitleText)
				{
					this._questTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuestTitleText");
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0001191F File Offset: 0x0000FB1F
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00011927 File Offset: 0x0000FB27
		[DataSourceProperty]
		public string OldQuestsText
		{
			get
			{
				return this._oldQuestsText;
			}
			set
			{
				if (value != this._oldQuestsText)
				{
					this._oldQuestsText = value;
					base.OnPropertyChangedWithValue<string>(value, "OldQuestsText");
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0001194A File Offset: 0x0000FB4A
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00011952 File Offset: 0x0000FB52
		[DataSourceProperty]
		public string ActiveQuestsText
		{
			get
			{
				return this._activeQuestsText;
			}
			set
			{
				if (value != this._activeQuestsText)
				{
					this._activeQuestsText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActiveQuestsText");
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00011975 File Offset: 0x0000FB75
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0001197D File Offset: 0x0000FB7D
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000119A0 File Offset: 0x0000FBA0
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000119A8 File Offset: 0x0000FBA8
		[DataSourceProperty]
		public string CurrentQuestTitle
		{
			get
			{
				return this._currentQuestTitle;
			}
			set
			{
				if (value != this._currentQuestTitle)
				{
					this._currentQuestTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentQuestTitle");
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000119CB File Offset: 0x0000FBCB
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000119D3 File Offset: 0x0000FBD3
		[DataSourceProperty]
		public bool IsCurrentQuestGiverHeroHidden
		{
			get
			{
				return this._isCurrentQuestGiverHeroHidden;
			}
			set
			{
				if (value != this._isCurrentQuestGiverHeroHidden)
				{
					this._isCurrentQuestGiverHeroHidden = value;
					base.OnPropertyChangedWithValue(value, "IsCurrentQuestGiverHeroHidden");
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000119F1 File Offset: 0x0000FBF1
		// (set) Token: 0x0600023F RID: 575 RVA: 0x000119F9 File Offset: 0x0000FBF9
		[DataSourceProperty]
		public MBBindingList<QuestStageVM> CurrentQuestStages
		{
			get
			{
				return this._currentQuestStages;
			}
			set
			{
				if (value != this._currentQuestStages)
				{
					this._currentQuestStages = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestStageVM>>(value, "CurrentQuestStages");
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00011A17 File Offset: 0x0000FC17
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00011A1F File Offset: 0x0000FC1F
		[DataSourceProperty]
		public HintViewModel TimeRemainingHint
		{
			get
			{
				return this._timeRemainingHint;
			}
			set
			{
				if (value != this._timeRemainingHint)
				{
					this._timeRemainingHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TimeRemainingHint");
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00011A3D File Offset: 0x0000FC3D
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00011A45 File Offset: 0x0000FC45
		[DataSourceProperty]
		public HintViewModel OldQuestsHint
		{
			get
			{
				return this._oldQuestsHint;
			}
			set
			{
				if (value != this._oldQuestsHint)
				{
					this._oldQuestsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OldQuestsHint");
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00011A63 File Offset: 0x0000FC63
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00011A6B File Offset: 0x0000FC6B
		[DataSourceProperty]
		public QuestItemSortControllerVM ActiveQuestsSortController
		{
			get
			{
				return this._activeQuestsSortController;
			}
			set
			{
				if (value != this._activeQuestsSortController)
				{
					this._activeQuestsSortController = value;
					base.OnPropertyChangedWithValue<QuestItemSortControllerVM>(value, "ActiveQuestsSortController");
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00011A89 File Offset: 0x0000FC89
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00011A91 File Offset: 0x0000FC91
		[DataSourceProperty]
		public QuestItemSortControllerVM OldQuestsSortController
		{
			get
			{
				return this._oldQuestsSortController;
			}
			set
			{
				if (value != this._oldQuestsSortController)
				{
					this._oldQuestsSortController = value;
					base.OnPropertyChangedWithValue<QuestItemSortControllerVM>(value, "OldQuestsSortController");
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00011AAF File Offset: 0x0000FCAF
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00011AB7 File Offset: 0x0000FCB7
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> SortSelector
		{
			get
			{
				return this._sortSelector;
			}
			set
			{
				if (value != this._sortSelector)
				{
					this._sortSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "SortSelector");
				}
			}
		}

		// Token: 0x040000F7 RID: 247
		private readonly Action _closeQuestsScreen;

		// Token: 0x040000F8 RID: 248
		private readonly IViewDataTracker _viewDataTracker;

		// Token: 0x040000F9 RID: 249
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040000FA RID: 250
		private MBBindingList<QuestItemVM> _activeQuestsList;

		// Token: 0x040000FB RID: 251
		private MBBindingList<QuestItemVM> _oldQuestsList;

		// Token: 0x040000FC RID: 252
		private QuestItemVM _selectedQuest;

		// Token: 0x040000FD RID: 253
		private HeroVM _currentQuestGiverHero;

		// Token: 0x040000FE RID: 254
		private string _activeQuestsText;

		// Token: 0x040000FF RID: 255
		private string _oldQuestsText;

		// Token: 0x04000100 RID: 256
		private string _timeRemainingLbl;

		// Token: 0x04000101 RID: 257
		private string _currentQuestTitle;

		// Token: 0x04000102 RID: 258
		private bool _isCurrentQuestGiverHeroHidden;

		// Token: 0x04000103 RID: 259
		private string _questGiverText;

		// Token: 0x04000104 RID: 260
		private string _questTitleText;

		// Token: 0x04000105 RID: 261
		private string _doneLbl;

		// Token: 0x04000106 RID: 262
		private string _noActiveQuestText;

		// Token: 0x04000107 RID: 263
		private string _sortQuestsText;

		// Token: 0x04000108 RID: 264
		private bool _isThereAnyQuest;

		// Token: 0x04000109 RID: 265
		private MBBindingList<QuestStageVM> _currentQuestStages;

		// Token: 0x0400010A RID: 266
		private HintViewModel _timeRemainingHint;

		// Token: 0x0400010B RID: 267
		private HintViewModel _oldQuestsHint;

		// Token: 0x0400010C RID: 268
		private QuestItemSortControllerVM _activeQuestsSortController;

		// Token: 0x0400010D RID: 269
		private QuestItemSortControllerVM _oldQuestsSortController;

		// Token: 0x0400010E RID: 270
		private SelectorVM<SelectorItemVM> _sortSelector;

		// Token: 0x0200016C RID: 364
		public enum QuestCompletionType
		{
			// Token: 0x04000F6B RID: 3947
			Active,
			// Token: 0x04000F6C RID: 3948
			Successful,
			// Token: 0x04000F6D RID: 3949
			UnSuccessful
		}
	}
}
