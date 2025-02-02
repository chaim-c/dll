using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Settlements
{
	// Token: 0x0200005D RID: 93
	public class KingdomSettlementVM : KingdomCategoryVM
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0002171C File Offset: 0x0001F91C
		public KingdomSettlementVM(Action<KingdomDecision> forceDecision, Action<Settlement> onGrantFief)
		{
			this._forceDecision = forceDecision;
			this._onGrantFief = onGrantFief;
			this._kingdom = (Hero.MainHero.MapFaction as Kingdom);
			this.AnnexCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAnnexation(Clan.PlayerClan);
			this.AnnexHint = new HintViewModel();
			base.IsAcceptableItemSelected = false;
			this.Settlements = new MBBindingList<KingdomSettlementItemVM>();
			this.RefreshSettlementList();
			base.NotificationCount = 0;
			this.SettlementSortController = new KingdomSettlementSortControllerVM(ref this._settlements);
			this.RefreshValues();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000217B4 File Offset: 0x0001F9B4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OwnerText = GameTexts.FindText("str_owner", null).ToString();
			this.NameText = GameTexts.FindText("str_scoreboard_header", "name").ToString();
			this.TypeText = GameTexts.FindText("str_sort_by_type_label", null).ToString();
			this.ProsperityText = GameTexts.FindText("str_prosperity_abbr", null).ToString();
			this.FoodText = GameTexts.FindText("str_inventory_category_tooltip", "6").ToString();
			this.GarrisonText = GameTexts.FindText("str_map_tooltip_garrison", null).ToString();
			this.MilitiaText = GameTexts.FindText("str_militia", null).ToString();
			this.ClanText = GameTexts.FindText("str_clans", null).ToString();
			this.VillagesText = GameTexts.FindText("str_villages", null).ToString();
			base.NoItemSelectedText = GameTexts.FindText("str_kingdom_no_settlement_selected", null).ToString();
			this.ProposeText = GameTexts.FindText("str_policy_propose", null).ToString();
			this.DefendersText = GameTexts.FindText("str_sort_by_defenders_label", null).ToString();
			base.CategoryNameText = new TextObject("{=qKUjgS6r}Settlement", null).ToString();
			this.Settlements.ApplyActionOnAllItems(delegate(KingdomSettlementItemVM x)
			{
				x.RefreshValues();
			});
			KingdomSettlementItemVM currentSelectedSettlement = this.CurrentSelectedSettlement;
			if (currentSelectedSettlement == null)
			{
				return;
			}
			currentSelectedSettlement.RefreshValues();
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00021928 File Offset: 0x0001FB28
		public void RefreshSettlementList()
		{
			this.Settlements.Clear();
			if (this._kingdom != null)
			{
				foreach (Settlement settlement in from S in this._kingdom.Settlements
				where S.IsCastle || S.IsTown
				select S)
				{
					this._settlements.Add(new KingdomSettlementItemVM(settlement, new Action<KingdomSettlementItemVM>(this.OnSettlementSelection)));
				}
			}
			if (this.Settlements.Count > 0)
			{
				this.SetCurrentSelectedSettlement(this.Settlements.FirstOrDefault<KingdomSettlementItemVM>());
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000219E8 File Offset: 0x0001FBE8
		private void SetCurrentSelectedSettlement(KingdomSettlementItemVM settlementItem)
		{
			if (this.CurrentSelectedSettlement != settlementItem)
			{
				if (this.CurrentSelectedSettlement != null)
				{
					this.CurrentSelectedSettlement.IsSelected = false;
				}
				this.CurrentSelectedSettlement = settlementItem;
				this.CurrentSelectedSettlement.IsSelected = true;
				if (settlementItem != null)
				{
					this._currenItemsUnresolvedDecision = this.GetSettlementsAnyWaitingDecision(settlementItem.Settlement);
					if (this._currenItemsUnresolvedDecision != null)
					{
						base.IsAcceptableItemSelected = true;
						this.AnnexCost = 0;
						this.AnnexText = GameTexts.FindText("str_resolve", null).ToString();
						this.AnnexActionExplanationText = GameTexts.FindText("str_resolve_explanation", null).ToString();
						this.AnnexHint.HintText = TextObject.Empty;
					}
					else if (settlementItem.Owner.Hero == Hero.MainHero)
					{
						if (Hero.MainHero.IsKingdomLeader)
						{
							this.AnnexActionExplanationText = new TextObject("{=G2h0V10w}Gift this settlement to a clan in your kingdom.", null).ToString();
							this.AnnexText = new TextObject("{=sffGeQ1g}Gift", null).ToString();
						}
						else
						{
							this.AnnexActionExplanationText = new TextObject("{=1UbocG5B}Denounce your rights and responsibilities from this fief by giving it back to the realm.", null).ToString();
							this.AnnexText = new TextObject("{=U3ksQXD3}Give Away", null).ToString();
						}
						if (Hero.MainHero.IsPrisoner)
						{
							this.CanAnnexCurrentSettlement = false;
							this.HasCost = true;
							this.AnnexHint.HintText = GameTexts.FindText("str_action_disabled_reason_prisoner", null);
						}
						else if (!Campaign.Current.Models.DiplomacyModel.CanSettlementBeGifted(this._currentSelectedSettlement.Settlement))
						{
							this.CanAnnexCurrentSettlement = false;
							this.HasCost = true;
							this.AnnexHint.HintText = GameTexts.FindText("str_cannot_annex_waiting_for_ruler_decision", null);
						}
						else if (PlayerEncounter.Current != null && PlayerEncounter.EncounterSettlement == null)
						{
							this.CanAnnexCurrentSettlement = false;
							this.HasCost = true;
							this.AnnexHint.HintText = GameTexts.FindText("str_action_disabled_reason_encounter", null);
						}
						else if (PlayerSiege.PlayerSiegeEvent != null)
						{
							this.CanAnnexCurrentSettlement = false;
							this.HasCost = true;
							this.AnnexHint.HintText = GameTexts.FindText("str_action_disabled_reason_siege", null);
						}
						else
						{
							this.CanAnnexCurrentSettlement = true;
							this.HasCost = false;
							this.AnnexHint.HintText = TextObject.Empty;
						}
					}
					else
					{
						this.AnnexText = GameTexts.FindText("str_policy_propose", null).ToString();
						this.AnnexCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAnnexation(Clan.PlayerClan);
						string text = GameTexts.FindText("str_annex_fief_action_explanation", null).ToString();
						int num = KingdomSettlementVM.CalculateLikelihood(settlementItem.Settlement);
						text = string.Format("{0} ({1}%)", text, num);
						this.AnnexActionExplanationText = text;
						TextObject hintText;
						this.CanAnnexCurrentSettlement = this.GetCanAnnexSettlementWithReason(this.AnnexCost, out hintText);
						this.AnnexHint.HintText = hintText;
						this.HasCost = true;
					}
				}
				base.IsAcceptableItemSelected = (this.CurrentSelectedSettlement != null);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00021CBC File Offset: 0x0001FEBC
		private bool GetCanAnnexSettlementWithReason(int annexCost, out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (Hero.MainHero.Clan.Influence < (float)annexCost)
			{
				disabledReason = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null);
				return false;
			}
			if (this.CurrentSelectedSettlement.Settlement.OwnerClan == this._kingdom.RulingClan)
			{
				disabledReason = GameTexts.FindText("str_cannot_annex_ruling_clan_settlement", null);
				return false;
			}
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				disabledReason = GameTexts.FindText("str_cannot_annex_while_mercenary", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00021D48 File Offset: 0x0001FF48
		public void SelectSettlement(Settlement settlement)
		{
			foreach (KingdomSettlementItemVM kingdomSettlementItemVM in this.Settlements)
			{
				if (kingdomSettlementItemVM.Settlement == settlement)
				{
					this.OnSettlementSelection(kingdomSettlementItemVM);
					break;
				}
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		private void OnSettlementSelection(KingdomSettlementItemVM settlement)
		{
			if (this._currentSelectedSettlement != settlement)
			{
				this.SetCurrentSelectedSettlement(settlement);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		private void ExecuteAnnex()
		{
			if (this._currentSelectedSettlement != null)
			{
				if (this._currenItemsUnresolvedDecision != null)
				{
					this._forceDecision(this._currenItemsUnresolvedDecision);
					return;
				}
				Settlement settlement = this._currentSelectedSettlement.Settlement;
				if (settlement.OwnerClan.Leader == Hero.MainHero)
				{
					this._onGrantFief(settlement);
					return;
				}
				if (Hero.MainHero.Clan.Influence >= (float)this.AnnexCost)
				{
					SettlementClaimantPreliminaryDecision settlementClaimantPreliminaryDecision = new SettlementClaimantPreliminaryDecision(Clan.PlayerClan, settlement);
					Clan.PlayerClan.Kingdom.AddDecision(settlementClaimantPreliminaryDecision, false);
					this._forceDecision(settlementClaimantPreliminaryDecision);
				}
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00021E54 File Offset: 0x00020054
		private KingdomDecision GetSettlementsAnyWaitingDecision(Settlement settlement)
		{
			KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault(delegate(KingdomDecision d)
			{
				SettlementClaimantDecision settlementClaimantDecision;
				return (settlementClaimantDecision = (d as SettlementClaimantDecision)) != null && settlementClaimantDecision.Settlement == settlement && !d.ShouldBeCancelled();
			});
			KingdomDecision kingdomDecision2 = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault(delegate(KingdomDecision d)
			{
				SettlementClaimantPreliminaryDecision settlementClaimantPreliminaryDecision;
				return (settlementClaimantPreliminaryDecision = (d as SettlementClaimantPreliminaryDecision)) != null && settlementClaimantPreliminaryDecision.Settlement == settlement && !d.ShouldBeCancelled();
			});
			return kingdomDecision ?? kingdomDecision2;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00021EB4 File Offset: 0x000200B4
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00021EBC File Offset: 0x000200BC
		[DataSourceProperty]
		public KingdomSettlementItemVM CurrentSelectedSettlement
		{
			get
			{
				return this._currentSelectedSettlement;
			}
			set
			{
				if (value != this._currentSelectedSettlement)
				{
					this._currentSelectedSettlement = value;
					base.OnPropertyChangedWithValue<KingdomSettlementItemVM>(value, "CurrentSelectedSettlement");
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00021EDA File Offset: 0x000200DA
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00021EE2 File Offset: 0x000200E2
		[DataSourceProperty]
		public KingdomSettlementSortControllerVM SettlementSortController
		{
			get
			{
				return this._settlementSortController;
			}
			set
			{
				if (value != this._settlementSortController)
				{
					this._settlementSortController = value;
					base.OnPropertyChangedWithValue<KingdomSettlementSortControllerVM>(value, "SettlementSortController");
				}
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00021F00 File Offset: 0x00020100
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00021F08 File Offset: 0x00020108
		[DataSourceProperty]
		public HintViewModel AnnexHint
		{
			get
			{
				return this._annexHint;
			}
			set
			{
				if (value != this._annexHint)
				{
					this._annexHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AnnexHint");
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00021F26 File Offset: 0x00020126
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00021F2E File Offset: 0x0002012E
		[DataSourceProperty]
		public string ProposeText
		{
			get
			{
				return this._proposeText;
			}
			set
			{
				if (value != this._proposeText)
				{
					this._proposeText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProposeText");
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00021F51 File Offset: 0x00020151
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00021F59 File Offset: 0x00020159
		[DataSourceProperty]
		public string AnnexActionExplanationText
		{
			get
			{
				return this._annexActionExplanationText;
			}
			set
			{
				if (value != this._annexActionExplanationText)
				{
					this._annexActionExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "AnnexActionExplanationText");
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00021F7C File Offset: 0x0002017C
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00021F84 File Offset: 0x00020184
		[DataSourceProperty]
		public string ProsperityText
		{
			get
			{
				return this._prosperityText;
			}
			set
			{
				if (value != this._prosperityText)
				{
					this._prosperityText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProsperityText");
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00021FA7 File Offset: 0x000201A7
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00021FAF File Offset: 0x000201AF
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

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00021FD2 File Offset: 0x000201D2
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x00021FDA File Offset: 0x000201DA
		[DataSourceProperty]
		public string OwnerText
		{
			get
			{
				return this._ownerText;
			}
			set
			{
				if (value != this._ownerText)
				{
					this._ownerText = value;
					base.OnPropertyChangedWithValue<string>(value, "OwnerText");
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00021FFD File Offset: 0x000201FD
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00022005 File Offset: 0x00020205
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

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00022028 File Offset: 0x00020228
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00022030 File Offset: 0x00020230
		[DataSourceProperty]
		public string ClanText
		{
			get
			{
				return this._clanText;
			}
			set
			{
				if (value != this._clanText)
				{
					this._clanText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClanText");
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00022053 File Offset: 0x00020253
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0002205B File Offset: 0x0002025B
		[DataSourceProperty]
		public string FoodText
		{
			get
			{
				return this._foodText;
			}
			set
			{
				if (value != this._foodText)
				{
					this._foodText = value;
					base.OnPropertyChangedWithValue<string>(value, "FoodText");
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002207E File Offset: 0x0002027E
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00022086 File Offset: 0x00020286
		[DataSourceProperty]
		public string GarrisonText
		{
			get
			{
				return this._garrisonText;
			}
			set
			{
				if (value != this._garrisonText)
				{
					this._garrisonText = value;
					base.OnPropertyChangedWithValue<string>(value, "GarrisonText");
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000220A9 File Offset: 0x000202A9
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x000220B1 File Offset: 0x000202B1
		[DataSourceProperty]
		public string MilitiaText
		{
			get
			{
				return this._militiaText;
			}
			set
			{
				if (value != this._militiaText)
				{
					this._militiaText = value;
					base.OnPropertyChangedWithValue<string>(value, "MilitiaText");
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000220D4 File Offset: 0x000202D4
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x000220DC File Offset: 0x000202DC
		[DataSourceProperty]
		public string AnnexText
		{
			get
			{
				return this._annexText;
			}
			set
			{
				if (value != this._annexText)
				{
					this._annexText = value;
					base.OnPropertyChangedWithValue<string>(value, "AnnexText");
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000220FF File Offset: 0x000202FF
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00022107 File Offset: 0x00020307
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002212A File Offset: 0x0002032A
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00022132 File Offset: 0x00020332
		[DataSourceProperty]
		public int AnnexCost
		{
			get
			{
				return this._annexCost;
			}
			set
			{
				if (value != this._annexCost)
				{
					this._annexCost = value;
					base.OnPropertyChangedWithValue(value, "AnnexCost");
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00022150 File Offset: 0x00020350
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00022158 File Offset: 0x00020358
		[DataSourceProperty]
		public string DefendersText
		{
			get
			{
				return this._defendersText;
			}
			set
			{
				if (value != this._defendersText)
				{
					this._defendersText = value;
					base.OnPropertyChangedWithValue<string>(value, "DefendersText");
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002217B File Offset: 0x0002037B
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00022183 File Offset: 0x00020383
		[DataSourceProperty]
		public MBBindingList<KingdomSettlementItemVM> Settlements
		{
			get
			{
				return this._settlements;
			}
			set
			{
				if (value != this._settlements)
				{
					this._settlements = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomSettlementItemVM>>(value, "Settlements");
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000221A1 File Offset: 0x000203A1
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000221A9 File Offset: 0x000203A9
		[DataSourceProperty]
		public bool CanAnnexCurrentSettlement
		{
			get
			{
				return this._canAnnexCurrentSettlement;
			}
			set
			{
				if (value != this._canAnnexCurrentSettlement)
				{
					this._canAnnexCurrentSettlement = value;
					base.OnPropertyChangedWithValue(value, "CanAnnexCurrentSettlement");
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000221C7 File Offset: 0x000203C7
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000221CF File Offset: 0x000203CF
		[DataSourceProperty]
		public bool HasCost
		{
			get
			{
				return this._hasCost;
			}
			set
			{
				if (value != this._hasCost)
				{
					this._hasCost = value;
					base.OnPropertyChangedWithValue(value, "HasCost");
				}
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000221ED File Offset: 0x000203ED
		private static int CalculateLikelihood(Settlement settlement)
		{
			return MathF.Round(new KingdomElection(new SettlementClaimantPreliminaryDecision(Clan.PlayerClan, settlement)).GetLikelihoodForSponsor(Clan.PlayerClan) * 100f);
		}

		// Token: 0x04000365 RID: 869
		private readonly Action<KingdomDecision> _forceDecision;

		// Token: 0x04000366 RID: 870
		private readonly Action<Settlement> _onGrantFief;

		// Token: 0x04000367 RID: 871
		private readonly Kingdom _kingdom;

		// Token: 0x04000368 RID: 872
		private KingdomDecision _currenItemsUnresolvedDecision;

		// Token: 0x04000369 RID: 873
		private MBBindingList<KingdomSettlementItemVM> _settlements;

		// Token: 0x0400036A RID: 874
		private KingdomSettlementItemVM _currentSelectedSettlement;

		// Token: 0x0400036B RID: 875
		private HintViewModel _annexHint;

		// Token: 0x0400036C RID: 876
		private string _ownerText;

		// Token: 0x0400036D RID: 877
		private string _nameText;

		// Token: 0x0400036E RID: 878
		private string _typeText;

		// Token: 0x0400036F RID: 879
		private string _prosperityText;

		// Token: 0x04000370 RID: 880
		private string _foodText;

		// Token: 0x04000371 RID: 881
		private string _garrisonText;

		// Token: 0x04000372 RID: 882
		private string _militiaText;

		// Token: 0x04000373 RID: 883
		private string _annexText;

		// Token: 0x04000374 RID: 884
		private string _clanText;

		// Token: 0x04000375 RID: 885
		private string _villagesText;

		// Token: 0x04000376 RID: 886
		private string _annexActionExplanationText;

		// Token: 0x04000377 RID: 887
		private string _proposeText;

		// Token: 0x04000378 RID: 888
		private string _defendersText;

		// Token: 0x04000379 RID: 889
		private int _annexCost;

		// Token: 0x0400037A RID: 890
		private bool _canAnnexCurrentSettlement;

		// Token: 0x0400037B RID: 891
		private bool _hasCost;

		// Token: 0x0400037C RID: 892
		private KingdomSettlementSortControllerVM _settlementSortController;
	}
}
