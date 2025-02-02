using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x02000119 RID: 281
	public class ClanFiefsSortControllerVM : ViewModel
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00061429 File Offset: 0x0005F629
		public ClanFiefsSortControllerVM(List<MBBindingList<ClanSettlementItemVM>> listsToControl)
		{
			this._listsToControl = listsToControl;
			this._nameComparer = new ClanFiefsSortControllerVM.ItemNameComparer();
			this._governorComparer = new ClanFiefsSortControllerVM.ItemGovernorComparer();
			this._profitComparer = new ClanFiefsSortControllerVM.ItemProfitComparer();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006145C File Offset: 0x0005F65C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.GovernorText = GameTexts.FindText("str_notable_governor", null).ToString();
			this.ProfitText = GameTexts.FindText("str_profit", null).ToString();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000614B4 File Offset: 0x0005F6B4
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
			foreach (MBBindingList<ClanSettlementItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._nameComparer);
			}
			this.IsNameSelected = true;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00061558 File Offset: 0x0005F758
		public void ExecuteSortByGovernor()
		{
			int governorState = this.GovernorState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.GovernorState = (governorState + 1) % 3;
			if (this.GovernorState == 0)
			{
				int governorState2 = this.GovernorState;
				this.GovernorState = governorState2 + 1;
			}
			this._governorComparer.SetSortMode(this.GovernorState == 1);
			foreach (MBBindingList<ClanSettlementItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._governorComparer);
			}
			this.IsGovernorSelected = true;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000615FC File Offset: 0x0005F7FC
		public void ExecuteSortByProfit()
		{
			int profitState = this.ProfitState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.ProfitState = (profitState + 1) % 3;
			if (this.ProfitState == 0)
			{
				int profitState2 = this.ProfitState;
				this.ProfitState = profitState2 + 1;
			}
			this._profitComparer.SetSortMode(this.ProfitState == 1);
			foreach (MBBindingList<ClanSettlementItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._profitComparer);
			}
			this.IsProfitSelected = true;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000616A0 File Offset: 0x0005F8A0
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.GovernorState = (int)state;
			this.ProfitState = (int)state;
			this.IsNameSelected = false;
			this.IsGovernorSelected = false;
			this.IsProfitSelected = false;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000616CC File Offset: 0x0005F8CC
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000616D5 File Offset: 0x0005F8D5
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x000616DD File Offset: 0x0005F8DD
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

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000616FB File Offset: 0x0005F8FB
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x00061703 File Offset: 0x0005F903
		[DataSourceProperty]
		public int GovernorState
		{
			get
			{
				return this._governorState;
			}
			set
			{
				if (value != this._governorState)
				{
					this._governorState = value;
					base.OnPropertyChangedWithValue(value, "GovernorState");
				}
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00061721 File Offset: 0x0005F921
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x00061729 File Offset: 0x0005F929
		[DataSourceProperty]
		public int ProfitState
		{
			get
			{
				return this._profitState;
			}
			set
			{
				if (value != this._profitState)
				{
					this._profitState = value;
					base.OnPropertyChangedWithValue(value, "ProfitState");
				}
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00061747 File Offset: 0x0005F947
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0006174F File Offset: 0x0005F94F
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

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0006176D File Offset: 0x0005F96D
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00061775 File Offset: 0x0005F975
		[DataSourceProperty]
		public bool IsGovernorSelected
		{
			get
			{
				return this._isGovernorSelected;
			}
			set
			{
				if (value != this._isGovernorSelected)
				{
					this._isGovernorSelected = value;
					base.OnPropertyChangedWithValue(value, "IsGovernorSelected");
				}
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00061793 File Offset: 0x0005F993
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x0006179B File Offset: 0x0005F99B
		[DataSourceProperty]
		public bool IsProfitSelected
		{
			get
			{
				return this._isProfitSelected;
			}
			set
			{
				if (value != this._isProfitSelected)
				{
					this._isProfitSelected = value;
					base.OnPropertyChangedWithValue(value, "IsProfitSelected");
				}
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000617B9 File Offset: 0x0005F9B9
		// (set) Token: 0x06001AF5 RID: 6901 RVA: 0x000617C1 File Offset: 0x0005F9C1
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

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000617E4 File Offset: 0x0005F9E4
		// (set) Token: 0x06001AF7 RID: 6903 RVA: 0x000617EC File Offset: 0x0005F9EC
		[DataSourceProperty]
		public string GovernorText
		{
			get
			{
				return this._governorText;
			}
			set
			{
				if (value != this._governorText)
				{
					this._governorText = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorText");
				}
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0006180F File Offset: 0x0005FA0F
		// (set) Token: 0x06001AF9 RID: 6905 RVA: 0x00061817 File Offset: 0x0005FA17
		[DataSourceProperty]
		public string ProfitText
		{
			get
			{
				return this._profitText;
			}
			set
			{
				if (value != this._profitText)
				{
					this._profitText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProfitText");
				}
			}
		}

		// Token: 0x04000CB3 RID: 3251
		private readonly List<MBBindingList<ClanSettlementItemVM>> _listsToControl;

		// Token: 0x04000CB4 RID: 3252
		private readonly ClanFiefsSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000CB5 RID: 3253
		private readonly ClanFiefsSortControllerVM.ItemGovernorComparer _governorComparer;

		// Token: 0x04000CB6 RID: 3254
		private readonly ClanFiefsSortControllerVM.ItemProfitComparer _profitComparer;

		// Token: 0x04000CB7 RID: 3255
		private int _nameState;

		// Token: 0x04000CB8 RID: 3256
		private int _governorState;

		// Token: 0x04000CB9 RID: 3257
		private int _profitState;

		// Token: 0x04000CBA RID: 3258
		private bool _isNameSelected;

		// Token: 0x04000CBB RID: 3259
		private bool _isGovernorSelected;

		// Token: 0x04000CBC RID: 3260
		private bool _isProfitSelected;

		// Token: 0x04000CBD RID: 3261
		private string _nameText;

		// Token: 0x04000CBE RID: 3262
		private string _governorText;

		// Token: 0x04000CBF RID: 3263
		private string _profitText;

		// Token: 0x0200025B RID: 603
		public abstract class ItemComparerBase : IComparer<ClanSettlementItemVM>
		{
			// Token: 0x0600231A RID: 8986 RVA: 0x000762E7 File Offset: 0x000744E7
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x0600231B RID: 8987
			public abstract int Compare(ClanSettlementItemVM x, ClanSettlementItemVM y);

			// Token: 0x04001199 RID: 4505
			protected bool _isAcending;
		}

		// Token: 0x0200025C RID: 604
		public class ItemNameComparer : ClanFiefsSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600231D RID: 8989 RVA: 0x000762F8 File Offset: 0x000744F8
			public override int Compare(ClanSettlementItemVM x, ClanSettlementItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x0200025D RID: 605
		public class ItemGovernorComparer : ClanFiefsSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600231F RID: 8991 RVA: 0x00076330 File Offset: 0x00074530
			public override int Compare(ClanSettlementItemVM x, ClanSettlementItemVM y)
			{
				if (this._isAcending)
				{
					if (y.HasGovernor && x.HasGovernor)
					{
						return y.Governor.NameText.CompareTo(x.Governor.NameText) * -1;
					}
					if (y.HasGovernor)
					{
						return 1;
					}
					if (x.HasGovernor)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y.HasGovernor && x.HasGovernor)
					{
						return y.Governor.NameText.CompareTo(x.Governor.NameText);
					}
					if (y.HasGovernor)
					{
						return 1;
					}
					if (x.HasGovernor)
					{
						return -1;
					}
					return 0;
				}
			}
		}

		// Token: 0x0200025E RID: 606
		public class ItemProfitComparer : ClanFiefsSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002321 RID: 8993 RVA: 0x000763D4 File Offset: 0x000745D4
			public override int Compare(ClanSettlementItemVM x, ClanSettlementItemVM y)
			{
				if (this._isAcending)
				{
					return y.TotalProfit.Value.CompareTo(x.TotalProfit.Value) * -1;
				}
				return y.TotalProfit.Value.CompareTo(x.TotalProfit.Value);
			}
		}
	}
}
