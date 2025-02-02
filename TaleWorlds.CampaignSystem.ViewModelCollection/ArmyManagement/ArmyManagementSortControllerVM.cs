using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200013E RID: 318
	public class ArmyManagementSortControllerVM : ViewModel
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x0006E408 File Offset: 0x0006C608
		public ArmyManagementSortControllerVM(MBBindingList<ArmyManagementItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._distanceComparer = new ArmyManagementSortControllerVM.ItemDistanceComparer();
			this._costComparer = new ArmyManagementSortControllerVM.ItemCostComparer();
			this._strengthComparer = new ArmyManagementSortControllerVM.ItemStrengthComparer();
			this._nameComparer = new ArmyManagementSortControllerVM.ItemNameComparer();
			this._clanComparer = new ArmyManagementSortControllerVM.ItemClanComparer();
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0006E45C File Offset: 0x0006C65C
		public void ExecuteSortByDistance()
		{
			int distanceState = this.DistanceState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.DistanceState = (distanceState + 1) % 3;
			if (this.DistanceState == 0)
			{
				int distanceState2 = this.DistanceState;
				this.DistanceState = distanceState2 + 1;
			}
			this._distanceComparer.SetSortMode(this.DistanceState == 1);
			this._listToControl.Sort(this._distanceComparer);
			this.IsDistanceSelected = true;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0006E4C8 File Offset: 0x0006C6C8
		public void ExecuteSortByCost()
		{
			int costState = this.CostState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.CostState = (costState + 1) % 3;
			if (this.CostState == 0)
			{
				int costState2 = this.CostState;
				this.CostState = costState2 + 1;
			}
			this._costComparer.SetSortMode(this.CostState == 1);
			this._listToControl.Sort(this._costComparer);
			this.IsCostSelected = true;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0006E534 File Offset: 0x0006C734
		public void ExecuteSortByStrength()
		{
			int strengthState = this.StrengthState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.StrengthState = (strengthState + 1) % 3;
			if (this.StrengthState == 0)
			{
				int strengthState2 = this.StrengthState;
				this.StrengthState = strengthState2 + 1;
			}
			this._strengthComparer.SetSortMode(this.StrengthState == 1);
			this._listToControl.Sort(this._strengthComparer);
			this.IsStrengthSelected = true;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0006E5A0 File Offset: 0x0006C7A0
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				int nameState2 = this.NameState;
				this.NameState = nameState2 + 1;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			this._listToControl.Sort(this._nameComparer);
			this.IsNameSelected = true;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0006E60C File Offset: 0x0006C80C
		public void ExecuteSortByClan()
		{
			int clanState = this.ClanState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.ClanState = (clanState + 1) % 3;
			if (this.ClanState == 0)
			{
				int clanState2 = this.ClanState;
				this.ClanState = clanState2 + 1;
			}
			this._clanComparer.SetSortMode(this.ClanState == 1);
			this._listToControl.Sort(this._clanComparer);
			this.IsClanSelected = true;
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0006E678 File Offset: 0x0006C878
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.DistanceState = (int)state;
			this.CostState = (int)state;
			this.StrengthState = (int)state;
			this.NameState = (int)state;
			this.ClanState = (int)state;
			this.IsDistanceSelected = false;
			this.IsCostSelected = false;
			this.IsNameSelected = false;
			this.IsClanSelected = false;
			this.IsStrengthSelected = false;
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0006E6CB File Offset: 0x0006C8CB
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0006E6D3 File Offset: 0x0006C8D3
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

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0006E6F1 File Offset: 0x0006C8F1
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0006E6F9 File Offset: 0x0006C8F9
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

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0006E717 File Offset: 0x0006C917
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0006E71F File Offset: 0x0006C91F
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

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0006E73D File Offset: 0x0006C93D
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0006E745 File Offset: 0x0006C945
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

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0006E763 File Offset: 0x0006C963
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0006E76B File Offset: 0x0006C96B
		[DataSourceProperty]
		public int ClanState
		{
			get
			{
				return this._clanState;
			}
			set
			{
				if (value != this._clanState)
				{
					this._clanState = value;
					base.OnPropertyChangedWithValue(value, "ClanState");
				}
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0006E789 File Offset: 0x0006C989
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0006E791 File Offset: 0x0006C991
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

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0006E7AF File Offset: 0x0006C9AF
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0006E7B7 File Offset: 0x0006C9B7
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

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0006E7D5 File Offset: 0x0006C9D5
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x0006E7DD File Offset: 0x0006C9DD
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

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0006E7FB File Offset: 0x0006C9FB
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0006E803 File Offset: 0x0006CA03
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

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0006E821 File Offset: 0x0006CA21
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x0006E829 File Offset: 0x0006CA29
		[DataSourceProperty]
		public bool IsClanSelected
		{
			get
			{
				return this._isClanSelected;
			}
			set
			{
				if (value != this._isClanSelected)
				{
					this._isClanSelected = value;
					base.OnPropertyChangedWithValue(value, "IsClanSelected");
				}
			}
		}

		// Token: 0x04000E97 RID: 3735
		private readonly MBBindingList<ArmyManagementItemVM> _listToControl;

		// Token: 0x04000E98 RID: 3736
		private readonly ArmyManagementSortControllerVM.ItemDistanceComparer _distanceComparer;

		// Token: 0x04000E99 RID: 3737
		private readonly ArmyManagementSortControllerVM.ItemCostComparer _costComparer;

		// Token: 0x04000E9A RID: 3738
		private readonly ArmyManagementSortControllerVM.ItemStrengthComparer _strengthComparer;

		// Token: 0x04000E9B RID: 3739
		private readonly ArmyManagementSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000E9C RID: 3740
		private readonly ArmyManagementSortControllerVM.ItemClanComparer _clanComparer;

		// Token: 0x04000E9D RID: 3741
		private int _distanceState;

		// Token: 0x04000E9E RID: 3742
		private int _costState;

		// Token: 0x04000E9F RID: 3743
		private int _strengthState;

		// Token: 0x04000EA0 RID: 3744
		private int _nameState;

		// Token: 0x04000EA1 RID: 3745
		private int _clanState;

		// Token: 0x04000EA2 RID: 3746
		private bool _isNameSelected;

		// Token: 0x04000EA3 RID: 3747
		private bool _isCostSelected;

		// Token: 0x04000EA4 RID: 3748
		private bool _isStrengthSelected;

		// Token: 0x04000EA5 RID: 3749
		private bool _isDistanceSelected;

		// Token: 0x04000EA6 RID: 3750
		private bool _isClanSelected;

		// Token: 0x020002A4 RID: 676
		public abstract class ItemComparerBase : IComparer<ArmyManagementItemVM>
		{
			// Token: 0x06002421 RID: 9249 RVA: 0x00077F53 File Offset: 0x00076153
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x06002422 RID: 9250
			public abstract int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y);

			// Token: 0x06002423 RID: 9251 RVA: 0x00077F5C File Offset: 0x0007615C
			protected int ResolveEquality(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				return x.LeaderNameText.CompareTo(y.LeaderNameText);
			}

			// Token: 0x04001255 RID: 4693
			protected bool _isAscending;
		}

		// Token: 0x020002A5 RID: 677
		public class ItemDistanceComparer : ArmyManagementSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002425 RID: 9253 RVA: 0x00077F78 File Offset: 0x00076178
			public override int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				int num = y.DistInTime.CompareTo(x.DistInTime);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020002A6 RID: 678
		public class ItemCostComparer : ArmyManagementSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002427 RID: 9255 RVA: 0x00077FBC File Offset: 0x000761BC
			public override int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				int num = y.Cost.CompareTo(x.Cost);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020002A7 RID: 679
		public class ItemStrengthComparer : ArmyManagementSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002429 RID: 9257 RVA: 0x00078000 File Offset: 0x00076200
			public override int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				int num = y.Strength.CompareTo(x.Strength);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020002A8 RID: 680
		public class ItemNameComparer : ArmyManagementSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600242B RID: 9259 RVA: 0x00078044 File Offset: 0x00076244
			public override int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				if (this._isAscending)
				{
					return y.LeaderNameText.CompareTo(x.LeaderNameText) * -1;
				}
				return y.LeaderNameText.CompareTo(x.LeaderNameText);
			}
		}

		// Token: 0x020002A9 RID: 681
		public class ItemClanComparer : ArmyManagementSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600242D RID: 9261 RVA: 0x0007807C File Offset: 0x0007627C
			public override int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				int num = y.Clan.Name.ToString().CompareTo(x.Clan.Name.ToString());
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
