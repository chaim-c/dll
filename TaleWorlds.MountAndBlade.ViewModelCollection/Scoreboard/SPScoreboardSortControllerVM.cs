using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard
{
	// Token: 0x02000012 RID: 18
	public class SPScoreboardSortControllerVM : ViewModel
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00006028 File Offset: 0x00004228
		public SPScoreboardSortControllerVM(ref MBBindingList<SPScoreboardPartyVM> listToControl)
		{
			this._listToControl = listToControl;
			this._remainingComparer = new SPScoreboardSortControllerVM.ItemRemainingComparer();
			this._killComparer = new SPScoreboardSortControllerVM.ItemKillComparer();
			this._upgradeComparer = new SPScoreboardSortControllerVM.ItemUpgradeComparer();
			this._deadComparer = new SPScoreboardSortControllerVM.ItemDeadComparer();
			this._woundedComparer = new SPScoreboardSortControllerVM.ItemWoundedComparer();
			this._routedComparer = new SPScoreboardSortControllerVM.ItemRoutedComparer();
			this._memberComparer = new SPScoreboardSortControllerVM.ItemMemberComparer();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006090 File Offset: 0x00004290
		public void ExecuteSortByRemaining()
		{
			int remainingState = this.RemainingState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.RemainingState = (remainingState + 1) % 3;
			this._remainingComparer.SetSortMode(this.RemainingState == 1);
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._remainingComparer;
			if (this.RemainingState == 0)
			{
				comparer = this._memberComparer;
			}
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsRemainingSelected = true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000612C File Offset: 0x0000432C
		public void ExecuteSortByKill()
		{
			int killState = this.KillState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.KillState = (killState + 1) % 3;
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._killComparer;
			if (this.KillState == 0)
			{
				comparer = this._memberComparer;
			}
			this._killComparer.SetSortMode(this.KillState == 1);
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsKillSelected = true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000061C8 File Offset: 0x000043C8
		public void ExecuteSortByUpgrade()
		{
			int upgradeState = this.UpgradeState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.UpgradeState = (upgradeState + 1) % 3;
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._upgradeComparer;
			if (this.UpgradeState == 0)
			{
				comparer = this._memberComparer;
			}
			this._upgradeComparer.SetSortMode(this.UpgradeState == 1);
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsUpgradeSelected = true;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006264 File Offset: 0x00004464
		public void ExecuteSortByDead()
		{
			int deadState = this.DeadState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.DeadState = (deadState + 1) % 3;
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._deadComparer;
			if (this.DeadState == 0)
			{
				comparer = this._memberComparer;
			}
			this._deadComparer.SetSortMode(this.DeadState == 1);
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsDeadSelected = true;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006300 File Offset: 0x00004500
		public void ExecuteSortByWounded()
		{
			int woundedState = this.WoundedState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.WoundedState = (woundedState + 1) % 3;
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._woundedComparer;
			if (this.WoundedState == 0)
			{
				comparer = this._memberComparer;
			}
			this._woundedComparer.SetSortMode(this.WoundedState == 1);
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsWoundedSelected = true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000639C File Offset: 0x0000459C
		public void ExecuteSortByRouted()
		{
			int routedState = this.RoutedState;
			this.SetAllStates(SPScoreboardSortControllerVM.SortState.Default);
			this.RoutedState = (routedState + 1) % 3;
			SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase comparer = this._routedComparer;
			if (this.RoutedState == 0)
			{
				comparer = this._memberComparer;
			}
			this._routedComparer.SetSortMode(this.RoutedState == 1);
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._listToControl)
			{
				spscoreboardPartyVM.Members.Sort(comparer);
			}
			this.IsRoutedSelected = true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006438 File Offset: 0x00004638
		private void SetAllStates(SPScoreboardSortControllerVM.SortState state)
		{
			this.RemainingState = (int)state;
			this.KillState = (int)state;
			this.UpgradeState = (int)state;
			this.DeadState = (int)state;
			this.WoundedState = (int)state;
			this.RoutedState = (int)state;
			this.IsRemainingSelected = false;
			this.IsKillSelected = false;
			this.IsUpgradeSelected = false;
			this.IsDeadSelected = false;
			this.IsWoundedSelected = false;
			this.IsRoutedSelected = false;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006499 File Offset: 0x00004699
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000064A1 File Offset: 0x000046A1
		[DataSourceProperty]
		public int RemainingState
		{
			get
			{
				return this._remainingState;
			}
			set
			{
				if (value != this._remainingState)
				{
					this._remainingState = value;
					base.OnPropertyChanged("RemainingState");
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000064BE File Offset: 0x000046BE
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000064C6 File Offset: 0x000046C6
		[DataSourceProperty]
		public bool IsRemainingSelected
		{
			get
			{
				return this._isRemainingSelected;
			}
			set
			{
				if (value != this._isRemainingSelected)
				{
					this._isRemainingSelected = value;
					base.OnPropertyChanged("IsRemainingSelected");
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000064E3 File Offset: 0x000046E3
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000064EB File Offset: 0x000046EB
		[DataSourceProperty]
		public int KillState
		{
			get
			{
				return this._killState;
			}
			set
			{
				if (value != this._killState)
				{
					this._killState = value;
					base.OnPropertyChanged("KillState");
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006508 File Offset: 0x00004708
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00006510 File Offset: 0x00004710
		[DataSourceProperty]
		public bool IsKillSelected
		{
			get
			{
				return this._isKillSelected;
			}
			set
			{
				if (value != this._isKillSelected)
				{
					this._isKillSelected = value;
					base.OnPropertyChanged("IsKillSelected");
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000652D File Offset: 0x0000472D
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00006535 File Offset: 0x00004735
		[DataSourceProperty]
		public int UpgradeState
		{
			get
			{
				return this._upgradeState;
			}
			set
			{
				if (value != this._upgradeState)
				{
					this._upgradeState = value;
					base.OnPropertyChanged("UpgradeState");
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006552 File Offset: 0x00004752
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000655A File Offset: 0x0000475A
		[DataSourceProperty]
		public bool IsUpgradeSelected
		{
			get
			{
				return this._isUpgradeSelected;
			}
			set
			{
				if (value != this._isUpgradeSelected)
				{
					this._isUpgradeSelected = value;
					base.OnPropertyChanged("IsUpgradeSelected");
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006577 File Offset: 0x00004777
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000657F File Offset: 0x0000477F
		[DataSourceProperty]
		public int DeadState
		{
			get
			{
				return this._deadState;
			}
			set
			{
				if (value != this._deadState)
				{
					this._deadState = value;
					base.OnPropertyChanged("DeadState");
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000659C File Offset: 0x0000479C
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000065A4 File Offset: 0x000047A4
		[DataSourceProperty]
		public bool IsDeadSelected
		{
			get
			{
				return this._isDeadSelected;
			}
			set
			{
				if (value != this._isDeadSelected)
				{
					this._isDeadSelected = value;
					base.OnPropertyChanged("IsDeadSelected");
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000065C1 File Offset: 0x000047C1
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000065C9 File Offset: 0x000047C9
		[DataSourceProperty]
		public int WoundedState
		{
			get
			{
				return this._woundedState;
			}
			set
			{
				if (value != this._woundedState)
				{
					this._woundedState = value;
					base.OnPropertyChanged("WoundedState");
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000065E6 File Offset: 0x000047E6
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000065EE File Offset: 0x000047EE
		[DataSourceProperty]
		public bool IsWoundedSelected
		{
			get
			{
				return this._isWoundedSelected;
			}
			set
			{
				if (value != this._isWoundedSelected)
				{
					this._isWoundedSelected = value;
					base.OnPropertyChanged("IsWoundedSelected");
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000660B File Offset: 0x0000480B
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006613 File Offset: 0x00004813
		[DataSourceProperty]
		public int RoutedState
		{
			get
			{
				return this._routedState;
			}
			set
			{
				if (value != this._routedState)
				{
					this._routedState = value;
					base.OnPropertyChanged("RoutedState");
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006630 File Offset: 0x00004830
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006638 File Offset: 0x00004838
		[DataSourceProperty]
		public bool IsRoutedSelected
		{
			get
			{
				return this._isRoutedSelected;
			}
			set
			{
				if (value != this._isRoutedSelected)
				{
					this._isRoutedSelected = value;
					base.OnPropertyChanged("IsRoutedSelected");
				}
			}
		}

		// Token: 0x04000099 RID: 153
		private readonly MBBindingList<SPScoreboardPartyVM> _listToControl;

		// Token: 0x0400009A RID: 154
		private readonly SPScoreboardSortControllerVM.ItemRemainingComparer _remainingComparer;

		// Token: 0x0400009B RID: 155
		private readonly SPScoreboardSortControllerVM.ItemKillComparer _killComparer;

		// Token: 0x0400009C RID: 156
		private readonly SPScoreboardSortControllerVM.ItemUpgradeComparer _upgradeComparer;

		// Token: 0x0400009D RID: 157
		private readonly SPScoreboardSortControllerVM.ItemDeadComparer _deadComparer;

		// Token: 0x0400009E RID: 158
		private readonly SPScoreboardSortControllerVM.ItemWoundedComparer _woundedComparer;

		// Token: 0x0400009F RID: 159
		private readonly SPScoreboardSortControllerVM.ItemRoutedComparer _routedComparer;

		// Token: 0x040000A0 RID: 160
		private readonly SPScoreboardSortControllerVM.ItemMemberComparer _memberComparer;

		// Token: 0x040000A1 RID: 161
		private int _remainingState;

		// Token: 0x040000A2 RID: 162
		private bool _isRemainingSelected;

		// Token: 0x040000A3 RID: 163
		private int _killState;

		// Token: 0x040000A4 RID: 164
		private bool _isKillSelected;

		// Token: 0x040000A5 RID: 165
		private int _upgradeState;

		// Token: 0x040000A6 RID: 166
		private bool _isUpgradeSelected;

		// Token: 0x040000A7 RID: 167
		private int _deadState;

		// Token: 0x040000A8 RID: 168
		private bool _isDeadSelected;

		// Token: 0x040000A9 RID: 169
		private int _woundedState;

		// Token: 0x040000AA RID: 170
		private bool _isWoundedSelected;

		// Token: 0x040000AB RID: 171
		private int _routedState;

		// Token: 0x040000AC RID: 172
		private bool _isRoutedSelected;

		// Token: 0x0200008A RID: 138
		private enum SortState
		{
			// Token: 0x04000500 RID: 1280
			Default,
			// Token: 0x04000501 RID: 1281
			Ascending,
			// Token: 0x04000502 RID: 1282
			Descending
		}

		// Token: 0x0200008B RID: 139
		public abstract class ScoreboardUnitItemComparerBase : IComparer<SPScoreboardUnitVM>
		{
			// Token: 0x06000A66 RID: 2662 RVA: 0x000274E9 File Offset: 0x000256E9
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x06000A67 RID: 2663
			public abstract int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y);

			// Token: 0x04000503 RID: 1283
			protected bool _isAscending;
		}

		// Token: 0x0200008C RID: 140
		public class ItemRemainingComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A69 RID: 2665 RVA: 0x000274FC File Offset: 0x000256FC
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.Remaining.CompareTo(x.Score.Remaining) * -1;
				}
				return y.Score.Remaining.CompareTo(x.Score.Remaining);
			}
		}

		// Token: 0x0200008D RID: 141
		public class ItemKillComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A6B RID: 2667 RVA: 0x00027558 File Offset: 0x00025758
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.Kill.CompareTo(x.Score.Kill) * -1;
				}
				return y.Score.Kill.CompareTo(x.Score.Kill);
			}
		}

		// Token: 0x0200008E RID: 142
		public class ItemUpgradeComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A6D RID: 2669 RVA: 0x000275B4 File Offset: 0x000257B4
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.ReadyToUpgrade.CompareTo(x.Score.ReadyToUpgrade) * -1;
				}
				return y.Score.ReadyToUpgrade.CompareTo(x.Score.ReadyToUpgrade);
			}
		}

		// Token: 0x0200008F RID: 143
		public class ItemDeadComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A6F RID: 2671 RVA: 0x00027610 File Offset: 0x00025810
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.Dead.CompareTo(x.Score.Dead) * -1;
				}
				return y.Score.Dead.CompareTo(x.Score.Dead);
			}
		}

		// Token: 0x02000090 RID: 144
		public class ItemWoundedComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A71 RID: 2673 RVA: 0x0002766C File Offset: 0x0002586C
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.Wounded.CompareTo(x.Score.Wounded) * -1;
				}
				return y.Score.Wounded.CompareTo(x.Score.Wounded);
			}
		}

		// Token: 0x02000091 RID: 145
		public class ItemRoutedComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A73 RID: 2675 RVA: 0x000276C8 File Offset: 0x000258C8
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (this._isAscending)
				{
					return y.Score.Routed.CompareTo(x.Score.Routed) * -1;
				}
				return y.Score.Routed.CompareTo(x.Score.Routed);
			}
		}

		// Token: 0x02000092 RID: 146
		public class ItemMemberComparer : SPScoreboardSortControllerVM.ScoreboardUnitItemComparerBase
		{
			// Token: 0x06000A75 RID: 2677 RVA: 0x00027724 File Offset: 0x00025924
			public override int Compare(SPScoreboardUnitVM x, SPScoreboardUnitVM y)
			{
				if (x.Character.IsPlayerCharacter && !y.Character.IsPlayerCharacter)
				{
					return -1;
				}
				if (!x.Character.IsPlayerCharacter && y.Character.IsPlayerCharacter)
				{
					return 1;
				}
				if (x.IsHero && !y.IsHero)
				{
					return -1;
				}
				if (!x.IsHero && y.IsHero)
				{
					return 1;
				}
				return x.Character.Name.ToString().CompareTo(y.Character.Name.ToString());
			}
		}
	}
}
