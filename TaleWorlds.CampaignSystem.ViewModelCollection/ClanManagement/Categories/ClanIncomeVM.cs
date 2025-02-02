using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Supporters;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011C RID: 284
	public class ClanIncomeVM : ViewModel
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000628EE File Offset: 0x00060AEE
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x000628F6 File Offset: 0x00060AF6
		public int TotalIncome { get; private set; }

		// Token: 0x06001B49 RID: 6985 RVA: 0x00062900 File Offset: 0x00060B00
		public ClanIncomeVM(Action onRefresh, Action<ClanCardSelectionInfo> openCardSelectionPopup)
		{
			this._onRefresh = onRefresh;
			this._openCardSelectionPopup = openCardSelectionPopup;
			this.Incomes = new MBBindingList<ClanFinanceWorkshopItemVM>();
			this.SupporterGroups = new MBBindingList<ClanSupporterGroupVM>();
			this.Alleys = new MBBindingList<ClanFinanceAlleyItemVM>();
			this.SortController = new ClanIncomeSortControllerVM(this._incomes, this._supporterGroups, this._alleys);
			this.RefreshList();
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00062968 File Offset: 0x00060B68
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.IncomeText = GameTexts.FindText("str_income", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.NoAdditionalIncomesText = GameTexts.FindText("str_clan_no_additional_incomes", null).ToString();
			this.Incomes.ApplyActionOnAllItems(delegate(ClanFinanceWorkshopItemVM x)
			{
				x.RefreshValues();
			});
			ClanFinanceWorkshopItemVM currentSelectedIncome = this.CurrentSelectedIncome;
			if (currentSelectedIncome != null)
			{
				currentSelectedIncome.RefreshValues();
			}
			this.SortController.RefreshValues();
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00062A1C File Offset: 0x00060C1C
		public void RefreshList()
		{
			this.Incomes.Clear();
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.IsTown)
				{
					foreach (Workshop workshop in settlement.Town.Workshops)
					{
						if (workshop.Owner == Hero.MainHero)
						{
							this.Incomes.Add(new ClanFinanceWorkshopItemVM(workshop, new Action<ClanFinanceWorkshopItemVM>(this.OnIncomeSelection), new Action(this.OnRefresh), this._openCardSelectionPopup));
						}
					}
				}
			}
			this.RefreshSupporters();
			this.RefreshAlleys();
			this.SortController.ResetAllStates();
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_clan_workshops", null));
			GameTexts.SetVariable("LEFT", Hero.MainHero.OwnedWorkshops.Count);
			GameTexts.SetVariable("RIGHT", Campaign.Current.Models.WorkshopModel.GetMaxWorkshopCountForClanTier(Clan.PlayerClan.Tier));
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null));
			this.WorkshopText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			int num = 0;
			foreach (ClanSupporterGroupVM clanSupporterGroupVM in this.SupporterGroups)
			{
				num += clanSupporterGroupVM.Supporters.Count;
			}
			GameTexts.SetVariable("RANK", new TextObject("{=RzFyGnWJ}Supporters", null).ToString());
			GameTexts.SetVariable("NUMBER", num);
			this.SupportersText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			GameTexts.SetVariable("RANK", new TextObject("{=7tKjfMSb}Alleys", null).ToString());
			GameTexts.SetVariable("NUMBER", this.Alleys.Count);
			this.AlleysText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			this.RefreshTotalIncome();
			this.OnIncomeSelection(this.GetDefaultIncome());
			this.RefreshValues();
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00062C58 File Offset: 0x00060E58
		private void RefreshSupporters()
		{
			foreach (ClanSupporterGroupVM clanSupporterGroupVM in this.SupporterGroups)
			{
				clanSupporterGroupVM.Supporters.Clear();
			}
			this.SupporterGroups.Clear();
			Dictionary<float, List<Hero>> dictionary = new Dictionary<float, List<Hero>>();
			NotablePowerModel notablePowerModel = Campaign.Current.Models.NotablePowerModel;
			foreach (Hero hero in from x in Clan.PlayerClan.SupporterNotables
			orderby x.Power
			select x)
			{
				if (hero.CurrentSettlement != null)
				{
					float influenceBonusToClan = notablePowerModel.GetInfluenceBonusToClan(hero);
					List<Hero> list;
					if (dictionary.TryGetValue(influenceBonusToClan, out list))
					{
						list.Add(hero);
					}
					else
					{
						dictionary.Add(influenceBonusToClan, new List<Hero>
						{
							hero
						});
					}
				}
			}
			foreach (KeyValuePair<float, List<Hero>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value.Count > 0)
				{
					ClanSupporterGroupVM clanSupporterGroupVM2 = new ClanSupporterGroupVM(notablePowerModel.GetPowerRankName(keyValuePair.Value.FirstOrDefault<Hero>()), keyValuePair.Key, new Action<ClanSupporterGroupVM>(this.OnSupporterSelection));
					foreach (Hero hero2 in keyValuePair.Value)
					{
						clanSupporterGroupVM2.AddSupporter(hero2);
					}
					this.SupporterGroups.Add(clanSupporterGroupVM2);
				}
			}
			foreach (ClanSupporterGroupVM clanSupporterGroupVM3 in this.SupporterGroups)
			{
				clanSupporterGroupVM3.Refresh();
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00062E6C File Offset: 0x0006106C
		private void RefreshAlleys()
		{
			this.Alleys.Clear();
			foreach (Alley alley in Hero.MainHero.OwnedAlleys)
			{
				this.Alleys.Add(new ClanFinanceAlleyItemVM(alley, this._openCardSelectionPopup, new Action<ClanFinanceAlleyItemVM>(this.OnAlleySelection), new Action(this.OnRefresh)));
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00062EF8 File Offset: 0x000610F8
		private ClanFinanceWorkshopItemVM GetDefaultIncome()
		{
			return this.Incomes.FirstOrDefault<ClanFinanceWorkshopItemVM>();
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00062F08 File Offset: 0x00061108
		public void SelectWorkshop(Workshop workshop)
		{
			foreach (ClanFinanceWorkshopItemVM clanFinanceWorkshopItemVM in this.Incomes)
			{
				if (clanFinanceWorkshopItemVM != null)
				{
					ClanFinanceWorkshopItemVM clanFinanceWorkshopItemVM2 = clanFinanceWorkshopItemVM;
					if (clanFinanceWorkshopItemVM2.Workshop == workshop)
					{
						this.OnIncomeSelection(clanFinanceWorkshopItemVM2);
						break;
					}
				}
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00062F68 File Offset: 0x00061168
		public void SelectAlley(Alley alley)
		{
			for (int i = 0; i < this.Alleys.Count; i++)
			{
				if (this.Alleys[i].Alley == alley)
				{
					this.OnAlleySelection(this.Alleys[i]);
					return;
				}
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00062FB4 File Offset: 0x000611B4
		private void OnAlleySelection(ClanFinanceAlleyItemVM alley)
		{
			if (alley == null)
			{
				if (this.CurrentSelectedAlley != null)
				{
					this.CurrentSelectedAlley.IsSelected = false;
				}
				this.CurrentSelectedAlley = null;
				return;
			}
			this.OnIncomeSelection(null);
			this.OnSupporterSelection(null);
			if (this.CurrentSelectedAlley != null)
			{
				this.CurrentSelectedAlley.IsSelected = false;
			}
			this.CurrentSelectedAlley = alley;
			if (alley != null)
			{
				alley.IsSelected = true;
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00063014 File Offset: 0x00061214
		private void OnIncomeSelection(ClanFinanceWorkshopItemVM income)
		{
			if (income == null)
			{
				if (this.CurrentSelectedIncome != null)
				{
					this.CurrentSelectedIncome.IsSelected = false;
				}
				this.CurrentSelectedIncome = null;
				return;
			}
			this.OnSupporterSelection(null);
			this.OnAlleySelection(null);
			if (this.CurrentSelectedIncome != null)
			{
				this.CurrentSelectedIncome.IsSelected = false;
			}
			this.CurrentSelectedIncome = income;
			if (income != null)
			{
				income.IsSelected = true;
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00063074 File Offset: 0x00061274
		private void OnSupporterSelection(ClanSupporterGroupVM supporter)
		{
			if (supporter == null)
			{
				if (this.CurrentSelectedSupporterGroup != null)
				{
					this.CurrentSelectedSupporterGroup.IsSelected = false;
				}
				this.CurrentSelectedSupporterGroup = null;
				return;
			}
			this.OnIncomeSelection(null);
			this.OnAlleySelection(null);
			if (this.CurrentSelectedSupporterGroup != null)
			{
				this.CurrentSelectedSupporterGroup.IsSelected = false;
			}
			this.CurrentSelectedSupporterGroup = supporter;
			if (this.CurrentSelectedSupporterGroup != null)
			{
				this.CurrentSelectedSupporterGroup.IsSelected = true;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000630DD File Offset: 0x000612DD
		public void RefreshTotalIncome()
		{
			this.TotalIncome = this.Incomes.Sum((ClanFinanceWorkshopItemVM i) => i.Income);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006310F File Offset: 0x0006130F
		public void OnRefresh()
		{
			Action onRefresh = this._onRefresh;
			if (onRefresh == null)
			{
				return;
			}
			onRefresh();
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00063121 File Offset: 0x00061321
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x00063129 File Offset: 0x00061329
		[DataSourceProperty]
		public ClanFinanceAlleyItemVM CurrentSelectedAlley
		{
			get
			{
				return this._currentSelectedAlley;
			}
			set
			{
				if (value != this._currentSelectedAlley)
				{
					this._currentSelectedAlley = value;
					base.OnPropertyChangedWithValue<ClanFinanceAlleyItemVM>(value, "CurrentSelectedAlley");
					this.IsAnyValidAlleySelected = (value != null);
					this.IsAnyValidIncomeSelected = false;
					this.IsAnyValidSupporterSelected = false;
				}
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0006315F File Offset: 0x0006135F
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x00063167 File Offset: 0x00061367
		[DataSourceProperty]
		public ClanFinanceWorkshopItemVM CurrentSelectedIncome
		{
			get
			{
				return this._currentSelectedIncome;
			}
			set
			{
				if (value != this._currentSelectedIncome)
				{
					this._currentSelectedIncome = value;
					base.OnPropertyChangedWithValue<ClanFinanceWorkshopItemVM>(value, "CurrentSelectedIncome");
					this.IsAnyValidIncomeSelected = (value != null);
					this.IsAnyValidSupporterSelected = false;
					this.IsAnyValidAlleySelected = false;
				}
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0006319D File Offset: 0x0006139D
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x000631A5 File Offset: 0x000613A5
		[DataSourceProperty]
		public ClanSupporterGroupVM CurrentSelectedSupporterGroup
		{
			get
			{
				return this._currentSelectedSupporterGroup;
			}
			set
			{
				if (value != this._currentSelectedSupporterGroup)
				{
					this._currentSelectedSupporterGroup = value;
					base.OnPropertyChangedWithValue<ClanSupporterGroupVM>(value, "CurrentSelectedSupporterGroup");
					this.IsAnyValidSupporterSelected = (value != null);
					this.IsAnyValidIncomeSelected = false;
					this.IsAnyValidAlleySelected = false;
				}
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000631DB File Offset: 0x000613DB
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x000631E3 File Offset: 0x000613E3
		[DataSourceProperty]
		public bool IsAnyValidAlleySelected
		{
			get
			{
				return this._isAnyValidAlleySelected;
			}
			set
			{
				if (value != this._isAnyValidAlleySelected)
				{
					this._isAnyValidAlleySelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidAlleySelected");
				}
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00063201 File Offset: 0x00061401
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x00063209 File Offset: 0x00061409
		[DataSourceProperty]
		public bool IsAnyValidIncomeSelected
		{
			get
			{
				return this._isAnyValidIncomeSelected;
			}
			set
			{
				if (value != this._isAnyValidIncomeSelected)
				{
					this._isAnyValidIncomeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidIncomeSelected");
				}
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00063227 File Offset: 0x00061427
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0006322F File Offset: 0x0006142F
		[DataSourceProperty]
		public bool IsAnyValidSupporterSelected
		{
			get
			{
				return this._isAnyValidSupporterSelected;
			}
			set
			{
				if (value != this._isAnyValidSupporterSelected)
				{
					this._isAnyValidSupporterSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidSupporterSelected");
				}
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0006324D File Offset: 0x0006144D
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x00063255 File Offset: 0x00061455
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

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00063278 File Offset: 0x00061478
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x00063280 File Offset: 0x00061480
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0006329E File Offset: 0x0006149E
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x000632A6 File Offset: 0x000614A6
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

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000632C9 File Offset: 0x000614C9
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x000632D1 File Offset: 0x000614D1
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

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000632F4 File Offset: 0x000614F4
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x000632FC File Offset: 0x000614FC
		[DataSourceProperty]
		public string WorkshopText
		{
			get
			{
				return this._workshopsText;
			}
			set
			{
				if (value != this._workshopsText)
				{
					this._workshopsText = value;
					base.OnPropertyChangedWithValue<string>(value, "WorkshopText");
				}
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0006331F File Offset: 0x0006151F
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x00063327 File Offset: 0x00061527
		[DataSourceProperty]
		public string SupportersText
		{
			get
			{
				return this._supportersText;
			}
			set
			{
				if (value != this._supportersText)
				{
					this._supportersText = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportersText");
				}
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0006334A File Offset: 0x0006154A
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x00063352 File Offset: 0x00061552
		[DataSourceProperty]
		public string AlleysText
		{
			get
			{
				return this._alleysText;
			}
			set
			{
				if (value != this._alleysText)
				{
					this._alleysText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlleysText");
				}
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00063375 File Offset: 0x00061575
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0006337D File Offset: 0x0006157D
		[DataSourceProperty]
		public string NoAdditionalIncomesText
		{
			get
			{
				return this._noAdditionalIncomesText;
			}
			set
			{
				if (this._noAdditionalIncomesText != value)
				{
					this._noAdditionalIncomesText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoAdditionalIncomesText");
				}
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000633A0 File Offset: 0x000615A0
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x000633A8 File Offset: 0x000615A8
		[DataSourceProperty]
		public MBBindingList<ClanFinanceWorkshopItemVM> Incomes
		{
			get
			{
				return this._incomes;
			}
			set
			{
				if (value != this._incomes)
				{
					this._incomes = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanFinanceWorkshopItemVM>>(value, "Incomes");
				}
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000633C6 File Offset: 0x000615C6
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x000633CE File Offset: 0x000615CE
		[DataSourceProperty]
		public MBBindingList<ClanSupporterGroupVM> SupporterGroups
		{
			get
			{
				return this._supporterGroups;
			}
			set
			{
				if (value != this._supporterGroups)
				{
					this._supporterGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanSupporterGroupVM>>(value, "SupporterGroups");
				}
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000633EC File Offset: 0x000615EC
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x000633F4 File Offset: 0x000615F4
		[DataSourceProperty]
		public MBBindingList<ClanFinanceAlleyItemVM> Alleys
		{
			get
			{
				return this._alleys;
			}
			set
			{
				if (value != this._alleys)
				{
					this._alleys = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanFinanceAlleyItemVM>>(value, "Alleys");
				}
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00063412 File Offset: 0x00061612
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0006341A File Offset: 0x0006161A
		[DataSourceProperty]
		public ClanIncomeSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<ClanIncomeSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000CEA RID: 3306
		private readonly Action _onRefresh;

		// Token: 0x04000CEB RID: 3307
		private readonly Action<ClanCardSelectionInfo> _openCardSelectionPopup;

		// Token: 0x04000CED RID: 3309
		private MBBindingList<ClanFinanceWorkshopItemVM> _incomes;

		// Token: 0x04000CEE RID: 3310
		private MBBindingList<ClanSupporterGroupVM> _supporterGroups;

		// Token: 0x04000CEF RID: 3311
		private MBBindingList<ClanFinanceAlleyItemVM> _alleys;

		// Token: 0x04000CF0 RID: 3312
		private ClanFinanceAlleyItemVM _currentSelectedAlley;

		// Token: 0x04000CF1 RID: 3313
		private ClanFinanceWorkshopItemVM _currentSelectedIncome;

		// Token: 0x04000CF2 RID: 3314
		private ClanSupporterGroupVM _currentSelectedSupporterGroup;

		// Token: 0x04000CF3 RID: 3315
		private bool _isSelected;

		// Token: 0x04000CF4 RID: 3316
		private string _nameText;

		// Token: 0x04000CF5 RID: 3317
		private string _incomeText;

		// Token: 0x04000CF6 RID: 3318
		private string _locationText;

		// Token: 0x04000CF7 RID: 3319
		private string _workshopsText;

		// Token: 0x04000CF8 RID: 3320
		private string _supportersText;

		// Token: 0x04000CF9 RID: 3321
		private string _alleysText;

		// Token: 0x04000CFA RID: 3322
		private string _noAdditionalIncomesText;

		// Token: 0x04000CFB RID: 3323
		private bool _isAnyValidAlleySelected;

		// Token: 0x04000CFC RID: 3324
		private bool _isAnyValidIncomeSelected;

		// Token: 0x04000CFD RID: 3325
		private bool _isAnyValidSupporterSelected;

		// Token: 0x04000CFE RID: 3326
		private ClanIncomeSortControllerVM _sortController;
	}
}
