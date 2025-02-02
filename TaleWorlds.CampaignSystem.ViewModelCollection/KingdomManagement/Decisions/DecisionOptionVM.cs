using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions
{
	// Token: 0x02000068 RID: 104
	public class DecisionOptionVM : ViewModel
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00025656 File Offset: 0x00023856
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0002565E File Offset: 0x0002385E
		public DecisionOutcome Option { get; private set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00025667 File Offset: 0x00023867
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0002566F File Offset: 0x0002386F
		public KingdomDecision Decision { get; private set; }

		// Token: 0x060008E4 RID: 2276 RVA: 0x00025678 File Offset: 0x00023878
		public DecisionOptionVM(DecisionOutcome option, KingdomDecision decision, KingdomElection kingdomDecisionMaker, Action<DecisionOptionVM> onSelect, Action<DecisionOptionVM> onSupportStrengthChange)
		{
			this._onSelect = onSelect;
			this._onSupportStrengthChange = onSupportStrengthChange;
			this._kingdomDecisionMaker = kingdomDecisionMaker;
			this.Decision = decision;
			this.CurrentSupportWeight = Supporter.SupportWeights.Choose;
			this.OptionHint = new HintViewModel();
			this.IsPlayerSupporter = !this._kingdomDecisionMaker.IsPlayerChooser;
			this.SupportersOfThisOption = new MBBindingList<DecisionSupporterVM>();
			this.Option = option;
			if (option != null)
			{
				Clan sponsorClan = option.SponsorClan;
				if (((sponsorClan != null) ? sponsorClan.Leader : null) != null)
				{
					this.Sponsor = new HeroVM(option.SponsorClan.Leader, false);
				}
				List<Supporter> supporterList = option.SupporterList;
				if (supporterList != null && supporterList.Count > 0)
				{
					foreach (Supporter supporter in option.SupporterList)
					{
						if (supporter.SupportWeight > Supporter.SupportWeights.StayNeutral)
						{
							if (supporter.Clan != option.SponsorClan)
							{
								this.SupportersOfThisOption.Add(new DecisionSupporterVM(supporter.Name, supporter.ImagePath, supporter.Clan, supporter.SupportWeight));
							}
							else
							{
								this.SponsorWeightImagePath = DecisionSupporterVM.GetSupporterWeightImagePath(supporter.SupportWeight);
							}
						}
					}
				}
				this.IsOptionForAbstain = false;
			}
			else
			{
				this.IsOptionForAbstain = true;
			}
			this.RefreshValues();
			this.RefreshSupportOptionEnabled();
			this.RefreshCanChooseOption();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000257F0 File Offset: 0x000239F0
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Option != null)
			{
				this.Name = this.Option.GetDecisionTitle().ToString();
				this.Description = this.Option.GetDecisionDescription().ToString();
			}
			else
			{
				this.Name = GameTexts.FindText("str_abstain", null).ToString();
				this.Description = GameTexts.FindText("str_kingdom_decision_abstain_desc", null).ToString();
			}
			MBBindingList<DecisionSupporterVM> supportersOfThisOption = this.SupportersOfThisOption;
			if (supportersOfThisOption == null)
			{
				return;
			}
			supportersOfThisOption.ApplyActionOnAllItems(delegate(DecisionSupporterVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00025894 File Offset: 0x00023A94
		private void ExecuteShowSupporterTooltip()
		{
			DecisionOutcome option = this.Option;
			if (option != null && option.SupporterList.Count > 0)
			{
				List<TooltipProperty> list = new List<TooltipProperty>();
				this._kingdomDecisionMaker.DetermineOfficialSupport();
				foreach (Supporter supporter in this.Option.SupporterList)
				{
					if (supporter.SupportWeight > Supporter.SupportWeights.StayNeutral && !supporter.IsPlayer)
					{
						int influenceCost = this.Decision.GetInfluenceCost(this.Option, supporter.Clan, supporter.SupportWeight);
						GameTexts.SetVariable("AMOUNT", influenceCost);
						GameTexts.SetVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
						list.Add(new TooltipProperty(supporter.Name.ToString(), GameTexts.FindText("str_amount_with_influence_icon", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
				InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
				{
					list
				});
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000259A4 File Offset: 0x00023BA4
		private void ExecuteHideSupporterTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000259AC File Offset: 0x00023BAC
		private void RefreshSupportOptionEnabled()
		{
			int influenceCostOfOutcome = this._kingdomDecisionMaker.GetInfluenceCostOfOutcome(this.Option, Clan.PlayerClan, Supporter.SupportWeights.SlightlyFavor);
			int influenceCostOfOutcome2 = this._kingdomDecisionMaker.GetInfluenceCostOfOutcome(this.Option, Clan.PlayerClan, Supporter.SupportWeights.StronglyFavor);
			int influenceCostOfOutcome3 = this._kingdomDecisionMaker.GetInfluenceCostOfOutcome(this.Option, Clan.PlayerClan, Supporter.SupportWeights.FullyPush);
			this.SupportOption1Text = influenceCostOfOutcome.ToString();
			this.SupportOption2Text = influenceCostOfOutcome2.ToString();
			this.SupportOption3Text = influenceCostOfOutcome3.ToString();
			this.IsSupportOption1Enabled = ((float)influenceCostOfOutcome <= Clan.PlayerClan.Influence && !this.IsOptionForAbstain);
			this.IsSupportOption2Enabled = ((float)influenceCostOfOutcome2 <= Clan.PlayerClan.Influence && !this.IsOptionForAbstain);
			this.IsSupportOption3Enabled = ((float)influenceCostOfOutcome3 <= Clan.PlayerClan.Influence && !this.IsOptionForAbstain);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00025A88 File Offset: 0x00023C88
		private void OnSupportStrengthChange(int index)
		{
			if (!this.IsOptionForAbstain)
			{
				switch (index)
				{
				case 0:
					this.CurrentSupportWeight = Supporter.SupportWeights.SlightlyFavor;
					break;
				case 1:
					this.CurrentSupportWeight = Supporter.SupportWeights.StronglyFavor;
					break;
				case 2:
					this.CurrentSupportWeight = Supporter.SupportWeights.FullyPush;
					break;
				}
				this._kingdomDecisionMaker.OnPlayerSupport((!this.IsOptionForAbstain) ? this.Option : null, this.CurrentSupportWeight);
				this._onSupportStrengthChange(this);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00025AF8 File Offset: 0x00023CF8
		public void AfterKingChooseOutcome()
		{
			this._hasKingChoosen = true;
			this.RefreshCanChooseOption();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00025B08 File Offset: 0x00023D08
		private void RefreshCanChooseOption()
		{
			if (this._hasKingChoosen)
			{
				this.CanBeChosen = false;
				return;
			}
			if (this.IsOptionForAbstain)
			{
				this.CanBeChosen = true;
				return;
			}
			if (this.IsPlayerSupporter)
			{
				this.CanBeChosen = ((float)this._kingdomDecisionMaker.GetInfluenceCostOfOutcome(this.Option, Clan.PlayerClan, Supporter.SupportWeights.SlightlyFavor) <= Clan.PlayerClan.Influence);
			}
			else
			{
				int influenceCostOfOutcome = this._kingdomDecisionMaker.GetInfluenceCostOfOutcome(this.Option, Clan.PlayerClan, Supporter.SupportWeights.Choose);
				this.CanBeChosen = ((float)influenceCostOfOutcome <= Clan.PlayerClan.Influence || influenceCostOfOutcome == 0);
			}
			this.OptionHint.HintText = (this.CanBeChosen ? TextObject.Empty : new TextObject("{=Xmw93W6a}Not Enough Influence", null));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00025BC4 File Offset: 0x00023DC4
		private void ExecuteSelection()
		{
			this._onSelect(this);
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			EventManager eventManager = game.EventManager;
			if (eventManager == null)
			{
				return;
			}
			eventManager.TriggerEvent<PlayerSelectedAKingdomDecisionOptionEvent>(new PlayerSelectedAKingdomDecisionOptionEvent(this.Option));
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00025BF6 File Offset: 0x00023DF6
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00025BFE File Offset: 0x00023DFE
		[DataSourceProperty]
		public HintViewModel OptionHint
		{
			get
			{
				return this._optionHint;
			}
			set
			{
				if (value != this._optionHint)
				{
					this._optionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OptionHint");
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00025C1C File Offset: 0x00023E1C
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00025C24 File Offset: 0x00023E24
		[DataSourceProperty]
		public MBBindingList<DecisionSupporterVM> SupportersOfThisOption
		{
			get
			{
				return this._supportersOfThisOption;
			}
			set
			{
				if (value != this._supportersOfThisOption)
				{
					this._supportersOfThisOption = value;
					base.OnPropertyChangedWithValue<MBBindingList<DecisionSupporterVM>>(value, "SupportersOfThisOption");
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00025C42 File Offset: 0x00023E42
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00025C4A File Offset: 0x00023E4A
		[DataSourceProperty]
		public HeroVM Sponsor
		{
			get
			{
				return this._sponsor;
			}
			set
			{
				if (value != this._sponsor)
				{
					this._sponsor = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Sponsor");
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00025C68 File Offset: 0x00023E68
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00025C70 File Offset: 0x00023E70
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00025C93 File Offset: 0x00023E93
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00025C9B File Offset: 0x00023E9B
		[DataSourceProperty]
		public string SponsorWeightImagePath
		{
			get
			{
				return this._sponsorWeightImagePath;
			}
			set
			{
				if (value != this._sponsorWeightImagePath)
				{
					this._sponsorWeightImagePath = value;
					base.OnPropertyChangedWithValue<string>(value, "SponsorWeightImagePath");
				}
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00025CBE File Offset: 0x00023EBE
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00025CC6 File Offset: 0x00023EC6
		[DataSourceProperty]
		public bool CanBeChosen
		{
			get
			{
				return this._canBeChosen;
			}
			set
			{
				if (value != this._canBeChosen)
				{
					this._canBeChosen = value;
					base.OnPropertyChangedWithValue(value, "CanBeChosen");
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00025CE4 File Offset: 0x00023EE4
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00025CEC File Offset: 0x00023EEC
		[DataSourceProperty]
		public bool IsKingsOutcome
		{
			get
			{
				return this._isKingsOutcome;
			}
			set
			{
				if (value != this._isKingsOutcome)
				{
					this._isKingsOutcome = value;
					base.OnPropertyChangedWithValue(value, "IsKingsOutcome");
				}
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00025D0A File Offset: 0x00023F0A
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00025D12 File Offset: 0x00023F12
		[DataSourceProperty]
		public bool IsPlayerSupporter
		{
			get
			{
				return this._isPlayerSupporter;
			}
			set
			{
				if (value != this._isPlayerSupporter)
				{
					this._isPlayerSupporter = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerSupporter");
				}
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00025D30 File Offset: 0x00023F30
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00025D38 File Offset: 0x00023F38
		[DataSourceProperty]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (value != this._isHighlightEnabled)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightEnabled");
				}
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00025D56 File Offset: 0x00023F56
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00025D5E File Offset: 0x00023F5E
		[DataSourceProperty]
		public int WinPercentage
		{
			get
			{
				return this._winPercentage;
			}
			set
			{
				if (value != this._winPercentage)
				{
					this._winPercentage = value;
					base.OnPropertyChangedWithValue(value, "WinPercentage");
					GameTexts.SetVariable("NUMBER", value);
					this.WinPercentageStr = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				}
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00025D9D File Offset: 0x00023F9D
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00025DA5 File Offset: 0x00023FA5
		[DataSourceProperty]
		public string WinPercentageStr
		{
			get
			{
				return this._winPercentageStr;
			}
			set
			{
				if (value != this._winPercentageStr)
				{
					this._winPercentageStr = value;
					base.OnPropertyChangedWithValue<string>(value, "WinPercentageStr");
				}
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00025DC8 File Offset: 0x00023FC8
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00025DD0 File Offset: 0x00023FD0
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00025DF3 File Offset: 0x00023FF3
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00025DFB File Offset: 0x00023FFB
		[DataSourceProperty]
		public int InitialPercentage
		{
			get
			{
				return this._initialPercentage;
			}
			set
			{
				if (value != this._initialPercentage)
				{
					this._initialPercentage = value;
					base.OnPropertyChangedWithValue(value, "InitialPercentage");
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00025E19 File Offset: 0x00024019
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00025E21 File Offset: 0x00024021
		[DataSourceProperty]
		public int InfluenceCost
		{
			get
			{
				return this._influenceCost;
			}
			set
			{
				if (value != this._influenceCost)
				{
					this._influenceCost = value;
					base.OnPropertyChangedWithValue(value, "InfluenceCost");
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00025E3F File Offset: 0x0002403F
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00025E47 File Offset: 0x00024047
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

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00025E65 File Offset: 0x00024065
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00025E6D File Offset: 0x0002406D
		[DataSourceProperty]
		public bool IsOptionForAbstain
		{
			get
			{
				return this._isOptionForAbstain;
			}
			set
			{
				if (value != this._isOptionForAbstain)
				{
					this._isOptionForAbstain = value;
					base.OnPropertyChangedWithValue(value, "IsOptionForAbstain");
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00025E8B File Offset: 0x0002408B
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00025E93 File Offset: 0x00024093
		[DataSourceProperty]
		public Supporter.SupportWeights CurrentSupportWeight
		{
			get
			{
				return this._currentSupportWeight;
			}
			set
			{
				if (value != this._currentSupportWeight)
				{
					this._currentSupportWeight = value;
					base.OnPropertyChanged("CurrentSupportWeight");
					this.CurrentSupportWeightIndex = (int)value;
				}
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00025EB7 File Offset: 0x000240B7
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00025EBF File Offset: 0x000240BF
		[DataSourceProperty]
		public int CurrentSupportWeightIndex
		{
			get
			{
				return this._currentSupportWeightIndex;
			}
			set
			{
				if (value != this._currentSupportWeightIndex)
				{
					this._currentSupportWeightIndex = value;
					base.OnPropertyChangedWithValue(value, "CurrentSupportWeightIndex");
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00025EDD File Offset: 0x000240DD
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00025EE5 File Offset: 0x000240E5
		[DataSourceProperty]
		public string SupportOption1Text
		{
			get
			{
				return this._supportOption1Text;
			}
			set
			{
				if (value != this._supportOption1Text)
				{
					this._supportOption1Text = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportOption1Text");
				}
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00025F08 File Offset: 0x00024108
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00025F10 File Offset: 0x00024110
		[DataSourceProperty]
		public string SupportOption2Text
		{
			get
			{
				return this._supportOption2Text;
			}
			set
			{
				if (value != this._supportOption2Text)
				{
					this._supportOption2Text = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportOption2Text");
				}
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00025F33 File Offset: 0x00024133
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00025F3B File Offset: 0x0002413B
		[DataSourceProperty]
		public string SupportOption3Text
		{
			get
			{
				return this._supportOption3Text;
			}
			set
			{
				if (value != this._supportOption3Text)
				{
					this._supportOption3Text = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportOption3Text");
				}
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00025F5E File Offset: 0x0002415E
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00025F66 File Offset: 0x00024166
		[DataSourceProperty]
		public bool IsSupportOption1Enabled
		{
			get
			{
				return this._isSupportOption1Enabled;
			}
			set
			{
				if (value != this._isSupportOption1Enabled)
				{
					this._isSupportOption1Enabled = value;
					base.OnPropertyChangedWithValue(value, "IsSupportOption1Enabled");
				}
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00025F84 File Offset: 0x00024184
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00025F8C File Offset: 0x0002418C
		[DataSourceProperty]
		public bool IsSupportOption2Enabled
		{
			get
			{
				return this._isSupportOption2Enabled;
			}
			set
			{
				if (value != this._isSupportOption2Enabled)
				{
					this._isSupportOption2Enabled = value;
					base.OnPropertyChangedWithValue(value, "IsSupportOption2Enabled");
				}
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00025FAA File Offset: 0x000241AA
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00025FB2 File Offset: 0x000241B2
		[DataSourceProperty]
		public bool IsSupportOption3Enabled
		{
			get
			{
				return this._isSupportOption3Enabled;
			}
			set
			{
				if (value != this._isSupportOption3Enabled)
				{
					this._isSupportOption3Enabled = value;
					base.OnPropertyChangedWithValue(value, "IsSupportOption3Enabled");
				}
			}
		}

		// Token: 0x040003FF RID: 1023
		private readonly Action<DecisionOptionVM> _onSelect;

		// Token: 0x04000400 RID: 1024
		private readonly Action<DecisionOptionVM> _onSupportStrengthChange;

		// Token: 0x04000401 RID: 1025
		private readonly KingdomElection _kingdomDecisionMaker;

		// Token: 0x04000402 RID: 1026
		private bool _hasKingChoosen;

		// Token: 0x04000403 RID: 1027
		private MBBindingList<DecisionSupporterVM> _supportersOfThisOption;

		// Token: 0x04000404 RID: 1028
		private HeroVM _sponsor;

		// Token: 0x04000405 RID: 1029
		private bool _isOptionForAbstain;

		// Token: 0x04000406 RID: 1030
		private bool _isPlayerSupporter;

		// Token: 0x04000407 RID: 1031
		private bool _isSelected;

		// Token: 0x04000408 RID: 1032
		private bool _canBeChosen;

		// Token: 0x04000409 RID: 1033
		private bool _isKingsOutcome;

		// Token: 0x0400040A RID: 1034
		private bool _isHighlightEnabled;

		// Token: 0x0400040B RID: 1035
		private int _winPercentage = -1;

		// Token: 0x0400040C RID: 1036
		private int _influenceCost;

		// Token: 0x0400040D RID: 1037
		private int _initialPercentage = -99;

		// Token: 0x0400040E RID: 1038
		private int _currentSupportWeightIndex;

		// Token: 0x0400040F RID: 1039
		private string _name;

		// Token: 0x04000410 RID: 1040
		private string _description;

		// Token: 0x04000411 RID: 1041
		private string _winPercentageStr;

		// Token: 0x04000412 RID: 1042
		private string _sponsorWeightImagePath;

		// Token: 0x04000413 RID: 1043
		private Supporter.SupportWeights _currentSupportWeight;

		// Token: 0x04000414 RID: 1044
		private string _supportOption1Text;

		// Token: 0x04000415 RID: 1045
		private bool _isSupportOption1Enabled;

		// Token: 0x04000416 RID: 1046
		private string _supportOption2Text;

		// Token: 0x04000417 RID: 1047
		private bool _isSupportOption2Enabled;

		// Token: 0x04000418 RID: 1048
		private string _supportOption3Text;

		// Token: 0x04000419 RID: 1049
		private bool _isSupportOption3Enabled;

		// Token: 0x0400041A RID: 1050
		private HintViewModel _optionHint;
	}
}
