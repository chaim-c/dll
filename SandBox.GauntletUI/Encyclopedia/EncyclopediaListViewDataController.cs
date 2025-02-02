using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.Encyclopedia
{
	// Token: 0x02000038 RID: 56
	public class EncyclopediaListViewDataController
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public EncyclopediaListViewDataController()
		{
			this._listData = new Dictionary<EncyclopediaPage, EncyclopediaListViewDataController.EncyclopediaListViewData>();
			foreach (EncyclopediaPage key in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages())
			{
				if (!this._listData.ContainsKey(key))
				{
					this._listData.Add(key, new EncyclopediaListViewDataController.EncyclopediaListViewData(new MBBindingList<EncyclopediaFilterGroupVM>(), 0, "", false));
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000EB00 File Offset: 0x0000CD00
		public void SaveListData(EncyclopediaListVM list, string id)
		{
			if (list != null && this._listData.ContainsKey(list.Page))
			{
				EncyclopediaListSortControllerVM sortController = list.SortController;
				int? num;
				if (sortController == null)
				{
					num = null;
				}
				else
				{
					EncyclopediaListSelectorVM sortSelection = sortController.SortSelection;
					num = ((sortSelection != null) ? new int?(sortSelection.SelectedIndex) : null);
				}
				int num2 = num ?? 0;
				Dictionary<EncyclopediaPage, EncyclopediaListViewDataController.EncyclopediaListViewData> listData = this._listData;
				EncyclopediaPage page = list.Page;
				MBBindingList<EncyclopediaFilterGroupVM> filterGroups = list.FilterGroups;
				int selectedSortIndex = num2;
				EncyclopediaListSortControllerVM sortController2 = list.SortController;
				listData[page] = new EncyclopediaListViewDataController.EncyclopediaListViewData(filterGroups, selectedSortIndex, id, sortController2 != null && sortController2.GetSortOrder());
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public void LoadListData(EncyclopediaListVM list)
		{
			if (list != null && this._listData.ContainsKey(list.Page))
			{
				EncyclopediaListViewDataController.EncyclopediaListViewData encyclopediaListViewData = this._listData[list.Page];
				EncyclopediaListSortControllerVM sortController = list.SortController;
				if (sortController != null)
				{
					sortController.SetSortSelection(encyclopediaListViewData.SelectedSortIndex);
				}
				EncyclopediaListSortControllerVM sortController2 = list.SortController;
				if (sortController2 != null)
				{
					sortController2.SetSortOrder(encyclopediaListViewData.IsAscending);
				}
				list.CopyFiltersFrom(encyclopediaListViewData.Filters);
				list.LastSelectedItemId = encyclopediaListViewData.LastSelectedItemId;
			}
		}

		// Token: 0x04000104 RID: 260
		private Dictionary<EncyclopediaPage, EncyclopediaListViewDataController.EncyclopediaListViewData> _listData;

		// Token: 0x0200004F RID: 79
		private readonly struct EncyclopediaListViewData
		{
			// Token: 0x060002CC RID: 716 RVA: 0x00013B64 File Offset: 0x00011D64
			public EncyclopediaListViewData(MBBindingList<EncyclopediaFilterGroupVM> filters, int selectedSortIndex, string lastSelectedItemId, bool isAscending)
			{
				Dictionary<EncyclopediaFilterItem, bool> dictionary = new Dictionary<EncyclopediaFilterItem, bool>();
				foreach (EncyclopediaFilterGroupVM encyclopediaFilterGroupVM in filters)
				{
					foreach (EncyclopediaListFilterVM encyclopediaListFilterVM in encyclopediaFilterGroupVM.Filters)
					{
						if (!dictionary.ContainsKey(encyclopediaListFilterVM.Filter))
						{
							dictionary.Add(encyclopediaListFilterVM.Filter, encyclopediaListFilterVM.IsSelected);
						}
					}
				}
				this.Filters = dictionary;
				this.SelectedSortIndex = selectedSortIndex;
				this.LastSelectedItemId = lastSelectedItemId;
				this.IsAscending = isAscending;
			}

			// Token: 0x040001AB RID: 427
			public readonly Dictionary<EncyclopediaFilterItem, bool> Filters;

			// Token: 0x040001AC RID: 428
			public readonly int SelectedSortIndex;

			// Token: 0x040001AD RID: 429
			public readonly string LastSelectedItemId;

			// Token: 0x040001AE RID: 430
			public readonly bool IsAscending;
		}
	}
}
