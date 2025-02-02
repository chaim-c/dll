using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x02000026 RID: 38
	public class PartySortControllerVM : ViewModel
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0001394C File Offset: 0x00011B4C
		public PartySortControllerVM(PartyScreenLogic.PartyRosterSide rosterSide, Action<PartyScreenLogic.PartyRosterSide, PartyScreenLogic.TroopSortType, bool> onSort)
		{
			this._rosterSide = rosterSide;
			this.SortOptions = new SelectorVM<TroopSortSelectorItemVM>(-1, new Action<SelectorVM<TroopSortSelectorItemVM>>(this.OnSortSelected));
			this.SortOptions.AddItem(new TroopSortSelectorItemVM(new TextObject("{=zMMqgxb1}Type", null), PartyScreenLogic.TroopSortType.Type));
			this.SortOptions.AddItem(new TroopSortSelectorItemVM(new TextObject("{=PDdh1sBj}Name", null), PartyScreenLogic.TroopSortType.Name));
			this.SortOptions.AddItem(new TroopSortSelectorItemVM(new TextObject("{=zFDoDbNj}Count", null), PartyScreenLogic.TroopSortType.Count));
			this.SortOptions.AddItem(new TroopSortSelectorItemVM(new TextObject("{=cc1d7mkq}Tier", null), PartyScreenLogic.TroopSortType.Tier));
			this.SortOptions.AddItem(new TroopSortSelectorItemVM(new TextObject("{=jvOYgHOe}Custom", null), PartyScreenLogic.TroopSortType.Custom));
			this.SortOptions.SelectedIndex = this.SortOptions.ItemList.Count - 1;
			this.IsAscending = true;
			this._onSort = onSort;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00013A38 File Offset: 0x00011C38
		private void OnSortSelected(SelectorVM<TroopSortSelectorItemVM> selector)
		{
			this._sortType = selector.SelectedItem.SortType;
			this.IsCustomSort = (this._sortType == PartyScreenLogic.TroopSortType.Custom);
			Action<PartyScreenLogic.PartyRosterSide, PartyScreenLogic.TroopSortType, bool> onSort = this._onSort;
			if (onSort == null)
			{
				return;
			}
			onSort(this._rosterSide, this._sortType, this.IsAscending);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00013A88 File Offset: 0x00011C88
		public void SelectSortType(PartyScreenLogic.TroopSortType sortType)
		{
			for (int i = 0; i < this.SortOptions.ItemList.Count; i++)
			{
				if (this.SortOptions.ItemList[i].SortType == sortType)
				{
					this.SortOptions.SelectedIndex = i;
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00013AD5 File Offset: 0x00011CD5
		public void SortWith(PartyScreenLogic.TroopSortType sortType, bool isAscending)
		{
			Action<PartyScreenLogic.PartyRosterSide, PartyScreenLogic.TroopSortType, bool> onSort = this._onSort;
			if (onSort == null)
			{
				return;
			}
			onSort(this._rosterSide, sortType, isAscending);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00013AEF File Offset: 0x00011CEF
		public void ExecuteToggleOrder()
		{
			this.IsAscending = !this.IsAscending;
			Action<PartyScreenLogic.PartyRosterSide, PartyScreenLogic.TroopSortType, bool> onSort = this._onSort;
			if (onSort == null)
			{
				return;
			}
			onSort(this._rosterSide, this._sortType, this.IsAscending);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00013B22 File Offset: 0x00011D22
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00013B2A File Offset: 0x00011D2A
		[DataSourceProperty]
		public bool IsAscending
		{
			get
			{
				return this._isAscending;
			}
			set
			{
				if (value != this._isAscending)
				{
					this._isAscending = value;
					base.OnPropertyChangedWithValue(value, "IsAscending");
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00013B48 File Offset: 0x00011D48
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00013B50 File Offset: 0x00011D50
		[DataSourceProperty]
		public bool IsCustomSort
		{
			get
			{
				return this._isCustomSort;
			}
			set
			{
				if (value != this._isCustomSort)
				{
					this._isCustomSort = value;
					base.OnPropertyChangedWithValue(value, "IsCustomSort");
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00013B6E File Offset: 0x00011D6E
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00013B76 File Offset: 0x00011D76
		[DataSourceProperty]
		public SelectorVM<TroopSortSelectorItemVM> SortOptions
		{
			get
			{
				return this._sortOptions;
			}
			set
			{
				if (value != this._sortOptions)
				{
					this._sortOptions = value;
					base.OnPropertyChangedWithValue<SelectorVM<TroopSortSelectorItemVM>>(value, "SortOptions");
				}
			}
		}

		// Token: 0x04000155 RID: 341
		private readonly PartyScreenLogic.PartyRosterSide _rosterSide;

		// Token: 0x04000156 RID: 342
		private readonly Action<PartyScreenLogic.PartyRosterSide, PartyScreenLogic.TroopSortType, bool> _onSort;

		// Token: 0x04000157 RID: 343
		private PartyScreenLogic.TroopSortType _sortType;

		// Token: 0x04000158 RID: 344
		private bool _isAscending;

		// Token: 0x04000159 RID: 345
		private bool _isCustomSort;

		// Token: 0x0400015A RID: 346
		private SelectorVM<TroopSortSelectorItemVM> _sortOptions;
	}
}
