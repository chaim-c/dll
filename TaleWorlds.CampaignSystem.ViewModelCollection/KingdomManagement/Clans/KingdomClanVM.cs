using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans
{
	// Token: 0x02000077 RID: 119
	public class KingdomClanVM : KingdomCategoryVM
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002A0A8 File Offset: 0x000282A8
		public KingdomClanVM(Action<KingdomDecision> forceDecide)
		{
			this._forceDecide = forceDecide;
			this.SupportHint = new HintViewModel();
			this.ExpelHint = new HintViewModel();
			this._clans = new MBBindingList<KingdomClanItemVM>();
			base.IsAcceptableItemSelected = false;
			this.RefreshClanList();
			base.NotificationCount = 0;
			this.SupportCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfSupportingClan();
			this.ExpelCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfExpellingClan(Clan.PlayerClan);
			TextObject hintText;
			this.CanSupportCurrentClan = this.GetCanSupportCurrentClanWithReason(this.SupportCost, out hintText);
			this.SupportHint.HintText = hintText;
			TextObject hintText2;
			this.CanExpelCurrentClan = this.GetCanExpelCurrentClanWithReason(this._isThereAPendingDecisionToExpelThisClan, this.ExpelCost, out hintText2);
			this.ExpelHint.HintText = hintText2;
			this.ClanSortController = new KingdomClanSortControllerVM(ref this._clans);
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			this.RefreshValues();
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002A1A4 File Offset: 0x000283A4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SupportText = new TextObject("{=N63XYX2r}Support", null).ToString();
			this.NameText = GameTexts.FindText("str_scoreboard_header", "name").ToString();
			this.InfluenceText = GameTexts.FindText("str_influence", null).ToString();
			this.FiefsText = GameTexts.FindText("str_fiefs", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.BannerText = GameTexts.FindText("str_banner", null).ToString();
			this.TypeText = GameTexts.FindText("str_sort_by_type_label", null).ToString();
			base.CategoryNameText = new TextObject("{=j4F7tTzy}Clan", null).ToString();
			base.NoItemSelectedText = GameTexts.FindText("str_kingdom_no_clan_selected", null).ToString();
			this.SupportActionExplanationText = GameTexts.FindText("str_support_clan_action_explanation", null).ToString();
			this.ExpelActionExplanationText = GameTexts.FindText("str_expel_clan_action_explanation", null).ToString();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002A2B0 File Offset: 0x000284B0
		private void SetCurrentSelectedClan(KingdomClanItemVM clan)
		{
			if (clan != this.CurrentSelectedClan)
			{
				if (this.CurrentSelectedClan != null)
				{
					this.CurrentSelectedClan.IsSelected = false;
				}
				this.CurrentSelectedClan = clan;
				this.CurrentSelectedClan.IsSelected = true;
				this.SupportCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfSupportingClan();
				this._isThereAPendingDecisionToExpelThisClan = Clan.PlayerClan.Kingdom.UnresolvedDecisions.Any(delegate(KingdomDecision x)
				{
					ExpelClanFromKingdomDecision expelClanFromKingdomDecision;
					return (expelClanFromKingdomDecision = (x as ExpelClanFromKingdomDecision)) != null && expelClanFromKingdomDecision.ClanToExpel == this.CurrentSelectedClan.Clan && !x.ShouldBeCancelled();
				});
				TextObject hintText;
				this.CanExpelCurrentClan = this.GetCanExpelCurrentClanWithReason(this._isThereAPendingDecisionToExpelThisClan, this.ExpelCost, out hintText);
				this.ExpelHint.HintText = hintText;
				if (this._isThereAPendingDecisionToExpelThisClan)
				{
					this.ExpelActionText = GameTexts.FindText("str_resolve", null).ToString();
					this.ExpelActionExplanationText = GameTexts.FindText("str_resolve_explanation", null).ToString();
					this.ExpelCost = 0;
					return;
				}
				this.ExpelActionText = GameTexts.FindText("str_policy_propose", null).ToString();
				this.ExpelCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfExpellingClan(Clan.PlayerClan);
				TextObject hintText2;
				this.CanSupportCurrentClan = this.GetCanSupportCurrentClanWithReason(this.SupportCost, out hintText2);
				this.SupportHint.HintText = hintText2;
				this.ExpelActionExplanationText = GameTexts.FindText("str_expel_clan_action_explanation", null).SetTextVariable("SUPPORT", this.CalculateExpelLikelihood(this.CurrentSelectedClan)).ToString();
				base.IsAcceptableItemSelected = (this.CurrentSelectedClan != null);
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002A424 File Offset: 0x00028624
		private bool GetCanSupportCurrentClanWithReason(int supportCost, out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (Hero.MainHero.Clan.Influence < (float)supportCost)
			{
				disabledReason = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null);
				return false;
			}
			if (this.CurrentSelectedClan.Clan == Clan.PlayerClan)
			{
				disabledReason = GameTexts.FindText("str_cannot_support_your_clan", null);
				return false;
			}
			if (Hero.MainHero.Clan.IsUnderMercenaryService)
			{
				disabledReason = GameTexts.FindText("str_mercenaries_cannot_support_clans", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002A4AC File Offset: 0x000286AC
		private bool GetCanExpelCurrentClanWithReason(bool isThereAPendingDecision, int expelCost, out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (Hero.MainHero.Clan.IsUnderMercenaryService)
			{
				disabledReason = GameTexts.FindText("str_mercenaries_cannot_expel_clans", null);
				return false;
			}
			if (!isThereAPendingDecision)
			{
				if (Hero.MainHero.Clan.Influence < (float)expelCost)
				{
					disabledReason = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null);
					return false;
				}
				if (this.CurrentSelectedClan.Clan == Clan.PlayerClan)
				{
					disabledReason = GameTexts.FindText("str_cannot_expel_your_clan", null);
					return false;
				}
				Clan clan = this.CurrentSelectedClan.Clan;
				Kingdom kingdom = this.CurrentSelectedClan.Clan.Kingdom;
				if (clan == ((kingdom != null) ? kingdom.RulingClan : null))
				{
					disabledReason = GameTexts.FindText("str_cannot_expel_ruling_clan", null);
					return false;
				}
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002A570 File Offset: 0x00028770
		public void RefreshClan()
		{
			this.RefreshClanList();
			foreach (KingdomClanItemVM kingdomClanItemVM in this.Clans)
			{
				kingdomClanItemVM.Refresh();
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002A5C0 File Offset: 0x000287C0
		public void SelectClan(Clan clan)
		{
			foreach (KingdomClanItemVM kingdomClanItemVM in this.Clans)
			{
				if (kingdomClanItemVM.Clan == clan)
				{
					this.OnClanSelection(kingdomClanItemVM);
					break;
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002A618 File Offset: 0x00028818
		private void OnClanSelection(KingdomClanItemVM clan)
		{
			if (this._currentSelectedClan != clan)
			{
				this.SetCurrentSelectedClan(clan);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002A62C File Offset: 0x0002882C
		private void ExecuteExpelCurrentClan()
		{
			if (Hero.MainHero.Clan.Influence >= (float)this.ExpelCost)
			{
				KingdomDecision kingdomDecision = new ExpelClanFromKingdomDecision(Clan.PlayerClan, this._currentSelectedClan.Clan);
				Clan.PlayerClan.Kingdom.AddDecision(kingdomDecision, false);
				this._forceDecide(kingdomDecision);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002A684 File Offset: 0x00028884
		private void ExecuteSupport()
		{
			if (Hero.MainHero.Clan.Influence >= (float)this.SupportCost)
			{
				this._currentSelectedClan.Clan.OnSupportedByClan(Hero.MainHero.Clan);
				Clan clan = this._currentSelectedClan.Clan;
				this.RefreshClan();
				this.SelectClan(clan);
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002A6DC File Offset: 0x000288DC
		private int CalculateExpelLikelihood(KingdomClanItemVM clan)
		{
			return MathF.Round(new KingdomElection(new ExpelClanFromKingdomDecision(Clan.PlayerClan, clan.Clan)).GetLikelihoodForSponsor(Clan.PlayerClan) * 100f);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002A708 File Offset: 0x00028908
		private void RefreshClanList()
		{
			this.Clans.Clear();
			if (Clan.PlayerClan.Kingdom != null)
			{
				foreach (Clan clan in Clan.PlayerClan.Kingdom.Clans)
				{
					this.Clans.Add(new KingdomClanItemVM(clan, new Action<KingdomClanItemVM>(this.OnClanSelection)));
				}
			}
			if (this.Clans.Count > 0)
			{
				this.SetCurrentSelectedClan(this.Clans.FirstOrDefault<KingdomClanItemVM>());
			}
			if (this.ClanSortController != null)
			{
				this.ClanSortController.SortByCurrentState();
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002A7C4 File Offset: 0x000289C4
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnClanChangedKingdomEvent.ClearListeners(this);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002A7D7 File Offset: 0x000289D7
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			if (clan != Clan.PlayerClan && (oldKingdom == Clan.PlayerClan.Kingdom || newKingdom == Clan.PlayerClan.Kingdom))
			{
				this.RefreshClanList();
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002A801 File Offset: 0x00028A01
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0002A809 File Offset: 0x00028A09
		[DataSourceProperty]
		public KingdomClanSortControllerVM ClanSortController
		{
			get
			{
				return this._clanSortController;
			}
			set
			{
				if (value != this._clanSortController)
				{
					this._clanSortController = value;
					base.OnPropertyChangedWithValue<KingdomClanSortControllerVM>(value, "ClanSortController");
				}
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002A827 File Offset: 0x00028A27
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0002A82F File Offset: 0x00028A2F
		[DataSourceProperty]
		public KingdomClanItemVM CurrentSelectedClan
		{
			get
			{
				return this._currentSelectedClan;
			}
			set
			{
				if (value != this._currentSelectedClan)
				{
					this._currentSelectedClan = value;
					base.OnPropertyChangedWithValue<KingdomClanItemVM>(value, "CurrentSelectedClan");
				}
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002A84D File Offset: 0x00028A4D
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0002A855 File Offset: 0x00028A55
		[DataSourceProperty]
		public string ExpelActionExplanationText
		{
			get
			{
				return this._expelActionExplanationText;
			}
			set
			{
				if (value != this._expelActionExplanationText)
				{
					this._expelActionExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExpelActionExplanationText");
				}
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002A878 File Offset: 0x00028A78
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002A880 File Offset: 0x00028A80
		[DataSourceProperty]
		public string SupportActionExplanationText
		{
			get
			{
				return this._supportActionExplanationText;
			}
			set
			{
				if (value != this._supportActionExplanationText)
				{
					this._supportActionExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportActionExplanationText");
				}
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002A8A3 File Offset: 0x00028AA3
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002A8AB File Offset: 0x00028AAB
		[DataSourceProperty]
		public string BannerText
		{
			get
			{
				return this._bannerText;
			}
			set
			{
				if (value != this._bannerText)
				{
					this._bannerText = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerText");
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002A8CE File Offset: 0x00028ACE
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002A8D6 File Offset: 0x00028AD6
		[DataSourceProperty]
		public string TypeText
		{
			get
			{
				return this._typeText;
			}
			set
			{
				if (value != this._typeText)
				{
					this._typeText = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeText");
				}
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002A8F9 File Offset: 0x00028AF9
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002A901 File Offset: 0x00028B01
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002A924 File Offset: 0x00028B24
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002A92C File Offset: 0x00028B2C
		[DataSourceProperty]
		public string InfluenceText
		{
			get
			{
				return this._influenceText;
			}
			set
			{
				if (value != this._influenceText)
				{
					this._influenceText = value;
					base.OnPropertyChangedWithValue<string>(value, "InfluenceText");
				}
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002A94F File Offset: 0x00028B4F
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0002A957 File Offset: 0x00028B57
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002A97A File Offset: 0x00028B7A
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0002A982 File Offset: 0x00028B82
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002A9A5 File Offset: 0x00028BA5
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0002A9AD File Offset: 0x00028BAD
		[DataSourceProperty]
		public MBBindingList<KingdomClanItemVM> Clans
		{
			get
			{
				return this._clans;
			}
			set
			{
				if (value != this._clans)
				{
					this._clans = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomClanItemVM>>(value, "Clans");
				}
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002A9CB File Offset: 0x00028BCB
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0002A9D3 File Offset: 0x00028BD3
		[DataSourceProperty]
		public bool CanSupportCurrentClan
		{
			get
			{
				return this._canSupportCurrentClan;
			}
			set
			{
				if (value != this._canSupportCurrentClan)
				{
					this._canSupportCurrentClan = value;
					base.OnPropertyChangedWithValue(value, "CanSupportCurrentClan");
				}
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002A9F1 File Offset: 0x00028BF1
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0002A9F9 File Offset: 0x00028BF9
		[DataSourceProperty]
		public bool CanExpelCurrentClan
		{
			get
			{
				return this._canExpelCurrentClan;
			}
			set
			{
				if (value != this._canExpelCurrentClan)
				{
					this._canExpelCurrentClan = value;
					base.OnPropertyChangedWithValue(value, "CanExpelCurrentClan");
				}
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002AA17 File Offset: 0x00028C17
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0002AA1F File Offset: 0x00028C1F
		[DataSourceProperty]
		public string SupportText
		{
			get
			{
				return this._supportText;
			}
			set
			{
				if (value != this._supportText)
				{
					this._supportText = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportText");
				}
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002AA42 File Offset: 0x00028C42
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0002AA4A File Offset: 0x00028C4A
		[DataSourceProperty]
		public string ExpelActionText
		{
			get
			{
				return this._expelActionText;
			}
			set
			{
				if (value != this._expelActionText)
				{
					this._expelActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExpelActionText");
				}
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002AA6D File Offset: 0x00028C6D
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0002AA75 File Offset: 0x00028C75
		[DataSourceProperty]
		public int SupportCost
		{
			get
			{
				return this._supportCost;
			}
			set
			{
				if (value != this._supportCost)
				{
					this._supportCost = value;
					base.OnPropertyChangedWithValue(value, "SupportCost");
				}
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002AA93 File Offset: 0x00028C93
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002AA9B File Offset: 0x00028C9B
		[DataSourceProperty]
		public int ExpelCost
		{
			get
			{
				return this._expelCost;
			}
			set
			{
				if (value != this._expelCost)
				{
					this._expelCost = value;
					base.OnPropertyChangedWithValue(value, "ExpelCost");
				}
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002AAB9 File Offset: 0x00028CB9
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002AAC1 File Offset: 0x00028CC1
		[DataSourceProperty]
		public HintViewModel ExpelHint
		{
			get
			{
				return this._expelHint;
			}
			set
			{
				if (value != this._expelHint)
				{
					this._expelHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ExpelHint");
				}
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002AADF File Offset: 0x00028CDF
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0002AAE7 File Offset: 0x00028CE7
		[DataSourceProperty]
		public HintViewModel SupportHint
		{
			get
			{
				return this._supportHint;
			}
			set
			{
				if (value != this._supportHint)
				{
					this._supportHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SupportHint");
				}
			}
		}

		// Token: 0x040004B6 RID: 1206
		private Action<KingdomDecision> _forceDecide;

		// Token: 0x040004B7 RID: 1207
		private bool _isThereAPendingDecisionToExpelThisClan;

		// Token: 0x040004B8 RID: 1208
		private MBBindingList<KingdomClanItemVM> _clans;

		// Token: 0x040004B9 RID: 1209
		private HintViewModel _expelHint;

		// Token: 0x040004BA RID: 1210
		private HintViewModel _supportHint;

		// Token: 0x040004BB RID: 1211
		private string _bannerText;

		// Token: 0x040004BC RID: 1212
		private string _nameText;

		// Token: 0x040004BD RID: 1213
		private string _influenceText;

		// Token: 0x040004BE RID: 1214
		private string _membersText;

		// Token: 0x040004BF RID: 1215
		private string _fiefsText;

		// Token: 0x040004C0 RID: 1216
		private string _typeText;

		// Token: 0x040004C1 RID: 1217
		private string _expelActionText;

		// Token: 0x040004C2 RID: 1218
		private string _expelActionExplanationText;

		// Token: 0x040004C3 RID: 1219
		private string _supportActionExplanationText;

		// Token: 0x040004C4 RID: 1220
		private int _expelCost;

		// Token: 0x040004C5 RID: 1221
		private string _supportText;

		// Token: 0x040004C6 RID: 1222
		private int _supportCost;

		// Token: 0x040004C7 RID: 1223
		private bool _canSupportCurrentClan;

		// Token: 0x040004C8 RID: 1224
		private bool _canExpelCurrentClan;

		// Token: 0x040004C9 RID: 1225
		private KingdomClanItemVM _currentSelectedClan;

		// Token: 0x040004CA RID: 1226
		private KingdomClanSortControllerVM _clanSortController;
	}
}
