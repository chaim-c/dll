using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A3 RID: 163
	public class CaravanConversationsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000329D8 File Offset: 0x00030BD8
		private int SmallCaravanFormingCost
		{
			get
			{
				return Campaign.Current.Models.PartyTradeModel.SmallCaravanFormingCostForPlayer;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000329EE File Offset: 0x00030BEE
		private int LargeCaravanFormingCost
		{
			get
			{
				return Campaign.Current.Models.PartyTradeModel.LargeCaravanFormingCostForPlayer;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00032A04 File Offset: 0x00030C04
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00032A1D File Offset: 0x00030C1D
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00032A1F File Offset: 0x00030C1F
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00032A28 File Offset: 0x00030C28
		protected void AddDialogs(CampaignGameStarter starter)
		{
			starter.AddPlayerLine("caravan_create_conversation_1", "hero_main_options", "magistrate_form_a_caravan_cost", "{=tuz8ZNT6}I wish to form a caravan in this town.", new ConversationSentence.OnConditionDelegate(this.conversation_caravan_build_on_condition), null, 100, new ConversationSentence.OnClickableConditionDelegate(this.conversation_caravan_build_clickable_condition), null);
			starter.AddDialogLine("caravan_create_conversation_2", "magistrate_form_a_caravan_cost", "magistrate_form_a_caravan_player_answer", "{=cZptYTYd}Well.. There are many goods around the town that can bring good money if you trade them. A caravan you formed will do this for you. You need to pay at least {AMOUNT}{GOLD_ICON} to hire {NUMBER_OF_MEN} caravan guards to form a caravan and you need one companion to lead the caravan guards.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_caravan_cost_on_condition), null, 100, null);
			starter.AddPlayerLine("caravan_create_conversation_3", "magistrate_form_a_caravan_player_answer", "lord_pretalk", "{=otVPaR6T}Actually I do not have a free companion right now.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_caravan_companion_condition), null, 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_4", "magistrate_form_a_caravan_player_answer", "lord_pretalk", "{=w6WFuDn0}I am sorry, I don't have that much money.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_caravan_gold_condition), null, 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_5", "magistrate_form_a_caravan_player_answer", "magistrate_form_a_caravan_accepted", "{=zOp48Fsg}I accept these conditions and I am ready to pay {AMOUNT}{GOLD_ICON} to create a caravan.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_small_caravan_accept_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_magistrate_form_a_small_caravan_accept_on_consequence), 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_6", "magistrate_form_a_caravan_player_answer", "magistrate_form_a_caravan_big", "{=4mhOs9Fb}Is there a way to form a caravan includes better troops?", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_small_caravan_accept_on_condition), null, 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_10", "magistrate_form_a_caravan_player_answer", "lord_pretalk", "{=2mJjDTAZ}That sounds expensive.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_caravan_reject_on_condition), null, 100, null, null);
			starter.AddDialogLine("caravan_create_conversation_7", "magistrate_form_a_caravan_big", "magistrate_form_a_caravan_big_player_answer", "{=DaBzJkIz}I can increase quality of troops, but cost will proportionally increase, too. It will cost {AMOUNT}{GOLD_ICON}.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_big_caravan_offer_condition), null, 100, null);
			starter.AddPlayerLine("caravan_create_conversation_8", "magistrate_form_a_caravan_big_player_answer", "magistrate_form_a_caravan_accepted", "{=AuMLELpp}Okay then lets go with better troops, I am ready to pay {AMOUNT}{GOLD_ICON} to create a caravan.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_big_caravan_accept_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_magistrate_form_a_big_caravan_accept_on_consequence), 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_9", "magistrate_form_a_caravan_big_player_answer", "lord_pretalk", "{=w6WFuDn0}I am sorry, I don't have that much money.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_big_caravan_gold_condition), null, 100, null, null);
			starter.AddPlayerLine("caravan_create_conversation_10_2", "magistrate_form_a_caravan_big_player_answer", "lord_pretalk", "{=2mJjDTAZ}That sounds expensive.", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_big_caravan_reject_on_condition), null, 100, null, null);
			starter.AddDialogLine("caravan_create_conversation_11", "magistrate_form_a_caravan_accepted", "magistrate_form_a_caravan_accepted_choose_leader", "{=aeCYFe1g}Whom do you want to lead the caravan?", null, null, 100, null);
			starter.AddRepeatablePlayerLine("caravan_create_conversation_12", "magistrate_form_a_caravan_accepted_choose_leader", "magistrate_form_a_caravan_accepted_leader_is_chosen", "{=!}{HERO.NAME}", "{=UNFE1BeG}I am thinking of a different person", "magistrate_form_a_caravan_accepted", new ConversationSentence.OnConditionDelegate(this.conversation_magistrate_form_a_caravan_accepted_choose_leader_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_magistrate_form_a_caravan_accept_on_consequence), 100, null);
			starter.AddPlayerLine("caravan_create_conversation_13", "magistrate_form_a_caravan_accepted_choose_leader", "lord_pretalk", "{=jOrPxxRM}Actually, never mind.", null, null, 100, null, null);
			starter.AddDialogLine("caravan_create_conversation_14", "magistrate_form_a_caravan_accepted_leader_is_chosen", "close_window", "{=Z2Lq2QLq}Ok then. I will call my men to help you form a caravan. I hope it brings you a good profit.", null, null, 100, null);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00032CD9 File Offset: 0x00030ED9
		private bool conversation_caravan_build_on_condition()
		{
			return Hero.OneToOneConversationHero != null && (Hero.OneToOneConversationHero.IsMerchant || Hero.OneToOneConversationHero.IsArtisan);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00032CFC File Offset: 0x00030EFC
		private bool conversation_caravan_build_clickable_condition(out TextObject explanation)
		{
			if (Campaign.Current.IsMainHeroDisguised)
			{
				explanation = new TextObject("{=jcEoUPCB}You are in disguise.", null);
				return false;
			}
			explanation = null;
			return true;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00032D1D File Offset: 0x00030F1D
		private bool conversation_magistrate_form_a_caravan_cost_on_condition()
		{
			MBTextManager.SetTextVariable("AMOUNT", this.SmallCaravanFormingCost);
			MBTextManager.SetTextVariable("NUMBER_OF_MEN", 29);
			return true;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00032D3C File Offset: 0x00030F3C
		private bool conversation_magistrate_form_caravan_companion_condition()
		{
			return this.FindSuitableCompanionsToLeadCaravan().Count == 0;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00032D4C File Offset: 0x00030F4C
		private bool conversation_magistrate_form_caravan_gold_condition()
		{
			return this.FindSuitableCompanionsToLeadCaravan().Count > 0 && Hero.MainHero.Gold < this.SmallCaravanFormingCost;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00032D70 File Offset: 0x00030F70
		private bool conversation_magistrate_form_a_small_caravan_accept_on_condition()
		{
			MBTextManager.SetTextVariable("AMOUNT", this.SmallCaravanFormingCost);
			MBTextManager.SetTextVariable("NUMBER_OF_MEN", 29);
			return this.FindSuitableCompanionsToLeadCaravan().Count > 0 && Hero.MainHero.Gold >= this.SmallCaravanFormingCost;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00032DBE File Offset: 0x00030FBE
		private void conversation_magistrate_form_a_small_caravan_accept_on_consequence()
		{
			this._selectedCaravanType = 0;
			this.conversation_magistrate_form_a_caravan_accepted_on_consequence();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00032DCD File Offset: 0x00030FCD
		private void conversation_magistrate_form_a_caravan_accepted_on_consequence()
		{
			ConversationSentence.SetObjectsToRepeatOver(this.FindSuitableCompanionsToLeadCaravan(), 5);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00032DDB File Offset: 0x00030FDB
		private bool conversation_magistrate_form_a_caravan_reject_on_condition()
		{
			return Hero.MainHero.Gold >= this.SmallCaravanFormingCost;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00032DF2 File Offset: 0x00030FF2
		private bool conversation_magistrate_form_a_big_caravan_offer_condition()
		{
			MBTextManager.SetTextVariable("AMOUNT", this.LargeCaravanFormingCost);
			return true;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00032E08 File Offset: 0x00031008
		private bool conversation_magistrate_form_a_big_caravan_accept_on_condition()
		{
			MBTextManager.SetTextVariable("AMOUNT", this.LargeCaravanFormingCost);
			MBTextManager.SetTextVariable("NUMBER_OF_MEN", 49);
			return this.FindSuitableCompanionsToLeadCaravan().Count > 0 && Hero.MainHero.Gold >= this.LargeCaravanFormingCost;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00032E56 File Offset: 0x00031056
		private bool conversation_magistrate_form_a_big_caravan_gold_condition()
		{
			return Hero.MainHero.Gold < this.LargeCaravanFormingCost;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00032E6A File Offset: 0x0003106A
		private void conversation_magistrate_form_a_big_caravan_accept_on_consequence()
		{
			this._selectedCaravanType = 1;
			this.conversation_magistrate_form_a_caravan_accepted_on_consequence();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00032E79 File Offset: 0x00031079
		private bool conversation_magistrate_form_a_big_caravan_reject_on_condition()
		{
			return Hero.MainHero.Gold >= this.LargeCaravanFormingCost;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00032E90 File Offset: 0x00031090
		private bool conversation_magistrate_form_a_caravan_accepted_choose_leader_on_condition()
		{
			CharacterObject characterObject = ConversationSentence.CurrentProcessedRepeatObject as CharacterObject;
			TextObject selectedRepeatLine = ConversationSentence.SelectedRepeatLine;
			if (characterObject != null)
			{
				StringHelpers.SetRepeatableCharacterProperties("HERO", characterObject, false);
				return true;
			}
			return false;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00032EC0 File Offset: 0x000310C0
		private void conversation_magistrate_form_a_caravan_accept_on_consequence()
		{
			CharacterObject characterObject = ConversationSentence.SelectedRepeatObject as CharacterObject;
			this.FadeOutSelectedCaravanCompanionInMission(characterObject);
			LeaveSettlementAction.ApplyForCharacterOnly(characterObject.HeroObject);
			MobileParty.MainParty.MemberRoster.AddToCounts(characterObject, -1, false, 0, 0, true, -1);
			CaravanPartyComponent.CreateCaravanParty(Hero.MainHero, Settlement.CurrentSettlement, false, characterObject.HeroObject, null, 29, this._selectedCaravanType == 1);
			GiveGoldAction.ApplyForCharacterToSettlement(Hero.MainHero, Settlement.CurrentSettlement, (this._selectedCaravanType == 0) ? this.SmallCaravanFormingCost : this.LargeCaravanFormingCost, false);
			TextObject textObject = new TextObject("{=RmtTsqcx}A new caravan is created for {HERO.NAME}.", null);
			StringHelpers.SetCharacterProperties("HERO", Hero.MainHero.CharacterObject, textObject, false);
			InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00032F80 File Offset: 0x00031180
		private void FadeOutSelectedCaravanCompanionInMission(CharacterObject caravanLeader)
		{
			Agent agent = null;
			if (Mission.Current != null)
			{
				foreach (Agent agent2 in Mission.Current.Agents)
				{
					if (agent2.Character != null && agent2.Character.StringId == caravanLeader.StringId)
					{
						agent = agent2;
						break;
					}
				}
				if (agent != null)
				{
					agent.FadeOut(true, true);
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00033008 File Offset: 0x00031208
		private List<CharacterObject> FindSuitableCompanionsToLeadCaravan()
		{
			List<CharacterObject> list = new List<CharacterObject>();
			foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.MemberRoster.GetTroopRoster())
			{
				Hero heroObject = troopRosterElement.Character.HeroObject;
				if (heroObject != null && heroObject != Hero.MainHero && heroObject.Clan == Clan.PlayerClan && heroObject.GovernorOf == null && heroObject.CanLeadParty())
				{
					list.Add(troopRosterElement.Character);
				}
			}
			return list;
		}

		// Token: 0x040002E2 RID: 738
		private const int NumberOfMenToFormASmallCaravan = 29;

		// Token: 0x040002E3 RID: 739
		private const int NumberOfMenToFormALargeCaravan = 49;

		// Token: 0x040002E4 RID: 740
		private int _selectedCaravanType;
	}
}
