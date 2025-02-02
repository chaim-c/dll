using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Library.Information;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200010A RID: 266
	public class ClanManagementVM : ViewModel
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x0005AC68 File Offset: 0x00058E68
		public ClanManagementVM(Action onClose, Action<Hero> showHeroOnMap, Action<Hero> openPartyAsManage, Action openBannerEditor)
		{
			this._onClose = onClose;
			this._openPartyAsManage = openPartyAsManage;
			this._openBannerEditor = openBannerEditor;
			this._showHeroOnMap = showHeroOnMap;
			this._clan = Hero.MainHero.Clan;
			this.CardSelectionPopup = new ClanCardSelectionPopupVM();
			this.ClanMembers = new ClanMembersVM(new Action(this.RefreshCategoryValues), this._showHeroOnMap);
			this.ClanFiefs = new ClanFiefsVM(new Action(this.RefreshCategoryValues), new Action<ClanCardSelectionInfo>(this.CardSelectionPopup.Open));
			this.ClanParties = new ClanPartiesVM(new Action(this.OnAnyExpenseChange), this._openPartyAsManage, new Action(this.RefreshCategoryValues), new Action<ClanCardSelectionInfo>(this.CardSelectionPopup.Open));
			this.ClanIncome = new ClanIncomeVM(new Action(this.RefreshCategoryValues), new Action<ClanCardSelectionInfo>(this.CardSelectionPopup.Open));
			this._categoryCount = 4;
			this.SetSelectedCategory(0);
			this.Leader = new HeroVM(this._clan.Leader, false);
			this.CurrentRenown = (int)Clan.PlayerClan.Renown;
			this.CurrentTier = Clan.PlayerClan.Tier;
			TextObject textObject;
			if (Campaign.Current.Models.ClanTierModel.HasUpcomingTier(Clan.PlayerClan, out textObject, false).Item2)
			{
				this.NextTierRenown = Clan.PlayerClan.RenownRequirementForNextTier;
				this.MinRenownForCurrentTier = Campaign.Current.Models.ClanTierModel.GetRequiredRenownForTier(this.CurrentTier);
				this.NextTier = Clan.PlayerClan.Tier + 1;
				this.IsRenownProgressComplete = false;
			}
			else
			{
				this.NextTierRenown = 1;
				this.MinRenownForCurrentTier = 1;
				this.NextTier = 0;
				this.IsRenownProgressComplete = true;
			}
			this.CurrentRenownOverPreviousTier = this.CurrentRenown - this.MinRenownForCurrentTier;
			this.CurrentTierRenownRange = this.NextTierRenown - this.MinRenownForCurrentTier;
			this.RenownHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetClanRenownTooltip(Clan.PlayerClan));
			this.GoldChangeTooltip = CampaignUIHelper.GetDenarTooltip();
			this.RefreshDailyValues();
			this.CanChooseBanner = true;
			TextObject hintText;
			this.PlayerCanChangeClanName = this.GetPlayerCanChangeClanNameWithReason(out hintText);
			this.ChangeClanNameHint = new HintViewModel(hintText, null);
			this.TutorialNotification = new ElementNotificationVM();
			this.UpdateKingdomRelatedProperties();
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005AEE0 File Offset: 0x000590E0
		private bool GetPlayerCanChangeClanNameWithReason(out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (this._clan.Leader != Hero.MainHero)
			{
				disabledReason = new TextObject("{=GCaYjA5W}You need to be the leader of the clan to change it's name.", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005AF24 File Offset: 0x00059124
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = Hero.MainHero.Clan.Name.ToString();
			this.CurrentGoldText = GameTexts.FindText("str_clan_finance_current_gold", null).ToString();
			this.TotalExpensesText = GameTexts.FindText("str_clan_finance_total_expenses", null).ToString();
			this.TotalIncomeText = GameTexts.FindText("str_clan_finance_total_income", null).ToString();
			this.DailyChangeText = GameTexts.FindText("str_clan_finance_daily_change", null).ToString();
			this.ExpectedGoldText = GameTexts.FindText("str_clan_finance_expected", null).ToString();
			this.ExpenseText = GameTexts.FindText("str_clan_expenses", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.PartiesText = GameTexts.FindText("str_parties", null).ToString();
			this.IncomeText = GameTexts.FindText("str_other", null).ToString();
			this.FiefsText = GameTexts.FindText("str_fiefs", null).ToString();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.LeaderText = GameTexts.FindText("str_sort_by_leader_name_label", null).ToString();
			this.FinanceText = GameTexts.FindText("str_finance", null).ToString();
			GameTexts.SetVariable("TIER", Clan.PlayerClan.Tier);
			this.CurrentRenownText = GameTexts.FindText("str_clan_tier", null).ToString();
			ElementNotificationVM tutorialNotification = this.TutorialNotification;
			if (tutorialNotification != null)
			{
				tutorialNotification.RefreshValues();
			}
			ClanMembersVM clanMembers = this._clanMembers;
			if (clanMembers != null)
			{
				clanMembers.RefreshValues();
			}
			ClanPartiesVM clanParties = this._clanParties;
			if (clanParties != null)
			{
				clanParties.RefreshValues();
			}
			ClanFiefsVM clanFiefs = this._clanFiefs;
			if (clanFiefs != null)
			{
				clanFiefs.RefreshValues();
			}
			ClanIncomeVM clanIncome = this._clanIncome;
			if (clanIncome != null)
			{
				clanIncome.RefreshValues();
			}
			HeroVM leader = this._leader;
			if (leader == null)
			{
				return;
			}
			leader.RefreshValues();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005B0FE File Offset: 0x000592FE
		public void SelectHero(Hero hero)
		{
			this.SetSelectedCategory(0);
			this.ClanMembers.SelectMember(hero);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005B113 File Offset: 0x00059313
		public void SelectParty(PartyBase party)
		{
			this.SetSelectedCategory(1);
			this.ClanParties.SelectParty(party);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005B128 File Offset: 0x00059328
		public void SelectSettlement(Settlement settlement)
		{
			this.SetSelectedCategory(2);
			this.ClanFiefs.SelectFief(settlement);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005B13D File Offset: 0x0005933D
		public void SelectWorkshop(Workshop workshop)
		{
			this.SetSelectedCategory(3);
			this.ClanIncome.SelectWorkshop(workshop);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005B152 File Offset: 0x00059352
		public void SelectAlley(Alley alley)
		{
			this.SetSelectedCategory(3);
			this.ClanIncome.SelectAlley(alley);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005B168 File Offset: 0x00059368
		public void SelectPreviousCategory()
		{
			int selectedCategory = (this._currentCategory == 0) ? (this._categoryCount - 1) : (this._currentCategory - 1);
			this.SetSelectedCategory(selectedCategory);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005B198 File Offset: 0x00059398
		public void SelectNextCategory()
		{
			int selectedCategory = (this._currentCategory + 1) % this._categoryCount;
			this.SetSelectedCategory(selectedCategory);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005B1BC File Offset: 0x000593BC
		public void ExecuteOpenBannerEditor()
		{
			this._openBannerEditor();
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005B1C9 File Offset: 0x000593C9
		public void UpdateBannerVisuals()
		{
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this._clan.Banner), true);
			this.ClanBannerHint = new HintViewModel(new TextObject("{=t1lSXN9O}Your clan's standard carried into battle", null), null);
			this.RefreshValues();
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0005B204 File Offset: 0x00059404
		public void SetSelectedCategory(int index)
		{
			this.ClanMembers.IsSelected = false;
			this.ClanParties.IsSelected = false;
			this.ClanFiefs.IsSelected = false;
			this.ClanIncome.IsSelected = false;
			this._currentCategory = index;
			if (index == 0)
			{
				this.ClanMembers.IsSelected = true;
			}
			else if (index == 1)
			{
				this.ClanParties.IsSelected = true;
			}
			else if (index == 2)
			{
				this.ClanFiefs.IsSelected = true;
			}
			else
			{
				this._currentCategory = 3;
				this.ClanIncome.IsSelected = true;
			}
			this.IsMembersSelected = this.ClanMembers.IsSelected;
			this.IsPartiesSelected = this.ClanParties.IsSelected;
			this.IsFiefsSelected = this.ClanFiefs.IsSelected;
			this.IsIncomeSelected = this.ClanIncome.IsSelected;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005B2D4 File Offset: 0x000594D4
		private void UpdateKingdomRelatedProperties()
		{
			this.ClanIsInAKingdom = (this._clan.Kingdom != null);
			if (this.ClanIsInAKingdom)
			{
				if (this._clan.Kingdom.RulingClan == this._clan)
				{
					this.IsKingdomActionEnabled = false;
					this.KingdomActionDisabledReasonHint = new BasicTooltipViewModel(() => new TextObject("{=vIPrZCZ1}You can abdicate leadership from the kingdom screen.", null).ToString());
					this.KingdomActionText = GameTexts.FindText("str_abdicate_leadership", null).ToString();
				}
				else
				{
					this.IsKingdomActionEnabled = (MobileParty.MainParty.Army == null);
					this.KingdomActionText = GameTexts.FindText("str_leave_kingdom", null).ToString();
					this.KingdomActionDisabledReasonHint = new BasicTooltipViewModel();
				}
			}
			else
			{
				List<TextObject> kingdomCreationDisabledReasons;
				this.IsKingdomActionEnabled = Campaign.Current.Models.KingdomCreationModel.IsPlayerKingdomCreationPossible(out kingdomCreationDisabledReasons);
				this.KingdomActionText = GameTexts.FindText("str_create_kingdom", null).ToString();
				this.KingdomActionDisabledReasonHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetHintTextFromReasons(kingdomCreationDisabledReasons));
			}
			this.UpdateBannerVisuals();
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005B3F8 File Offset: 0x000595F8
		public void RefreshDailyValues()
		{
			if (this.ClanIncome != null)
			{
				this.CurrentGold = Hero.MainHero.Gold;
				this.TotalIncome = (int)Campaign.Current.Models.ClanFinanceModel.CalculateClanIncome(this._clan, false, false, false).ResultNumber;
				this.TotalExpenses = (int)Campaign.Current.Models.ClanFinanceModel.CalculateClanExpenses(this._clan, false, false, false).ResultNumber;
				this.DailyChange = MathF.Abs(this.TotalIncome) - MathF.Abs(this.TotalExpenses);
				this.ExpectedGold = this.CurrentGold + this.DailyChange;
				if (this.TotalIncome == 0)
				{
					this.TotalIncomeValueText = GameTexts.FindText("str_clan_finance_value_zero", null).ToString();
				}
				else
				{
					GameTexts.SetVariable("IS_POSITIVE", (this.TotalIncome > 0) ? 1 : 0);
					GameTexts.SetVariable("NUMBER", MathF.Abs(this.TotalIncome));
					this.TotalIncomeValueText = GameTexts.FindText("str_clan_finance_value", null).ToString();
				}
				if (this.TotalExpenses == 0)
				{
					this.TotalExpensesValueText = GameTexts.FindText("str_clan_finance_value_zero", null).ToString();
				}
				else
				{
					GameTexts.SetVariable("IS_POSITIVE", (this.TotalExpenses > 0) ? 1 : 0);
					GameTexts.SetVariable("NUMBER", MathF.Abs(this.TotalExpenses));
					this.TotalExpensesValueText = GameTexts.FindText("str_clan_finance_value", null).ToString();
				}
				if (this.DailyChange == 0)
				{
					this.DailyChangeValueText = GameTexts.FindText("str_clan_finance_value_zero", null).ToString();
					return;
				}
				GameTexts.SetVariable("IS_POSITIVE", (this.DailyChange > 0) ? 1 : 0);
				GameTexts.SetVariable("NUMBER", MathF.Abs(this.DailyChange));
				this.DailyChangeValueText = GameTexts.FindText("str_clan_finance_value", null).ToString();
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005B5CD File Offset: 0x000597CD
		public void RefreshCategoryValues()
		{
			this.ClanFiefs.RefreshAllLists();
			this.ClanMembers.RefreshMembersList();
			this.ClanParties.RefreshPartiesList();
			this.ClanIncome.RefreshList();
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005B5FC File Offset: 0x000597FC
		public void ExecuteChangeClanName()
		{
			GameTexts.SetVariable("MAX_LETTER_COUNT", 50);
			GameTexts.SetVariable("MIN_LETTER_COUNT", 1);
			InformationManager.ShowTextInquiry(new TextInquiryData(GameTexts.FindText("str_change_clan_name", null).ToString(), string.Empty, true, true, GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action<string>(this.OnChangeClanNameDone), null, false, new Func<string, Tuple<bool, string>>(FactionHelper.IsClanNameApplicable), "", ""), false, false);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0005B688 File Offset: 0x00059888
		private void OnChangeClanNameDone(string newClanName)
		{
			TextObject textObject = GameTexts.FindText("str_generic_clan_name", null);
			textObject.SetTextVariable("CLAN_NAME", new TextObject(newClanName, null));
			this._clan.InitializeClan(textObject, textObject, this._clan.Culture, this._clan.Banner, default(Vec2), false);
			this.RefreshCategoryValues();
			this.RefreshValues();
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0005B6ED File Offset: 0x000598ED
		private void OnAnyExpenseChange()
		{
			this.RefreshDailyValues();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0005B6F5 File Offset: 0x000598F5
		public void ExecuteClose()
		{
			this._onClose();
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0005B704 File Offset: 0x00059904
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.ClanFiefs.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.PreviousTabInputKey.OnFinalize();
			this.NextTabInputKey.OnFinalize();
			this.CardSelectionPopup.OnFinalize();
			this.ClanMembers.OnFinalize();
			this.ClanParties.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005B77F File Offset: 0x0005997F
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0005B787 File Offset: 0x00059987
		[DataSourceProperty]
		public HeroVM Leader
		{
			get
			{
				return this._leader;
			}
			set
			{
				if (value != this._leader)
				{
					this._leader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Leader");
				}
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0005B7A5 File Offset: 0x000599A5
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0005B7AD File Offset: 0x000599AD
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0005B7CB File Offset: 0x000599CB
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0005B7D3 File Offset: 0x000599D3
		[DataSourceProperty]
		public ClanCardSelectionPopupVM CardSelectionPopup
		{
			get
			{
				return this._cardSelectionPopup;
			}
			set
			{
				if (value != this._cardSelectionPopup)
				{
					this._cardSelectionPopup = value;
					base.OnPropertyChangedWithValue<ClanCardSelectionPopupVM>(value, "CardSelectionPopup");
				}
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005B7F1 File Offset: 0x000599F1
		// (set) Token: 0x060018F5 RID: 6389 RVA: 0x0005B7F9 File Offset: 0x000599F9
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0005B81C File Offset: 0x00059A1C
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x0005B824 File Offset: 0x00059A24
		[DataSourceProperty]
		public string LeaderText
		{
			get
			{
				return this._leaderText;
			}
			set
			{
				if (value != this._leaderText)
				{
					this._leaderText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaderText");
				}
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005B847 File Offset: 0x00059A47
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0005B84F File Offset: 0x00059A4F
		[DataSourceProperty]
		public ClanMembersVM ClanMembers
		{
			get
			{
				return this._clanMembers;
			}
			set
			{
				if (value != this._clanMembers)
				{
					this._clanMembers = value;
					base.OnPropertyChangedWithValue<ClanMembersVM>(value, "ClanMembers");
				}
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005B86D File Offset: 0x00059A6D
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0005B875 File Offset: 0x00059A75
		[DataSourceProperty]
		public ClanPartiesVM ClanParties
		{
			get
			{
				return this._clanParties;
			}
			set
			{
				if (value != this._clanParties)
				{
					this._clanParties = value;
					base.OnPropertyChangedWithValue<ClanPartiesVM>(value, "ClanParties");
				}
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0005B893 File Offset: 0x00059A93
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x0005B89B File Offset: 0x00059A9B
		[DataSourceProperty]
		public ClanFiefsVM ClanFiefs
		{
			get
			{
				return this._clanFiefs;
			}
			set
			{
				if (value != this._clanFiefs)
				{
					this._clanFiefs = value;
					base.OnPropertyChangedWithValue<ClanFiefsVM>(value, "ClanFiefs");
				}
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0005B8B9 File Offset: 0x00059AB9
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x0005B8C1 File Offset: 0x00059AC1
		[DataSourceProperty]
		public ClanIncomeVM ClanIncome
		{
			get
			{
				return this._clanIncome;
			}
			set
			{
				if (value != this._clanIncome)
				{
					this._clanIncome = value;
					base.OnPropertyChangedWithValue<ClanIncomeVM>(value, "ClanIncome");
				}
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005B8DF File Offset: 0x00059ADF
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0005B8E7 File Offset: 0x00059AE7
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

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005B905 File Offset: 0x00059B05
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0005B90D File Offset: 0x00059B0D
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

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005B92B File Offset: 0x00059B2B
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0005B933 File Offset: 0x00059B33
		[DataSourceProperty]
		public bool CanSwitchTabs
		{
			get
			{
				return this._canSwitchTabs;
			}
			set
			{
				if (value != this._canSwitchTabs)
				{
					this._canSwitchTabs = value;
					base.OnPropertyChangedWithValue(value, "CanSwitchTabs");
				}
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005B951 File Offset: 0x00059B51
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0005B959 File Offset: 0x00059B59
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

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0005B977 File Offset: 0x00059B77
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x0005B97F File Offset: 0x00059B7F
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

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0005B99D File Offset: 0x00059B9D
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0005B9A5 File Offset: 0x00059BA5
		[DataSourceProperty]
		public bool ClanIsInAKingdom
		{
			get
			{
				return this._clanIsInAKingdom;
			}
			set
			{
				if (value != this._clanIsInAKingdom)
				{
					this._clanIsInAKingdom = value;
					base.OnPropertyChangedWithValue(value, "ClanIsInAKingdom");
				}
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0005B9C3 File Offset: 0x00059BC3
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x0005B9CB File Offset: 0x00059BCB
		[DataSourceProperty]
		public bool IsKingdomActionEnabled
		{
			get
			{
				return this._isKingdomActionEnabled;
			}
			set
			{
				if (value != this._isKingdomActionEnabled)
				{
					this._isKingdomActionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsKingdomActionEnabled");
				}
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0005B9E9 File Offset: 0x00059BE9
		// (set) Token: 0x0600190F RID: 6415 RVA: 0x0005B9F1 File Offset: 0x00059BF1
		[DataSourceProperty]
		public bool PlayerCanChangeClanName
		{
			get
			{
				return this._playerCanChangeClanName;
			}
			set
			{
				if (value != this._playerCanChangeClanName)
				{
					this._playerCanChangeClanName = value;
					base.OnPropertyChangedWithValue(value, "PlayerCanChangeClanName");
				}
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0005BA0F File Offset: 0x00059C0F
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x0005BA17 File Offset: 0x00059C17
		[DataSourceProperty]
		public bool CanChooseBanner
		{
			get
			{
				return this._canChooseBanner;
			}
			set
			{
				if (value != this._canChooseBanner)
				{
					this._canChooseBanner = value;
					base.OnPropertyChangedWithValue(value, "CanChooseBanner");
				}
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0005BA35 File Offset: 0x00059C35
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0005BA3D File Offset: 0x00059C3D
		[DataSourceProperty]
		public bool IsRenownProgressComplete
		{
			get
			{
				return this._isRenownProgressComplete;
			}
			set
			{
				if (value != this._isRenownProgressComplete)
				{
					this._isRenownProgressComplete = value;
					base.OnPropertyChangedWithValue(value, "IsRenownProgressComplete");
				}
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0005BA5B File Offset: 0x00059C5B
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x0005BA63 File Offset: 0x00059C63
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0005BA86 File Offset: 0x00059C86
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0005BA8E File Offset: 0x00059C8E
		[DataSourceProperty]
		public string CurrentRenownText
		{
			get
			{
				return this._currentRenownText;
			}
			set
			{
				if (value != this._currentRenownText)
				{
					this._currentRenownText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentRenownText");
				}
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0005BAB1 File Offset: 0x00059CB1
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0005BAB9 File Offset: 0x00059CB9
		[DataSourceProperty]
		public string KingdomActionText
		{
			get
			{
				return this._kingdomActionText;
			}
			set
			{
				if (value != this._kingdomActionText)
				{
					this._kingdomActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "KingdomActionText");
				}
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0005BADC File Offset: 0x00059CDC
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x0005BAE4 File Offset: 0x00059CE4
		[DataSourceProperty]
		public int NextTierRenown
		{
			get
			{
				return this._nextTierRenown;
			}
			set
			{
				if (value != this._nextTierRenown)
				{
					this._nextTierRenown = value;
					base.OnPropertyChangedWithValue(value, "NextTierRenown");
				}
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0005BB02 File Offset: 0x00059D02
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x0005BB0A File Offset: 0x00059D0A
		[DataSourceProperty]
		public int CurrentTier
		{
			get
			{
				return this._currentTier;
			}
			set
			{
				if (value != this._currentTier)
				{
					this._currentTier = value;
					base.OnPropertyChangedWithValue(value, "CurrentTier");
				}
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0005BB28 File Offset: 0x00059D28
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0005BB30 File Offset: 0x00059D30
		[DataSourceProperty]
		public int MinRenownForCurrentTier
		{
			get
			{
				return this._minRenownForCurrentTier;
			}
			set
			{
				if (value != this._minRenownForCurrentTier)
				{
					this._minRenownForCurrentTier = value;
					base.OnPropertyChangedWithValue(value, "MinRenownForCurrentTier");
				}
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0005BB4E File Offset: 0x00059D4E
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0005BB56 File Offset: 0x00059D56
		[DataSourceProperty]
		public int NextTier
		{
			get
			{
				return this._nextTier;
			}
			set
			{
				if (value != this._nextTier)
				{
					this._nextTier = value;
					base.OnPropertyChangedWithValue(value, "NextTier");
				}
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0005BB74 File Offset: 0x00059D74
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0005BB7C File Offset: 0x00059D7C
		[DataSourceProperty]
		public int CurrentRenown
		{
			get
			{
				return this._currentRenown;
			}
			set
			{
				if (value != this._currentRenown)
				{
					this._currentRenown = value;
					base.OnPropertyChangedWithValue(value, "CurrentRenown");
				}
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0005BB9A File Offset: 0x00059D9A
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x0005BBA2 File Offset: 0x00059DA2
		[DataSourceProperty]
		public int CurrentTierRenownRange
		{
			get
			{
				return this._currentTierRenownRange;
			}
			set
			{
				if (value != this._currentTierRenownRange)
				{
					this._currentTierRenownRange = value;
					base.OnPropertyChangedWithValue(value, "CurrentTierRenownRange");
				}
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x0005BBC8 File Offset: 0x00059DC8
		[DataSourceProperty]
		public int CurrentRenownOverPreviousTier
		{
			get
			{
				return this._currentRenownOverPreviousTier;
			}
			set
			{
				if (value != this._currentRenownOverPreviousTier)
				{
					this._currentRenownOverPreviousTier = value;
					base.OnPropertyChangedWithValue(value, "CurrentRenownOverPreviousTier");
				}
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0005BBE6 File Offset: 0x00059DE6
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x0005BBEE File Offset: 0x00059DEE
		[DataSourceProperty]
		public string MembersText
		{
			get
			{
				return this._membersText;
			}
			set
			{
				if (value != this._membersText)
				{
					this._membersText = value;
					base.OnPropertyChangedWithValue<string>(value, "MembersText");
				}
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0005BC11 File Offset: 0x00059E11
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0005BC19 File Offset: 0x00059E19
		[DataSourceProperty]
		public string PartiesText
		{
			get
			{
				return this._partiesText;
			}
			set
			{
				if (value != this._partiesText)
				{
					this._partiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartiesText");
				}
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0005BC3C File Offset: 0x00059E3C
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x0005BC44 File Offset: 0x00059E44
		[DataSourceProperty]
		public string FiefsText
		{
			get
			{
				return this._fiefsText;
			}
			set
			{
				if (value != this._fiefsText)
				{
					this._fiefsText = value;
					base.OnPropertyChangedWithValue<string>(value, "FiefsText");
				}
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0005BC67 File Offset: 0x00059E67
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x0005BC6F File Offset: 0x00059E6F
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
					base.OnPropertyChanged("OtherText");
				}
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0005BC91 File Offset: 0x00059E91
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x0005BC99 File Offset: 0x00059E99
		[DataSourceProperty]
		public BasicTooltipViewModel RenownHint
		{
			get
			{
				return this._renownHint;
			}
			set
			{
				if (value != this._renownHint)
				{
					this._renownHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RenownHint");
				}
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0005BCB7 File Offset: 0x00059EB7
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0005BCBF File Offset: 0x00059EBF
		[DataSourceProperty]
		public HintViewModel ClanBannerHint
		{
			get
			{
				return this._clanBannerHint;
			}
			set
			{
				if (value != this._clanBannerHint)
				{
					this._clanBannerHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ClanBannerHint");
				}
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0005BCDD File Offset: 0x00059EDD
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x0005BCE5 File Offset: 0x00059EE5
		[DataSourceProperty]
		public HintViewModel ChangeClanNameHint
		{
			get
			{
				return this._changeClanNameHint;
			}
			set
			{
				if (value != this._changeClanNameHint)
				{
					this._changeClanNameHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ChangeClanNameHint");
				}
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x0005BD03 File Offset: 0x00059F03
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x0005BD0B File Offset: 0x00059F0B
		[DataSourceProperty]
		public BasicTooltipViewModel KingdomActionDisabledReasonHint
		{
			get
			{
				return this._kingdomActionDisabledReasonHint;
			}
			set
			{
				if (value != this._kingdomActionDisabledReasonHint)
				{
					this._kingdomActionDisabledReasonHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "KingdomActionDisabledReasonHint");
				}
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0005BD29 File Offset: 0x00059F29
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0005BD31 File Offset: 0x00059F31
		[DataSourceProperty]
		public TooltipTriggerVM GoldChangeTooltip
		{
			get
			{
				return this._goldChangeTooltip;
			}
			set
			{
				if (value != this._goldChangeTooltip)
				{
					this._goldChangeTooltip = value;
					base.OnPropertyChangedWithValue<TooltipTriggerVM>(value, "GoldChangeTooltip");
				}
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0005BD4F File Offset: 0x00059F4F
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x0005BD57 File Offset: 0x00059F57
		[DataSourceProperty]
		public string CurrentGoldText
		{
			get
			{
				return this._currentGoldText;
			}
			set
			{
				if (value != this._currentGoldText)
				{
					this._currentGoldText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentGoldText");
				}
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0005BD7A File Offset: 0x00059F7A
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0005BD82 File Offset: 0x00059F82
		[DataSourceProperty]
		public int CurrentGold
		{
			get
			{
				return this._currentGold;
			}
			set
			{
				if (value != this._currentGold)
				{
					this._currentGold = value;
					base.OnPropertyChangedWithValue(value, "CurrentGold");
				}
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0005BDA0 File Offset: 0x00059FA0
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		[DataSourceProperty]
		public string ExpenseText
		{
			get
			{
				return this._expenseText;
			}
			set
			{
				if (value != this._expenseText)
				{
					this._expenseText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExpenseText");
				}
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0005BDCB File Offset: 0x00059FCB
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x0005BDD3 File Offset: 0x00059FD3
		[DataSourceProperty]
		public string TotalIncomeText
		{
			get
			{
				return this._totalIncomeText;
			}
			set
			{
				if (value != this._totalIncomeText)
				{
					this._totalIncomeText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalIncomeText");
				}
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0005BDF6 File Offset: 0x00059FF6
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x0005BDFE File Offset: 0x00059FFE
		[DataSourceProperty]
		public string FinanceText
		{
			get
			{
				return this._financeText;
			}
			set
			{
				if (value != this._financeText)
				{
					this._financeText = value;
					base.OnPropertyChangedWithValue<string>(value, "FinanceText");
				}
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x0005BE21 File Offset: 0x0005A021
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x0005BE29 File Offset: 0x0005A029
		[DataSourceProperty]
		public int TotalIncome
		{
			get
			{
				return this._totalIncome;
			}
			set
			{
				if (value != this._totalIncome)
				{
					this._totalIncome = value;
					base.OnPropertyChangedWithValue(value, "TotalIncome");
				}
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x0005BE47 File Offset: 0x0005A047
		// (set) Token: 0x06001947 RID: 6471 RVA: 0x0005BE4F File Offset: 0x0005A04F
		[DataSourceProperty]
		public string TotalExpensesText
		{
			get
			{
				return this._totalExpensesText;
			}
			set
			{
				if (value != this._totalExpensesText)
				{
					this._totalExpensesText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalExpensesText");
				}
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0005BE72 File Offset: 0x0005A072
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x0005BE7A File Offset: 0x0005A07A
		[DataSourceProperty]
		public int TotalExpenses
		{
			get
			{
				return this._totalExpenses;
			}
			set
			{
				if (value != this._totalExpenses)
				{
					this._totalExpenses = value;
					base.OnPropertyChangedWithValue(value, "TotalExpenses");
				}
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0005BE98 File Offset: 0x0005A098
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x0005BEA0 File Offset: 0x0005A0A0
		[DataSourceProperty]
		public string DailyChangeText
		{
			get
			{
				return this._dailyChangeText;
			}
			set
			{
				if (value != this._dailyChangeText)
				{
					this._dailyChangeText = value;
					base.OnPropertyChangedWithValue<string>(value, "DailyChangeText");
				}
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0005BEC3 File Offset: 0x0005A0C3
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x0005BECB File Offset: 0x0005A0CB
		[DataSourceProperty]
		public int DailyChange
		{
			get
			{
				return this._dailyChange;
			}
			set
			{
				if (value != this._dailyChange)
				{
					this._dailyChange = value;
					base.OnPropertyChangedWithValue(value, "DailyChange");
				}
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0005BEE9 File Offset: 0x0005A0E9
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x0005BEF1 File Offset: 0x0005A0F1
		[DataSourceProperty]
		public string ExpectedGoldText
		{
			get
			{
				return this._expectedGoldText;
			}
			set
			{
				if (value != this._expectedGoldText)
				{
					this._expectedGoldText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExpectedGoldText");
				}
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x0005BF14 File Offset: 0x0005A114
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x0005BF1C File Offset: 0x0005A11C
		[DataSourceProperty]
		public int ExpectedGold
		{
			get
			{
				return this._expectedGold;
			}
			set
			{
				if (value != this._expectedGold)
				{
					this._expectedGold = value;
					base.OnPropertyChangedWithValue(value, "ExpectedGold");
				}
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x0005BF3A File Offset: 0x0005A13A
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x0005BF42 File Offset: 0x0005A142
		[DataSourceProperty]
		public string DailyChangeValueText
		{
			get
			{
				return this._dailyChangeValueText;
			}
			set
			{
				if (value != this._dailyChangeValueText)
				{
					this._dailyChangeValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "DailyChangeValueText");
				}
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x0005BF65 File Offset: 0x0005A165
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x0005BF6D File Offset: 0x0005A16D
		[DataSourceProperty]
		public string TotalExpensesValueText
		{
			get
			{
				return this._totalExpensesValueText;
			}
			set
			{
				if (value != this._totalExpensesValueText)
				{
					this._totalExpensesValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalExpensesValueText");
				}
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x0005BF90 File Offset: 0x0005A190
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x0005BF98 File Offset: 0x0005A198
		[DataSourceProperty]
		public string TotalIncomeValueText
		{
			get
			{
				return this._totalIncomeValueText;
			}
			set
			{
				if (value != this._totalIncomeValueText)
				{
					this._totalIncomeValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalIncomeValueText");
				}
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0005BFBB File Offset: 0x0005A1BB
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
			this.CardSelectionPopup.SetDoneInputKey(hotkey);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005BFD6 File Offset: 0x0005A1D6
		public void SetPreviousTabInputKey(HotKey hotkey)
		{
			this.PreviousTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005BFE5 File Offset: 0x0005A1E5
		public void SetNextTabInputKey(HotKey hotkey)
		{
			this.NextTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0005BFF4 File Offset: 0x0005A1F4
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x0005BFFC File Offset: 0x0005A1FC
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0005C01A File Offset: 0x0005A21A
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0005C022 File Offset: 0x0005A222
		public InputKeyItemVM PreviousTabInputKey
		{
			get
			{
				return this._previousTabInputKey;
			}
			set
			{
				if (value != this._previousTabInputKey)
				{
					this._previousTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousTabInputKey");
				}
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0005C040 File Offset: 0x0005A240
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0005C048 File Offset: 0x0005A248
		public InputKeyItemVM NextTabInputKey
		{
			get
			{
				return this._nextTabInputKey;
			}
			set
			{
				if (value != this._nextTabInputKey)
				{
					this._nextTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextTabInputKey");
				}
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0005C066 File Offset: 0x0005A266
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x0005C06E File Offset: 0x0005A26E
		[DataSourceProperty]
		public ElementNotificationVM TutorialNotification
		{
			get
			{
				return this._tutorialNotification;
			}
			set
			{
				if (value != this._tutorialNotification)
				{
					this._tutorialNotification = value;
					base.OnPropertyChangedWithValue<ElementNotificationVM>(value, "TutorialNotification");
				}
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005C08C File Offset: 0x0005A28C
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = string.Empty;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = this._latestTutorialElementID;
					if (this._latestTutorialElementID == "RoleAssignmentWidget")
					{
						this.SetSelectedCategory(1);
					}
				}
			}
		}

		// Token: 0x04000BC1 RID: 3009
		private readonly Action _onClose;

		// Token: 0x04000BC2 RID: 3010
		private readonly Action _openBannerEditor;

		// Token: 0x04000BC3 RID: 3011
		private readonly Action<Hero> _openPartyAsManage;

		// Token: 0x04000BC4 RID: 3012
		private readonly Action<Hero> _showHeroOnMap;

		// Token: 0x04000BC5 RID: 3013
		private readonly Clan _clan;

		// Token: 0x04000BC6 RID: 3014
		private readonly int _categoryCount;

		// Token: 0x04000BC7 RID: 3015
		private int _currentCategory;

		// Token: 0x04000BC8 RID: 3016
		private ClanMembersVM _clanMembers;

		// Token: 0x04000BC9 RID: 3017
		private ClanPartiesVM _clanParties;

		// Token: 0x04000BCA RID: 3018
		private ClanFiefsVM _clanFiefs;

		// Token: 0x04000BCB RID: 3019
		private ClanIncomeVM _clanIncome;

		// Token: 0x04000BCC RID: 3020
		private HeroVM _leader;

		// Token: 0x04000BCD RID: 3021
		private ImageIdentifierVM _clanBanner;

		// Token: 0x04000BCE RID: 3022
		private ClanCardSelectionPopupVM _cardSelectionPopup;

		// Token: 0x04000BCF RID: 3023
		private bool _canSwitchTabs;

		// Token: 0x04000BD0 RID: 3024
		private bool _isPartiesSelected;

		// Token: 0x04000BD1 RID: 3025
		private bool _isMembersSelected;

		// Token: 0x04000BD2 RID: 3026
		private bool _isFiefsSelected;

		// Token: 0x04000BD3 RID: 3027
		private bool _isIncomeSelected;

		// Token: 0x04000BD4 RID: 3028
		private bool _canChooseBanner;

		// Token: 0x04000BD5 RID: 3029
		private bool _isRenownProgressComplete;

		// Token: 0x04000BD6 RID: 3030
		private bool _playerCanChangeClanName;

		// Token: 0x04000BD7 RID: 3031
		private bool _clanIsInAKingdom;

		// Token: 0x04000BD8 RID: 3032
		private string _doneLbl;

		// Token: 0x04000BD9 RID: 3033
		private bool _isKingdomActionEnabled;

		// Token: 0x04000BDA RID: 3034
		private string _name;

		// Token: 0x04000BDB RID: 3035
		private string _kingdomActionText;

		// Token: 0x04000BDC RID: 3036
		private string _leaderText;

		// Token: 0x04000BDD RID: 3037
		private int _minRenownForCurrentTier;

		// Token: 0x04000BDE RID: 3038
		private int _currentRenown;

		// Token: 0x04000BDF RID: 3039
		private int _currentTier = -1;

		// Token: 0x04000BE0 RID: 3040
		private int _nextTierRenown;

		// Token: 0x04000BE1 RID: 3041
		private int _nextTier;

		// Token: 0x04000BE2 RID: 3042
		private int _currentTierRenownRange;

		// Token: 0x04000BE3 RID: 3043
		private int _currentRenownOverPreviousTier;

		// Token: 0x04000BE4 RID: 3044
		private string _currentRenownText;

		// Token: 0x04000BE5 RID: 3045
		private string _membersText;

		// Token: 0x04000BE6 RID: 3046
		private string _partiesText;

		// Token: 0x04000BE7 RID: 3047
		private string _fiefsText;

		// Token: 0x04000BE8 RID: 3048
		private string _incomeText;

		// Token: 0x04000BE9 RID: 3049
		private BasicTooltipViewModel _renownHint;

		// Token: 0x04000BEA RID: 3050
		private BasicTooltipViewModel _kingdomActionDisabledReasonHint;

		// Token: 0x04000BEB RID: 3051
		private HintViewModel _clanBannerHint;

		// Token: 0x04000BEC RID: 3052
		private HintViewModel _changeClanNameHint;

		// Token: 0x04000BED RID: 3053
		private string _financeText;

		// Token: 0x04000BEE RID: 3054
		private string _currentGoldText;

		// Token: 0x04000BEF RID: 3055
		private int _currentGold;

		// Token: 0x04000BF0 RID: 3056
		private string _totalIncomeText;

		// Token: 0x04000BF1 RID: 3057
		private int _totalIncome;

		// Token: 0x04000BF2 RID: 3058
		private string _totalIncomeValueText;

		// Token: 0x04000BF3 RID: 3059
		private string _totalExpensesText;

		// Token: 0x04000BF4 RID: 3060
		private int _totalExpenses;

		// Token: 0x04000BF5 RID: 3061
		private string _totalExpensesValueText;

		// Token: 0x04000BF6 RID: 3062
		private string _dailyChangeText;

		// Token: 0x04000BF7 RID: 3063
		private int _dailyChange;

		// Token: 0x04000BF8 RID: 3064
		private string _dailyChangeValueText;

		// Token: 0x04000BF9 RID: 3065
		private string _expectedGoldText;

		// Token: 0x04000BFA RID: 3066
		private int _expectedGold;

		// Token: 0x04000BFB RID: 3067
		private string _expenseText;

		// Token: 0x04000BFC RID: 3068
		private TooltipTriggerVM _goldChangeTooltip;

		// Token: 0x04000BFD RID: 3069
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000BFE RID: 3070
		private InputKeyItemVM _previousTabInputKey;

		// Token: 0x04000BFF RID: 3071
		private InputKeyItemVM _nextTabInputKey;

		// Token: 0x04000C00 RID: 3072
		private ElementNotificationVM _tutorialNotification;

		// Token: 0x04000C01 RID: 3073
		private string _latestTutorialElementID;
	}
}
