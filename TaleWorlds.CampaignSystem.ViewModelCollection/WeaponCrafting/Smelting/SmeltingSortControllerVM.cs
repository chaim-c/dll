using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting
{
	// Token: 0x020000F4 RID: 244
	public class SmeltingSortControllerVM : ViewModel
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00055D3F File Offset: 0x00053F3F
		public SmeltingSortControllerVM()
		{
			this._yieldComparer = new SmeltingSortControllerVM.ItemYieldComparer();
			this._typeComparer = new SmeltingSortControllerVM.ItemTypeComparer();
			this._nameComparer = new SmeltingSortControllerVM.ItemNameComparer();
			this.RefreshValues();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00055D70 File Offset: 0x00053F70
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SortNameText = new TextObject("{=PDdh1sBj}Name", null).ToString();
			this.SortTypeText = new TextObject("{=zMMqgxb1}Type", null).ToString();
			this.SortYieldText = new TextObject("{=v3OF6vBg}Yield", null).ToString();
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00055DC5 File Offset: 0x00053FC5
		public void SetListToControl(MBBindingList<SmeltingItemVM> listToControl)
		{
			this._listToControl = listToControl;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00055DD0 File Offset: 0x00053FD0
		public void SortByCurrentState()
		{
			if (this.IsNameSelected)
			{
				this._listToControl.Sort(this._nameComparer);
				return;
			}
			if (this.IsYieldSelected)
			{
				this._listToControl.Sort(this._yieldComparer);
				return;
			}
			if (this.IsTypeSelected)
			{
				this._listToControl.Sort(this._typeComparer);
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00055E2C File Offset: 0x0005402C
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				this.NameState++;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			this._listToControl.Sort(this._nameComparer);
			this.IsNameSelected = true;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00055E94 File Offset: 0x00054094
		public void ExecuteSortByYield()
		{
			int yieldState = this.YieldState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.YieldState = (yieldState + 1) % 3;
			if (this.YieldState == 0)
			{
				this.YieldState++;
			}
			this._yieldComparer.SetSortMode(this.YieldState == 1);
			this._listToControl.Sort(this._yieldComparer);
			this.IsYieldSelected = true;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00055EFC File Offset: 0x000540FC
		public void ExecuteSortByType()
		{
			int typeState = this.TypeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.TypeState = (typeState + 1) % 3;
			if (this.TypeState == 0)
			{
				this.TypeState++;
			}
			this._typeComparer.SetSortMode(this.TypeState == 1);
			this._listToControl.Sort(this._typeComparer);
			this.IsTypeSelected = true;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00055F64 File Offset: 0x00054164
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.TypeState = (int)state;
			this.YieldState = (int)state;
			this.IsNameSelected = false;
			this.IsTypeSelected = false;
			this.IsYieldSelected = false;
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00055F90 File Offset: 0x00054190
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x00055F98 File Offset: 0x00054198
		[DataSourceProperty]
		public int NameState
		{
			get
			{
				return this._nameState;
			}
			set
			{
				if (value != this._nameState)
				{
					this._nameState = value;
					base.OnPropertyChangedWithValue(value, "NameState");
				}
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00055FB6 File Offset: 0x000541B6
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x00055FBE File Offset: 0x000541BE
		[DataSourceProperty]
		public int TypeState
		{
			get
			{
				return this._typeState;
			}
			set
			{
				if (value != this._typeState)
				{
					this._typeState = value;
					base.OnPropertyChangedWithValue(value, "TypeState");
				}
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00055FDC File Offset: 0x000541DC
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x00055FE4 File Offset: 0x000541E4
		[DataSourceProperty]
		public int YieldState
		{
			get
			{
				return this._yieldState;
			}
			set
			{
				if (value != this._yieldState)
				{
					this._yieldState = value;
					base.OnPropertyChangedWithValue(value, "YieldState");
				}
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00056002 File Offset: 0x00054202
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0005600A File Offset: 0x0005420A
		[DataSourceProperty]
		public bool IsNameSelected
		{
			get
			{
				return this._isNameSelected;
			}
			set
			{
				if (value != this._isNameSelected)
				{
					this._isNameSelected = value;
					base.OnPropertyChangedWithValue(value, "IsNameSelected");
				}
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00056028 File Offset: 0x00054228
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x00056030 File Offset: 0x00054230
		[DataSourceProperty]
		public bool IsTypeSelected
		{
			get
			{
				return this._isTypeSelected;
			}
			set
			{
				if (value != this._isTypeSelected)
				{
					this._isTypeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsTypeSelected");
				}
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005604E File Offset: 0x0005424E
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00056056 File Offset: 0x00054256
		[DataSourceProperty]
		public bool IsYieldSelected
		{
			get
			{
				return this._isYieldSelected;
			}
			set
			{
				if (value != this._isYieldSelected)
				{
					this._isYieldSelected = value;
					base.OnPropertyChangedWithValue(value, "IsYieldSelected");
				}
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00056074 File Offset: 0x00054274
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x0005607C File Offset: 0x0005427C
		[DataSourceProperty]
		public string SortTypeText
		{
			get
			{
				return this._sortTypeText;
			}
			set
			{
				if (value != this._sortTypeText)
				{
					this._sortTypeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortTypeText");
				}
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0005609F File Offset: 0x0005429F
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x000560A7 File Offset: 0x000542A7
		[DataSourceProperty]
		public string SortNameText
		{
			get
			{
				return this._sortNameText;
			}
			set
			{
				if (value != this._sortNameText)
				{
					this._sortNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortNameText");
				}
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000560CA File Offset: 0x000542CA
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x000560D2 File Offset: 0x000542D2
		[DataSourceProperty]
		public string SortYieldText
		{
			get
			{
				return this._sortYieldText;
			}
			set
			{
				if (value != this._sortYieldText)
				{
					this._sortYieldText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortYieldText");
				}
			}
		}

		// Token: 0x04000AC2 RID: 2754
		private MBBindingList<SmeltingItemVM> _listToControl;

		// Token: 0x04000AC3 RID: 2755
		private readonly SmeltingSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000AC4 RID: 2756
		private readonly SmeltingSortControllerVM.ItemYieldComparer _yieldComparer;

		// Token: 0x04000AC5 RID: 2757
		private readonly SmeltingSortControllerVM.ItemTypeComparer _typeComparer;

		// Token: 0x04000AC6 RID: 2758
		private int _nameState;

		// Token: 0x04000AC7 RID: 2759
		private int _yieldState;

		// Token: 0x04000AC8 RID: 2760
		private int _typeState;

		// Token: 0x04000AC9 RID: 2761
		private bool _isNameSelected;

		// Token: 0x04000ACA RID: 2762
		private bool _isYieldSelected;

		// Token: 0x04000ACB RID: 2763
		private bool _isTypeSelected;

		// Token: 0x04000ACC RID: 2764
		private string _sortTypeText;

		// Token: 0x04000ACD RID: 2765
		private string _sortNameText;

		// Token: 0x04000ACE RID: 2766
		private string _sortYieldText;

		// Token: 0x02000232 RID: 562
		public abstract class ItemComparerBase : IComparer<SmeltingItemVM>
		{
			// Token: 0x06002286 RID: 8838 RVA: 0x000751C5 File Offset: 0x000733C5
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x06002287 RID: 8839
			public abstract int Compare(SmeltingItemVM x, SmeltingItemVM y);

			// Token: 0x06002288 RID: 8840 RVA: 0x000751CE File Offset: 0x000733CE
			protected int ResolveEquality(SmeltingItemVM x, SmeltingItemVM y)
			{
				return x.Name.CompareTo(y.Name);
			}

			// Token: 0x0400112C RID: 4396
			protected bool _isAscending;
		}

		// Token: 0x02000233 RID: 563
		public class ItemNameComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600228A RID: 8842 RVA: 0x000751E9 File Offset: 0x000733E9
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				if (this._isAscending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x02000234 RID: 564
		public class ItemYieldComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600228C RID: 8844 RVA: 0x00075220 File Offset: 0x00073420
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				int num = y.Yield.Count.CompareTo(x.Yield.Count);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x02000235 RID: 565
		public class ItemTypeComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600228E RID: 8846 RVA: 0x00075270 File Offset: 0x00073470
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				int itemObjectTypeSortIndex = CampaignUIHelper.GetItemObjectTypeSortIndex(x.EquipmentElement.Item);
				int num = CampaignUIHelper.GetItemObjectTypeSortIndex(y.EquipmentElement.Item).CompareTo(itemObjectTypeSortIndex);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
