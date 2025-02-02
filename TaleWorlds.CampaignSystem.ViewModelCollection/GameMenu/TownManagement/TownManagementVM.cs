using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x0200009B RID: 155
	public class TownManagementVM : ViewModel
	{
		// Token: 0x06000EEE RID: 3822 RVA: 0x0003B500 File Offset: 0x00039700
		public TownManagementVM()
		{
			this._settlement = Settlement.CurrentSettlement;
			Settlement settlement = this._settlement;
			if (((settlement != null) ? settlement.Town : null) == null)
			{
				Debug.FailedAssert("Town management initialized with null settlement and/or town!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\TownManagement\\TownManagementVM.cs", ".ctor", 27);
				Debug.Print("Town management initialized with null settlement and/or town!", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			this.ProjectSelection = new SettlementProjectSelectionVM(this._settlement, new Action(this.OnChangeInBuildingQueue));
			this.GovernorSelection = new SettlementGovernorSelectionVM(this._settlement, new Action<Hero>(this.OnGovernorSelectionDone));
			this.ReserveControl = new TownManagementReserveControlVM(this._settlement, new Action(this.OnReserveUpdated));
			this.MiddleFirstTextList = new MBBindingList<TownManagementDescriptionItemVM>();
			this.MiddleSecondTextList = new MBBindingList<TownManagementDescriptionItemVM>();
			this.Shops = new MBBindingList<TownManagementShopItemVM>();
			this.Villages = new MBBindingList<TownManagementVillageItemVM>();
			this.Show = false;
			this.IsTown = this._settlement.IsTown;
			this.IsThereCurrentProject = (this._settlement.Town.CurrentBuilding != null);
			this.CurrentGovernor = new HeroVM(this._settlement.Town.Governor ?? CampaignUIHelper.GetTeleportingGovernor(this._settlement, Campaign.Current.GetCampaignBehavior<ITeleportationCampaignBehavior>()), true);
			this.UpdateGovernorSelectionProperties();
			this.RefreshCurrentDevelopment();
			this.RefreshTownManagementStats();
			foreach (Workshop workshop in this._settlement.Town.Workshops)
			{
				WorkshopType workshopType = workshop.WorkshopType;
				if (workshopType != null && !workshopType.IsHidden)
				{
					this.Shops.Add(new TownManagementShopItemVM(workshop));
				}
			}
			foreach (Village village in this._settlement.BoundVillages)
			{
				this.Villages.Add(new TownManagementVillageItemVM(village));
			}
			this.ConsumptionTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetSettlementConsumptionTooltip(this._settlement));
			this.RefreshValues();
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003B718 File Offset: 0x00039918
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CurrentProjectText = new TextObject("{=qBq70qDq}Current Project", null).ToString();
			this.CompletionText = new TextObject("{=Rkh2k1OA}Completion:", null).ToString();
			this.ManageText = new TextObject("{=XseYJYka}Manage", null).ToString();
			this.DoneText = new TextObject("{=WiNRdfsm}Done", null).ToString();
			this.WallsText = new TextObject("{=LsZEdD2z}Walls", null).ToString();
			this.VillagesText = GameTexts.FindText("str_bound_village", null).ToString();
			this.ShopsInSettlementText = GameTexts.FindText("str_shops_in_settlement", null).ToString();
			this.GovernorText = GameTexts.FindText("str_sort_by_governor_label", null).ToString();
			this.MiddleFirstTextList.ApplyActionOnAllItems(delegate(TownManagementDescriptionItemVM x)
			{
				x.RefreshValues();
			});
			this.MiddleSecondTextList.ApplyActionOnAllItems(delegate(TownManagementDescriptionItemVM x)
			{
				x.RefreshValues();
			});
			this.ProjectSelection.RefreshValues();
			this.GovernorSelection.RefreshValues();
			this.ReserveControl.RefreshValues();
			this.Shops.ApplyActionOnAllItems(delegate(TownManagementShopItemVM x)
			{
				x.RefreshValues();
			});
			this.Villages.ApplyActionOnAllItems(delegate(TownManagementVillageItemVM x)
			{
				x.RefreshValues();
			});
			this.CurrentGovernor.RefreshValues();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003B8B0 File Offset: 0x00039AB0
		private void RefreshTownManagementStats()
		{
			this.MiddleFirstTextList.Clear();
			this.MiddleSecondTextList.Clear();
			ExplainedNumber taxExplanation = Campaign.Current.Models.SettlementTaxModel.CalculateTownTax(this._settlement.Town, true);
			int taxValue = (int)taxExplanation.ResultNumber;
			BasicTooltipViewModel hint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(GameTexts.FindText("str_town_management_population_tax", null).ToString(), (float)taxValue, ref taxExplanation));
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_town_management_population_tax", null));
			this.MiddleFirstTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_LEFT_colon", null), taxValue, 0, TownManagementDescriptionItemVM.DescriptionType.Gold, hint));
			BasicTooltipViewModel hint2 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this._settlement.Town));
			this.MiddleFirstTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_prosperity", null), (int)this._settlement.Town.Prosperity, (int)Campaign.Current.Models.SettlementProsperityModel.CalculateProsperityChange(this._settlement.Town, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Prosperity, hint2));
			BasicTooltipViewModel hint3 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownDailyProductionTooltip(this._settlement.Town));
			this.MiddleFirstTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_daily_production", null), (int)Campaign.Current.Models.BuildingConstructionModel.CalculateDailyConstructionPower(this._settlement.Town, false).ResultNumber, 0, TownManagementDescriptionItemVM.DescriptionType.Production, hint3));
			BasicTooltipViewModel hint4 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this._settlement.Town));
			this.MiddleFirstTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_security", null), (int)this._settlement.Town.Security, (int)Campaign.Current.Models.SettlementSecurityModel.CalculateSecurityChange(this._settlement.Town, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Security, hint4));
			BasicTooltipViewModel hint5 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this._settlement.Town));
			this.MiddleSecondTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_loyalty", null), (int)this._settlement.Town.Loyalty, (int)Campaign.Current.Models.SettlementLoyaltyModel.CalculateLoyaltyChange(this._settlement.Town, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Loyalty, hint5));
			BasicTooltipViewModel hint6 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this._settlement.Town));
			this.MiddleSecondTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_food", null), (int)this._settlement.Town.FoodStocks, (int)Campaign.Current.Models.SettlementFoodModel.CalculateTownFoodStocksChange(this._settlement.Town, true, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Food, hint6));
			BasicTooltipViewModel hint7 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownMilitiaTooltip(this._settlement.Town));
			this.MiddleSecondTextList.Add(new TownManagementDescriptionItemVM(GameTexts.FindText("str_town_management_militia", null), (int)this._settlement.Militia, (int)Campaign.Current.Models.SettlementMilitiaModel.CalculateMilitiaChange(this._settlement, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Militia, hint7));
			BasicTooltipViewModel hint8 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this._settlement.Town));
			Collection<TownManagementDescriptionItemVM> middleSecondTextList = this.MiddleSecondTextList;
			TextObject title = GameTexts.FindText("str_town_management_garrison", null);
			MobileParty garrisonParty = this._settlement.Town.GarrisonParty;
			middleSecondTextList.Add(new TownManagementDescriptionItemVM(title, (garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers : 0, (int)Campaign.Current.Models.SettlementGarrisonModel.CalculateGarrisonChange(this._settlement, false).ResultNumber, TownManagementDescriptionItemVM.DescriptionType.Garrison, hint8));
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003BC45 File Offset: 0x00039E45
		private void OnChangeInBuildingQueue()
		{
			this.OnProjectSelectionDone();
			this.RefreshTownManagementStats();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003BC54 File Offset: 0x00039E54
		private void RefreshCurrentDevelopment()
		{
			if (this._settlement.Town.CurrentBuilding != null)
			{
				this.IsCurrentProjectDaily = this._settlement.Town.CurrentBuilding.BuildingType.IsDefaultProject;
				if (!this.IsCurrentProjectDaily)
				{
					this.CurrentProjectProgress = (int)(BuildingHelper.GetProgressOfBuilding(this.ProjectSelection.CurrentSelectedProject.Building, this._settlement.Town) * 100f);
					this.ProjectSelection.CurrentSelectedProject.RefreshProductionText();
				}
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		private void OnProjectSelectionDone()
		{
			List<Building> localDevelopmentList = this.ProjectSelection.LocalDevelopmentList;
			Building building = this.ProjectSelection.CurrentDailyDefault.Building;
			if (localDevelopmentList != null)
			{
				BuildingHelper.ChangeCurrentBuildingQueue(localDevelopmentList, this._settlement.Town);
			}
			if (building != this._settlement.Town.CurrentDefaultBuilding)
			{
				BuildingHelper.ChangeDefaultBuilding(building, this._settlement.Town);
			}
			this.RefreshCurrentDevelopment();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003BD40 File Offset: 0x00039F40
		private void OnGovernorSelectionDone(Hero selectedGovernor)
		{
			if (selectedGovernor != this.CurrentGovernor.Hero)
			{
				this.CurrentGovernor = new HeroVM(selectedGovernor, true);
				if (this.CurrentGovernor.Hero != null)
				{
					ChangeGovernorAction.Apply(this._settlement.Town, this.CurrentGovernor.Hero);
				}
				else
				{
					ChangeGovernorAction.RemoveGovernorOfIfExists(this._settlement.Town);
				}
			}
			this.UpdateGovernorSelectionProperties();
			this.RefreshTownManagementStats();
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003BDB0 File Offset: 0x00039FB0
		private void UpdateGovernorSelectionProperties()
		{
			this.HasGovernor = (this.CurrentGovernor.Hero != null);
			TextObject hintText;
			this.IsGovernorSelectionEnabled = this.GetCanChangeGovernor(out hintText);
			this.GovernorSelectionDisabledHint = new HintViewModel(hintText, null);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003BDEC File Offset: 0x00039FEC
		private bool GetCanChangeGovernor(out TextObject disabledReason)
		{
			HeroVM currentGovernor = this.CurrentGovernor;
			bool flag;
			if (currentGovernor == null)
			{
				flag = false;
			}
			else
			{
				Hero hero = currentGovernor.Hero;
				bool? flag2 = (hero != null) ? new bool?(hero.IsTraveling) : null;
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
			}
			if (flag)
			{
				disabledReason = new TextObject("{=qbqimqMb}{GOVERNOR.NAME} is on the way to be the new governor of {SETTLEMENT_NAME}", null);
				if (this.CurrentGovernor.Hero.CharacterObject != null)
				{
					StringHelpers.SetCharacterProperties("GOVERNOR", this.CurrentGovernor.Hero.CharacterObject, disabledReason, false);
				}
				TextObject textObject = disabledReason;
				string tag = "SETTLEMENT_NAME";
				TextObject name = this._settlement.Name;
				textObject.SetTextVariable(tag, ((name != null) ? name.ToString() : null) ?? string.Empty);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003BEAF File Offset: 0x0003A0AF
		private void OnReserveUpdated()
		{
			this.RefreshCurrentDevelopment();
			this.RefreshTownManagementStats();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003BEBD File Offset: 0x0003A0BD
		public void ExecuteDone()
		{
			this.OnProjectSelectionDone();
			this.Show = false;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003BECC File Offset: 0x0003A0CC
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003BEDF File Offset: 0x0003A0DF
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0003BEEE File Offset: 0x0003A0EE
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0003BEF6 File Offset: 0x0003A0F6
		[DataSourceProperty]
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003BF14 File Offset: 0x0003A114
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003BF1C File Offset: 0x0003A11C
		[DataSourceProperty]
		public string CompletionText
		{
			get
			{
				return this._completionText;
			}
			set
			{
				if (value != this._completionText)
				{
					this._completionText = value;
					base.OnPropertyChangedWithValue<string>(value, "CompletionText");
				}
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003BF3F File Offset: 0x0003A13F
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003BF47 File Offset: 0x0003A147
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003BF6A File Offset: 0x0003A16A
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0003BF72 File Offset: 0x0003A172
		[DataSourceProperty]
		public string ManageText
		{
			get
			{
				return this._manageText;
			}
			set
			{
				if (value != this._manageText)
				{
					this._manageText = value;
					base.OnPropertyChangedWithValue<string>(value, "ManageText");
				}
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0003BF95 File Offset: 0x0003A195
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0003BF9D File Offset: 0x0003A19D
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0003BFC8 File Offset: 0x0003A1C8
		[DataSourceProperty]
		public string WallsText
		{
			get
			{
				return this._wallsText;
			}
			set
			{
				if (value != this._wallsText)
				{
					this._wallsText = value;
					base.OnPropertyChangedWithValue<string>(value, "WallsText");
				}
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003BFEB File Offset: 0x0003A1EB
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0003BFF3 File Offset: 0x0003A1F3
		[DataSourceProperty]
		public string CurrentProjectText
		{
			get
			{
				return this._currentProjectText;
			}
			set
			{
				if (value != this._currentProjectText)
				{
					this._currentProjectText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentProjectText");
				}
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0003C016 File Offset: 0x0003A216
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0003C01E File Offset: 0x0003A21E
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0003C041 File Offset: 0x0003A241
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0003C049 File Offset: 0x0003A249
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0003C067 File Offset: 0x0003A267
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0003C06F File Offset: 0x0003A26F
		[DataSourceProperty]
		public bool IsGovernorSelectionEnabled
		{
			get
			{
				return this._isGovernorSelectionEnabled;
			}
			set
			{
				if (value != this._isGovernorSelectionEnabled)
				{
					this._isGovernorSelectionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsGovernorSelectionEnabled");
				}
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0003C08D File Offset: 0x0003A28D
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0003C095 File Offset: 0x0003A295
		[DataSourceProperty]
		public bool IsTown
		{
			get
			{
				return this._isTown;
			}
			set
			{
				if (value != this._isTown)
				{
					this._isTown = value;
					base.OnPropertyChangedWithValue(value, "IsTown");
				}
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003C0B3 File Offset: 0x0003A2B3
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0003C0BB File Offset: 0x0003A2BB
		[DataSourceProperty]
		public bool Show
		{
			get
			{
				return this._show;
			}
			set
			{
				if (value != this._show)
				{
					this._show = value;
					base.OnPropertyChangedWithValue(value, "Show");
				}
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0003C0D9 File Offset: 0x0003A2D9
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0003C0E1 File Offset: 0x0003A2E1
		[DataSourceProperty]
		public bool IsThereCurrentProject
		{
			get
			{
				return this._isThereCurrentProject;
			}
			set
			{
				if (value != this._isThereCurrentProject)
				{
					this._isThereCurrentProject = value;
					base.OnPropertyChangedWithValue(value, "IsThereCurrentProject");
				}
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0003C0FF File Offset: 0x0003A2FF
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0003C107 File Offset: 0x0003A307
		[DataSourceProperty]
		public bool IsSelectingGovernor
		{
			get
			{
				return this._isSelectingGovernor;
			}
			set
			{
				if (value != this._isSelectingGovernor)
				{
					this._isSelectingGovernor = value;
					base.OnPropertyChangedWithValue(value, "IsSelectingGovernor");
				}
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0003C125 File Offset: 0x0003A325
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0003C12D File Offset: 0x0003A32D
		[DataSourceProperty]
		public MBBindingList<TownManagementDescriptionItemVM> MiddleFirstTextList
		{
			get
			{
				return this._middleLeftTextList;
			}
			set
			{
				if (value != this._middleLeftTextList)
				{
					this._middleLeftTextList = value;
					base.OnPropertyChanged("MiddleLeftTextList");
				}
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0003C14A File Offset: 0x0003A34A
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0003C152 File Offset: 0x0003A352
		[DataSourceProperty]
		public MBBindingList<TownManagementDescriptionItemVM> MiddleSecondTextList
		{
			get
			{
				return this._middleRightTextList;
			}
			set
			{
				if (value != this._middleRightTextList)
				{
					this._middleRightTextList = value;
					base.OnPropertyChanged("MiddleRightTextList");
				}
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0003C16F File Offset: 0x0003A36F
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0003C177 File Offset: 0x0003A377
		[DataSourceProperty]
		public MBBindingList<TownManagementShopItemVM> Shops
		{
			get
			{
				return this._shops;
			}
			set
			{
				if (value != this._shops)
				{
					this._shops = value;
					base.OnPropertyChangedWithValue<MBBindingList<TownManagementShopItemVM>>(value, "Shops");
				}
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003C195 File Offset: 0x0003A395
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0003C19D File Offset: 0x0003A39D
		[DataSourceProperty]
		public MBBindingList<TownManagementVillageItemVM> Villages
		{
			get
			{
				return this._villages;
			}
			set
			{
				if (value != this._villages)
				{
					this._villages = value;
					base.OnPropertyChangedWithValue<MBBindingList<TownManagementVillageItemVM>>(value, "Villages");
				}
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003C1BB File Offset: 0x0003A3BB
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0003C1C3 File Offset: 0x0003A3C3
		[DataSourceProperty]
		public HintViewModel GovernorSelectionDisabledHint
		{
			get
			{
				return this._governorSelectionDisabledHint;
			}
			set
			{
				if (value != this._governorSelectionDisabledHint)
				{
					this._governorSelectionDisabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "GovernorSelectionDisabledHint");
				}
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003C1E1 File Offset: 0x0003A3E1
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0003C1E9 File Offset: 0x0003A3E9
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003C20C File Offset: 0x0003A40C
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0003C214 File Offset: 0x0003A414
		[DataSourceProperty]
		public string ShopsInSettlementText
		{
			get
			{
				return this._shopsInSettlementText;
			}
			set
			{
				if (value != this._shopsInSettlementText)
				{
					this._shopsInSettlementText = value;
					base.OnPropertyChangedWithValue<string>(value, "ShopsInSettlementText");
				}
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0003C237 File Offset: 0x0003A437
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x0003C23F File Offset: 0x0003A43F
		[DataSourceProperty]
		public bool IsCurrentProjectDaily
		{
			get
			{
				return this._isCurrentProjectDaily;
			}
			set
			{
				if (value != this._isCurrentProjectDaily)
				{
					this._isCurrentProjectDaily = value;
					base.OnPropertyChangedWithValue(value, "IsCurrentProjectDaily");
				}
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003C25D File Offset: 0x0003A45D
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0003C265 File Offset: 0x0003A465
		[DataSourceProperty]
		public int CurrentProjectProgress
		{
			get
			{
				return this._currentProjectProgress;
			}
			set
			{
				if (value != this._currentProjectProgress)
				{
					this._currentProjectProgress = value;
					base.OnPropertyChangedWithValue(value, "CurrentProjectProgress");
				}
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0003C283 File Offset: 0x0003A483
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0003C28B File Offset: 0x0003A48B
		[DataSourceProperty]
		public SettlementProjectSelectionVM ProjectSelection
		{
			get
			{
				return this._projectSelection;
			}
			set
			{
				if (value != this._projectSelection)
				{
					this._projectSelection = value;
					base.OnPropertyChangedWithValue<SettlementProjectSelectionVM>(value, "ProjectSelection");
				}
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0003C2A9 File Offset: 0x0003A4A9
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0003C2B1 File Offset: 0x0003A4B1
		[DataSourceProperty]
		public SettlementGovernorSelectionVM GovernorSelection
		{
			get
			{
				return this._governorSelection;
			}
			set
			{
				if (value != this._governorSelection)
				{
					this._governorSelection = value;
					base.OnPropertyChangedWithValue<SettlementGovernorSelectionVM>(value, "GovernorSelection");
				}
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0003C2CF File Offset: 0x0003A4CF
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0003C2D7 File Offset: 0x0003A4D7
		[DataSourceProperty]
		public TownManagementReserveControlVM ReserveControl
		{
			get
			{
				return this._reserveControl;
			}
			set
			{
				if (value != this._reserveControl)
				{
					this._reserveControl = value;
					base.OnPropertyChangedWithValue<TownManagementReserveControlVM>(value, "ReserveControl");
				}
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0003C2F5 File Offset: 0x0003A4F5
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0003C2FD File Offset: 0x0003A4FD
		[DataSourceProperty]
		public HeroVM CurrentGovernor
		{
			get
			{
				return this._currentGovernor;
			}
			set
			{
				if (value != this._currentGovernor)
				{
					this._currentGovernor = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "CurrentGovernor");
				}
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0003C31B File Offset: 0x0003A51B
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x0003C323 File Offset: 0x0003A523
		[DataSourceProperty]
		public BasicTooltipViewModel ConsumptionTooltip
		{
			get
			{
				return this._consumptionTooltip;
			}
			set
			{
				if (value != this._consumptionTooltip)
				{
					this._consumptionTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ConsumptionTooltip");
				}
			}
		}

		// Token: 0x040006EF RID: 1775
		private readonly Settlement _settlement;

		// Token: 0x040006F0 RID: 1776
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040006F1 RID: 1777
		private bool _isThereCurrentProject;

		// Token: 0x040006F2 RID: 1778
		private bool _isSelectingGovernor;

		// Token: 0x040006F3 RID: 1779
		private SettlementProjectSelectionVM _projectSelection;

		// Token: 0x040006F4 RID: 1780
		private SettlementGovernorSelectionVM _governorSelection;

		// Token: 0x040006F5 RID: 1781
		private TownManagementReserveControlVM _reserveControl;

		// Token: 0x040006F6 RID: 1782
		private MBBindingList<TownManagementDescriptionItemVM> _middleLeftTextList;

		// Token: 0x040006F7 RID: 1783
		private MBBindingList<TownManagementDescriptionItemVM> _middleRightTextList;

		// Token: 0x040006F8 RID: 1784
		private MBBindingList<TownManagementShopItemVM> _shops;

		// Token: 0x040006F9 RID: 1785
		private MBBindingList<TownManagementVillageItemVM> _villages;

		// Token: 0x040006FA RID: 1786
		private HintViewModel _governorSelectionDisabledHint;

		// Token: 0x040006FB RID: 1787
		private bool _show;

		// Token: 0x040006FC RID: 1788
		private bool _isTown;

		// Token: 0x040006FD RID: 1789
		private bool _hasGovernor;

		// Token: 0x040006FE RID: 1790
		private bool _isGovernorSelectionEnabled;

		// Token: 0x040006FF RID: 1791
		private string _titleText;

		// Token: 0x04000700 RID: 1792
		private bool _isCurrentProjectDaily;

		// Token: 0x04000701 RID: 1793
		private int _currentProjectProgress;

		// Token: 0x04000702 RID: 1794
		private string _currentProjectText;

		// Token: 0x04000703 RID: 1795
		private HeroVM _currentGovernor;

		// Token: 0x04000704 RID: 1796
		private string _manageText;

		// Token: 0x04000705 RID: 1797
		private string _doneText;

		// Token: 0x04000706 RID: 1798
		private string _wallsText;

		// Token: 0x04000707 RID: 1799
		private string _completionText;

		// Token: 0x04000708 RID: 1800
		private string _villagesText;

		// Token: 0x04000709 RID: 1801
		private string _shopsInSettlementText;

		// Token: 0x0400070A RID: 1802
		private BasicTooltipViewModel _consumptionTooltip;

		// Token: 0x0400070B RID: 1803
		private string _governorText;
	}
}
