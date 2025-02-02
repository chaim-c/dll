using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C3 RID: 195
	public class EncyclopediaListSortControllerVM : ViewModel
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0004AB34 File Offset: 0x00048D34
		public EncyclopediaListSortControllerVM(EncyclopediaPage page, MBBindingList<EncyclopediaListItemVM> items)
		{
			this._page = page;
			this._items = items;
			this.UpdateSortItemsFromPage(page);
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004AB82 File Offset: 0x00048D82
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsHighlightEnabled = (evnt.NewNotificationElementID == "EncyclopediaSortButton");
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004AB9C File Offset: 0x00048D9C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameLabel = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.SortByLabel = GameTexts.FindText("str_sort_by_label", null).ToString();
			this.SortedValueLabelText = this._sortedValueLabel.ToString();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004ABEC File Offset: 0x00048DEC
		public void SetSortSelection(int index)
		{
			this.SortSelection.SelectedIndex = index;
			this.OnSortSelectionChanged(this.SortSelection);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004AC08 File Offset: 0x00048E08
		private void UpdateSortItemsFromPage(EncyclopediaPage page)
		{
			this.SortSelection = new EncyclopediaListSelectorVM(0, new Action<SelectorVM<EncyclopediaListSelectorItemVM>>(this.OnSortSelectionChanged), new Action(this.OnSortSelectionActivated));
			foreach (EncyclopediaSortController sortController in page.GetSortControllers())
			{
				EncyclopediaListItemComparer comparer = new EncyclopediaListItemComparer(sortController);
				this.SortSelection.AddItem(new EncyclopediaListSelectorItemVM(comparer));
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004AC88 File Offset: 0x00048E88
		private void UpdateAlternativeSortState(EncyclopediaListItemComparerBase comparer)
		{
			CampaignUIHelper.SortState alternativeSortState = comparer.IsAscending ? CampaignUIHelper.SortState.Ascending : CampaignUIHelper.SortState.Descending;
			this.AlternativeSortState = (int)alternativeSortState;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004ACAC File Offset: 0x00048EAC
		private void OnSortSelectionChanged(SelectorVM<EncyclopediaListSelectorItemVM> s)
		{
			EncyclopediaListItemComparer comparer = s.SelectedItem.Comparer;
			comparer.SortController.Comparer.SetDefaultSortOrder();
			this._items.Sort(comparer);
			this._items.ApplyActionOnAllItems(delegate(EncyclopediaListItemVM x)
			{
				x.SetComparedValue(comparer.SortController.Comparer);
			});
			this._sortedValueLabel = comparer.SortController.Name;
			this.SortedValueLabelText = this._sortedValueLabel.ToString();
			this.IsAlternativeSortVisible = (this.SortSelection.SelectedIndex != 0);
			this.UpdateAlternativeSortState(comparer.SortController.Comparer);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004AD60 File Offset: 0x00048F60
		public void ExecuteSwitchSortOrder()
		{
			EncyclopediaListItemComparer comparer = this.SortSelection.SelectedItem.Comparer;
			comparer.SortController.Comparer.SwitchSortOrder();
			this._items.Sort(comparer);
			this.UpdateAlternativeSortState(comparer.SortController.Comparer);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004ADAC File Offset: 0x00048FAC
		public void SetSortOrder(bool isAscending)
		{
			EncyclopediaListItemComparer comparer = this.SortSelection.SelectedItem.Comparer;
			if (comparer.SortController.Comparer.IsAscending != isAscending)
			{
				comparer.SortController.Comparer.SetSortOrder(isAscending);
				this._items.Sort(comparer);
				this.UpdateAlternativeSortState(comparer.SortController.Comparer);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004AE0B File Offset: 0x0004900B
		public bool GetSortOrder()
		{
			return this.SortSelection.SelectedItem.Comparer.SortController.Comparer.IsAscending;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004AE2C File Offset: 0x0004902C
		private void OnSortSelectionActivated()
		{
			Game.Current.EventManager.TriggerEvent<OnEncyclopediaListSortedEvent>(new OnEncyclopediaListSortedEvent());
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0004AE42 File Offset: 0x00049042
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x0004AE4A File Offset: 0x0004904A
		[DataSourceProperty]
		public EncyclopediaListSelectorVM SortSelection
		{
			get
			{
				return this._sortSelection;
			}
			set
			{
				if (value != this._sortSelection)
				{
					this._sortSelection = value;
					base.OnPropertyChangedWithValue<EncyclopediaListSelectorVM>(value, "SortSelection");
				}
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0004AE68 File Offset: 0x00049068
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0004AE70 File Offset: 0x00049070
		[DataSourceProperty]
		public string NameLabel
		{
			get
			{
				return this._nameLabel;
			}
			set
			{
				if (value != this._nameLabel)
				{
					this._nameLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "NameLabel");
				}
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0004AE93 File Offset: 0x00049093
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0004AE9B File Offset: 0x0004909B
		[DataSourceProperty]
		public string SortedValueLabelText
		{
			get
			{
				return this._sortedValueLabelText;
			}
			set
			{
				if (value != this._sortedValueLabelText)
				{
					this._sortedValueLabelText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortedValueLabelText");
				}
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004AEBE File Offset: 0x000490BE
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0004AEC6 File Offset: 0x000490C6
		[DataSourceProperty]
		public string SortByLabel
		{
			get
			{
				return this._sortByLabel;
			}
			set
			{
				if (value != this._sortByLabel)
				{
					this._sortByLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "SortByLabel");
				}
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0004AEE9 File Offset: 0x000490E9
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0004AEF1 File Offset: 0x000490F1
		[DataSourceProperty]
		public int AlternativeSortState
		{
			get
			{
				return this._alternativeSortState;
			}
			set
			{
				if (value != this._alternativeSortState)
				{
					this._alternativeSortState = value;
					base.OnPropertyChangedWithValue(value, "AlternativeSortState");
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004AF0F File Offset: 0x0004910F
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0004AF17 File Offset: 0x00049117
		[DataSourceProperty]
		public bool IsAlternativeSortVisible
		{
			get
			{
				return this._isAlternativeSortVisible;
			}
			set
			{
				if (value != this._isAlternativeSortVisible)
				{
					this._isAlternativeSortVisible = value;
					base.OnPropertyChangedWithValue(value, "IsAlternativeSortVisible");
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004AF35 File Offset: 0x00049135
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0004AF3D File Offset: 0x0004913D
		[DataSourceProperty]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (value != this._isHighlightEnabled)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightEnabled");
				}
			}
		}

		// Token: 0x040008F4 RID: 2292
		private TextObject _sortedValueLabel = TextObject.Empty;

		// Token: 0x040008F5 RID: 2293
		private MBBindingList<EncyclopediaListItemVM> _items;

		// Token: 0x040008F6 RID: 2294
		private EncyclopediaPage _page;

		// Token: 0x040008F7 RID: 2295
		private EncyclopediaListSelectorVM _sortSelection;

		// Token: 0x040008F8 RID: 2296
		private string _nameLabel;

		// Token: 0x040008F9 RID: 2297
		private string _sortedValueLabelText;

		// Token: 0x040008FA RID: 2298
		private string _sortByLabel;

		// Token: 0x040008FB RID: 2299
		private int _alternativeSortState;

		// Token: 0x040008FC RID: 2300
		private bool _isAlternativeSortVisible;

		// Token: 0x040008FD RID: 2301
		private bool _isHighlightEnabled;
	}
}
