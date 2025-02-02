using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011F RID: 287
	public class ClanPartiesSortControllerVM : ViewModel
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x0006403B File Offset: 0x0006223B
		public ClanPartiesSortControllerVM(MBBindingList<MBBindingList<ClanPartyItemVM>> listsToControl)
		{
			this._listsToControl = listsToControl;
			this._nameComparer = new ClanPartiesSortControllerVM.ItemNameComparer();
			this._locationComparer = new ClanPartiesSortControllerVM.ItemLocationComparer();
			this._sizeComparer = new ClanPartiesSortControllerVM.ItemSizeComparer();
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0006406C File Offset: 0x0006226C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.SizeText = GameTexts.FindText("str_clan_party_size", null).ToString();
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000640C4 File Offset: 0x000622C4
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
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._nameComparer);
			}
			this.IsNameSelected = true;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00064160 File Offset: 0x00062360
		public void ExecuteSortByLocation()
		{
			int locationState = this.LocationState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.LocationState = (locationState + 1) % 3;
			if (this.LocationState == 0)
			{
				int locationState2 = this.LocationState;
				this.LocationState = locationState2 + 1;
			}
			this._locationComparer.SetSortMode(this.LocationState == 1);
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._locationComparer);
			}
			this.IsLocationSelected = true;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000641FC File Offset: 0x000623FC
		public void ExecuteSortBySize()
		{
			int sizeState = this.SizeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.SizeState = (sizeState + 1) % 3;
			if (this.SizeState == 0)
			{
				int sizeState2 = this.SizeState;
				this.SizeState = sizeState2 + 1;
			}
			this._sizeComparer.SetSortMode(this.SizeState == 1);
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._sizeComparer);
			}
			this.IsSizeSelected = true;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00064298 File Offset: 0x00062498
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.LocationState = (int)state;
			this.SizeState = (int)state;
			this.IsNameSelected = false;
			this.IsLocationSelected = false;
			this.IsSizeSelected = false;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000642C4 File Offset: 0x000624C4
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x000642CD File Offset: 0x000624CD
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x000642D5 File Offset: 0x000624D5
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

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x000642F3 File Offset: 0x000624F3
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x000642FB File Offset: 0x000624FB
		[DataSourceProperty]
		public int LocationState
		{
			get
			{
				return this._locationState;
			}
			set
			{
				if (value != this._locationState)
				{
					this._locationState = value;
					base.OnPropertyChangedWithValue(value, "LocationState");
				}
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00064319 File Offset: 0x00062519
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00064321 File Offset: 0x00062521
		[DataSourceProperty]
		public int SizeState
		{
			get
			{
				return this._sizeState;
			}
			set
			{
				if (value != this._sizeState)
				{
					this._sizeState = value;
					base.OnPropertyChangedWithValue(value, "SizeState");
				}
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0006433F File Offset: 0x0006253F
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00064347 File Offset: 0x00062547
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

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00064365 File Offset: 0x00062565
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0006436D File Offset: 0x0006256D
		[DataSourceProperty]
		public bool IsLocationSelected
		{
			get
			{
				return this._isLocationSelected;
			}
			set
			{
				if (value != this._isLocationSelected)
				{
					this._isLocationSelected = value;
					base.OnPropertyChangedWithValue(value, "IsLocationSelected");
				}
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0006438B File Offset: 0x0006258B
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x00064393 File Offset: 0x00062593
		[DataSourceProperty]
		public bool IsSizeSelected
		{
			get
			{
				return this._isSizeSelected;
			}
			set
			{
				if (value != this._isSizeSelected)
				{
					this._isSizeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSizeSelected");
				}
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000643B1 File Offset: 0x000625B1
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x000643B9 File Offset: 0x000625B9
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x000643DC File Offset: 0x000625DC
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x000643E4 File Offset: 0x000625E4
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00064407 File Offset: 0x00062607
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0006440F File Offset: 0x0006260F
		[DataSourceProperty]
		public string SizeText
		{
			get
			{
				return this._sizeText;
			}
			set
			{
				if (value != this._sizeText)
				{
					this._sizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SizeText");
				}
			}
		}

		// Token: 0x04000D18 RID: 3352
		private readonly MBBindingList<MBBindingList<ClanPartyItemVM>> _listsToControl;

		// Token: 0x04000D19 RID: 3353
		private readonly ClanPartiesSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000D1A RID: 3354
		private readonly ClanPartiesSortControllerVM.ItemLocationComparer _locationComparer;

		// Token: 0x04000D1B RID: 3355
		private readonly ClanPartiesSortControllerVM.ItemSizeComparer _sizeComparer;

		// Token: 0x04000D1C RID: 3356
		private int _nameState;

		// Token: 0x04000D1D RID: 3357
		private int _locationState;

		// Token: 0x04000D1E RID: 3358
		private int _sizeState;

		// Token: 0x04000D1F RID: 3359
		private bool _isNameSelected;

		// Token: 0x04000D20 RID: 3360
		private bool _isLocationSelected;

		// Token: 0x04000D21 RID: 3361
		private bool _isSizeSelected;

		// Token: 0x04000D22 RID: 3362
		private string _nameText;

		// Token: 0x04000D23 RID: 3363
		private string _locationText;

		// Token: 0x04000D24 RID: 3364
		private string _sizeText;

		// Token: 0x02000277 RID: 631
		public abstract class ItemComparerBase : IComparer<ClanPartyItemVM>
		{
			// Token: 0x0600237C RID: 9084 RVA: 0x000771F8 File Offset: 0x000753F8
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x0600237D RID: 9085
			public abstract int Compare(ClanPartyItemVM x, ClanPartyItemVM y);

			// Token: 0x040011CB RID: 4555
			protected bool _isAcending;
		}

		// Token: 0x02000278 RID: 632
		public class ItemNameComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600237F RID: 9087 RVA: 0x00077209 File Offset: 0x00075409
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x02000279 RID: 633
		public class ItemLocationComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002381 RID: 9089 RVA: 0x00077240 File Offset: 0x00075440
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Party.MobileParty.GetTrackDistanceToMainAgent().CompareTo(x.Party.MobileParty.GetTrackDistanceToMainAgent()) * -1;
				}
				return y.Party.MobileParty.GetTrackDistanceToMainAgent().CompareTo(x.Party.MobileParty.GetTrackDistanceToMainAgent());
			}
		}

		// Token: 0x0200027A RID: 634
		public class ItemSizeComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002383 RID: 9091 RVA: 0x000772B0 File Offset: 0x000754B0
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Party.MobileParty.MemberRoster.TotalManCount.CompareTo(x.Party.MobileParty.MemberRoster.TotalManCount) * -1;
				}
				return y.Party.MobileParty.MemberRoster.TotalManCount.CompareTo(x.Party.MobileParty.MemberRoster.TotalManCount);
			}
		}
	}
}
