using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000110 RID: 272
	public class ClanSettlementItemVM : ViewModel
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x0005E8DC File Offset: 0x0005CADC
		public ClanSettlementItemVM(Settlement settlement, Action<ClanSettlementItemVM> onSelection, Action onShowSendMembers, ITeleportationCampaignBehavior teleportationBehavior)
		{
			this.Settlement = settlement;
			this._onSelection = onSelection;
			this._onShowSendMembers = onShowSendMembers;
			this._teleportationBehavior = teleportationBehavior;
			this.IsFortification = settlement.IsFortification;
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.FileName = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.ItemProperties = new MBBindingList<SelectableFiefItemPropertyVM>();
			this.ProfitItemProperties = new MBBindingList<ProfitItemPropertyVM>();
			this.TotalProfit = new ProfitItemPropertyVM(GameTexts.FindText("str_profit", null).ToString(), 0, ProfitItemPropertyVM.PropertyType.None, null, null);
			this.ImageName = ((settlementComponent != null) ? settlementComponent.WaitMeshName : "");
			this.VillagesOwned = new MBBindingList<ClanSettlementItemVM>();
			this.Notables = new MBBindingList<HeroVM>();
			this.Members = new MBBindingList<HeroVM>();
			this.RefreshValues();
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0005E9B4 File Offset: 0x0005CBB4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.VillagesText = GameTexts.FindText("str_villages", null).ToString();
			this.NotablesText = GameTexts.FindText("str_center_notables", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.Name = this.Settlement.Name.ToString();
			this.UpdateProperties();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0005EA25 File Offset: 0x0005CC25
		public void OnSettlementSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0005EA33 File Offset: 0x0005CC33
		public void ExecuteLink()
		{
			MBInformationManager.HideInformations();
			Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0005EA54 File Offset: 0x0005CC54
		public void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0005EA5B File Offset: 0x0005CC5B
		public void ExecuteOpenTooltip()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this.Settlement,
				true
			});
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0005EA84 File Offset: 0x0005CC84
		public void ExecuteSendMembers()
		{
			Action onShowSendMembers = this._onShowSendMembers;
			if (onShowSendMembers == null)
			{
				return;
			}
			onShowSendMembers();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0005EA96 File Offset: 0x0005CC96
		private void OnGovernorChanged(Hero oldHero, Hero newHero)
		{
			ChangeGovernorAction.Apply(this.Settlement.Town, newHero);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0005EAA9 File Offset: 0x0005CCA9
		private bool IsGovernorAssignable(Hero oldHero, Hero newHero)
		{
			return newHero.IsActive && newHero.GovernorOf == null;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0005EAC0 File Offset: 0x0005CCC0
		private void UpdateProperties()
		{
			this.ItemProperties.Clear();
			this.VillagesOwned.Clear();
			this.Notables.Clear();
			this.Members.Clear();
			foreach (Village village in this.Settlement.BoundVillages)
			{
				this.VillagesOwned.Add(new ClanSettlementItemVM(village.Settlement, null, null, null));
			}
			this.HasNotables = !this.Settlement.Notables.IsEmpty<Hero>();
			foreach (Hero hero in this.Settlement.Notables)
			{
				this.Notables.Add(new HeroVM(hero, false));
			}
			foreach (Hero hero2 in from h in this.Settlement.HeroesWithoutParty
			where h.Clan == Clan.PlayerClan
			select h)
			{
				this.Members.Add(new HeroVM(hero2, false));
			}
			this.HasGovernor = false;
			if (!this.Settlement.IsVillage)
			{
				Town town = this.Settlement.Town;
				Hero hero3 = (((town != null) ? town.Governor : null) != null) ? this.Settlement.Town.Governor : CampaignUIHelper.GetTeleportingGovernor(this.Settlement, this._teleportationBehavior);
				this.HasGovernor = (hero3 != null);
				this.Governor = (this.HasGovernor ? new HeroVM(hero3, false) : null);
			}
			this.IsFortification = this.Settlement.IsFortification;
			BasicTooltipViewModel hint2;
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel hint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this.Settlement.Town));
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_walls", null).ToString(), this.Settlement.Town.GetWallLevel().ToString(), 0, SelectableItemPropertyVM.PropertyType.Wall, hint, false));
				hint2 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this.Settlement.Town));
			}
			else
			{
				hint2 = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this.Settlement.Village));
			}
			int changeAmount = (this.Settlement.Town != null) ? ((int)this.Settlement.Town.ProsperityChange) : ((int)this.Settlement.Village.HearthChange);
			if (this.Settlement.IsFortification)
			{
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_prosperity", null).ToString(), string.Format("{0:0.##}", this.Settlement.Town.Prosperity), changeAmount, SelectableItemPropertyVM.PropertyType.Prosperity, hint2, false));
			}
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel hint3 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this.Settlement.Town));
				int changeAmount2 = (int)this.Settlement.Town.FoodChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_food_stocks", null).ToString(), ((int)this.Settlement.Town.FoodStocks).ToString(), changeAmount2, SelectableItemPropertyVM.PropertyType.Food, hint3, false));
				BasicTooltipViewModel hint4 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this.Settlement.Town));
				int changeAmount3 = (int)this.Settlement.Town.LoyaltyChange;
				bool isWarning = this.Settlement.IsTown && this.Settlement.Town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_loyalty", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Loyalty), changeAmount3, SelectableItemPropertyVM.PropertyType.Loyalty, hint4, isWarning));
				BasicTooltipViewModel hint5 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this.Settlement.Town));
				int changeAmount4 = (int)this.Settlement.Town.SecurityChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_security", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Security), changeAmount4, SelectableItemPropertyVM.PropertyType.Security, hint5, false));
			}
			int num = (int)this.Settlement.Militia;
			List<TooltipProperty> militiaHint = this.Settlement.IsVillage ? CampaignUIHelper.GetVillageMilitiaTooltip(this.Settlement.Village) : CampaignUIHelper.GetTownMilitiaTooltip(this.Settlement.Town);
			int changeAmount5 = (this.Settlement.Town != null) ? ((int)this.Settlement.Town.MilitiaChange) : ((int)this.Settlement.Village.MilitiaChange);
			this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_militia", null).ToString(), num.ToString(), changeAmount5, SelectableItemPropertyVM.PropertyType.Militia, new BasicTooltipViewModel(() => militiaHint), false));
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel hint6 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this.Settlement.Town));
				int garrisonChange = this.Settlement.Town.GarrisonChange;
				Collection<SelectableFiefItemPropertyVM> itemProperties = this.ItemProperties;
				string name = GameTexts.FindText("str_garrison", null).ToString();
				MobileParty garrisonParty = this.Settlement.Town.GarrisonParty;
				itemProperties.Add(new SelectableFiefItemPropertyVM(name, ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers.ToString() : null) ?? "0", garrisonChange, SelectableItemPropertyVM.PropertyType.Garrison, hint6, false));
			}
			TextObject textObject;
			this.IsSendMembersEnabled = CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject);
			TextObject textObject2 = new TextObject("{=uGMGjUZy}Send your clan members to {SETTLEMENT_NAME}", null);
			textObject2.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name.ToString());
			this.SendMembersHint = new HintViewModel(this.IsSendMembersEnabled ? textObject2 : textObject, null);
			this.UpdateProfitProperties();
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		private void UpdateProfitProperties()
		{
			this.ProfitItemProperties.Clear();
			if (this.Settlement.Town != null)
			{
				Town town = this.Settlement.Town;
				ClanFinanceModel clanFinanceModel = Campaign.Current.Models.ClanFinanceModel;
				int num = 0;
				int num2 = (int)Campaign.Current.Models.SettlementTaxModel.CalculateTownTax(town, false).ResultNumber;
				int num3 = (int)clanFinanceModel.CalculateTownIncomeFromTariffs(Clan.PlayerClan, town, false).ResultNumber;
				int num4 = clanFinanceModel.CalculateTownIncomeFromProjects(town);
				if (num2 != 0)
				{
					this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=qeclv74c}Taxes", null).ToString(), num2, ProfitItemPropertyVM.PropertyType.Tax, null, null));
					num += num2;
				}
				if (num3 != 0)
				{
					this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=eIgC6YGp}Tariffs", null).ToString(), num3, ProfitItemPropertyVM.PropertyType.Tariff, null, null));
					num += num3;
				}
				if (town.GarrisonParty != null && town.GarrisonParty.IsActive)
				{
					int totalWage = town.GarrisonParty.TotalWage;
					if (totalWage != 0)
					{
						this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=5dkPxmZG}Garrison Wages", null).ToString(), -totalWage, ProfitItemPropertyVM.PropertyType.Garrison, null, null));
						num -= totalWage;
					}
				}
				foreach (Village village in town.Villages)
				{
					int num5 = clanFinanceModel.CalculateVillageIncome(Clan.PlayerClan, village, false);
					if (num5 != 0)
					{
						this.ProfitItemProperties.Add(new ProfitItemPropertyVM(village.Name.ToString(), num5, ProfitItemPropertyVM.PropertyType.Village, null, null));
						num += num5;
					}
				}
				if (num4 != 0)
				{
					Collection<ProfitItemPropertyVM> profitItemProperties = this.ProfitItemProperties;
					string name = new TextObject("{=J8ddrAOf}Governor Effects", null).ToString();
					int value = num4;
					ProfitItemPropertyVM.PropertyType type = ProfitItemPropertyVM.PropertyType.Governor;
					HeroVM governor = this.Governor;
					profitItemProperties.Add(new ProfitItemPropertyVM(name, value, type, (governor != null) ? governor.ImageIdentifier : null, null));
					num += num4;
				}
				this.TotalProfit.Value = num;
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0005F2F0 File Offset: 0x0005D4F0
		private bool IsSettlementSlotAssignable(Hero oldHero, Hero newHero)
		{
			return (oldHero == null || !oldHero.IsHumanPlayerCharacter) && !newHero.IsHumanPlayerCharacter && newHero.IsActive && (newHero.PartyBelongedTo == null || newHero.PartyBelongedTo.LeaderHero != newHero) && newHero.PartyBelongedToAsPrisoner == null;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0005F33F File Offset: 0x0005D53F
		private void ExecuteOpenSettlementPage()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0005F35B File Offset: 0x0005D55B
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x0005F363 File Offset: 0x0005D563
		[DataSourceProperty]
		public HeroVM Governor
		{
			get
			{
				return this._governor;
			}
			set
			{
				if (value != this._governor)
				{
					this._governor = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Governor");
				}
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0005F381 File Offset: 0x0005D581
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x0005F389 File Offset: 0x0005D589
		[DataSourceProperty]
		public MBBindingList<SelectableFiefItemPropertyVM> ItemProperties
		{
			get
			{
				return this._itemProperties;
			}
			set
			{
				if (value != this._itemProperties)
				{
					this._itemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SelectableFiefItemPropertyVM>>(value, "ItemProperties");
				}
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0005F3A7 File Offset: 0x0005D5A7
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x0005F3AF File Offset: 0x0005D5AF
		[DataSourceProperty]
		public MBBindingList<ProfitItemPropertyVM> ProfitItemProperties
		{
			get
			{
				return this._profitItemProperties;
			}
			set
			{
				if (value != this._profitItemProperties)
				{
					this._profitItemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ProfitItemPropertyVM>>(value, "ProfitItemProperties");
				}
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0005F3CD File Offset: 0x0005D5CD
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x0005F3D5 File Offset: 0x0005D5D5
		[DataSourceProperty]
		public ProfitItemPropertyVM TotalProfit
		{
			get
			{
				return this._totalProfit;
			}
			set
			{
				if (value != this._totalProfit)
				{
					this._totalProfit = value;
					base.OnPropertyChangedWithValue<ProfitItemPropertyVM>(value, "TotalProfit");
				}
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0005F3F3 File Offset: 0x0005D5F3
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x0005F3FB File Offset: 0x0005D5FB
		[DataSourceProperty]
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if (value != this._fileName)
				{
					this._fileName = value;
					base.OnPropertyChangedWithValue<string>(value, "FileName");
				}
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0005F41E File Offset: 0x0005D61E
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0005F426 File Offset: 0x0005D626
		[DataSourceProperty]
		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				if (value != this._imageName)
				{
					this._imageName = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageName");
				}
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0005F449 File Offset: 0x0005D649
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0005F451 File Offset: 0x0005D651
		[DataSourceProperty]
		public string VillagesText
		{
			get
			{
				return this._villagesText;
			}
			set
			{
				if (value != this._villagesText)
				{
					this._villagesText = value;
					base.OnPropertyChangedWithValue<string>(value, "VillagesText");
				}
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0005F474 File Offset: 0x0005D674
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x0005F47C File Offset: 0x0005D67C
		[DataSourceProperty]
		public string NotablesText
		{
			get
			{
				return this._notablesText;
			}
			set
			{
				if (value != this._notablesText)
				{
					this._notablesText = value;
					base.OnPropertyChangedWithValue<string>(value, "NotablesText");
				}
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0005F49F File Offset: 0x0005D69F
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x0005F4A7 File Offset: 0x0005D6A7
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

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0005F4CA File Offset: 0x0005D6CA
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0005F4D2 File Offset: 0x0005D6D2
		[DataSourceProperty]
		public bool IsFortification
		{
			get
			{
				return this._isFortification;
			}
			set
			{
				if (value != this._isFortification)
				{
					this._isFortification = value;
					base.OnPropertyChangedWithValue(value, "IsFortification");
				}
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0005F4F0 File Offset: 0x0005D6F0
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		[DataSourceProperty]
		public bool HasGovernor
		{
			get
			{
				return this._hasGovernor;
			}
			set
			{
				if (value != this._hasGovernor)
				{
					this._hasGovernor = value;
					base.OnPropertyChangedWithValue(value, "HasGovernor");
				}
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0005F516 File Offset: 0x0005D716
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x0005F51E File Offset: 0x0005D71E
		[DataSourceProperty]
		public bool HasNotables
		{
			get
			{
				return this._hasNotables;
			}
			set
			{
				if (value != this._hasNotables)
				{
					this._hasNotables = value;
					base.OnPropertyChangedWithValue(value, "HasNotables");
				}
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0005F53C File Offset: 0x0005D73C
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0005F544 File Offset: 0x0005D744
		[DataSourceProperty]
		public bool IsSendMembersEnabled
		{
			get
			{
				return this._isSendMembersEnabled;
			}
			set
			{
				if (value != this._isSendMembersEnabled)
				{
					this._isSendMembersEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsSendMembersEnabled");
				}
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0005F562 File Offset: 0x0005D762
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x0005F56A File Offset: 0x0005D76A
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

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0005F588 File Offset: 0x0005D788
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x0005F590 File Offset: 0x0005D790
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

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0005F5B3 File Offset: 0x0005D7B3
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x0005F5BB File Offset: 0x0005D7BB
		[DataSourceProperty]
		public MBBindingList<ClanSettlementItemVM> VillagesOwned
		{
			get
			{
				return this._villagesOwned;
			}
			set
			{
				if (value != this._villagesOwned)
				{
					this._villagesOwned = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanSettlementItemVM>>(value, "VillagesOwned");
				}
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0005F5D9 File Offset: 0x0005D7D9
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x0005F5E1 File Offset: 0x0005D7E1
		[DataSourceProperty]
		public MBBindingList<HeroVM> Notables
		{
			get
			{
				return this._notables;
			}
			set
			{
				if (value != this._notables)
				{
					this._notables = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Notables");
				}
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0005F5FF File Offset: 0x0005D7FF
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x0005F607 File Offset: 0x0005D807
		[DataSourceProperty]
		public MBBindingList<HeroVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Members");
				}
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0005F625 File Offset: 0x0005D825
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x0005F62D File Offset: 0x0005D82D
		[DataSourceProperty]
		public HintViewModel SendMembersHint
		{
			get
			{
				return this._sendMembersHint;
			}
			set
			{
				if (value != this._sendMembersHint)
				{
					this._sendMembersHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SendMembersHint");
				}
			}
		}

		// Token: 0x04000C60 RID: 3168
		private readonly Action<ClanSettlementItemVM> _onSelection;

		// Token: 0x04000C61 RID: 3169
		private readonly Action _onShowSendMembers;

		// Token: 0x04000C62 RID: 3170
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000C63 RID: 3171
		public readonly Settlement Settlement;

		// Token: 0x04000C64 RID: 3172
		private string _name;

		// Token: 0x04000C65 RID: 3173
		private HeroVM _governor;

		// Token: 0x04000C66 RID: 3174
		private string _fileName;

		// Token: 0x04000C67 RID: 3175
		private string _imageName;

		// Token: 0x04000C68 RID: 3176
		private string _villagesText;

		// Token: 0x04000C69 RID: 3177
		private string _notablesText;

		// Token: 0x04000C6A RID: 3178
		private string _membersText;

		// Token: 0x04000C6B RID: 3179
		private bool _isFortification;

		// Token: 0x04000C6C RID: 3180
		private bool _isSelected;

		// Token: 0x04000C6D RID: 3181
		private bool _hasGovernor;

		// Token: 0x04000C6E RID: 3182
		private bool _hasNotables;

		// Token: 0x04000C6F RID: 3183
		private bool _isSendMembersEnabled;

		// Token: 0x04000C70 RID: 3184
		private MBBindingList<SelectableFiefItemPropertyVM> _itemProperties;

		// Token: 0x04000C71 RID: 3185
		private MBBindingList<ProfitItemPropertyVM> _profitItemProperties;

		// Token: 0x04000C72 RID: 3186
		private ProfitItemPropertyVM _totalProfit;

		// Token: 0x04000C73 RID: 3187
		private MBBindingList<ClanSettlementItemVM> _villagesOwned;

		// Token: 0x04000C74 RID: 3188
		private MBBindingList<HeroVM> _notables;

		// Token: 0x04000C75 RID: 3189
		private MBBindingList<HeroVM> _members;

		// Token: 0x04000C76 RID: 3190
		private HintViewModel _sendMembersHint;
	}
}
