using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Policies;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement
{
	// Token: 0x02000059 RID: 89
	public class KingdomManagementVM : ViewModel
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001FA14 File Offset: 0x0001DC14
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001FA1C File Offset: 0x0001DC1C
		public Kingdom Kingdom { get; private set; }

		// Token: 0x06000716 RID: 1814 RVA: 0x0001FA28 File Offset: 0x0001DC28
		public KingdomManagementVM(Action onClose, Action onManageArmy, Action<Army> onShowArmyOnMap)
		{
			this._onClose = onClose;
			this._onShowArmyOnMap = onShowArmyOnMap;
			this.Army = new KingdomArmyVM(onManageArmy, new Action(this.OnRefreshDecision), this._onShowArmyOnMap);
			this.Settlement = new KingdomSettlementVM(new Action<KingdomDecision>(this.ForceDecideDecision), new Action<Settlement>(this.OnGrantFief));
			this.Clan = new KingdomClanVM(new Action<KingdomDecision>(this.ForceDecideDecision));
			this.Policy = new KingdomPoliciesVM(new Action<KingdomDecision>(this.ForceDecideDecision));
			this.Diplomacy = new KingdomDiplomacyVM(new Action<KingdomDecision>(this.ForceDecideDecision));
			this.GiftFief = new KingdomGiftFiefPopupVM(new Action(this.OnSettlementGranted));
			this.Decision = new KingdomDecisionsVM(new Action(this.OnRefresh));
			this._categoryCount = 5;
			this._leaveKingdomPermissionEvent = new LeaveKingdomPermissionEvent(new Action<bool, TextObject>(this.OnLeaveKingdomRequest));
			this.SetSelectedCategory(1);
			this.RefreshValues();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001FB28 File Offset: 0x0001DD28
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.LeaderText = GameTexts.FindText("str_sort_by_leader_name_label", null).ToString();
			this.ClansText = GameTexts.FindText("str_encyclopedia_clans", null).ToString();
			this.FiefsText = GameTexts.FindText("str_fiefs", null).ToString();
			this.PoliciesText = GameTexts.FindText("str_policies", null).ToString();
			this.ArmiesText = GameTexts.FindText("str_armies", null).ToString();
			this.DiplomacyText = GameTexts.FindText("str_diplomatic_group", null).ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.ChangeKingdomNameHint = new HintViewModel();
			this.RefreshDynamicKingdomProperties();
			this.Army.RefreshValues();
			this.Policy.RefreshValues();
			this.Clan.RefreshValues();
			this.Settlement.RefreshValues();
			this.Diplomacy.RefreshValues();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001FC20 File Offset: 0x0001DE20
		private void RefreshDynamicKingdomProperties()
		{
			this.Name = ((Hero.MainHero.MapFaction == null) ? new TextObject("{=kQsXUvgO}You are not under a kingdom.", null).ToString() : Hero.MainHero.MapFaction.Name.ToString());
			this.PlayerHasKingdom = (Hero.MainHero.MapFaction is Kingdom);
			if (this.PlayerHasKingdom)
			{
				this.Kingdom = (Hero.MainHero.MapFaction as Kingdom);
				this.Leader = new HeroVM(this.Kingdom.Leader, false);
				this.KingdomBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.Kingdom.Banner), true);
				Kingdom kingdom = this.Kingdom;
				this._isPlayerTheRuler = (((kingdom != null) ? kingdom.Leader : null) == Hero.MainHero);
				TextObject hintText;
				this.PlayerCanChangeKingdomName = this.GetCanChangeKingdomNameWithReason(out hintText);
				this.ChangeKingdomNameHint.HintText = hintText;
			}
			this.KingdomActionText = (this._isPlayerTheRuler ? GameTexts.FindText("str_abdicate_leadership", null).ToString() : GameTexts.FindText("str_leave_kingdom", null).ToString());
			List<TextObject> kingdomActionDisabledReasons;
			this.IsKingdomActionEnabled = this.GetIsKingdomActionEnabledWithReason(this._isPlayerTheRuler, out kingdomActionDisabledReasons);
			this.KingdomActionHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetHintTextFromReasons(kingdomActionDisabledReasons));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001FD70 File Offset: 0x0001DF70
		private bool GetCanChangeKingdomNameWithReason(out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (!this._isPlayerTheRuler)
			{
				disabledReason = new TextObject("{=HFZdseH9}Only the ruler of the kingdom can change it's name.", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		private bool GetIsKingdomActionEnabledWithReason(bool isPlayerTheRuler, out List<TextObject> disabledReasons)
		{
			disabledReasons = new List<TextObject>();
			TextObject item;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out item))
			{
				disabledReasons.Add(item);
				return false;
			}
			List<TextObject> collection;
			if (isPlayerTheRuler && !Campaign.Current.Models.KingdomCreationModel.IsPlayerKingdomAbdicationPossible(out collection))
			{
				disabledReasons.AddRange(collection);
				return false;
			}
			if (!isPlayerTheRuler && MobileParty.MainParty.Army != null)
			{
				disabledReasons.Add(new TextObject("{=4Y8u4JKO}You can't leave the kingdom while in an army", null));
				return false;
			}
			Game.Current.EventManager.TriggerEvent<LeaveKingdomPermissionEvent>(this._leaveKingdomPermissionEvent);
			if (this._mostRecentLeaveKingdomPermission != null && !this._mostRecentLeaveKingdomPermission.GetValueOrDefault().Item1)
			{
				disabledReasons.Add((this._mostRecentLeaveKingdomPermission != null) ? this._mostRecentLeaveKingdomPermission.GetValueOrDefault().Item2 : null);
				return false;
			}
			return true;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001FE77 File Offset: 0x0001E077
		public void OnRefresh()
		{
			this.RefreshDynamicKingdomProperties();
			this.Army.RefreshArmyList();
			this.Policy.RefreshPolicyList();
			this.Clan.RefreshClan();
			this.Settlement.RefreshSettlementList();
			this.Diplomacy.RefreshDiplomacyList();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001FEB6 File Offset: 0x0001E0B6
		public void OnFrameTick()
		{
			KingdomDecisionsVM decision = this.Decision;
			if (decision == null)
			{
				return;
			}
			decision.OnFrameTick();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		private void OnRefreshDecision()
		{
			this.Decision.HandleNextDecision();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001FED5 File Offset: 0x0001E0D5
		private void ForceDecideDecision(KingdomDecision decision)
		{
			this.Decision.RefreshWith(decision);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		private void OnGrantFief(Settlement settlement)
		{
			if (this.Kingdom.Leader == Hero.MainHero)
			{
				this.GiftFief.OpenWith(settlement);
				return;
			}
			string titleText = new TextObject("{=eIGFuGOx}Give Settlement", null).ToString();
			string text = new TextObject("{=rkubGa4K}Are you sure want to give this settlement back to your kingdom?", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(titleText, text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
			{
				Campaign.Current.KingdomManager.RelinquishSettlementOwnership(settlement);
				this.ForceDecideDecision(this.Kingdom.UnresolvedDecisions[this.Kingdom.UnresolvedDecisions.Count - 1]);
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001FF93 File Offset: 0x0001E193
		private void OnSettlementGranted()
		{
			this.Settlement.RefreshSettlementList();
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public void ExecuteClose()
		{
			this._onClose();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001FFAD File Offset: 0x0001E1AD
		private void ExecuteShowClan()
		{
			this.SetSelectedCategory(0);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001FFB6 File Offset: 0x0001E1B6
		private void ExecuteShowFiefs()
		{
			this.SetSelectedCategory(1);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001FFBF File Offset: 0x0001E1BF
		private void ExecuteShowPolicies()
		{
			if (this.PlayerHasKingdom)
			{
				this.SetSelectedCategory(2);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		private void ExecuteShowDiplomacy()
		{
			if (this.PlayerHasKingdom)
			{
				this.SetSelectedCategory(4);
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001FFE1 File Offset: 0x0001E1E1
		private void ExecuteShowArmy()
		{
			this.SetSelectedCategory(3);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		private void ExecuteKingdomAction()
		{
			if (this.IsKingdomActionEnabled)
			{
				if (this._mostRecentLeaveKingdomPermission != null && this._mostRecentLeaveKingdomPermission.GetValueOrDefault().Item1 && ((this._mostRecentLeaveKingdomPermission != null) ? this._mostRecentLeaveKingdomPermission.GetValueOrDefault().Item2 : null) != null)
				{
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=3sxtCWPe}Leaving Kingdom", null).ToString(), (this._mostRecentLeaveKingdomPermission != null) ? this._mostRecentLeaveKingdomPermission.GetValueOrDefault().Item2.ToString() : null, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.OnConfirmLeaveKingdom), null, "", 0f, null, null, null), false, false);
					return;
				}
				if (this._isPlayerTheRuler)
				{
					GameTexts.SetVariable("WILL_DESTROY", (this.Kingdom.Clans.Count == 1) ? 1 : 0);
					InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_abdicate_leadership", null).ToString(), GameTexts.FindText("str_abdicate_leadership_question", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.OnConfirmAbdicateLeadership), null, "", 0f, null, null, null), false, false);
					return;
				}
				if (TaleWorlds.CampaignSystem.Clan.PlayerClan.Settlements.Count == 0)
				{
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=3sxtCWPe}Leaving Kingdom", null).ToString(), new TextObject("{=BgqZWbga}The nobles of the realm will dislike you for abandoning your fealty. Are you sure you want to leave the Kingdom?", null).ToString(), true, true, new TextObject("{=5Unqsx3N}Confirm", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(this.OnConfirmLeaveKingdom), null, "", 0f, null, null, null), false, false);
					return;
				}
				List<InquiryElement> inquiryElements = new List<InquiryElement>
				{
					new InquiryElement("keep", new TextObject("{=z8h0BRAb}Keep all holdings", null).ToString(), null, true, new TextObject("{=lkJfq1ap}Owned settlements remain under your control but nobles will dislike this dishonorable act and the kingdom will declare war on you.", null).ToString()),
					new InquiryElement("dontkeep", new TextObject("{=JIr3Jc7b}Relinquish all holdings", null).ToString(), null, true, new TextObject("{=ZjaSde0X}Owned settlements are returned to the kingdom. This will avert a war and nobles will dislike you less for abandoning your fealty.", null).ToString())
				};
				MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("{=3sxtCWPe}Leaving Kingdom", null).ToString(), new TextObject("{=xtlIFKaa}Are you sure you want to leave the Kingdom?{newline}If so, choose how you want to leave the kingdom.", null).ToString(), inquiryElements, true, 1, 1, new TextObject("{=5Unqsx3N}Confirm", null).ToString(), string.Empty, new Action<List<InquiryElement>>(this.OnConfirmLeaveKingdomWithOption), null, "", false), false, false);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0002028E File Offset: 0x0001E48E
		private void OnLeaveKingdomRequest(bool isPossible, TextObject disabledReasonOrWarning)
		{
			this._mostRecentLeaveKingdomPermission = new ValueTuple<bool, TextObject>?(new ValueTuple<bool, TextObject>(isPossible, disabledReasonOrWarning));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000202A4 File Offset: 0x0001E4A4
		private void OnConfirmAbdicateLeadership()
		{
			Campaign.Current.KingdomManager.AbdicateTheThrone(this.Kingdom);
			KingdomDecision kingdomDecision = this.Kingdom.UnresolvedDecisions.LastOrDefault<KingdomDecision>();
			if (kingdomDecision != null)
			{
				this.ForceDecideDecision(kingdomDecision);
				return;
			}
			this.ExecuteClose();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000202E8 File Offset: 0x0001E4E8
		private void OnConfirmLeaveKingdomWithOption(List<InquiryElement> obj)
		{
			InquiryElement inquiryElement = obj.FirstOrDefault<InquiryElement>();
			if (inquiryElement != null)
			{
				string a = inquiryElement.Identifier as string;
				if (a == "keep")
				{
					ChangeKingdomAction.ApplyByLeaveWithRebellionAgainstKingdom(TaleWorlds.CampaignSystem.Clan.PlayerClan, true);
				}
				else if (a == "dontkeep")
				{
					ChangeKingdomAction.ApplyByLeaveKingdom(TaleWorlds.CampaignSystem.Clan.PlayerClan, true);
				}
				this.ExecuteClose();
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00020343 File Offset: 0x0001E543
		private void OnConfirmLeaveKingdom()
		{
			if (TaleWorlds.CampaignSystem.Clan.PlayerClan.IsUnderMercenaryService)
			{
				ChangeKingdomAction.ApplyByLeaveKingdomAsMercenary(TaleWorlds.CampaignSystem.Clan.PlayerClan, true);
			}
			else
			{
				ChangeKingdomAction.ApplyByLeaveKingdom(TaleWorlds.CampaignSystem.Clan.PlayerClan, true);
			}
			this.ExecuteClose();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00020370 File Offset: 0x0001E570
		private void ExecuteChangeKingdomName()
		{
			InformationManager.ShowTextInquiry(new TextInquiryData(GameTexts.FindText("str_change_kingdom_name", null).ToString(), string.Empty, true, true, GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action<string>(this.OnChangeKingdomNameDone), null, false, new Func<string, Tuple<bool, string>>(FactionHelper.IsKingdomNameApplicable), "", ""), false, false);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000203E4 File Offset: 0x0001E5E4
		private void OnChangeKingdomNameDone(string newKingdomName)
		{
			TextObject variable = new TextObject(newKingdomName, null);
			TextObject textObject = GameTexts.FindText("str_generic_kingdom_name", null);
			TextObject textObject2 = GameTexts.FindText("str_generic_kingdom_short_name", null);
			textObject.SetTextVariable("KINGDOM_NAME", variable);
			textObject2.SetTextVariable("KINGDOM_SHORT_NAME", variable);
			this.Kingdom.ChangeKingdomName(textObject, textObject2);
			this.OnRefresh();
			this.RefreshValues();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00020444 File Offset: 0x0001E644
		public void SelectArmy(Army army)
		{
			this.SetSelectedCategory(3);
			this.Army.SelectArmy(army);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00020459 File Offset: 0x0001E659
		public void SelectSettlement(Settlement settlement)
		{
			this.SetSelectedCategory(1);
			this.Settlement.SelectSettlement(settlement);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002046E File Offset: 0x0001E66E
		public void SelectClan(Clan clan)
		{
			this.SetSelectedCategory(0);
			this.Clan.SelectClan(clan);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00020483 File Offset: 0x0001E683
		public void SelectPolicy(PolicyObject policy)
		{
			this.SetSelectedCategory(2);
			this.Policy.SelectPolicy(policy);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00020498 File Offset: 0x0001E698
		public void SelectKingdom(Kingdom kingdom)
		{
			this.SetSelectedCategory(4);
			this.Diplomacy.SelectKingdom(kingdom);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000204B0 File Offset: 0x0001E6B0
		public void SelectPreviousCategory()
		{
			int selectedCategory = (this._currentCategory == 0) ? (this._categoryCount - 1) : (this._currentCategory - 1);
			this.SetSelectedCategory(selectedCategory);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000204E0 File Offset: 0x0001E6E0
		public void SelectNextCategory()
		{
			int selectedCategory = (this._currentCategory + 1) % this._categoryCount;
			this.SetSelectedCategory(selectedCategory);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00020504 File Offset: 0x0001E704
		private void SetSelectedCategory(int index)
		{
			this.Clan.Show = false;
			this.Settlement.Show = false;
			this.Policy.Show = false;
			this.Army.Show = false;
			this.Diplomacy.Show = false;
			this._currentCategory = index;
			if (index == 0)
			{
				this.Clan.Show = true;
				return;
			}
			if (index == 1)
			{
				this.Settlement.Show = true;
				return;
			}
			if (index == 2)
			{
				this.Policy.Show = true;
				return;
			}
			if (index == 3)
			{
				this.Army.Show = true;
				return;
			}
			this._currentCategory = 4;
			this.Diplomacy.Show = true;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000205AA File Offset: 0x0001E7AA
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.PreviousTabInputKey.OnFinalize();
			this.NextTabInputKey.OnFinalize();
			this.Decision.OnFinalize();
			this.Clan.OnFinalize();
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000205E9 File Offset: 0x0001E7E9
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000205F1 File Offset: 0x0001E7F1
		[DataSourceProperty]
		public BasicTooltipViewModel KingdomActionHint
		{
			get
			{
				return this._kingdomActionHint;
			}
			set
			{
				if (value != this._kingdomActionHint)
				{
					this._kingdomActionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "KingdomActionHint");
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0002060F File Offset: 0x0001E80F
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00020617 File Offset: 0x0001E817
		[DataSourceProperty]
		public ImageIdentifierVM KingdomBanner
		{
			get
			{
				return this._kingdomBanner;
			}
			set
			{
				if (value != this._kingdomBanner)
				{
					this._kingdomBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "KingdomBanner");
				}
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00020635 File Offset: 0x0001E835
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0002063D File Offset: 0x0001E83D
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0002065B File Offset: 0x0001E85B
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00020663 File Offset: 0x0001E863
		[DataSourceProperty]
		public KingdomArmyVM Army
		{
			get
			{
				return this._army;
			}
			set
			{
				if (value != this._army)
				{
					this._army = value;
					base.OnPropertyChangedWithValue<KingdomArmyVM>(value, "Army");
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00020681 File Offset: 0x0001E881
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00020689 File Offset: 0x0001E889
		[DataSourceProperty]
		public KingdomSettlementVM Settlement
		{
			get
			{
				return this._settlement;
			}
			set
			{
				if (value != this._settlement)
				{
					this._settlement = value;
					base.OnPropertyChangedWithValue<KingdomSettlementVM>(value, "Settlement");
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000206A7 File Offset: 0x0001E8A7
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x000206AF File Offset: 0x0001E8AF
		[DataSourceProperty]
		public KingdomClanVM Clan
		{
			get
			{
				return this._clan;
			}
			set
			{
				if (value != this._clan)
				{
					this._clan = value;
					base.OnPropertyChangedWithValue<KingdomClanVM>(value, "Clan");
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000206CD File Offset: 0x0001E8CD
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x000206D5 File Offset: 0x0001E8D5
		[DataSourceProperty]
		public KingdomPoliciesVM Policy
		{
			get
			{
				return this._policy;
			}
			set
			{
				if (value != this._policy)
				{
					this._policy = value;
					base.OnPropertyChangedWithValue<KingdomPoliciesVM>(value, "Policy");
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000206F3 File Offset: 0x0001E8F3
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000206FB File Offset: 0x0001E8FB
		[DataSourceProperty]
		public KingdomDiplomacyVM Diplomacy
		{
			get
			{
				return this._diplomacy;
			}
			set
			{
				if (value != this._diplomacy)
				{
					this._diplomacy = value;
					base.OnPropertyChangedWithValue<KingdomDiplomacyVM>(value, "Diplomacy");
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00020719 File Offset: 0x0001E919
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00020721 File Offset: 0x0001E921
		[DataSourceProperty]
		public KingdomGiftFiefPopupVM GiftFief
		{
			get
			{
				return this._giftFief;
			}
			set
			{
				if (value != this._giftFief)
				{
					this._giftFief = value;
					base.OnPropertyChangedWithValue<KingdomGiftFiefPopupVM>(value, "GiftFief");
				}
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0002073F File Offset: 0x0001E93F
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00020747 File Offset: 0x0001E947
		[DataSourceProperty]
		public KingdomDecisionsVM Decision
		{
			get
			{
				return this._decision;
			}
			set
			{
				if (value != this._decision)
				{
					this._decision = value;
					base.OnPropertyChangedWithValue<KingdomDecisionsVM>(value, "Decision");
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00020765 File Offset: 0x0001E965
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0002076D File Offset: 0x0001E96D
		[DataSourceProperty]
		public HintViewModel ChangeKingdomNameHint
		{
			get
			{
				return this._changeKingdomNameHint;
			}
			set
			{
				if (value != this._changeKingdomNameHint)
				{
					this._changeKingdomNameHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ChangeKingdomNameHint");
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0002078B File Offset: 0x0001E98B
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x00020793 File Offset: 0x0001E993
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

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000207B6 File Offset: 0x0001E9B6
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x000207BE File Offset: 0x0001E9BE
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000207DC File Offset: 0x0001E9DC
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x000207E4 File Offset: 0x0001E9E4
		[DataSourceProperty]
		public bool PlayerHasKingdom
		{
			get
			{
				return this._playerHasKingdom;
			}
			set
			{
				if (value != this._playerHasKingdom)
				{
					this._playerHasKingdom = value;
					base.OnPropertyChangedWithValue(value, "PlayerHasKingdom");
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00020802 File Offset: 0x0001EA02
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0002080A File Offset: 0x0001EA0A
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00020828 File Offset: 0x0001EA28
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00020830 File Offset: 0x0001EA30
		[DataSourceProperty]
		public bool PlayerCanChangeKingdomName
		{
			get
			{
				return this._playerCanChangeKingdomName;
			}
			set
			{
				if (value != this._playerCanChangeKingdomName)
				{
					this._playerCanChangeKingdomName = value;
					base.OnPropertyChangedWithValue(value, "PlayerCanChangeKingdomName");
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0002084E File Offset: 0x0001EA4E
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x00020856 File Offset: 0x0001EA56
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00020879 File Offset: 0x0001EA79
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00020881 File Offset: 0x0001EA81
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x000208A4 File Offset: 0x0001EAA4
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x000208AC File Offset: 0x0001EAAC
		[DataSourceProperty]
		public string ClansText
		{
			get
			{
				return this._clansText;
			}
			set
			{
				if (value != this._clansText)
				{
					this._clansText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClansText");
				}
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000208CF File Offset: 0x0001EACF
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x000208D7 File Offset: 0x0001EAD7
		[DataSourceProperty]
		public string DiplomacyText
		{
			get
			{
				return this._diplomacyText;
			}
			set
			{
				if (value != this._diplomacyText)
				{
					this._diplomacyText = value;
					base.OnPropertyChangedWithValue<string>(value, "DiplomacyText");
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000208FA File Offset: 0x0001EAFA
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00020902 File Offset: 0x0001EB02
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

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00020925 File Offset: 0x0001EB25
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0002092D File Offset: 0x0001EB2D
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00020950 File Offset: 0x0001EB50
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00020958 File Offset: 0x0001EB58
		[DataSourceProperty]
		public string PoliciesText
		{
			get
			{
				return this._policiesText;
			}
			set
			{
				if (value != this._policiesText)
				{
					this._policiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "PoliciesText");
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0002097B File Offset: 0x0001EB7B
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00020983 File Offset: 0x0001EB83
		[DataSourceProperty]
		public string ArmiesText
		{
			get
			{
				return this._armiesText;
			}
			set
			{
				if (value != this._armiesText)
				{
					this._armiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "ArmiesText");
				}
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000209A6 File Offset: 0x0001EBA6
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
			this.Decision.SetDoneInputKey(hotkey);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000209C1 File Offset: 0x0001EBC1
		public void SetPreviousTabInputKey(HotKey hotkey)
		{
			this.PreviousTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000209D0 File Offset: 0x0001EBD0
		public void SetNextTabInputKey(HotKey hotkey)
		{
			this.NextTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000209DF File Offset: 0x0001EBDF
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x000209E7 File Offset: 0x0001EBE7
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00020A05 File Offset: 0x0001EC05
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x00020A0D File Offset: 0x0001EC0D
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00020A2B File Offset: 0x0001EC2B
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00020A33 File Offset: 0x0001EC33
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

		// Token: 0x04000320 RID: 800
		private readonly Action _onClose;

		// Token: 0x04000321 RID: 801
		private readonly Action<Army> _onShowArmyOnMap;

		// Token: 0x04000322 RID: 802
		private readonly int _categoryCount;

		// Token: 0x04000323 RID: 803
		private readonly LeaveKingdomPermissionEvent _leaveKingdomPermissionEvent;

		// Token: 0x04000324 RID: 804
		private ValueTuple<bool, TextObject>? _mostRecentLeaveKingdomPermission;

		// Token: 0x04000325 RID: 805
		private int _currentCategory;

		// Token: 0x04000326 RID: 806
		private bool _isPlayerTheRuler;

		// Token: 0x04000328 RID: 808
		private KingdomArmyVM _army;

		// Token: 0x04000329 RID: 809
		private KingdomSettlementVM _settlement;

		// Token: 0x0400032A RID: 810
		private KingdomClanVM _clan;

		// Token: 0x0400032B RID: 811
		private KingdomPoliciesVM _policy;

		// Token: 0x0400032C RID: 812
		private KingdomDiplomacyVM _diplomacy;

		// Token: 0x0400032D RID: 813
		private KingdomGiftFiefPopupVM _giftFief;

		// Token: 0x0400032E RID: 814
		private ImageIdentifierVM _kingdomBanner;

		// Token: 0x0400032F RID: 815
		private HeroVM _leader;

		// Token: 0x04000330 RID: 816
		private KingdomDecisionsVM _decision;

		// Token: 0x04000331 RID: 817
		private HintViewModel _changeKingdomNameHint;

		// Token: 0x04000332 RID: 818
		private string _name;

		// Token: 0x04000333 RID: 819
		private bool _canSwitchTabs;

		// Token: 0x04000334 RID: 820
		private bool _playerHasKingdom;

		// Token: 0x04000335 RID: 821
		private bool _isKingdomActionEnabled;

		// Token: 0x04000336 RID: 822
		private bool _playerCanChangeKingdomName;

		// Token: 0x04000337 RID: 823
		private string _kingdomActionText;

		// Token: 0x04000338 RID: 824
		private string _leaderText;

		// Token: 0x04000339 RID: 825
		private string _clansText;

		// Token: 0x0400033A RID: 826
		private string _fiefsText;

		// Token: 0x0400033B RID: 827
		private string _policiesText;

		// Token: 0x0400033C RID: 828
		private string _armiesText;

		// Token: 0x0400033D RID: 829
		private string _diplomacyText;

		// Token: 0x0400033E RID: 830
		private string _doneText;

		// Token: 0x0400033F RID: 831
		private BasicTooltipViewModel _kingdomActionHint;

		// Token: 0x04000340 RID: 832
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000341 RID: 833
		private InputKeyItemVM _previousTabInputKey;

		// Token: 0x04000342 RID: 834
		private InputKeyItemVM _nextTabInputKey;
	}
}
