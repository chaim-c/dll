using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TournamentLeaderboard
{
	// Token: 0x0200009D RID: 157
	public class TournamentLeaderboardSortControllerVM : ViewModel
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
		public TournamentLeaderboardSortControllerVM(ref MBBindingList<TournamentLeaderboardEntryItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._prizeComparer = new TournamentLeaderboardSortControllerVM.ItemPrizeComparer();
			this._nameComparer = new TournamentLeaderboardSortControllerVM.ItemNameComparer();
			this._placementComparer = new TournamentLeaderboardSortControllerVM.ItemPlacementComparer();
			this._victoriesComparer = new TournamentLeaderboardSortControllerVM.ItemVictoriesComparer();
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003C620 File Offset: 0x0003A820
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

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003C68C File Offset: 0x0003A88C
		public void ExecuteSortByPrize()
		{
			int prizeState = this.PrizeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.PrizeState = (prizeState + 1) % 3;
			if (this.PrizeState == 0)
			{
				int prizeState2 = this.PrizeState;
				this.PrizeState = prizeState2 + 1;
			}
			this._prizeComparer.SetSortMode(this.PrizeState == 1);
			this._listToControl.Sort(this._prizeComparer);
			this.IsPrizeSelected = true;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		public void ExecuteSortByPlacement()
		{
			int placementState = this.PlacementState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.PlacementState = (placementState + 1) % 3;
			if (this.PlacementState == 0)
			{
				int placementState2 = this.PlacementState;
				this.PlacementState = placementState2 + 1;
			}
			this._placementComparer.SetSortMode(this.PlacementState == 1);
			this._listToControl.Sort(this._placementComparer);
			this.IsPlacementSelected = true;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003C764 File Offset: 0x0003A964
		public void ExecuteSortByVictories()
		{
			int victoriesState = this.VictoriesState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.VictoriesState = (victoriesState + 1) % 3;
			if (this.VictoriesState == 0)
			{
				int victoriesState2 = this.VictoriesState;
				this.VictoriesState = victoriesState2 + 1;
			}
			this._victoriesComparer.SetSortMode(this.VictoriesState == 1);
			this._listToControl.Sort(this._victoriesComparer);
			this.IsVictoriesSelected = true;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003C7CE File Offset: 0x0003A9CE
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.PrizeState = (int)state;
			this.PlacementState = (int)state;
			this.VictoriesState = (int)state;
			this.IsNameSelected = false;
			this.IsVictoriesSelected = false;
			this.IsPrizeSelected = false;
			this.IsPlacementSelected = false;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003C808 File Offset: 0x0003AA08
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0003C810 File Offset: 0x0003AA10
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003C82E File Offset: 0x0003AA2E
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0003C836 File Offset: 0x0003AA36
		[DataSourceProperty]
		public int VictoriesState
		{
			get
			{
				return this._victoriesState;
			}
			set
			{
				if (value != this._victoriesState)
				{
					this._victoriesState = value;
					base.OnPropertyChangedWithValue(value, "VictoriesState");
				}
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003C854 File Offset: 0x0003AA54
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0003C85C File Offset: 0x0003AA5C
		[DataSourceProperty]
		public int PrizeState
		{
			get
			{
				return this._prizeState;
			}
			set
			{
				if (value != this._prizeState)
				{
					this._prizeState = value;
					base.OnPropertyChangedWithValue(value, "PrizeState");
				}
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003C87A File Offset: 0x0003AA7A
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0003C882 File Offset: 0x0003AA82
		[DataSourceProperty]
		public int PlacementState
		{
			get
			{
				return this._placementState;
			}
			set
			{
				if (value != this._placementState)
				{
					this._placementState = value;
					base.OnPropertyChangedWithValue(value, "PlacementState");
				}
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
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

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0003C8C6 File Offset: 0x0003AAC6
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0003C8CE File Offset: 0x0003AACE
		[DataSourceProperty]
		public bool IsPrizeSelected
		{
			get
			{
				return this._isPrizeSelected;
			}
			set
			{
				if (value != this._isPrizeSelected)
				{
					this._isPrizeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsPrizeSelected");
				}
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003C8EC File Offset: 0x0003AAEC
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0003C8F4 File Offset: 0x0003AAF4
		[DataSourceProperty]
		public bool IsPlacementSelected
		{
			get
			{
				return this._isPlacementSelected;
			}
			set
			{
				if (value != this._isPlacementSelected)
				{
					this._isPlacementSelected = value;
					base.OnPropertyChangedWithValue(value, "IsPlacementSelected");
				}
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0003C912 File Offset: 0x0003AB12
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0003C91A File Offset: 0x0003AB1A
		[DataSourceProperty]
		public bool IsVictoriesSelected
		{
			get
			{
				return this._isVictoriesSelected;
			}
			set
			{
				if (value != this._isVictoriesSelected)
				{
					this._isVictoriesSelected = value;
					base.OnPropertyChangedWithValue(value, "IsVictoriesSelected");
				}
			}
		}

		// Token: 0x04000718 RID: 1816
		private readonly MBBindingList<TournamentLeaderboardEntryItemVM> _listToControl;

		// Token: 0x04000719 RID: 1817
		private readonly TournamentLeaderboardSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x0400071A RID: 1818
		private readonly TournamentLeaderboardSortControllerVM.ItemPrizeComparer _prizeComparer;

		// Token: 0x0400071B RID: 1819
		private readonly TournamentLeaderboardSortControllerVM.ItemPlacementComparer _placementComparer;

		// Token: 0x0400071C RID: 1820
		private readonly TournamentLeaderboardSortControllerVM.ItemVictoriesComparer _victoriesComparer;

		// Token: 0x0400071D RID: 1821
		private int _nameState;

		// Token: 0x0400071E RID: 1822
		private int _prizeState;

		// Token: 0x0400071F RID: 1823
		private int _placementState;

		// Token: 0x04000720 RID: 1824
		private int _victoriesState;

		// Token: 0x04000721 RID: 1825
		private bool _isNameSelected;

		// Token: 0x04000722 RID: 1826
		private bool _isPrizeSelected;

		// Token: 0x04000723 RID: 1827
		private bool _isPlacementSelected;

		// Token: 0x04000724 RID: 1828
		private bool _isVictoriesSelected;

		// Token: 0x020001E0 RID: 480
		public abstract class ItemComparerBase : IComparer<TournamentLeaderboardEntryItemVM>
		{
			// Token: 0x06002195 RID: 8597 RVA: 0x000741E0 File Offset: 0x000723E0
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x06002196 RID: 8598
			public abstract int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y);

			// Token: 0x0400107F RID: 4223
			protected bool _isAcending;
		}

		// Token: 0x020001E1 RID: 481
		public class ItemNameComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002198 RID: 8600 RVA: 0x000741F1 File Offset: 0x000723F1
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x020001E2 RID: 482
		public class ItemPrizeComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600219A RID: 8602 RVA: 0x00074228 File Offset: 0x00072428
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.PrizeValue.CompareTo(x.PrizeValue) * -1;
				}
				return y.PrizeValue.CompareTo(x.PrizeValue);
			}
		}

		// Token: 0x020001E3 RID: 483
		public class ItemPlacementComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600219C RID: 8604 RVA: 0x00074270 File Offset: 0x00072470
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.PlacementOnLeaderboard.CompareTo(x.PlacementOnLeaderboard) * -1;
				}
				return y.PlacementOnLeaderboard.CompareTo(x.PlacementOnLeaderboard);
			}
		}

		// Token: 0x020001E4 RID: 484
		public class ItemVictoriesComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600219E RID: 8606 RVA: 0x000742B8 File Offset: 0x000724B8
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.Victories.CompareTo(x.Victories) * -1;
				}
				return y.Victories.CompareTo(x.Victories);
			}
		}
	}
}
