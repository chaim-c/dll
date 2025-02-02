using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000082 RID: 130
	public class SPInventorySortControllerVM : ViewModel
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002FE7C File Offset: 0x0002E07C
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0002FE84 File Offset: 0x0002E084
		public SPInventorySortControllerVM.InventoryItemSortOption? CurrentSortOption { get; private set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002FE8D File Offset: 0x0002E08D
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0002FE95 File Offset: 0x0002E095
		public SPInventorySortControllerVM.InventoryItemSortState? CurrentSortState { get; private set; }

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		public SPInventorySortControllerVM(ref MBBindingList<SPItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._typeComparer = new SPInventorySortControllerVM.ItemTypeComparer();
			this._nameComparer = new SPInventorySortControllerVM.ItemNameComparer();
			this._quantityComparer = new SPInventorySortControllerVM.ItemQuantityComparer();
			this._costComparer = new SPInventorySortControllerVM.ItemCostComparer();
			this.RefreshValues();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002FEED File Offset: 0x0002E0ED
		public void SortByOption(SPInventorySortControllerVM.InventoryItemSortOption sortOption, SPInventorySortControllerVM.InventoryItemSortState sortState)
		{
			this.SetAllStates((sortState == SPInventorySortControllerVM.InventoryItemSortState.Ascending) ? SPInventorySortControllerVM.InventoryItemSortState.Descending : SPInventorySortControllerVM.InventoryItemSortState.Ascending);
			if (sortOption == SPInventorySortControllerVM.InventoryItemSortOption.Type)
			{
				this.ExecuteSortByType();
				return;
			}
			if (sortOption == SPInventorySortControllerVM.InventoryItemSortOption.Name)
			{
				this.ExecuteSortByName();
				return;
			}
			if (sortOption == SPInventorySortControllerVM.InventoryItemSortOption.Quantity)
			{
				this.ExecuteSortByQuantity();
				return;
			}
			if (sortOption == SPInventorySortControllerVM.InventoryItemSortOption.Cost)
			{
				this.ExecuteSortByCost();
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002FF27 File Offset: 0x0002E127
		public void SortByDefaultState()
		{
			this.ExecuteSortByType();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002FF30 File Offset: 0x0002E130
		public void SortByCurrentState()
		{
			if (this.IsTypeSelected)
			{
				this._listToControl.Sort(this._typeComparer);
				this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Type);
				return;
			}
			if (this.IsNameSelected)
			{
				this._listToControl.Sort(this._nameComparer);
				this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Name);
				return;
			}
			if (this.IsQuantitySelected)
			{
				this._listToControl.Sort(this._quantityComparer);
				this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Quantity);
				return;
			}
			if (this.IsCostSelected)
			{
				this._listToControl.Sort(this._costComparer);
				this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Cost);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(SPInventorySortControllerVM.InventoryItemSortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				this.NameState++;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			this.CurrentSortState = new SPInventorySortControllerVM.InventoryItemSortState?((this.NameState == 1) ? SPInventorySortControllerVM.InventoryItemSortState.Ascending : SPInventorySortControllerVM.InventoryItemSortState.Descending);
			this._listToControl.Sort(this._nameComparer);
			this.IsNameSelected = true;
			this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Name);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00030060 File Offset: 0x0002E260
		public void ExecuteSortByType()
		{
			int typeState = this.TypeState;
			this.SetAllStates(SPInventorySortControllerVM.InventoryItemSortState.Default);
			this.TypeState = (typeState + 1) % 3;
			if (this.TypeState == 0)
			{
				this.TypeState++;
			}
			this._typeComparer.SetSortMode(this.TypeState == 1);
			this.CurrentSortState = new SPInventorySortControllerVM.InventoryItemSortState?((this.TypeState == 1) ? SPInventorySortControllerVM.InventoryItemSortState.Ascending : SPInventorySortControllerVM.InventoryItemSortState.Descending);
			this._listToControl.Sort(this._typeComparer);
			this.IsTypeSelected = true;
			this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Type);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000300EC File Offset: 0x0002E2EC
		public void ExecuteSortByQuantity()
		{
			int quantityState = this.QuantityState;
			this.SetAllStates(SPInventorySortControllerVM.InventoryItemSortState.Default);
			this.QuantityState = (quantityState + 1) % 3;
			if (this.QuantityState == 0)
			{
				this.QuantityState++;
			}
			this._quantityComparer.SetSortMode(this.QuantityState == 1);
			this.CurrentSortState = new SPInventorySortControllerVM.InventoryItemSortState?((this.QuantityState == 1) ? SPInventorySortControllerVM.InventoryItemSortState.Ascending : SPInventorySortControllerVM.InventoryItemSortState.Descending);
			this._listToControl.Sort(this._quantityComparer);
			this.IsQuantitySelected = true;
			this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Quantity);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00030178 File Offset: 0x0002E378
		public void ExecuteSortByCost()
		{
			int costState = this.CostState;
			this.SetAllStates(SPInventorySortControllerVM.InventoryItemSortState.Default);
			this.CostState = (costState + 1) % 3;
			if (this.CostState == 0)
			{
				this.CostState++;
			}
			this._costComparer.SetSortMode(this.CostState == 1);
			this.CurrentSortState = new SPInventorySortControllerVM.InventoryItemSortState?((this.CostState == 1) ? SPInventorySortControllerVM.InventoryItemSortState.Ascending : SPInventorySortControllerVM.InventoryItemSortState.Descending);
			this._listToControl.Sort(this._costComparer);
			this.IsCostSelected = true;
			this.CurrentSortOption = new SPInventorySortControllerVM.InventoryItemSortOption?(SPInventorySortControllerVM.InventoryItemSortOption.Cost);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00030204 File Offset: 0x0002E404
		private void SetAllStates(SPInventorySortControllerVM.InventoryItemSortState state)
		{
			this.TypeState = (int)state;
			this.NameState = (int)state;
			this.QuantityState = (int)state;
			this.CostState = (int)state;
			this.IsTypeSelected = false;
			this.IsNameSelected = false;
			this.IsQuantitySelected = false;
			this.IsCostSelected = false;
			this.CurrentSortState = new SPInventorySortControllerVM.InventoryItemSortState?(state);
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00030255 File Offset: 0x0002E455
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0003025D File Offset: 0x0002E45D
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003027B File Offset: 0x0002E47B
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00030283 File Offset: 0x0002E483
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000302A1 File Offset: 0x0002E4A1
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x000302A9 File Offset: 0x0002E4A9
		[DataSourceProperty]
		public int QuantityState
		{
			get
			{
				return this._quantityState;
			}
			set
			{
				if (value != this._quantityState)
				{
					this._quantityState = value;
					base.OnPropertyChangedWithValue(value, "QuantityState");
				}
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000302C7 File Offset: 0x0002E4C7
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000302CF File Offset: 0x0002E4CF
		[DataSourceProperty]
		public int CostState
		{
			get
			{
				return this._costState;
			}
			set
			{
				if (value != this._costState)
				{
					this._costState = value;
					base.OnPropertyChangedWithValue(value, "CostState");
				}
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000302ED File Offset: 0x0002E4ED
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x000302F5 File Offset: 0x0002E4F5
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00030313 File Offset: 0x0002E513
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0003031B File Offset: 0x0002E51B
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00030339 File Offset: 0x0002E539
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00030341 File Offset: 0x0002E541
		[DataSourceProperty]
		public bool IsQuantitySelected
		{
			get
			{
				return this._isQuantitySelected;
			}
			set
			{
				if (value != this._isQuantitySelected)
				{
					this._isQuantitySelected = value;
					base.OnPropertyChangedWithValue(value, "IsQuantitySelected");
				}
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003035F File Offset: 0x0002E55F
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00030367 File Offset: 0x0002E567
		[DataSourceProperty]
		public bool IsCostSelected
		{
			get
			{
				return this._isCostSelected;
			}
			set
			{
				if (value != this._isCostSelected)
				{
					this._isCostSelected = value;
					base.OnPropertyChangedWithValue(value, "IsCostSelected");
				}
			}
		}

		// Token: 0x0400056B RID: 1387
		private MBBindingList<SPItemVM> _listToControl;

		// Token: 0x0400056C RID: 1388
		private SPInventorySortControllerVM.ItemTypeComparer _typeComparer;

		// Token: 0x0400056D RID: 1389
		private SPInventorySortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x0400056E RID: 1390
		private SPInventorySortControllerVM.ItemQuantityComparer _quantityComparer;

		// Token: 0x0400056F RID: 1391
		private SPInventorySortControllerVM.ItemCostComparer _costComparer;

		// Token: 0x04000572 RID: 1394
		private int _typeState;

		// Token: 0x04000573 RID: 1395
		private int _nameState;

		// Token: 0x04000574 RID: 1396
		private int _quantityState;

		// Token: 0x04000575 RID: 1397
		private int _costState;

		// Token: 0x04000576 RID: 1398
		private bool _isTypeSelected;

		// Token: 0x04000577 RID: 1399
		private bool _isNameSelected;

		// Token: 0x04000578 RID: 1400
		private bool _isQuantitySelected;

		// Token: 0x04000579 RID: 1401
		private bool _isCostSelected;

		// Token: 0x020001C4 RID: 452
		public enum InventoryItemSortState
		{
			// Token: 0x0400101D RID: 4125
			Default,
			// Token: 0x0400101E RID: 4126
			Ascending,
			// Token: 0x0400101F RID: 4127
			Descending
		}

		// Token: 0x020001C5 RID: 453
		public enum InventoryItemSortOption
		{
			// Token: 0x04001021 RID: 4129
			Type,
			// Token: 0x04001022 RID: 4130
			Name,
			// Token: 0x04001023 RID: 4131
			Quantity,
			// Token: 0x04001024 RID: 4132
			Cost
		}

		// Token: 0x020001C6 RID: 454
		public abstract class ItemComparer : IComparer<SPItemVM>
		{
			// Token: 0x06002146 RID: 8518 RVA: 0x00073BEF File Offset: 0x00071DEF
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x06002147 RID: 8519
			public abstract int Compare(SPItemVM x, SPItemVM y);

			// Token: 0x06002148 RID: 8520 RVA: 0x00073BF8 File Offset: 0x00071DF8
			protected int ResolveEquality(SPItemVM x, SPItemVM y)
			{
				return x.ItemDescription.CompareTo(y.ItemDescription);
			}

			// Token: 0x04001025 RID: 4133
			protected bool _isAscending;
		}

		// Token: 0x020001C7 RID: 455
		public class ItemTypeComparer : SPInventorySortControllerVM.ItemComparer
		{
			// Token: 0x0600214A RID: 8522 RVA: 0x00073C14 File Offset: 0x00071E14
			public override int Compare(SPItemVM x, SPItemVM y)
			{
				int itemObjectTypeSortIndex = CampaignUIHelper.GetItemObjectTypeSortIndex(x.ItemRosterElement.EquipmentElement.Item);
				int num = CampaignUIHelper.GetItemObjectTypeSortIndex(y.ItemRosterElement.EquipmentElement.Item).CompareTo(itemObjectTypeSortIndex);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				num = x.ItemCost.CompareTo(y.ItemCost);
				if (num != 0)
				{
					return num;
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001C8 RID: 456
		public class ItemNameComparer : SPInventorySortControllerVM.ItemComparer
		{
			// Token: 0x0600214C RID: 8524 RVA: 0x00073C99 File Offset: 0x00071E99
			public override int Compare(SPItemVM x, SPItemVM y)
			{
				if (this._isAscending)
				{
					return y.ItemDescription.CompareTo(x.ItemDescription) * -1;
				}
				return y.ItemDescription.CompareTo(x.ItemDescription);
			}
		}

		// Token: 0x020001C9 RID: 457
		public class ItemQuantityComparer : SPInventorySortControllerVM.ItemComparer
		{
			// Token: 0x0600214E RID: 8526 RVA: 0x00073CD0 File Offset: 0x00071ED0
			public override int Compare(SPItemVM x, SPItemVM y)
			{
				int num = y.ItemCount.CompareTo(x.ItemCount);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001CA RID: 458
		public class ItemCostComparer : SPInventorySortControllerVM.ItemComparer
		{
			// Token: 0x06002150 RID: 8528 RVA: 0x00073D14 File Offset: 0x00071F14
			public override int Compare(SPItemVM x, SPItemVM y)
			{
				int num = y.ItemCost.CompareTo(x.ItemCost);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
