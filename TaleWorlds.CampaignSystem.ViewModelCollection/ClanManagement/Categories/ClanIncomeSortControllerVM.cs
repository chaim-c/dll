using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Supporters;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011B RID: 283
	public class ClanIncomeSortControllerVM : ViewModel
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x00062484 File Offset: 0x00060684
		public ClanIncomeSortControllerVM(MBBindingList<ClanFinanceWorkshopItemVM> workshopList, MBBindingList<ClanSupporterGroupVM> supporterList, MBBindingList<ClanFinanceAlleyItemVM> alleyList)
		{
			this._workshopList = workshopList;
			this._supporterList = supporterList;
			this._alleyList = alleyList;
			this._workshopNameComparer = new ClanIncomeSortControllerVM.WorkshopItemNameComparer();
			this._supporterNameComparer = new ClanIncomeSortControllerVM.SupporterItemNameComparer();
			this._alleyNameComparer = new ClanIncomeSortControllerVM.AlleyItemNameComparer();
			this._workshopLocationComparer = new ClanIncomeSortControllerVM.WorkshopItemLocationComparer();
			this._alleyLocationComparer = new ClanIncomeSortControllerVM.AlleyItemLocationComparer();
			this._workshopIncomeComparer = new ClanIncomeSortControllerVM.WorkshopItemIncomeComparer();
			this._supporterIncomeComparer = new ClanIncomeSortControllerVM.SupporterItemIncomeComparer();
			this._alleyIncomeComparer = new ClanIncomeSortControllerVM.AlleyItemIncomeComparer();
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00062504 File Offset: 0x00060704
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.IncomeText = GameTexts.FindText("str_income", null).ToString();
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006255C File Offset: 0x0006075C
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
			this._workshopNameComparer.SetSortMode(this.NameState == 1);
			this._supporterNameComparer.SetSortMode(this.NameState == 1);
			this._alleyNameComparer.SetSortMode(this.NameState == 1);
			this._workshopList.Sort(this._workshopNameComparer);
			this._supporterList.Sort(this._supporterNameComparer);
			this._alleyList.Sort(this._alleyNameComparer);
			this.IsNameSelected = true;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00062610 File Offset: 0x00060810
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
			this._workshopLocationComparer.SetSortMode(this.LocationState == 1);
			this._alleyLocationComparer.SetSortMode(this.LocationState == 1);
			this._workshopList.Sort(this._workshopLocationComparer);
			this._alleyList.Sort(this._alleyLocationComparer);
			this.IsLocationSelected = true;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000626A0 File Offset: 0x000608A0
		public void ExecuteSortByIncome()
		{
			int incomeState = this.IncomeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.IncomeState = (incomeState + 1) % 3;
			if (this.IncomeState == 0)
			{
				int incomeState2 = this.IncomeState;
				this.IncomeState = incomeState2 + 1;
			}
			this._workshopIncomeComparer.SetSortMode(this.IncomeState == 1);
			this._supporterIncomeComparer.SetSortMode(this.IncomeState == 1);
			this._alleyIncomeComparer.SetSortMode(this.IncomeState == 1);
			this._workshopList.Sort(this._workshopIncomeComparer);
			this._supporterList.Sort(this._supporterIncomeComparer);
			this._alleyList.Sort(this._alleyIncomeComparer);
			this.IsIncomeSelected = true;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00062754 File Offset: 0x00060954
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.LocationState = (int)state;
			this.IncomeState = (int)state;
			this.IsNameSelected = false;
			this.IsLocationSelected = false;
			this.IsIncomeSelected = false;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00062780 File Offset: 0x00060980
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00062789 File Offset: 0x00060989
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x00062791 File Offset: 0x00060991
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

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000627AF File Offset: 0x000609AF
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x000627B7 File Offset: 0x000609B7
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

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000627D5 File Offset: 0x000609D5
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x000627DD File Offset: 0x000609DD
		[DataSourceProperty]
		public int IncomeState
		{
			get
			{
				return this._incomeState;
			}
			set
			{
				if (value != this._incomeState)
				{
					this._incomeState = value;
					base.OnPropertyChangedWithValue(value, "IncomeState");
				}
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000627FB File Offset: 0x000609FB
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00062803 File Offset: 0x00060A03
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

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00062821 File Offset: 0x00060A21
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00062829 File Offset: 0x00060A29
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

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00062847 File Offset: 0x00060A47
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0006284F File Offset: 0x00060A4F
		[DataSourceProperty]
		public bool IsIncomeSelected
		{
			get
			{
				return this._isIncomeSelected;
			}
			set
			{
				if (value != this._isIncomeSelected)
				{
					this._isIncomeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsIncomeSelected");
				}
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0006286D File Offset: 0x00060A6D
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x00062875 File Offset: 0x00060A75
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

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00062898 File Offset: 0x00060A98
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x000628A0 File Offset: 0x00060AA0
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

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x000628C3 File Offset: 0x00060AC3
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x000628CB File Offset: 0x00060ACB
		[DataSourceProperty]
		public string IncomeText
		{
			get
			{
				return this._incomeText;
			}
			set
			{
				if (value != this._incomeText)
				{
					this._incomeText = value;
					base.OnPropertyChangedWithValue<string>(value, "IncomeText");
				}
			}
		}

		// Token: 0x04000CD6 RID: 3286
		private readonly MBBindingList<ClanFinanceWorkshopItemVM> _workshopList;

		// Token: 0x04000CD7 RID: 3287
		private readonly MBBindingList<ClanSupporterGroupVM> _supporterList;

		// Token: 0x04000CD8 RID: 3288
		private readonly MBBindingList<ClanFinanceAlleyItemVM> _alleyList;

		// Token: 0x04000CD9 RID: 3289
		private readonly ClanIncomeSortControllerVM.WorkshopItemNameComparer _workshopNameComparer;

		// Token: 0x04000CDA RID: 3290
		private readonly ClanIncomeSortControllerVM.SupporterItemNameComparer _supporterNameComparer;

		// Token: 0x04000CDB RID: 3291
		private readonly ClanIncomeSortControllerVM.AlleyItemNameComparer _alleyNameComparer;

		// Token: 0x04000CDC RID: 3292
		private readonly ClanIncomeSortControllerVM.WorkshopItemLocationComparer _workshopLocationComparer;

		// Token: 0x04000CDD RID: 3293
		private readonly ClanIncomeSortControllerVM.AlleyItemLocationComparer _alleyLocationComparer;

		// Token: 0x04000CDE RID: 3294
		private readonly ClanIncomeSortControllerVM.WorkshopItemIncomeComparer _workshopIncomeComparer;

		// Token: 0x04000CDF RID: 3295
		private readonly ClanIncomeSortControllerVM.SupporterItemIncomeComparer _supporterIncomeComparer;

		// Token: 0x04000CE0 RID: 3296
		private readonly ClanIncomeSortControllerVM.AlleyItemIncomeComparer _alleyIncomeComparer;

		// Token: 0x04000CE1 RID: 3297
		private int _nameState;

		// Token: 0x04000CE2 RID: 3298
		private int _locationState;

		// Token: 0x04000CE3 RID: 3299
		private int _incomeState;

		// Token: 0x04000CE4 RID: 3300
		private bool _isNameSelected;

		// Token: 0x04000CE5 RID: 3301
		private bool _isLocationSelected;

		// Token: 0x04000CE6 RID: 3302
		private bool _isIncomeSelected;

		// Token: 0x04000CE7 RID: 3303
		private string _nameText;

		// Token: 0x04000CE8 RID: 3304
		private string _locationText;

		// Token: 0x04000CE9 RID: 3305
		private string _incomeText;

		// Token: 0x02000267 RID: 615
		public abstract class WorkshopItemComparerBase : IComparer<ClanFinanceWorkshopItemVM>
		{
			// Token: 0x06002350 RID: 9040 RVA: 0x00076E48 File Offset: 0x00075048
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x06002351 RID: 9041
			public abstract int Compare(ClanFinanceWorkshopItemVM x, ClanFinanceWorkshopItemVM y);

			// Token: 0x040011BD RID: 4541
			protected bool _isAcending;
		}

		// Token: 0x02000268 RID: 616
		public abstract class SupporterItemComparerBase : IComparer<ClanSupporterGroupVM>
		{
			// Token: 0x06002353 RID: 9043 RVA: 0x00076E59 File Offset: 0x00075059
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x06002354 RID: 9044
			public abstract int Compare(ClanSupporterGroupVM x, ClanSupporterGroupVM y);

			// Token: 0x040011BE RID: 4542
			protected bool _isAcending;
		}

		// Token: 0x02000269 RID: 617
		public abstract class AlleyItemComparerBase : IComparer<ClanFinanceAlleyItemVM>
		{
			// Token: 0x06002356 RID: 9046 RVA: 0x00076E6A File Offset: 0x0007506A
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x06002357 RID: 9047
			public abstract int Compare(ClanFinanceAlleyItemVM x, ClanFinanceAlleyItemVM y);

			// Token: 0x040011BF RID: 4543
			protected bool _isAcending;
		}

		// Token: 0x0200026A RID: 618
		public class WorkshopItemNameComparer : ClanIncomeSortControllerVM.WorkshopItemComparerBase
		{
			// Token: 0x06002359 RID: 9049 RVA: 0x00076E7B File Offset: 0x0007507B
			public override int Compare(ClanFinanceWorkshopItemVM x, ClanFinanceWorkshopItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x0200026B RID: 619
		public class SupporterItemNameComparer : ClanIncomeSortControllerVM.SupporterItemComparerBase
		{
			// Token: 0x0600235B RID: 9051 RVA: 0x00076EB2 File Offset: 0x000750B2
			public override int Compare(ClanSupporterGroupVM x, ClanSupporterGroupVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x0200026C RID: 620
		public class AlleyItemNameComparer : ClanIncomeSortControllerVM.AlleyItemComparerBase
		{
			// Token: 0x0600235D RID: 9053 RVA: 0x00076EE9 File Offset: 0x000750E9
			public override int Compare(ClanFinanceAlleyItemVM x, ClanFinanceAlleyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x0200026D RID: 621
		public class WorkshopItemLocationComparer : ClanIncomeSortControllerVM.WorkshopItemComparerBase
		{
			// Token: 0x0600235F RID: 9055 RVA: 0x00076F20 File Offset: 0x00075120
			public override int Compare(ClanFinanceWorkshopItemVM x, ClanFinanceWorkshopItemVM y)
			{
				if (this._isAcending)
				{
					return y.Workshop.Settlement.GetTrackDistanceToMainAgent().CompareTo(x.Workshop.Settlement.GetTrackDistanceToMainAgent()) * -1;
				}
				return y.Workshop.Settlement.GetTrackDistanceToMainAgent().CompareTo(x.Workshop.Settlement.GetTrackDistanceToMainAgent());
			}
		}

		// Token: 0x0200026E RID: 622
		public class AlleyItemLocationComparer : ClanIncomeSortControllerVM.AlleyItemComparerBase
		{
			// Token: 0x06002361 RID: 9057 RVA: 0x00076F90 File Offset: 0x00075190
			public override int Compare(ClanFinanceAlleyItemVM x, ClanFinanceAlleyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Alley.Settlement.GetTrackDistanceToMainAgent().CompareTo(x.Alley.Settlement.GetTrackDistanceToMainAgent()) * -1;
				}
				return y.Alley.Settlement.GetTrackDistanceToMainAgent().CompareTo(x.Alley.Settlement.GetTrackDistanceToMainAgent());
			}
		}

		// Token: 0x0200026F RID: 623
		public class WorkshopItemIncomeComparer : ClanIncomeSortControllerVM.WorkshopItemComparerBase
		{
			// Token: 0x06002363 RID: 9059 RVA: 0x00077000 File Offset: 0x00075200
			public override int Compare(ClanFinanceWorkshopItemVM x, ClanFinanceWorkshopItemVM y)
			{
				if (this._isAcending)
				{
					return y.Workshop.ProfitMade.CompareTo(x.Workshop.ProfitMade) * -1;
				}
				return y.Workshop.ProfitMade.CompareTo(x.Workshop.ProfitMade);
			}
		}

		// Token: 0x02000270 RID: 624
		public class SupporterItemIncomeComparer : ClanIncomeSortControllerVM.SupporterItemComparerBase
		{
			// Token: 0x06002365 RID: 9061 RVA: 0x0007705C File Offset: 0x0007525C
			public override int Compare(ClanSupporterGroupVM x, ClanSupporterGroupVM y)
			{
				if (this._isAcending)
				{
					return y.TotalInfluenceBonus.CompareTo(x.TotalInfluenceBonus) * -1;
				}
				return y.TotalInfluenceBonus.CompareTo(x.TotalInfluenceBonus);
			}
		}

		// Token: 0x02000271 RID: 625
		public class AlleyItemIncomeComparer : ClanIncomeSortControllerVM.AlleyItemComparerBase
		{
			// Token: 0x06002367 RID: 9063 RVA: 0x000770A4 File Offset: 0x000752A4
			public override int Compare(ClanFinanceAlleyItemVM x, ClanFinanceAlleyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Income.CompareTo(x.Income) * -1;
				}
				return y.Income.CompareTo(x.Income);
			}
		}
	}
}
