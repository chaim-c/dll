using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C7 RID: 199
	public class EncyclopediaListVM : EncyclopediaPageVM
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x0004B014 File Offset: 0x00049214
		public EncyclopediaListVM(EncyclopediaPageArgs args) : base(args)
		{
			this.Page = (base.Obj as EncyclopediaPage);
			this.Items = new MBBindingList<EncyclopediaListItemVM>();
			this.FilterGroups = new MBBindingList<EncyclopediaFilterGroupVM>();
			this.SortController = new EncyclopediaListSortControllerVM(this.Page, this.Items);
			this.IsInitializationOver = true;
			foreach (EncyclopediaFilterGroup filterGroup in this.Page.GetFilterItems())
			{
				this.FilterGroups.Add(new EncyclopediaFilterGroupVM(filterGroup, new Action<EncyclopediaListFilterVM>(this.UpdateFilters)));
			}
			this.IsInitializationOver = false;
			this.Items.Clear();
			foreach (EncyclopediaListItem listItem in this.Page.GetListItems())
			{
				EncyclopediaListItemVM encyclopediaListItemVM = new EncyclopediaListItemVM(listItem);
				encyclopediaListItemVM.IsFiltered = this.Page.IsFiltered(encyclopediaListItemVM.Object);
				this.Items.Add(encyclopediaListItemVM);
			}
			this.RefreshValues();
			this.IsInitializationOver = true;
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004B164 File Offset: 0x00049364
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsFilterHighlightEnabled = (evnt.NewNotificationElementID == "EncyclopediaFiltersContainer");
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004B17C File Offset: 0x0004937C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SortController.RefreshValues();
			this.EmptyListText = GameTexts.FindText("str_encyclopedia_empty_list_error", null).ToString();
			this.Items.ApplyActionOnAllItems(delegate(EncyclopediaListItemVM x)
			{
				x.RefreshValues();
			});
			this.FilterGroups.ApplyActionOnAllItems(delegate(EncyclopediaFilterGroupVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004B204 File Offset: 0x00049404
		public override string GetName()
		{
			return this.Page.GetName().ToString();
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004B218 File Offset: 0x00049418
		public override string GetNavigationBarURL()
		{
			string text = HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ ";
			if (this.Page.HasIdentifierType(typeof(Kingdom)))
			{
				text += GameTexts.FindText("str_encyclopedia_kingdoms", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Clan)))
			{
				text += GameTexts.FindText("str_encyclopedia_clans", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Hero)))
			{
				text += GameTexts.FindText("str_encyclopedia_heroes", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Settlement)))
			{
				text += GameTexts.FindText("str_encyclopedia_settlements", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(CharacterObject)))
			{
				text += GameTexts.FindText("str_encyclopedia_troops", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Concept)))
			{
				text += GameTexts.FindText("str_encyclopedia_concepts", null).ToString();
			}
			return text;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004B374 File Offset: 0x00049574
		private void ExecuteResetFilters()
		{
			foreach (EncyclopediaFilterGroupVM encyclopediaFilterGroupVM in this.FilterGroups)
			{
				foreach (EncyclopediaListFilterVM encyclopediaListFilterVM in encyclopediaFilterGroupVM.Filters)
				{
					encyclopediaListFilterVM.IsSelected = false;
				}
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004B3F4 File Offset: 0x000495F4
		public void CopyFiltersFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			this.FilterGroups.ApplyActionOnAllItems(delegate(EncyclopediaFilterGroupVM x)
			{
				x.CopyFiltersFrom(filters);
			});
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004B428 File Offset: 0x00049628
		public override void Refresh()
		{
			base.Refresh();
			foreach (EncyclopediaListItemVM encyclopediaListItemVM in this.Items)
			{
				Hero hero;
				Clan clan;
				Concept concept;
				Kingdom kingdom;
				Settlement settlement;
				CharacterObject unit;
				if ((hero = (encyclopediaListItemVM.Object as Hero)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(hero);
				}
				else if ((clan = (encyclopediaListItemVM.Object as Clan)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(clan);
				}
				else if ((concept = (encyclopediaListItemVM.Object as Concept)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(concept);
				}
				else if ((kingdom = (encyclopediaListItemVM.Object as Kingdom)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(kingdom);
				}
				else if ((settlement = (encyclopediaListItemVM.Object as Settlement)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(settlement);
				}
				else if ((unit = (encyclopediaListItemVM.Object as CharacterObject)) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(unit);
				}
			}
			this._isInitializationOver = false;
			this.IsInitializationOver = true;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004B5AC File Offset: 0x000497AC
		private void UpdateFilters(EncyclopediaListFilterVM filterVM)
		{
			this.IsInitializationOver = false;
			foreach (EncyclopediaListItemVM encyclopediaListItemVM in this.Items)
			{
				encyclopediaListItemVM.IsFiltered = this.Page.IsFiltered(encyclopediaListItemVM.Object);
			}
			this.IsInitializationOver = true;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004B618 File Offset: 0x00049818
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004B620 File Offset: 0x00049820
		[DataSourceProperty]
		public string EmptyListText
		{
			get
			{
				return this._emptyListText;
			}
			set
			{
				if (value != this._emptyListText)
				{
					this._emptyListText = value;
					base.OnPropertyChangedWithValue<string>(value, "EmptyListText");
				}
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004B643 File Offset: 0x00049843
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x0004B64B File Offset: 0x0004984B
		[DataSourceProperty]
		public string LastSelectedItemId
		{
			get
			{
				return this._lastSelectedItemId;
			}
			set
			{
				if (value != this._lastSelectedItemId)
				{
					this._lastSelectedItemId = value;
					base.OnPropertyChangedWithValue<string>(value, "LastSelectedItemId");
				}
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0004B66E File Offset: 0x0004986E
		// (set) Token: 0x06001380 RID: 4992 RVA: 0x0004B676 File Offset: 0x00049876
		[DataSourceProperty]
		public override MBBindingList<EncyclopediaListItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaListItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0004B694 File Offset: 0x00049894
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x0004B69C File Offset: 0x0004989C
		[DataSourceProperty]
		public override EncyclopediaListSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<EncyclopediaListSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0004B6BA File Offset: 0x000498BA
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0004B6C2 File Offset: 0x000498C2
		[DataSourceProperty]
		public bool IsInitializationOver
		{
			get
			{
				return this._isInitializationOver;
			}
			set
			{
				if (value != this._isInitializationOver)
				{
					this._isInitializationOver = value;
					base.OnPropertyChangedWithValue(value, "IsInitializationOver");
				}
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0004B6E0 File Offset: 0x000498E0
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0004B6E8 File Offset: 0x000498E8
		[DataSourceProperty]
		public bool IsFilterHighlightEnabled
		{
			get
			{
				return this._isFilterHighlightEnabled;
			}
			set
			{
				if (value != this._isFilterHighlightEnabled)
				{
					this._isFilterHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsFilterHighlightEnabled");
				}
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0004B706 File Offset: 0x00049906
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x0004B70E File Offset: 0x0004990E
		[DataSourceProperty]
		public override MBBindingList<EncyclopediaFilterGroupVM> FilterGroups
		{
			get
			{
				return this._filterGroups;
			}
			set
			{
				if (value != this._filterGroups)
				{
					this._filterGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFilterGroupVM>>(value, "FilterGroups");
				}
			}
		}

		// Token: 0x04000901 RID: 2305
		public readonly EncyclopediaPage Page;

		// Token: 0x04000902 RID: 2306
		private MBBindingList<EncyclopediaFilterGroupVM> _filterGroups;

		// Token: 0x04000903 RID: 2307
		private MBBindingList<EncyclopediaListItemVM> _items;

		// Token: 0x04000904 RID: 2308
		private EncyclopediaListSortControllerVM _sortController;

		// Token: 0x04000905 RID: 2309
		private bool _isInitializationOver;

		// Token: 0x04000906 RID: 2310
		private bool _isFilterHighlightEnabled;

		// Token: 0x04000907 RID: 2311
		private string _emptyListText;

		// Token: 0x04000908 RID: 2312
		private string _lastSelectedItemId;
	}
}
