using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans
{
	// Token: 0x02000076 RID: 118
	public class KingdomClanSortControllerVM : ViewModel
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00029BEC File Offset: 0x00027DEC
		public KingdomClanSortControllerVM(ref MBBindingList<KingdomClanItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._influenceComparer = new KingdomClanSortControllerVM.ItemInfluenceComparer();
			this._membersComparer = new KingdomClanSortControllerVM.ItemMembersComparer();
			this._nameComparer = new KingdomClanSortControllerVM.ItemNameComparer();
			this._fiefsComparer = new KingdomClanSortControllerVM.ItemFiefsComparer();
			this._typeComparer = new KingdomClanSortControllerVM.ItemTypeComparer();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00029C40 File Offset: 0x00027E40
		public void SortByCurrentState()
		{
			if (this.IsNameSelected)
			{
				this._listToControl.Sort(this._nameComparer);
				return;
			}
			if (this.IsTypeSelected)
			{
				this._listToControl.Sort(this._typeComparer);
				return;
			}
			if (this.IsInfluenceSelected)
			{
				this._listToControl.Sort(this._influenceComparer);
				return;
			}
			if (this.IsMembersSelected)
			{
				this._listToControl.Sort(this._membersComparer);
				return;
			}
			if (this.IsFiefsSelected)
			{
				this._listToControl.Sort(this._fiefsComparer);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00029CD0 File Offset: 0x00027ED0
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

		// Token: 0x06000A56 RID: 2646 RVA: 0x00029D38 File Offset: 0x00027F38
		private void ExecuteSortByType()
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

		// Token: 0x06000A57 RID: 2647 RVA: 0x00029DA0 File Offset: 0x00027FA0
		private void ExecuteSortByInfluence()
		{
			int influenceState = this.InfluenceState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.InfluenceState = (influenceState + 1) % 3;
			if (this.InfluenceState == 0)
			{
				this.InfluenceState++;
			}
			this._influenceComparer.SetSortMode(this.InfluenceState == 1);
			this._listToControl.Sort(this._influenceComparer);
			this.IsInfluenceSelected = true;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00029E08 File Offset: 0x00028008
		private void ExecuteSortByMembers()
		{
			int membersState = this.MembersState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.MembersState = (membersState + 1) % 3;
			if (this.MembersState == 0)
			{
				this.MembersState++;
			}
			this._membersComparer.SetSortMode(this.MembersState == 1);
			this._listToControl.Sort(this._membersComparer);
			this.IsMembersSelected = true;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00029E70 File Offset: 0x00028070
		private void ExecuteSortByFiefs()
		{
			int fiefsState = this.FiefsState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.FiefsState = (fiefsState + 1) % 3;
			if (this.FiefsState == 0)
			{
				this.FiefsState++;
			}
			this._fiefsComparer.SetSortMode(this.FiefsState == 1);
			this._listToControl.Sort(this._fiefsComparer);
			this.IsFiefsSelected = true;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00029ED8 File Offset: 0x000280D8
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.InfluenceState = (int)state;
			this.FiefsState = (int)state;
			this.MembersState = (int)state;
			this.NameState = (int)state;
			this.TypeState = (int)state;
			this.IsInfluenceSelected = false;
			this.IsFiefsSelected = false;
			this.IsNameSelected = false;
			this.IsMembersSelected = false;
			this.IsTypeSelected = false;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00029F2B File Offset: 0x0002812B
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00029F33 File Offset: 0x00028133
		[DataSourceProperty]
		public int InfluenceState
		{
			get
			{
				return this._influenceState;
			}
			set
			{
				if (value != this._influenceState)
				{
					this._influenceState = value;
					base.OnPropertyChangedWithValue(value, "InfluenceState");
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00029F51 File Offset: 0x00028151
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00029F59 File Offset: 0x00028159
		[DataSourceProperty]
		public int FiefsState
		{
			get
			{
				return this._fiefsState;
			}
			set
			{
				if (value != this._fiefsState)
				{
					this._fiefsState = value;
					base.OnPropertyChangedWithValue(value, "FiefsState");
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00029F77 File Offset: 0x00028177
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00029F7F File Offset: 0x0002817F
		[DataSourceProperty]
		public int MembersState
		{
			get
			{
				return this._membersState;
			}
			set
			{
				if (value != this._membersState)
				{
					this._membersState = value;
					base.OnPropertyChangedWithValue(value, "MembersState");
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00029F9D File Offset: 0x0002819D
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00029FA5 File Offset: 0x000281A5
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00029FC3 File Offset: 0x000281C3
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00029FCB File Offset: 0x000281CB
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00029FE9 File Offset: 0x000281E9
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00029FF1 File Offset: 0x000281F1
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002A00F File Offset: 0x0002820F
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002A017 File Offset: 0x00028217
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002A035 File Offset: 0x00028235
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0002A03D File Offset: 0x0002823D
		[DataSourceProperty]
		public bool IsFiefsSelected
		{
			get
			{
				return this._isFiefsSelected;
			}
			set
			{
				if (value != this._isFiefsSelected)
				{
					this._isFiefsSelected = value;
					base.OnPropertyChangedWithValue(value, "IsFiefsSelected");
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002A05B File Offset: 0x0002825B
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0002A063 File Offset: 0x00028263
		[DataSourceProperty]
		public bool IsMembersSelected
		{
			get
			{
				return this._isMembersSelected;
			}
			set
			{
				if (value != this._isMembersSelected)
				{
					this._isMembersSelected = value;
					base.OnPropertyChangedWithValue(value, "IsMembersSelected");
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0002A081 File Offset: 0x00028281
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0002A089 File Offset: 0x00028289
		[DataSourceProperty]
		public bool IsInfluenceSelected
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
					base.OnPropertyChangedWithValue(value, "IsInfluenceSelected");
				}
			}
		}

		// Token: 0x040004A6 RID: 1190
		private readonly MBBindingList<KingdomClanItemVM> _listToControl;

		// Token: 0x040004A7 RID: 1191
		private readonly KingdomClanSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x040004A8 RID: 1192
		private readonly KingdomClanSortControllerVM.ItemTypeComparer _typeComparer;

		// Token: 0x040004A9 RID: 1193
		private readonly KingdomClanSortControllerVM.ItemInfluenceComparer _influenceComparer;

		// Token: 0x040004AA RID: 1194
		private readonly KingdomClanSortControllerVM.ItemMembersComparer _membersComparer;

		// Token: 0x040004AB RID: 1195
		private readonly KingdomClanSortControllerVM.ItemFiefsComparer _fiefsComparer;

		// Token: 0x040004AC RID: 1196
		private int _influenceState;

		// Token: 0x040004AD RID: 1197
		private int _fiefsState;

		// Token: 0x040004AE RID: 1198
		private int _membersState;

		// Token: 0x040004AF RID: 1199
		private int _nameState;

		// Token: 0x040004B0 RID: 1200
		private int _typeState;

		// Token: 0x040004B1 RID: 1201
		private bool _isNameSelected;

		// Token: 0x040004B2 RID: 1202
		private bool _isTypeSelected;

		// Token: 0x040004B3 RID: 1203
		private bool _isFiefsSelected;

		// Token: 0x040004B4 RID: 1204
		private bool _isMembersSelected;

		// Token: 0x040004B5 RID: 1205
		private bool _isDistanceSelected;

		// Token: 0x020001B4 RID: 436
		public abstract class ItemComparerBase : IComparer<KingdomClanItemVM>
		{
			// Token: 0x0600211B RID: 8475 RVA: 0x000737FC File Offset: 0x000719FC
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x0600211C RID: 8476
			public abstract int Compare(KingdomClanItemVM x, KingdomClanItemVM y);

			// Token: 0x0600211D RID: 8477 RVA: 0x00073805 File Offset: 0x00071A05
			protected int ResolveEquality(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				return x.Clan.Name.ToString().CompareTo(y.Clan.Name.ToString());
			}

			// Token: 0x0400100F RID: 4111
			protected bool _isAscending;
		}

		// Token: 0x020001B5 RID: 437
		public class ItemNameComparer : KingdomClanSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600211F RID: 8479 RVA: 0x00073834 File Offset: 0x00071A34
			public override int Compare(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				if (this._isAscending)
				{
					return y.Clan.Name.ToString().CompareTo(x.Clan.Name.ToString()) * -1;
				}
				return y.Clan.Name.ToString().CompareTo(x.Clan.Name.ToString());
			}
		}

		// Token: 0x020001B6 RID: 438
		public class ItemTypeComparer : KingdomClanSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002121 RID: 8481 RVA: 0x000738A0 File Offset: 0x00071AA0
			public override int Compare(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				int num = y.ClanType.CompareTo(x.ClanType);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001B7 RID: 439
		public class ItemInfluenceComparer : KingdomClanSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002123 RID: 8483 RVA: 0x000738E4 File Offset: 0x00071AE4
			public override int Compare(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				int num = y.Influence.CompareTo(x.Influence);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001B8 RID: 440
		public class ItemMembersComparer : KingdomClanSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002125 RID: 8485 RVA: 0x00073928 File Offset: 0x00071B28
			public override int Compare(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				int num = y.Members.Count.CompareTo(x.Members.Count);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x020001B9 RID: 441
		public class ItemFiefsComparer : KingdomClanSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002127 RID: 8487 RVA: 0x00073978 File Offset: 0x00071B78
			public override int Compare(KingdomClanItemVM x, KingdomClanItemVM y)
			{
				int num = y.Fiefs.Count.CompareTo(x.Fiefs.Count);
				if (num != 0)
				{
					return num * (this._isAscending ? -1 : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
