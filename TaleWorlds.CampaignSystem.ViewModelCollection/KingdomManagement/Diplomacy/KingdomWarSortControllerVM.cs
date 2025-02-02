using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000067 RID: 103
	public class KingdomWarSortControllerVM : ViewModel
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00025572 File Offset: 0x00023772
		public KingdomWarSortControllerVM(ref MBBindingList<KingdomWarItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._scoreComparer = new KingdomWarSortControllerVM.ItemScoreComparer();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00025590 File Offset: 0x00023790
		private void ExecuteSortByScore()
		{
			int scoreState = this.ScoreState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.ScoreState = (scoreState + 1) % 3;
			if (this.ScoreState == 0)
			{
				int scoreState2 = this.ScoreState;
				this.ScoreState = scoreState2 + 1;
			}
			this._scoreComparer.SetSortMode(this.ScoreState == 1);
			this._listToControl.Sort(this._scoreComparer);
			this.IsScoreSelected = true;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000255FA File Offset: 0x000237FA
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.ScoreState = (int)state;
			this.IsScoreSelected = false;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0002560A File Offset: 0x0002380A
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00025612 File Offset: 0x00023812
		[DataSourceProperty]
		public int ScoreState
		{
			get
			{
				return this._scoreState;
			}
			set
			{
				if (value != this._scoreState)
				{
					this._scoreState = value;
					base.OnPropertyChangedWithValue(value, "ScoreState");
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00025630 File Offset: 0x00023830
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00025638 File Offset: 0x00023838
		[DataSourceProperty]
		public bool IsScoreSelected
		{
			get
			{
				return this._isScoreSelected;
			}
			set
			{
				if (value != this._isScoreSelected)
				{
					this._isScoreSelected = value;
					base.OnPropertyChangedWithValue(value, "IsScoreSelected");
				}
			}
		}

		// Token: 0x040003F9 RID: 1017
		private readonly MBBindingList<KingdomWarItemVM> _listToControl;

		// Token: 0x040003FA RID: 1018
		private readonly KingdomWarSortControllerVM.ItemScoreComparer _scoreComparer;

		// Token: 0x040003FB RID: 1019
		private int _scoreState;

		// Token: 0x040003FC RID: 1020
		private bool _isScoreSelected;

		// Token: 0x020001A5 RID: 421
		public abstract class ItemComparerBase : IComparer<KingdomWarItemVM>
		{
			// Token: 0x060020EB RID: 8427 RVA: 0x00073565 File Offset: 0x00071765
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x060020EC RID: 8428
			public abstract int Compare(KingdomWarItemVM x, KingdomWarItemVM y);

			// Token: 0x04000FE3 RID: 4067
			protected bool _isAscending;
		}

		// Token: 0x020001A6 RID: 422
		public class ItemScoreComparer : KingdomWarSortControllerVM.ItemComparerBase
		{
			// Token: 0x060020EE RID: 8430 RVA: 0x00073578 File Offset: 0x00071778
			public override int Compare(KingdomWarItemVM x, KingdomWarItemVM y)
			{
				if (this._isAscending)
				{
					return x.Score.CompareTo(y.Score);
				}
				return x.Score.CompareTo(y.Score) * -1;
			}
		}
	}
}
