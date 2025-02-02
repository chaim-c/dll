using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies
{
	// Token: 0x0200007A RID: 122
	public class KingdomArmySortControllerVM : ViewModel
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0002B0B4 File Offset: 0x000292B4
		public KingdomArmySortControllerVM(ref MBBindingList<KingdomArmyItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._ownerComparer = new KingdomArmySortControllerVM.ItemOwnerComparer();
			this._strengthComparer = new KingdomArmySortControllerVM.ItemStrengthComparer();
			this._nameComparer = new KingdomArmySortControllerVM.ItemNameComparer();
			this._partiesComparer = new KingdomArmySortControllerVM.ItemPartiesComparer();
			this._distanceComparer = new KingdomArmySortControllerVM.ItemDistanceComparer();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002B108 File Offset: 0x00029308
		private void ExecuteSortByName()
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

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002B170 File Offset: 0x00029370
		private void ExecuteSortByOwner()
		{
			int ownerState = this.OwnerState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.OwnerState = (ownerState + 1) % 3;
			if (this.OwnerState == 0)
			{
				this.OwnerState++;
			}
			this._ownerComparer.SetSortMode(this.OwnerState == 1);
			this._listToControl.Sort(this._ownerComparer);
			this.IsOwnerSelected = true;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002B1D8 File Offset: 0x000293D8
		private void ExecuteSortByStrength()
		{
			int strengthState = this.StrengthState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.StrengthState = (strengthState + 1) % 3;
			if (this.StrengthState == 0)
			{
				this.StrengthState++;
			}
			this._strengthComparer.SetSortMode(this.StrengthState == 1);
			this._listToControl.Sort(this._strengthComparer);
			this.IsStrengthSelected = true;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002B240 File Offset: 0x00029440
		private void ExecuteSortByParties()
		{
			int partiesState = this.PartiesState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.PartiesState = (partiesState + 1) % 3;
			if (this.PartiesState == 0)
			{
				this.PartiesState++;
			}
			this._partiesComparer.SetSortMode(this.PartiesState == 1);
			this._listToControl.Sort(this._partiesComparer);
			this.IsPartiesSelected = true;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002B2A8 File Offset: 0x000294A8
		private void ExecuteSortByDistance()
		{
			int distanceState = this.DistanceState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.DistanceState = (distanceState + 1) % 3;
			if (this.DistanceState == 0)
			{
				this.DistanceState++;
			}
			this._distanceComparer.SetSortMode(this.DistanceState == 1);
			this._listToControl.Sort(this._distanceComparer);
			this.IsDistanceSelected = true;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002B310 File Offset: 0x00029510
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.OwnerState = (int)state;
			this.StrengthState = (int)state;
			this.PartiesState = (int)state;
			this.DistanceState = (int)state;
			this.IsNameSelected = false;
			this.IsOwnerSelected = false;
			this.IsStrengthSelected = false;
			this.IsPartiesSelected = false;
			this.IsDistanceSelected = false;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002B363 File Offset: 0x00029563
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002B36B File Offset: 0x0002956B
		[DataSourceProperty]
		public int OwnerState
		{
			get
			{
				return this._ownerState;
			}
			set
			{
				if (value != this._ownerState)
				{
					this._ownerState = value;
					base.OnPropertyChangedWithValue(value, "OwnerState");
				}
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002B389 File Offset: 0x00029589
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0002B391 File Offset: 0x00029591
		[DataSourceProperty]
		public int PartiesState
		{
			get
			{
				return this._partiesState;
			}
			set
			{
				if (value != this._partiesState)
				{
					this._partiesState = value;
					base.OnPropertyChangedWithValue(value, "PartiesState");
				}
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002B3AF File Offset: 0x000295AF
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0002B3B7 File Offset: 0x000295B7
		[DataSourceProperty]
		public int StrengthState
		{
			get
			{
				return this._strengthState;
			}
			set
			{
				if (value != this._strengthState)
				{
					this._strengthState = value;
					base.OnPropertyChangedWithValue(value, "StrengthState");
				}
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0002B3D5 File Offset: 0x000295D5
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0002B3DD File Offset: 0x000295DD
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002B3FB File Offset: 0x000295FB
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0002B403 File Offset: 0x00029603
		[DataSourceProperty]
		public int DistanceState
		{
			get
			{
				return this._distanceState;
			}
			set
			{
				if (value != this._distanceState)
				{
					this._distanceState = value;
					base.OnPropertyChangedWithValue(value, "DistanceState");
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0002B421 File Offset: 0x00029621
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0002B429 File Offset: 0x00029629
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002B447 File Offset: 0x00029647
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0002B44F File Offset: 0x0002964F
		[DataSourceProperty]
		public bool IsPartiesSelected
		{
			get
			{
				return this._isPartiesSelected;
			}
			set
			{
				if (value != this._isPartiesSelected)
				{
					this._isPartiesSelected = value;
					base.OnPropertyChangedWithValue(value, "IsPartiesSelected");
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002B46D File Offset: 0x0002966D
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0002B475 File Offset: 0x00029675
		[DataSourceProperty]
		public bool IsStrengthSelected
		{
			get
			{
				return this._isStrengthSelected;
			}
			set
			{
				if (value != this._isStrengthSelected)
				{
					this._isStrengthSelected = value;
					base.OnPropertyChangedWithValue(value, "IsStrengthSelected");
				}
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002B493 File Offset: 0x00029693
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002B49B File Offset: 0x0002969B
		[DataSourceProperty]
		public bool IsOwnerSelected
		{
			get
			{
				return this._isOwnerSelected;
			}
			set
			{
				if (value != this._isOwnerSelected)
				{
					this._isOwnerSelected = value;
					base.OnPropertyChangedWithValue(value, "IsOwnerSelected");
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002B4B9 File Offset: 0x000296B9
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0002B4C1 File Offset: 0x000296C1
		[DataSourceProperty]
		public bool IsDistanceSelected
		{
			get
			{
				return this._isDistanceSelected;
			}
			set
			{
				if (value != this._isDistanceSelected)
				{
					this._isDistanceSelected = value;
					base.OnPropertyChangedWithValue(value, "IsDistanceSelected");
				}
			}
		}

		// Token: 0x040004DC RID: 1244
		private readonly MBBindingList<KingdomArmyItemVM> _listToControl;

		// Token: 0x040004DD RID: 1245
		private readonly KingdomArmySortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x040004DE RID: 1246
		private readonly KingdomArmySortControllerVM.ItemOwnerComparer _ownerComparer;

		// Token: 0x040004DF RID: 1247
		private readonly KingdomArmySortControllerVM.ItemStrengthComparer _strengthComparer;

		// Token: 0x040004E0 RID: 1248
		private readonly KingdomArmySortControllerVM.ItemPartiesComparer _partiesComparer;

		// Token: 0x040004E1 RID: 1249
		private readonly KingdomArmySortControllerVM.ItemDistanceComparer _distanceComparer;

		// Token: 0x040004E2 RID: 1250
		private int _nameState;

		// Token: 0x040004E3 RID: 1251
		private int _ownerState;

		// Token: 0x040004E4 RID: 1252
		private int _strengthState;

		// Token: 0x040004E5 RID: 1253
		private int _partiesState;

		// Token: 0x040004E6 RID: 1254
		private int _distanceState;

		// Token: 0x040004E7 RID: 1255
		private bool _isNameSelected;

		// Token: 0x040004E8 RID: 1256
		private bool _isOwnerSelected;

		// Token: 0x040004E9 RID: 1257
		private bool _isStrengthSelected;

		// Token: 0x040004EA RID: 1258
		private bool _isPartiesSelected;

		// Token: 0x040004EB RID: 1259
		private bool _isDistanceSelected;

		// Token: 0x020001BB RID: 443
		public abstract class ItemComparerBase : IComparer<KingdomArmyItemVM>
		{
			// Token: 0x0600212D RID: 8493 RVA: 0x000739EF File Offset: 0x00071BEF
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x0600212E RID: 8494
			public abstract int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y);

			// Token: 0x0600212F RID: 8495 RVA: 0x000739F8 File Offset: 0x00071BF8
			protected int ResolveEquality(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				return x.ArmyName.CompareTo(y.ArmyName);
			}

			// Token: 0x04001013 RID: 4115
			protected bool _isAscending;
		}

		// Token: 0x020001BC RID: 444
		public class ItemNameComparer : KingdomArmySortControllerVM.ItemComparerBase
		{
			// Token: 0x06002131 RID: 8497 RVA: 0x00073A13 File Offset: 0x00071C13
			public override int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				if (this._isAscending)
				{
					return y.ArmyName.CompareTo(x.ArmyName) * -1;
				}
				return y.ArmyName.CompareTo(x.ArmyName);
			}
		}

		// Token: 0x020001BD RID: 445
		public class ItemOwnerComparer : KingdomArmySortControllerVM.ItemComparerBase
		{
			// Token: 0x06002133 RID: 8499 RVA: 0x00073A4C File Offset: 0x00071C4C
			public override int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				int num = y.Leader.NameText.ToString().CompareTo(x.Leader.NameText.ToString());
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001BE RID: 446
		public class ItemStrengthComparer : KingdomArmySortControllerVM.ItemComparerBase
		{
			// Token: 0x06002135 RID: 8501 RVA: 0x00073AA4 File Offset: 0x00071CA4
			public override int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				int num = y.Strength.CompareTo(x.Strength);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001BF RID: 447
		public class ItemPartiesComparer : KingdomArmySortControllerVM.ItemComparerBase
		{
			// Token: 0x06002137 RID: 8503 RVA: 0x00073AE8 File Offset: 0x00071CE8
			public override int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				int num = y.Parties.Count.CompareTo(x.Parties.Count);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001C0 RID: 448
		public class ItemDistanceComparer : KingdomArmySortControllerVM.ItemComparerBase
		{
			// Token: 0x06002139 RID: 8505 RVA: 0x00073B38 File Offset: 0x00071D38
			public override int Compare(KingdomArmyItemVM x, KingdomArmyItemVM y)
			{
				int num = y.DistanceToMainParty.CompareTo(x.DistanceToMainParty);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
