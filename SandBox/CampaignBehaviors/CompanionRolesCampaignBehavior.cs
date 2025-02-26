﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A7 RID: 167
	public class CompanionRolesCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000382D7 File Offset: 0x000364D7
		private static CompanionRolesCampaignBehavior CurrentBehavior
		{
			get
			{
				return Campaign.Current.GetCampaignBehavior<CompanionRolesCampaignBehavior>();
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000382E4 File Offset: 0x000364E4
		public override void RegisterEvents()
		{
			CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.HeroRelationChanged.AddNonSerializedListener(this, new Action<Hero, Hero, int, bool, ChangeRelationAction.ChangeRelationDetail, Hero, Hero>(this.OnHeroRelationChanged));
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00038336 File Offset: 0x00036536
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<int>>("_alreadyUsedIconIdsForNewClans", ref this._alreadyUsedIconIdsForNewClans);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0003834C File Offset: 0x0003654C
		private void OnHeroRelationChanged(Hero effectiveHero, Hero effectiveHeroGainedRelationWith, int relationChange, bool showNotification, ChangeRelationAction.ChangeRelationDetail detail, Hero originalHero, Hero originalGainedRelationWith)
		{
			if (((effectiveHero == Hero.MainHero && effectiveHeroGainedRelationWith.IsPlayerCompanion) || (effectiveHero.IsPlayerCompanion && effectiveHeroGainedRelationWith == Hero.MainHero)) && relationChange < 0 && effectiveHero.GetRelation(effectiveHeroGainedRelationWith) < -10)
			{
				KillCharacterAction.ApplyByRemove(effectiveHero.IsPlayerCompanion ? effectiveHero : effectiveHeroGainedRelationWith, false, true);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0003839B File Offset: 0x0003659B
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000383A4 File Offset: 0x000365A4
		protected void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddPlayerLine("companion_rejoin_after_emprisonment_role", "hero_main_options", "companion_rejoin", "{=!}{COMPANION_REJOIN_LINE}", new ConversationSentence.OnConditionDelegate(this.companion_rejoin_after_emprisonment_role_on_condition), new ConversationSentence.OnConsequenceDelegate(this.companion_rejoin_after_emprisonment_role_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("companion_rejoin", "companion_rejoin", "close_window", "{=akpaap9e}As you wish.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_start_role", "hero_main_options", "companion_role_pretalk", "{=d4t6oUCn}About your position in the clan...", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_role_discuss_on_condition), null, 100, null, null);
			campaignGameStarter.AddDialogLine("companion_pretalk", "companion_role_pretalk", "companion_role", "{=V6jXzuMv}{COMPANION_ROLE}", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_has_role_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_talk_fire", "companion_role", "companion_fire", "{=pRsCnGoo}I no longer have need of your services.", () => Hero.OneToOneConversationHero.IsPlayerCompanion && Settlement.CurrentSettlement == null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_talk_fire_2", "companion_role", "companion_assign_new_role", "{=2g18dlwo}I would like to assign you a new role.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_assign_role_on_condition), null, 100, null, null);
			campaignGameStarter.AddDialogLine("companion_assign_new_role", "companion_assign_new_role", "companion_roles", "{=5ajobQiL}What role do you have in mind?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_talk_fire_3", "companion_role", "companion_okay", "{=D33fIGQe}Never mind.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_engineer", "companion_roles", "companion_okay", "{=E91oU7oi}I no longer need you as Engineer.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_fire_engineer_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_delete_party_role_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_surgeon", "companion_roles", "companion_okay", "{=Dga7sQOu}I no longer need you as Surgeon.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_fire_surgeon_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_delete_party_role_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_quartermaster", "companion_roles", "companion_okay", "{=GjpJN2xE}I no longer need you as Quartermaster.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_fire_quartermaster_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_delete_party_role_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_scout", "companion_roles", "companion_okay", "{=EUQnsZFb}I no longer need you as Scout.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_fire_scout_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_delete_party_role_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("companion_role_response", "companion_okay", "hero_main_options", "{=dzXaXKaC}Very well.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_engineer_2", "companion_roles", "give_companion_roles", "{=UuFPafDj}Engineer {CURRENTLY_HELD_ENGINEER}", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_becomes_engineer_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_becomes_engineer_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_surgeon_2", "companion_roles", "give_companion_roles", "{=6xZ8U3Yz}Surgeon {CURRENTLY_HELD_SURGEON}", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_becomes_surgeon_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_becomes_surgeon_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_quartermaster_2", "companion_roles", "give_companion_roles", "{=B0VLXHHz}Quartermaster {CURRENTLY_HELD_QUARTERMASTER}", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_becomes_quartermaster_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_becomes_quartermaster_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_becomes_scout_2", "companion_roles", "give_companion_roles", "{=3aziL3Gs}Scout {CURRENTLY_HELD_SCOUT}", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_becomes_scout_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_becomes_scout_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("companion_role_response_2", "give_companion_roles", "hero_main_options", "{=5hhxQBTj}I would be honored.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_talk_return", "companion_roles", "companion_okay", "{=D33fIGQe}Never mind.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("companion_start_mission", "hero_main_options", "companion_mission_pretalk", "{=4ry48jbg}I have a mission for you...", () => HeroHelper.IsCompanionInPlayerParty(Hero.OneToOneConversationHero), null, 100, null);
			campaignGameStarter.AddDialogLine("companion_pretalk_2", "companion_mission_pretalk", "companion_mission", "{=7EoBCTX0}What do you want me to do?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_mission_gather_troops", "companion_mission", "companion_recruit_troops", "{=MDik3Kfn}I want you to recruit some troops.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_mission_forage", "companion_mission", "companion_forage", "{=kAbebv72}I want you to go forage some food.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_mission_patrol", "companion_mission", "companion_patrol", "{=OMaM6ihN}I want you to patrol the area.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_mission_cancel", "companion_mission", "hero_main_options", "{=D33fIGQe}Never mind.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("companion_forage_1", "companion_forage", "companion_forage_2", "{=o2g6Wi9K}As you wish. Will I take some troops with me?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_forage_2", "companion_forage_2", "companion_forage_troops", "{=lVbQCibL}Yes. Take these troops with you.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_forage_3", "companion_forage_2", "companion_forage_3", "{=3bOcF1Cw}I can't spare anyone now. You will need to go alone.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("companion_fire", "companion_fire", "companion_fire2", "{=bUzU50P8}What? Why? Did I do something wrong?[ib:closed]", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("companion_fire_age", "companion_fire2", "companion_fire3", "{=ywtuRAmP}Time has taken its toll on us all, friend. It's time that you retire.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_fire_no_fit", "companion_fire2", "companion_fire3", "{=1s3bHupn}You're not getting along with the rest of the company. It's better you go.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_fire_no_fit_2", "companion_fire2", "companion_fire3", "{=Q0xPr6CP}I cannot be sure of your loyalty any longer.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_fire_underperforming", "companion_fire2", "companion_fire3", "{=aCwCaWGC}Your skills are not what I need.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("companion_fire_cancel", "companion_fire2", "companion_fire_cancel", "{=8VlqJteC}I was just jesting. I need you more than ever. Now go back to your job.", null, new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_talk_done_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("companion_fire_cancel2", "companion_fire_cancel", "close_window", "{=vctta154}Well {PLAYER.NAME}, it is certainly good to see you still retain your sense of humor.[if:convo_nervous][ib:normal2]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("companion_fire_farewell", "companion_fire3", "close_window", "{=LrbyNgAa}{AGREE_TO_LEAVE}[ib:nervous2]", new ConversationSentence.OnConditionDelegate(this.companion_agrees_to_leave_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_fire_on_consequence), 100, null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_start", "hero_main_options", "turn_companion_to_lord_talk_answer", "{=B9uT9wa6}I wish to reward you for your services.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.turn_companion_to_lord_on_condition), null, 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_start_answer_2", "turn_companion_to_lord_talk_answer", "companion_leading_caravan", "{=IkH0pVhC}I would be honored, my {?PLAYER.GENDER}lady{?}lord{\\?}. But I can't take on any new responsibilities while leading this caravan. If you wish to relieve me of my duties, we can discuss this further.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_is_leading_caravan_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_start_answer_player", "companion_leading_caravan", "lord_pretalk", "{=i7k0AXsO}I see. We will speak again when you are relieved from your duty.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_start_answer", "turn_companion_to_lord_talk_answer", "turn_companion_to_lord_talk", "{=TXO1ihiZ}Thank you, my {?PLAYER.GENDER}lady{?}lord{\\?}. I have often thought about that. If I had a fief, with revenues, and perhaps a title to go with it, I could marry well and pass my wealth down to my heirs, and of course raise troops to help defend the realm.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_has_fief", "turn_companion_to_lord_talk", "check_player_has_fief_to_grant", "{=KqazzTWV}Indeed. You have shed your blood for me, and you deserve a fief of your own..", null, new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.fief_grant_answer_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_has_no_fief", "check_player_has_fief_to_grant", "player_has_no_fief_to_grant", "{=Wx5ysDp1}My {?PLAYER.GENDER}lady{?}lord{\\?}, as much as I appreciate the gesture, I am not sure that you have a suitable estate to grant me.", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.turn_companion_to_lord_no_fief_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_has_no_fief_player_answer", "player_has_no_fief_to_grant", "player_has_no_fief_to_grant_answer", "{=6uUzWz46}I see. Maybe we will speak again when I have one.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_has_no_fief_companion_answer", "player_has_no_fief_to_grant_answer", "hero_main_options", "{=PP3LzCKk}As you wish, my {?PLAYER.GENDER}lady{?}lord{\\?}.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_has_fief_answer", "check_player_has_fief_to_grant", "player_has_fief_list", "{=ArNB7aaL}Where exactly did you have in mind?[if:convo_happy]", null, null, 100, null);
			campaignGameStarter.AddRepeatablePlayerLine("turn_companion_to_lord_has_fief_list", "player_has_fief_list", "player_selected_fief_to_grant", "{=3rHeoq6r}{SETTLEMENT_NAME}.", "{=sxc2D6NJ}I am thinking of a different location.", "check_player_has_fief_to_grant", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.list_player_fief_on_condition), new ConversationSentence.OnConsequenceDelegate(this.list_player_fief_selected_on_consequence), 100, new ConversationSentence.OnClickableConditionDelegate(CompanionRolesCampaignBehavior.list_player_fief_clickable_condition));
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_has_fief_list_cancel", "player_has_fief_list", "turn_companion_to_lord_fief_conclude", "{=UEbesbKZ}Actually, I have changed my mind.", null, new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.list_player_fief_cancel_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_fief_selected", "player_selected_fief_to_grant", "turn_companion_to_lord_fief_selected_answer", "{=Mt9abZzi}{SETTLEMENT_NAME}? This is a great honor, my {?PLAYER.GENDER}lady{?}lord{\\?}. I will protect it until the last drop of my blood.[ib:hip][if:convo_happy]", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.fief_selected_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_fief_selected_confirm", "turn_companion_to_lord_fief_selected_answer", "turn_companion_to_lord_fief_selected_confirm_box", "{=TtlwXnVc}I am pleased to grant you the title of {CULTURE_SPECIFIC_TITLE} and the fiefdom of {SETTLEMENT_NAME}.. You richly deserve it.", null, null, 100, new ConversationSentence.OnClickableConditionDelegate(CompanionRolesCampaignBehavior.fief_selected_confirm_clickable_on_condition), null);
			campaignGameStarter.AddPlayerLine("turn_companion_to_lord_fief_selected_reject", "turn_companion_to_lord_fief_selected_answer", "turn_companion_to_lord_fief_conclude", "{=LDGMSQJJ}Very well. Let me think on this a bit longer", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_fief_selected_confirm_box", "turn_companion_to_lord_fief_selected_confirm_box", "turn_companion_to_lord_fief_conclude", "{=LOiZfCEy}My {?PLAYER.GENDER}lady{?}lord{\\?}, it would be an honor if you were to choose the name of my noble house.", null, new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.turn_companion_to_lord_consequence), 100, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_done_answer_thanks", "turn_companion_to_lord_fief_conclude", "close_window", "{=dpYhBgAC}Thank you my {?PLAYER.GENDER}lady{?}lord{\\?}. I will always remember this grand gesture.[ib:hip][if:convo_happy]", new ConversationSentence.OnConditionDelegate(CompanionRolesCampaignBehavior.companion_thanks_on_condition), new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_talk_done_on_consequence), 100, null);
			campaignGameStarter.AddDialogLine("turn_companion_to_lord_done_answer_rejected", "turn_companion_to_lord_fief_conclude", "hero_main_options", "{=SVEptNxR}It's only normal that you have second thoughts. I will be right by your side if you change your mind, my {?PLAYER.GENDER}lady{?}lord{\\?}.[ib:hip][if:convo_nervous]", null, new ConversationSentence.OnConsequenceDelegate(CompanionRolesCampaignBehavior.companion_talk_done_on_consequence), 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_start", "start", "rescue_companion_option_acknowledgement", "{=FVOfzPot}{SALUTATION}... Thank you for freeing me.", new ConversationSentence.OnConditionDelegate(this.companion_rescue_start_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("rescue_companion_option_acknowledgement", "rescue_companion_option_acknowledgement", "rescue_companion_preoptions", "{=YyNywO6Z}Think nothing of it. I'm glad you're safe.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("rescue_companion_preoptions", "rescue_companion_preoptions", "rescue_companion_options", "{=kaVMFgBs}What now?", new ConversationSentence.OnConditionDelegate(this.companion_rescue_start_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("rescue_companion_option_1", "rescue_companion_options", "rescue_companion_join_party", "{=drIfaTa7}Rejoin the others and let's be off.", null, new ConversationSentence.OnConsequenceDelegate(this.companion_rescue_answer_options_join_party_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("rescue_companion_option_2", "rescue_companion_options", "rescue_companion_lead_party", "{=Y6Z8qNW9}I'll need you to lead a party.", null, null, 100, new ConversationSentence.OnClickableConditionDelegate(this.lead_a_party_clickable_condition), null);
			campaignGameStarter.AddPlayerLine("rescue_companion_option_3", "rescue_companion_options", "rescue_companion_do_nothing", "{=dRKk0E1V}Unfortunately, I can't take you back right now.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("rescue_companion_lead_party_answer", "rescue_companion_lead_party", "close_window", "{=Q9Ltufg5}Tell me who to command.", null, new ConversationSentence.OnConsequenceDelegate(this.companion_rescue_answer_options_lead_party_consequence), 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_join_party_answer", "rescue_companion_join_party", "close_window", "{=92mngWSd}All right. It's good to be back.", null, new ConversationSentence.OnConsequenceDelegate(this.end_rescue_companion), 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_do_nothing_answer", "rescue_companion_do_nothing", "close_window", "{=gT2O4YXc}I will go off on my own, then. I can stay busy. But I'll remember - I owe you one!", null, new ConversationSentence.OnConsequenceDelegate(this.end_rescue_companion), 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_lead_party_create_party_continue_0", "start", "party_screen_rescue_continue", "{=ppi6eVos}As you wish.", new ConversationSentence.OnConditionDelegate(this.party_screen_continue_conversation_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_lead_party_create_party_continue_1", "party_screen_rescue_continue", "rescue_companion_options", "{=ttWBYlxS}So, what shall I do?", new ConversationSentence.OnConditionDelegate(this.party_screen_opened_but_party_is_not_created_after_rescue_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("rescue_companion_lead_party_create_party_continue_2", "party_screen_rescue_continue", "close_window", "{=DiEKuVGF}We'll make ready to set out at once.", new ConversationSentence.OnConditionDelegate(this.party_screen_opened_and_party_is_created_after_rescue_condition), new ConversationSentence.OnConsequenceDelegate(this.end_rescue_companion), 100, null);
			campaignGameStarter.AddDialogLine("default_conversation_for_wrongly_created_heroes", "start", "close_window", "{=BaeqKlQ6}I am not allowed to talk with you.", null, null, 0, null);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00038E75 File Offset: 0x00037075
		private static bool turn_companion_to_lord_no_fief_on_condition()
		{
			return !Hero.MainHero.Clan.Settlements.Any((Settlement x) => x.IsTown || x.IsCastle);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00038EB0 File Offset: 0x000370B0
		private static bool turn_companion_to_lord_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			if (oneToOneConversationHero != null && oneToOneConversationHero.IsPlayerCompanion && Hero.MainHero.IsKingdomLeader)
			{
				CompanionRolesCampaignBehavior.CurrentBehavior._playerConfirmedTheAction = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00038EE8 File Offset: 0x000370E8
		private static bool companion_is_leading_caravan_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			return oneToOneConversationHero != null && oneToOneConversationHero.IsPlayerCompanion && oneToOneConversationHero.PartyBelongedTo != null && oneToOneConversationHero.PartyBelongedTo.IsCaravan;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00038F1B File Offset: 0x0003711B
		private static void fief_grant_answer_consequence()
		{
			ConversationSentence.SetObjectsToRepeatOver((from x in Hero.MainHero.Clan.Settlements
			where x.IsTown || x.IsCastle
			select x).ToList<Settlement>(), 5);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00038F5C File Offset: 0x0003715C
		private static bool list_player_fief_clickable_condition(out TextObject explanation)
		{
			explanation = TextObject.Empty;
			Kingdom kingdom = Hero.MainHero.MapFaction as Kingdom;
			Settlement fief = ConversationSentence.CurrentProcessedRepeatObject as Settlement;
			if (fief.SiegeEvent != null)
			{
				explanation = new TextObject("{=arCGUuR5}The settlement is under siege.", null);
				return false;
			}
			if (fief.Town.IsOwnerUnassigned || kingdom.UnresolvedDecisions.Any(delegate(KingdomDecision x)
			{
				SettlementClaimantDecision settlementClaimantDecision;
				SettlementClaimantPreliminaryDecision settlementClaimantPreliminaryDecision;
				return ((settlementClaimantDecision = (x as SettlementClaimantDecision)) != null && settlementClaimantDecision.Settlement == fief) || ((settlementClaimantPreliminaryDecision = (x as SettlementClaimantPreliminaryDecision)) != null && settlementClaimantPreliminaryDecision.Settlement == fief);
			}))
			{
				explanation = new TextObject("{=OiPqa3L8}This settlement's ownership will be decided through voting.", null);
				return false;
			}
			return true;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00038FF0 File Offset: 0x000371F0
		private static bool list_player_fief_on_condition()
		{
			Settlement settlement = ConversationSentence.CurrentProcessedRepeatObject as Settlement;
			if (settlement != null)
			{
				ConversationSentence.SelectedRepeatLine.SetTextVariable("SETTLEMENT_NAME", settlement.Name);
			}
			return true;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00039022 File Offset: 0x00037222
		private void list_player_fief_selected_on_consequence()
		{
			this._selectedFief = (ConversationSentence.SelectedRepeatObject as Settlement);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00039034 File Offset: 0x00037234
		private static void turn_companion_to_lord_consequence()
		{
			TextObject textObject = new TextObject("{=ntDH7J3H}This action costs {NEEDED_GOLD_TO_GRANT_FIEF}{GOLD_ICON} and {NEEDED_INFLUENCE_TO_GRANT_FIEF}{INFLUENCE_ICON}. You will also be granting {SETTLEMENT} to {COMPANION.NAME}.", null);
			textObject.SetTextVariable("NEEDED_GOLD_TO_GRANT_FIEF", 20000);
			textObject.SetTextVariable("NEEDED_INFLUENCE_TO_GRANT_FIEF", 500);
			textObject.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
			textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
			textObject.SetCharacterProperties("COMPANION", Hero.OneToOneConversationHero.CharacterObject, false);
			textObject.SetTextVariable("SETTLEMENT", CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief.Name);
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=awjomtnJ}Are you sure?", null).ToString(), textObject.ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), new Action(CompanionRolesCampaignBehavior.ConfirmTurningCompanionToLordConsequence), new Action(CompanionRolesCampaignBehavior.RejectTurningCompanionToLordConsequence), "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0003912C File Offset: 0x0003732C
		private static void ConfirmTurningCompanionToLordConsequence()
		{
			CompanionRolesCampaignBehavior.CurrentBehavior._playerConfirmedTheAction = true;
			object obj = new TextObject("{=4eStbG4S}Select {COMPANION.NAME}{.o} clan name: ", null);
			StringHelpers.SetCharacterProperties("COMPANION", Hero.OneToOneConversationHero.CharacterObject, null, false);
			InformationManager.ShowTextInquiry(new TextInquiryData(obj.ToString(), string.Empty, true, false, GameTexts.FindText("str_done", null).ToString(), null, new Action<string>(CompanionRolesCampaignBehavior.ClanNameSelectionIsDone), null, false, new Func<string, Tuple<bool, string>>(FactionHelper.IsClanNameApplicable), "", ""), false, false);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000391B3 File Offset: 0x000373B3
		private static void RejectTurningCompanionToLordConsequence()
		{
			CompanionRolesCampaignBehavior.CurrentBehavior._playerConfirmedTheAction = false;
			Campaign.Current.ConversationManager.ContinueConversation();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000391D0 File Offset: 0x000373D0
		private static void ClanNameSelectionIsDone(string clanName)
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			RemoveCompanionAction.ApplyByByTurningToLord(Hero.MainHero.Clan, oneToOneConversationHero);
			oneToOneConversationHero.SetNewOccupation(Occupation.Lord);
			TextObject textObject = GameTexts.FindText("str_generic_clan_name", null);
			textObject.SetTextVariable("CLAN_NAME", new TextObject(clanName, null));
			int randomBannerIdForNewClan = CompanionRolesCampaignBehavior.GetRandomBannerIdForNewClan();
			Clan clan = Clan.CreateCompanionToLordClan(oneToOneConversationHero, CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief, textObject, randomBannerIdForNewClan);
			if (oneToOneConversationHero.PartyBelongedTo == MobileParty.MainParty)
			{
				MobileParty.MainParty.MemberRoster.AddToCounts(oneToOneConversationHero.CharacterObject, -1, false, 0, 0, true, -1);
			}
			MobileParty partyBelongedTo = oneToOneConversationHero.PartyBelongedTo;
			if (partyBelongedTo == null)
			{
				MobileParty mobileParty = LordPartyComponent.CreateLordParty(null, oneToOneConversationHero, MobileParty.MainParty.Position2D, 3f, CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief, oneToOneConversationHero);
				mobileParty.MemberRoster.AddToCounts(clan.Culture.BasicTroop, MBRandom.RandomInt(12, 15), false, 0, 0, true, -1);
				mobileParty.MemberRoster.AddToCounts(clan.Culture.EliteBasicTroop, MBRandom.RandomInt(10, 15), false, 0, 0, true, -1);
			}
			else
			{
				partyBelongedTo.ActualClan = clan;
				partyBelongedTo.Party.SetVisualAsDirty();
			}
			CompanionRolesCampaignBehavior.AdjustCompanionsEquipment(oneToOneConversationHero);
			CompanionRolesCampaignBehavior.SpawnNewHeroesForNewCompanionClan(oneToOneConversationHero, clan, CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief);
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, oneToOneConversationHero, 20000, false);
			GainKingdomInfluenceAction.ApplyForDefault(Hero.MainHero, -500f);
			ChangeRelationAction.ApplyPlayerRelation(oneToOneConversationHero, 50, true, true);
			Campaign.Current.ConversationManager.ContinueConversation();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00039338 File Offset: 0x00037538
		private static void AdjustCompanionsEquipment(Hero companionHero)
		{
			Equipment newEquipmentForCompanion = CompanionRolesCampaignBehavior.GetNewEquipmentForCompanion(companionHero, true);
			Equipment newEquipmentForCompanion2 = CompanionRolesCampaignBehavior.GetNewEquipmentForCompanion(companionHero, false);
			Equipment equipment = new Equipment(true);
			Equipment equipment2 = new Equipment(false);
			for (int i = 0; i < 12; i++)
			{
				if (newEquipmentForCompanion2[i].Item != null && (companionHero.BattleEquipment[i].Item == null || companionHero.BattleEquipment[i].Item.Tier < newEquipmentForCompanion2[i].Item.Tier))
				{
					equipment2[i] = newEquipmentForCompanion2[i];
				}
				else
				{
					equipment2[i] = companionHero.BattleEquipment[i];
				}
				if (newEquipmentForCompanion[i].Item != null && (companionHero.CivilianEquipment[i].Item == null || companionHero.CivilianEquipment[i].Item.Tier < newEquipmentForCompanion[i].Item.Tier))
				{
					equipment[i] = newEquipmentForCompanion[i];
				}
				else
				{
					equipment[i] = companionHero.CivilianEquipment[i];
				}
			}
			EquipmentHelper.AssignHeroEquipmentFromEquipment(companionHero, equipment);
			EquipmentHelper.AssignHeroEquipmentFromEquipment(companionHero, equipment2);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00039494 File Offset: 0x00037694
		private static int GetRandomBannerIdForNewClan()
		{
			MBReadOnlyList<int> possibleClanBannerIconsIDs = Hero.MainHero.MapFaction.Culture.PossibleClanBannerIconsIDs;
			int num = possibleClanBannerIconsIDs.GetRandomElement<int>();
			if (CompanionRolesCampaignBehavior.CurrentBehavior._alreadyUsedIconIdsForNewClans.Contains(num))
			{
				int num2 = 0;
				do
				{
					num = possibleClanBannerIconsIDs.GetRandomElement<int>();
					num2++;
				}
				while (CompanionRolesCampaignBehavior.CurrentBehavior._alreadyUsedIconIdsForNewClans.Contains(num) && num2 < 20);
				bool flag = num2 != 20;
				if (!flag)
				{
					for (int i = 0; i < possibleClanBannerIconsIDs.Count; i++)
					{
						if (!CompanionRolesCampaignBehavior.CurrentBehavior._alreadyUsedIconIdsForNewClans.Contains(possibleClanBannerIconsIDs[i]))
						{
							num = possibleClanBannerIconsIDs[i];
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					num = possibleClanBannerIconsIDs.GetRandomElement<int>();
				}
			}
			if (!CompanionRolesCampaignBehavior.CurrentBehavior._alreadyUsedIconIdsForNewClans.Contains(num))
			{
				CompanionRolesCampaignBehavior.CurrentBehavior._alreadyUsedIconIdsForNewClans.Add(num);
			}
			return num;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0003956C File Offset: 0x0003776C
		private static void SpawnNewHeroesForNewCompanionClan(Hero companionHero, Clan clan, Settlement settlement)
		{
			MBReadOnlyList<CharacterObject> lordTemplates = companionHero.Culture.LordTemplates;
			List<Hero> list = new List<Hero>();
			list.Add(CompanionRolesCampaignBehavior.CreateNewHeroForNewCompanionClan(lordTemplates.GetRandomElement<CharacterObject>(), settlement, new Dictionary<SkillObject, int>
			{
				{
					DefaultSkills.Steward,
					MBRandom.RandomInt(100, 175)
				},
				{
					DefaultSkills.Leadership,
					MBRandom.RandomInt(125, 175)
				},
				{
					DefaultSkills.OneHanded,
					MBRandom.RandomInt(125, 175)
				},
				{
					DefaultSkills.Medicine,
					MBRandom.RandomInt(125, 175)
				}
			}));
			list.Add(CompanionRolesCampaignBehavior.CreateNewHeroForNewCompanionClan(lordTemplates.GetRandomElement<CharacterObject>(), settlement, new Dictionary<SkillObject, int>
			{
				{
					DefaultSkills.OneHanded,
					MBRandom.RandomInt(100, 175)
				},
				{
					DefaultSkills.Leadership,
					MBRandom.RandomInt(125, 175)
				},
				{
					DefaultSkills.Tactics,
					MBRandom.RandomInt(125, 175)
				},
				{
					DefaultSkills.Engineering,
					MBRandom.RandomInt(125, 175)
				}
			}));
			list.Add(companionHero);
			foreach (Hero hero in list)
			{
				hero.Clan = clan;
				hero.ChangeState(Hero.CharacterStates.Active);
				ChangeRelationAction.ApplyRelationChangeBetweenHeroes(hero, Hero.MainHero, MBRandom.RandomInt(5, 10), false);
				if (hero != companionHero)
				{
					EnterSettlementAction.ApplyForCharacterOnly(hero, settlement);
				}
				foreach (Hero hero2 in list)
				{
					if (hero != hero2)
					{
						ChangeRelationAction.ApplyRelationChangeBetweenHeroes(hero, hero2, MBRandom.RandomInt(5, 10), false);
					}
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00039730 File Offset: 0x00037930
		private static Hero CreateNewHeroForNewCompanionClan(CharacterObject templateCharacter, Settlement settlement, Dictionary<SkillObject, int> startingSkills)
		{
			Hero hero = HeroCreator.CreateSpecialHero(templateCharacter, settlement, null, null, MBRandom.RandomInt(Campaign.Current.Models.AgeModel.HeroComesOfAge, 50));
			foreach (KeyValuePair<SkillObject, int> keyValuePair in startingSkills)
			{
				hero.HeroDeveloper.SetInitialSkillLevel(keyValuePair.Key, keyValuePair.Value);
			}
			return hero;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000397B8 File Offset: 0x000379B8
		private static Equipment GetNewEquipmentForCompanion(Hero companionHero, bool isCivilian)
		{
			return Campaign.Current.Models.EquipmentSelectionModel.GetEquipmentRostersForCompanion(companionHero, isCivilian).GetRandomElementInefficiently<MBEquipmentRoster>().AllEquipments.GetRandomElement<Equipment>();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000397DF File Offset: 0x000379DF
		private static void list_player_fief_cancel_on_consequence()
		{
			CompanionRolesCampaignBehavior.CurrentBehavior._playerConfirmedTheAction = false;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000397EC File Offset: 0x000379EC
		private static bool fief_selected_on_condition()
		{
			MBTextManager.SetTextVariable("SETTLEMENT_NAME", CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief.Name, false);
			return true;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00039809 File Offset: 0x00037A09
		private static bool companion_thanks_on_condition()
		{
			return CompanionRolesCampaignBehavior.CurrentBehavior._playerConfirmedTheAction;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00039818 File Offset: 0x00037A18
		private static bool fief_selected_confirm_clickable_on_condition(out TextObject explanation)
		{
			explanation = TextObject.Empty;
			MBTextManager.SetTextVariable("CULTURE_SPECIFIC_TITLE", HeroHelper.GetTitleInIndefiniteCase(Hero.OneToOneConversationHero), false);
			MBTextManager.SetTextVariable("SETTLEMENT_NAME", CompanionRolesCampaignBehavior.CurrentBehavior._selectedFief.Name, false);
			bool flag = Hero.MainHero.Gold >= 20000;
			bool flag2 = Hero.MainHero.Clan.Influence >= 500f;
			MBTextManager.SetTextVariable("NEEDED_GOLD_TO_GRANT_FIEF", 20000);
			MBTextManager.SetTextVariable("NEEDED_INFLUENCE_TO_GRANT_FIEF", 500);
			MBTextManager.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">", false);
			MBTextManager.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">", false);
			if (flag && flag2)
			{
				explanation = new TextObject("{=PxQEwCha}You will pay {NEEDED_GOLD_TO_GRANT_FIEF}{GOLD_ICON}, {NEEDED_INFLUENCE_TO_GRANT_FIEF}{INFLUENCE_ICON}.", null);
				return true;
			}
			explanation = new TextObject("{=!}{GOLD_REQUIREMENT}{INFLUENCE_REQUIREMENT}", null);
			if (!flag)
			{
				TextObject variable = new TextObject("{=yo2NvkQQ}You need {NEEDED_GOLD_TO_GRANT_FIEF}{GOLD_ICON}. ", null);
				explanation.SetTextVariable("GOLD_REQUIREMENT", variable);
			}
			if (!flag2)
			{
				TextObject variable2 = new TextObject("{=pDeFXZJd}You need {NEEDED_INFLUENCE_TO_GRANT_FIEF}{INFLUENCE_ICON}.", null);
				explanation.SetTextVariable("INFLUENCE_REQUIREMENT", variable2);
			}
			return false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00039924 File Offset: 0x00037B24
		private static void companion_talk_done_on_consequence()
		{
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.LeaveEncounter = true;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00039934 File Offset: 0x00037B34
		private static void companion_fire_on_consequence()
		{
			Agent oneToOneConversationAgent = ConversationMission.OneToOneConversationAgent;
			RemoveCompanionAction.ApplyByFire(Hero.OneToOneConversationHero.CompanionOf, Hero.OneToOneConversationHero);
			KillCharacterAction.ApplyByRemove(Hero.OneToOneConversationHero, false, true);
			if (Hero.MainHero.CurrentSettlement != null)
			{
				AgentNavigator agentNavigator = oneToOneConversationAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
				if (((agentNavigator != null) ? agentNavigator.GetActiveBehavior() : null) is FollowAgentBehavior)
				{
					agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().RemoveBehavior<FollowAgentBehavior>();
				}
				LocationCharacter locationCharacter = LocationComplex.Current.FindCharacter(oneToOneConversationAgent);
				PlayerEncounter.LocationEncounter.RemoveAccompanyingCharacter(locationCharacter);
			}
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.LeaveEncounter = true;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000399C4 File Offset: 0x00037BC4
		private bool companion_rejoin_after_emprisonment_role_on_condition()
		{
			if (Hero.OneToOneConversationHero != null && !Hero.OneToOneConversationHero.IsPartyLeader && Hero.OneToOneConversationHero.IsPlayerCompanion && Hero.OneToOneConversationHero.PartyBelongedTo != MobileParty.MainParty && (Hero.OneToOneConversationHero.PartyBelongedTo == null || !Hero.OneToOneConversationHero.PartyBelongedTo.IsCaravan))
			{
				if (Settlement.CurrentSettlement != null && Hero.OneToOneConversationHero.GovernorOf == Settlement.CurrentSettlement.Town)
				{
					MBTextManager.SetTextVariable("COMPANION_REJOIN_LINE", "{=Z5zAok5G}I need to recall you to my party, and to stop governing this town.", false);
				}
				else
				{
					MBTextManager.SetTextVariable("COMPANION_REJOIN_LINE", "{=1QthEZ9R}I am glad that I found you. Please rejoin my party.", false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00039A63 File Offset: 0x00037C63
		private void companion_rejoin_after_emprisonment_role_on_consequence()
		{
			AddHeroToPartyAction.Apply(Hero.OneToOneConversationHero, MobileParty.MainParty, true);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00039A75 File Offset: 0x00037C75
		private void OnCompanionRemoved(Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			if (LocationComplex.Current != null)
			{
				LocationComplex.Current.RemoveCharacterIfExists(companion);
			}
			if (PlayerEncounter.LocationEncounter != null)
			{
				PlayerEncounter.LocationEncounter.RemoveAccompanyingCharacter(companion);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00039A9B File Offset: 0x00037C9B
		private bool companion_agrees_to_leave_on_condition()
		{
			MBTextManager.SetTextVariable("AGREE_TO_LEAVE", new TextObject("{=0geP718k}Well... I don't know what to say. Goodbye, then.", null), false);
			return true;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00039AB4 File Offset: 0x00037CB4
		private static bool companion_has_role_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			SkillEffect.PerkRole heroPerkRole = MobileParty.MainParty.GetHeroPerkRole(oneToOneConversationHero);
			if (heroPerkRole == SkillEffect.PerkRole.None)
			{
				MBTextManager.SetTextVariable("COMPANION_ROLE", new TextObject("{=k7ebznzr}Yes?", null), false);
			}
			else
			{
				MBTextManager.SetTextVariable("COMPANION_ROLE", new TextObject("{=n3bvfe8t}I am currently working as {COMPANION_JOB}.", null), false);
				MBTextManager.SetTextVariable("COMPANION_JOB", GameTexts.FindText("role", heroPerkRole.ToString()), false);
			}
			return true;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00039B27 File Offset: 0x00037D27
		private static bool companion_role_discuss_on_condition()
		{
			return Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero.Clan == Clan.PlayerClan;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00039B43 File Offset: 0x00037D43
		private static bool companion_assign_role_on_condition()
		{
			return Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero.Clan == Clan.PlayerClan && Hero.OneToOneConversationHero.PartyBelongedTo != null;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00039B6C File Offset: 0x00037D6C
		private static bool companion_becomes_engineer_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			Hero roleHolder = oneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Engineer);
			if (roleHolder != null)
			{
				TextObject textObject = new TextObject("{=QEp8t8u0}(Currently held by {COMPANION.LINK})", null);
				StringHelpers.SetCharacterProperties("COMPANION", roleHolder.CharacterObject, textObject, false);
				MBTextManager.SetTextVariable("CURRENTLY_HELD_ENGINEER", textObject, false);
			}
			else
			{
				MBTextManager.SetTextVariable("CURRENTLY_HELD_ENGINEER", "{=kNQMkh3j}(Currently unassigned)", false);
			}
			return roleHolder != oneToOneConversationHero && MobilePartyHelper.IsHeroAssignableForEngineerInParty(oneToOneConversationHero, oneToOneConversationHero.PartyBelongedTo);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00039BDE File Offset: 0x00037DDE
		private static void companion_becomes_engineer_on_consequence()
		{
			Hero.OneToOneConversationHero.PartyBelongedTo.SetPartyEngineer(Hero.OneToOneConversationHero);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00039BF4 File Offset: 0x00037DF4
		private static bool companion_becomes_surgeon_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			Hero roleHolder = oneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Surgeon);
			if (roleHolder != null)
			{
				TextObject textObject = new TextObject("{=QEp8t8u0}(Currently held by {COMPANION.LINK})", null);
				StringHelpers.SetCharacterProperties("COMPANION", roleHolder.CharacterObject, textObject, false);
				MBTextManager.SetTextVariable("CURRENTLY_HELD_SURGEON", textObject, false);
			}
			else
			{
				MBTextManager.SetTextVariable("CURRENTLY_HELD_SURGEON", "{=kNQMkh3j}(Currently unassigned)", false);
			}
			return roleHolder != oneToOneConversationHero && MobilePartyHelper.IsHeroAssignableForSurgeonInParty(oneToOneConversationHero, oneToOneConversationHero.PartyBelongedTo);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00039C66 File Offset: 0x00037E66
		private static void companion_becomes_surgeon_on_consequence()
		{
			Hero.OneToOneConversationHero.PartyBelongedTo.SetPartySurgeon(Hero.OneToOneConversationHero);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00039C7C File Offset: 0x00037E7C
		private static bool companion_becomes_quartermaster_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			Hero roleHolder = oneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Quartermaster);
			if (roleHolder != null)
			{
				TextObject textObject = new TextObject("{=QEp8t8u0}(Currently held by {COMPANION.LINK})", null);
				StringHelpers.SetCharacterProperties("COMPANION", roleHolder.CharacterObject, textObject, false);
				MBTextManager.SetTextVariable("CURRENTLY_HELD_QUARTERMASTER", textObject, false);
			}
			else
			{
				MBTextManager.SetTextVariable("CURRENTLY_HELD_QUARTERMASTER", "{=kNQMkh3j}(Currently unassigned)", false);
			}
			Hero oneToOneConversationHero2 = Hero.OneToOneConversationHero;
			return roleHolder != oneToOneConversationHero && MobilePartyHelper.IsHeroAssignableForQuartermasterInParty(oneToOneConversationHero2, Hero.OneToOneConversationHero.PartyBelongedTo);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00039CF9 File Offset: 0x00037EF9
		private static void companion_becomes_quartermaster_on_consequence()
		{
			Hero.OneToOneConversationHero.PartyBelongedTo.SetPartyQuartermaster(Hero.OneToOneConversationHero);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00039D10 File Offset: 0x00037F10
		private static bool companion_becomes_scout_on_condition()
		{
			Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
			Hero roleHolder = oneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Scout);
			if (roleHolder != null)
			{
				TextObject textObject = new TextObject("{=QEp8t8u0}(Currently held by {COMPANION.LINK})", null);
				StringHelpers.SetCharacterProperties("COMPANION", roleHolder.CharacterObject, textObject, false);
				MBTextManager.SetTextVariable("CURRENTLY_HELD_SCOUT", textObject, false);
			}
			else
			{
				MBTextManager.SetTextVariable("CURRENTLY_HELD_SCOUT", "{=kNQMkh3j}(Currently unassigned)", false);
			}
			return roleHolder != oneToOneConversationHero && MobilePartyHelper.IsHeroAssignableForScoutInParty(oneToOneConversationHero, oneToOneConversationHero.PartyBelongedTo);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00039D83 File Offset: 0x00037F83
		private static void companion_becomes_scout_on_consequence()
		{
			Hero.OneToOneConversationHero.PartyBelongedTo.SetPartyScout(Hero.OneToOneConversationHero);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00039D99 File Offset: 0x00037F99
		private static void companion_delete_party_role_consequence()
		{
			Hero.OneToOneConversationHero.PartyBelongedTo.RemoveHeroPerkRole(Hero.OneToOneConversationHero);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00039DAF File Offset: 0x00037FAF
		private static bool companion_fire_engineer_on_condition()
		{
			return Hero.OneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Engineer) == Hero.OneToOneConversationHero && Hero.OneToOneConversationHero != Hero.OneToOneConversationHero.PartyBelongedTo.LeaderHero;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00039DE3 File Offset: 0x00037FE3
		private static bool companion_fire_surgeon_on_condition()
		{
			return Hero.OneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Surgeon) == Hero.OneToOneConversationHero && Hero.OneToOneConversationHero != Hero.OneToOneConversationHero.PartyBelongedTo.LeaderHero;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00039E17 File Offset: 0x00038017
		private static bool companion_fire_quartermaster_on_condition()
		{
			return Hero.OneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Quartermaster) == Hero.OneToOneConversationHero && Hero.OneToOneConversationHero != Hero.OneToOneConversationHero.PartyBelongedTo.LeaderHero;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00039E4C File Offset: 0x0003804C
		private static bool companion_fire_scout_on_condition()
		{
			return Hero.OneToOneConversationHero.PartyBelongedTo.GetRoleHolder(SkillEffect.PerkRole.Scout) == Hero.OneToOneConversationHero && Hero.OneToOneConversationHero != Hero.OneToOneConversationHero.PartyBelongedTo.LeaderHero;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00039E84 File Offset: 0x00038084
		private bool companion_rescue_start_condition()
		{
			if (Campaign.Current.CurrentConversationContext == ConversationContext.FreedHero)
			{
				Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
				if (((oneToOneConversationHero != null) ? oneToOneConversationHero.CompanionOf : null) == Clan.PlayerClan && CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Wanderer)
				{
					MBTextManager.SetTextVariable("SALUTATION", Campaign.Current.ConversationManager.FindMatchingTextOrNull("str_salutation", CharacterObject.OneToOneConversationCharacter), false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00039EEB File Offset: 0x000380EB
		private void companion_rescue_answer_options_join_party_consequence()
		{
			EndCaptivityAction.ApplyByReleasedAfterBattle(Hero.OneToOneConversationHero);
			Hero.OneToOneConversationHero.ChangeState(Hero.CharacterStates.Active);
			MobileParty.MainParty.AddElementToMemberRoster(CharacterObject.OneToOneConversationCharacter, 1, false);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00039F14 File Offset: 0x00038114
		private bool lead_a_party_clickable_condition(out TextObject reason)
		{
			bool flag = Clan.PlayerClan.CommanderLimit > Clan.PlayerClan.WarPartyComponents.Count;
			reason = TextObject.Empty;
			if (!flag)
			{
				reason = GameTexts.FindText("str_clan_doesnt_have_empty_party_slots", null);
			}
			return flag;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00039F48 File Offset: 0x00038148
		private void companion_rescue_answer_options_lead_party_consequence()
		{
			this.OpenPartyScreenForRescue();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00039F50 File Offset: 0x00038150
		private void OpenPartyScreenForRescue()
		{
			PartyScreenManager.OpenScreenAsCreateClanPartyForHero(Hero.OneToOneConversationHero, new PartyScreenClosedDelegate(this.PartyScreenClosed), new IsTroopTransferableDelegate(this.TroopTransferableDelegate));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00039F74 File Offset: 0x00038174
		private void PartyScreenClosed(PartyBase leftOwnerParty, TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, PartyBase rightOwnerParty, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, bool fromCancel)
		{
			if (!fromCancel)
			{
				CharacterObject characterAtIndex = leftMemberRoster.GetCharacterAtIndex(0);
				EndCaptivityAction.ApplyByReleasedAfterBattle(characterAtIndex.HeroObject);
				characterAtIndex.HeroObject.ChangeState(Hero.CharacterStates.Active);
				MobileParty.MainParty.AddElementToMemberRoster(characterAtIndex, 1, false);
				this._partyCreatedAfterRescueForCompanion = true;
				MobileParty mobileParty = Clan.PlayerClan.CreateNewMobileParty(characterAtIndex.HeroObject);
				foreach (TroopRosterElement troopRosterElement in leftMemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character != characterAtIndex)
					{
						mobileParty.MemberRoster.Add(troopRosterElement);
						rightOwnerParty.MemberRoster.AddToCounts(troopRosterElement.Character, -troopRosterElement.Number, false, -troopRosterElement.WoundedNumber, -troopRosterElement.Xp, true, -1);
					}
				}
				foreach (TroopRosterElement troopRosterElement2 in leftPrisonRoster.GetTroopRoster())
				{
					mobileParty.MemberRoster.Add(troopRosterElement2);
					rightOwnerParty.PrisonRoster.AddToCounts(troopRosterElement2.Character, -troopRosterElement2.Number, false, -troopRosterElement2.WoundedNumber, -troopRosterElement2.Xp, true, -1);
				}
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003A0C8 File Offset: 0x000382C8
		private bool TroopTransferableDelegate(CharacterObject character, PartyScreenLogic.TroopType type, PartyScreenLogic.PartyRosterSide side, PartyBase LeftOwnerParty)
		{
			return !character.IsHero;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003A0D3 File Offset: 0x000382D3
		private bool party_screen_continue_conversation_condition()
		{
			if (Campaign.Current.CurrentConversationContext == ConversationContext.FreedHero)
			{
				Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
				if (((oneToOneConversationHero != null) ? oneToOneConversationHero.CompanionOf : null) == Clan.PlayerClan)
				{
					return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Wanderer;
				}
			}
			return false;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003A10A File Offset: 0x0003830A
		private bool party_screen_opened_but_party_is_not_created_after_rescue_condition()
		{
			return !this._partyCreatedAfterRescueForCompanion;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003A115 File Offset: 0x00038315
		private bool party_screen_opened_and_party_is_created_after_rescue_condition()
		{
			return this._partyCreatedAfterRescueForCompanion;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003A11D File Offset: 0x0003831D
		private void end_rescue_companion()
		{
			this._partyCreatedAfterRescueForCompanion = false;
			if (Hero.OneToOneConversationHero.IsPrisoner)
			{
				EndCaptivityAction.ApplyByReleasedAfterBattle(Hero.OneToOneConversationHero);
			}
		}

		// Token: 0x040002F5 RID: 757
		private const int CompanionRelationLimit = -10;

		// Token: 0x040002F6 RID: 758
		private const int NeededGoldToGrantFief = 20000;

		// Token: 0x040002F7 RID: 759
		private const int NeededInfluenceToGrantFief = 500;

		// Token: 0x040002F8 RID: 760
		private const int RelationGainWhenCompanionToLordAction = 50;

		// Token: 0x040002F9 RID: 761
		private const int NewCreatedHeroForCompanionClanMaxAge = 50;

		// Token: 0x040002FA RID: 762
		private const int NewHeroSkillUpperLimit = 175;

		// Token: 0x040002FB RID: 763
		private const int NewHeroSkillLowerLimit = 125;

		// Token: 0x040002FC RID: 764
		private Settlement _selectedFief;

		// Token: 0x040002FD RID: 765
		private bool _playerConfirmedTheAction;

		// Token: 0x040002FE RID: 766
		private List<int> _alreadyUsedIconIdsForNewClans = new List<int>();

		// Token: 0x040002FF RID: 767
		private bool _partyCreatedAfterRescueForCompanion;
	}
}
