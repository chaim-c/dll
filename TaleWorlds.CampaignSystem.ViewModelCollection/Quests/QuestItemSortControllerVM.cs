using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x0200001E RID: 30
	public class QuestItemSortControllerVM : ViewModel
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000100A2 File Offset: 0x0000E2A2
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000100AA File Offset: 0x0000E2AA
		public QuestItemSortControllerVM.QuestItemSortOption? CurrentSortOption { get; private set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x000100B4 File Offset: 0x0000E2B4
		public QuestItemSortControllerVM(ref MBBindingList<QuestItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._dateStartedComparer = new QuestItemSortControllerVM.QuestItemDateStartedComparer();
			this._lastUpdatedComparer = new QuestItemSortControllerVM.QuestItemLastUpdatedComparer();
			this._timeDueComparer = new QuestItemSortControllerVM.QuestItemTimeDueComparer();
			this.IsThereAnyQuest = (this._listToControl.Count > 0);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00010104 File Offset: 0x0000E304
		private void ExecuteSortByDateStarted()
		{
			this._listToControl.Sort(this._dateStartedComparer);
			this.CurrentSortOption = new QuestItemSortControllerVM.QuestItemSortOption?(QuestItemSortControllerVM.QuestItemSortOption.DateStarted);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00010123 File Offset: 0x0000E323
		private void ExecuteSortByLastUpdated()
		{
			this._listToControl.Sort(this._lastUpdatedComparer);
			this.CurrentSortOption = new QuestItemSortControllerVM.QuestItemSortOption?(QuestItemSortControllerVM.QuestItemSortOption.LastUpdated);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00010142 File Offset: 0x0000E342
		private void ExecuteSortByTimeDue()
		{
			this._listToControl.Sort(this._timeDueComparer);
			this.CurrentSortOption = new QuestItemSortControllerVM.QuestItemSortOption?(QuestItemSortControllerVM.QuestItemSortOption.TimeDue);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00010161 File Offset: 0x0000E361
		public void SortByOption(QuestItemSortControllerVM.QuestItemSortOption sortOption)
		{
			if (sortOption == QuestItemSortControllerVM.QuestItemSortOption.DateStarted)
			{
				this.ExecuteSortByDateStarted();
				return;
			}
			if (sortOption == QuestItemSortControllerVM.QuestItemSortOption.LastUpdated)
			{
				this.ExecuteSortByLastUpdated();
				return;
			}
			if (sortOption == QuestItemSortControllerVM.QuestItemSortOption.TimeDue)
			{
				this.ExecuteSortByTimeDue();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00010182 File Offset: 0x0000E382
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0001018A File Offset: 0x0000E38A
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

		// Token: 0x040000C6 RID: 198
		private MBBindingList<QuestItemVM> _listToControl;

		// Token: 0x040000C7 RID: 199
		private QuestItemSortControllerVM.QuestItemDateStartedComparer _dateStartedComparer;

		// Token: 0x040000C8 RID: 200
		private QuestItemSortControllerVM.QuestItemLastUpdatedComparer _lastUpdatedComparer;

		// Token: 0x040000C9 RID: 201
		private QuestItemSortControllerVM.QuestItemTimeDueComparer _timeDueComparer;

		// Token: 0x040000CB RID: 203
		private bool _isThereAnyQuest;

		// Token: 0x02000166 RID: 358
		public enum QuestItemSortOption
		{
			// Token: 0x04000F64 RID: 3940
			DateStarted,
			// Token: 0x04000F65 RID: 3941
			LastUpdated,
			// Token: 0x04000F66 RID: 3942
			TimeDue
		}

		// Token: 0x02000167 RID: 359
		private abstract class QuestItemComparerBase : IComparer<QuestItemVM>
		{
			// Token: 0x06002023 RID: 8227
			public abstract int Compare(QuestItemVM x, QuestItemVM y);

			// Token: 0x06002024 RID: 8228 RVA: 0x000725C8 File Offset: 0x000707C8
			protected JournalLog GetJournalLogAt(QuestItemVM questItem, QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex logIndex)
			{
				if (questItem.Quest == null && questItem.Stages.Count > 0)
				{
					int index = (logIndex == QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.First) ? 0 : (questItem.Stages.Count - 1);
					return questItem.Stages[index].Log;
				}
				if (questItem.Quest != null && questItem.Quest.JournalEntries.Count > 0)
				{
					int index2 = (logIndex == QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.First) ? 0 : (questItem.Quest.JournalEntries.Count - 1);
					return questItem.Quest.JournalEntries[index2];
				}
				return null;
			}

			// Token: 0x020002AD RID: 685
			protected enum JournalLogIndex
			{
				// Token: 0x0400125C RID: 4700
				First,
				// Token: 0x0400125D RID: 4701
				Last
			}
		}

		// Token: 0x02000168 RID: 360
		private class QuestItemDateStartedComparer : QuestItemSortControllerVM.QuestItemComparerBase
		{
			// Token: 0x06002026 RID: 8230 RVA: 0x00072660 File Offset: 0x00070860
			public override int Compare(QuestItemVM first, QuestItemVM second)
			{
				JournalLog journalLogAt = base.GetJournalLogAt(first, QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.First);
				JournalLog journalLogAt2 = base.GetJournalLogAt(second, QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.First);
				if (journalLogAt != null && journalLogAt2 != null)
				{
					return journalLogAt.LogTime.CompareTo(journalLogAt2.LogTime);
				}
				if (journalLogAt == null && journalLogAt2 != null)
				{
					return -1;
				}
				if (journalLogAt != null && journalLogAt2 == null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000169 RID: 361
		private class QuestItemLastUpdatedComparer : QuestItemSortControllerVM.QuestItemComparerBase
		{
			// Token: 0x06002028 RID: 8232 RVA: 0x000726B4 File Offset: 0x000708B4
			public override int Compare(QuestItemVM first, QuestItemVM second)
			{
				JournalLog journalLogAt = base.GetJournalLogAt(first, QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.Last);
				JournalLog journalLogAt2 = base.GetJournalLogAt(second, QuestItemSortControllerVM.QuestItemComparerBase.JournalLogIndex.Last);
				if (journalLogAt != null && journalLogAt2 != null)
				{
					return journalLogAt2.LogTime.CompareTo(journalLogAt.LogTime);
				}
				if (journalLogAt == null && journalLogAt2 != null)
				{
					return -1;
				}
				if (journalLogAt != null && journalLogAt2 == null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0200016A RID: 362
		private class QuestItemTimeDueComparer : QuestItemSortControllerVM.QuestItemComparerBase
		{
			// Token: 0x0600202A RID: 8234 RVA: 0x00072708 File Offset: 0x00070908
			public override int Compare(QuestItemVM first, QuestItemVM second)
			{
				CampaignTime campaignTime = CampaignTime.Now;
				CampaignTime other = CampaignTime.Now;
				if (first.Quest != null)
				{
					campaignTime = first.Quest.QuestDueTime;
				}
				if (second.Quest != null)
				{
					other = second.Quest.QuestDueTime;
				}
				return campaignTime.CompareTo(other);
			}
		}
	}
}
