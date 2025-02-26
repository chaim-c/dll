﻿using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.MissionLogics.Arena;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A0 RID: 160
	public class ArenaMasterCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0002F488 File Offset: 0x0002D688
		public override void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
			CampaignEvents.AfterMissionStarted.AddNonSerializedListener(this, new Action<IMission>(this.AfterMissionStarted));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002F4F1 File Offset: 0x0002D6F1
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<Settlement>>("_arenaMasterHasMetInSettlements", ref this._arenaMasterHasMetInSettlements);
			dataStore.SyncData<bool>("_knowTournaments", ref this._knowTournaments);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002F517 File Offset: 0x0002D717
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
			this.AddGameMenus(campaignGameStarter);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0002F528 File Offset: 0x0002D728
		private void AddGameMenus(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddGameMenuOption("town_arena", "mno_enter_practice_fight", "{=9pg3qc6N}Practice fight", new GameMenuOption.OnConditionDelegate(this.game_menu_enter_practice_fight_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_enter_practice_fight_on_consequence), false, 1, false, null);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002F566 File Offset: 0x0002D766
		public void OnSettlementEntered(MobileParty mobileParty, Settlement settlement, Hero hero)
		{
			if (mobileParty != MobileParty.MainParty || !settlement.IsTown)
			{
				return;
			}
			this.AddArenaMaster(settlement);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0002F580 File Offset: 0x0002D780
		private void OnGameLoadFinished()
		{
			if (Settlement.CurrentSettlement != null && !Hero.MainHero.IsPrisoner && LocationComplex.Current != null && Settlement.CurrentSettlement.IsTown && PlayerEncounter.LocationEncounter != null && !Settlement.CurrentSettlement.IsUnderSiege)
			{
				this.AddArenaMaster(Settlement.CurrentSettlement);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0002F5D1 File Offset: 0x0002D7D1
		private void AddArenaMaster(Settlement settlement)
		{
			settlement.LocationComplex.GetLocationWithId("arena").AddLocationCharacters(new CreateLocationCharacterDelegate(ArenaMasterCampaignBehavior.CreateTournamentMaster), settlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		protected void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("arena_master_tournament_meet", "start", "arena_intro_1", "{=GAsVO8cZ}Good day, friend. I'll bet you came here for the games, or as they say nowadays, the tournament!", new ConversationSentence.OnConditionDelegate(this.conversation_arena_master_tournament_meet_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_tournament_meet_2", "start", "arena_intro_1a", "{=rqFKxm24}Greetings, friend. If you came for the games, the big fights, I'm afraid you're out of luck. There won't be games, or a 'tournament' as they say nowadays, any time soon.", new ConversationSentence.OnConditionDelegate(this.conversation_arena_master_no_tournament_meet_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_meet_no_intro", "start", "arena_master_talk", "{=ZvzxcRbc}Good day, friend. You look like you know your way around an arena. How can I help you?", new ConversationSentence.OnConditionDelegate(this.conversation_arena_master_player_knows_arenas_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_meet_start", "start", "arena_master_talk", "{=dgNCuuUL}Hello, {PLAYER.NAME}. Good to see you again.", new ConversationSentence.OnConditionDelegate(this.conversation_arena_master_meet_start_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_meet_start_2", "start", "arena_master_post_practice_fight_talk", "{=nmPaCLHp}{FIGHT_DEBRIEF} Do you want to give it another go?", new ConversationSentence.OnConditionDelegate(this.conversation_arena_master_post_fight_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_intro_1", "arena_intro_1", "arena_intro_tournament", "{=j9RrkCvM}There's a tournament going on?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_intro_2", "arena_intro_1a", "arena_intro_no_tournament", "{=W1wVPNpy}I've heard of these games...", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("arena_intro_3", "arena_intro_tournament", "arena_intro_tournament_2", "{=GAq7KAf0}You bet! Say, you look like a fighter. You should join. Back in the old days it was all condemned criminals and fights to the death, but nowadays they use blunted weapons.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_3a", "arena_intro_tournament_2", "arena_intro_practice_fights", "{=VH27tpkT}It's quite the opportunity to make your name. You risk no more than your teeth, and didn't the Heavens give us thirty of those, just to have a few spare for grand opportunities like this?", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_4", "arena_intro_no_tournament", "arena_intro_no_tournament_2", "{=EA2JcVcb}As well you might! They're a grand old imperial custom that's now spread all over Calradia. Back in the old days, they'd give a hundred condemned criminals swords and have them slash at each other until the sands were drenched in blood![if:convo_merry]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_4a", "arena_intro_no_tournament_2", "arena_intro_no_tournament_3", "{=EFKxbLaO}Nowadays things are a little different, of course. The emperors got worried about the people's morals and steered them toward more virtuous kinds of killing, like wars. But the games still go on, just with blunted weapons.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_5", "arena_intro_no_tournament_3", "arena_intro_no_tournament_4", "{=LqkxF5Op}During the games, all the best fighters from the area form teams and pummel each other. Not quite as much fun for the crowd as watching gladiators spill their guts out, of course, but healthier for the participants.[if:convo_approving]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_5a", "arena_intro_no_tournament_4", "arena_intro_practice_fights", "{=jy1o5cNT}You're a warrior, are you not? The games are a fine way to make your name. The local merchants put together a nice fat purse for the winner to attract the talent.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_intro_6", "arena_intro_practice_fights", "arena_intro_perk_reset", "{=iLuezAbk}When there's no tournament, it's still worth coming by. A lot of fighters spend their time here practicing to keep in trim, and we'll award the winners a few coins for their troubles.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_tournament_rules", "arena_intro_4", "arena_tournament_rules", "{=aHGbTpLp}Tell me how tournaments work.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_practice_fight_rules", "arena_intro_4", "arena_practice_fight_rules", "{=H2aaMAe5}Tell me how the practice fights work.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_prizes", "arena_intro_4", "arena_prizes", "{=7pH9MzS1}So you pay us to fight? What's in it for you?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_prizes_2", "arena_intro_4", "arena_master_pre_talk", "{=R2HP4EiX}I don't have any more questions.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("arena_master_reminder", "arena_master_reminder", "arena_intro_4", "{=k7ebznzr}Yes?", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_prizes_answer", "arena_prizes", "arena_prizes_amounts", "{=bUmacxw7}Well, even the practice fights draw those who like to bet on the outcome. But the tournaments! Those pull in crowds from miles around. The merchants love a tournament, and that's why they pony up the silver we need to pay the good souls like you who take and receive the hard knocks.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro", "arena_tournament_rules", "arena_intro_3a", "{=o0H8Qs0D}The rules of the tournament are standard across Calradia, even outside the Empire. We match the fighters up by drawing lots. Sometimes you're part of a team, and sometimes you fight by yourself.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_1c", "arena_intro_3a", "arena_intro_4", "{=Jgkz4uo6}The lots also determine what weapons you get. The winners of each match proceed to the next round. When only two are left, they battle each other to be declared champion.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_1a", "arena_practice_fight_rules", "arena_intro_4", "{=cPmV8S4e}We leave the arena open to anyone who wants to practice. There are no rules, no teams. Everyone beats at each other until there is only one fighter left standing. Sounds like fun, eh?[ib:confident2]", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_training_practice_fight_intro_2", "arena_prizes_amounts", "arena_tournament_reward", "{=WwbDoZXg}How much are the prizes in the tournaments?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_training_practice_fight_intro_3", "arena_prizes_amounts", "arena_practice_fight_reward", "{=Z4MreMZz}How much are the prizes in the practice fights?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_training_practice_fight_intro_4", "arena_prizes_amounts", "arena_master_pre_talk", "{=4vAbAIqi}Okay. I think I get it.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_reward", "arena_practice_fight_reward", "arena_joining_ask", "{=!}{ARENA_REWARD}", new ConversationSentence.OnConditionDelegate(ArenaMasterCampaignBehavior.conversation_arena_practice_fight_explain_reward_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_reward_2", "arena_tournament_reward", "arena_joining_ask", "{=!}{TOURNAMENT_REWARD}", new ConversationSentence.OnConditionDelegate(ArenaMasterCampaignBehavior.conversation_arena_tournament_explain_reward_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_training_practice_fight_intro_5", "arena_joining_ask", "arena_joining_answer", "{=Te4pxfWF}So can I join?", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_6", "arena_joining_answer", "arena_master_talk", "{=bBVLVT7L}Certainly! Looks like a few of our lads are warming up now for the tournament. You can go and hop in if you want to. Or come back later if you just want to practice.[ib:warrior]", new ConversationSentence.OnConditionDelegate(this.conversation_town_has_tournament_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_training_practice_fight_intro_7", "arena_joining_answer", "arena_master_talk", "{=KtrZs3yA}Certainly! The arena is open to anyone who doesn't mind hard knocks. Looks like a few of our lads are warming up now. You can go and hop in if you want to. Or come back later when there's a tournament.[ib:warrior]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_pre_talk_explain", "arena_master_explain", "arena_prizes_amounts", "{=ke0IvBXb}Anything else I can explain?", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_ask_what_to_do", "arena_master_pre_talk", "arena_master_talk", "{=arena_master_24}So, what would you like to do?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_master_sign_up_tournament", "arena_master_talk", "arena_master_enter_tournament", "{=arena_master_25}Sign me up for the tournament.", new ConversationSentence.OnConditionDelegate(this.conversation_town_has_tournament_on_condition), null, 100, new ConversationSentence.OnClickableConditionDelegate(this.conversation_town_arena_fight_join_check_on_condition), null);
			campaignGameStarter.AddPlayerLine("arena_master_ask_for_practice_fight_fight", "arena_master_talk", "arena_master_enter_practice_fight", "{=arena_master_26}I'd like to participate in a practice fight...", null, null, 100, new ConversationSentence.OnClickableConditionDelegate(this.conversation_town_arena_fight_join_check_on_condition), null);
			campaignGameStarter.AddPlayerLine("arena_master_ask_tournaments", "arena_master_talk", "arena_master_ask_tournaments", "{=arena_master_27}Are there any tournaments going on in nearby towns?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_master_remind_something", "arena_master_talk", "arena_master_reminder", "{=iSNrQKEN}I want to go back to something you'd mentioned earlier...", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_master_leave", "arena_master_talk", "close_window", "{=arena_master_30}I need to leave now. Good bye.", null, null, 80, null, null);
			campaignGameStarter.AddDialogLine("arena_master_tournament_location", "arena_master_ask_tournaments", "arena_master_talk", "{=arena_master_31}{NEARBY_TOURNAMENT_STRING}", new ConversationSentence.OnConditionDelegate(ArenaMasterCampaignBehavior.conversation_tournament_soon_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_ask_tournaments_2", "arena_master_ask_tournaments", "arena_master_talk", "{=arena_master_32}There won't be any tournaments any time soon.", null, null, 1, null);
			campaignGameStarter.AddDialogLine("arena_master_enter_practice_fight_master_confirm", "arena_master_enter_practice_fight", "arena_master_enter_practice_fight_confirm", "{=arena_master_33}Go to it! Grab a practice weapon on your way down.[if:convo_approving]", new ConversationSentence.OnConditionDelegate(this.conversation_arena_join_practice_fight_confirm_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_enter_practice_fight_master_decline", "arena_master_enter_practice_fight", "close_window", "{=FguHzavX}You can't practice in the arena because there is a tournament going on right now.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("arena_master_enter_tournament", "arena_master_enter_tournament", "arena_master_enter_tournament_confirm", "{=arena_master_34}Very well - we'll enter your name in the lots, and when your turn comes up, be ready to go out there and start swinging![if:convo_merry]", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("arena_master_enter_practice_fight_confirm", "arena_master_enter_practice_fight_confirm", "close_window", "{=arena_master_35}I'll do that.", null, new ConversationSentence.OnConsequenceDelegate(ArenaMasterCampaignBehavior.conversation_arena_join_fight_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_master_enter_practice_fight_decline", "arena_master_enter_practice_fight_confirm", "arena_master_pre_talk", "{=arena_master_36}On second thought, I'll hold off.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_master_enter_tournament_confirm", "arena_master_enter_tournament_confirm", "close_window", "{=arena_master_37}I'll be ready.", null, new ConversationSentence.OnConsequenceDelegate(ArenaMasterCampaignBehavior.conversation_arena_join_tournament_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_master_enter_tournament_decline", "arena_master_enter_tournament_confirm", "arena_master_pre_talk", "{=arena_master_38}Actually, never mind.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("arena_join_fight", "arena_master_post_practice_fight_talk", "close_window", "{=GmIluR4H}Sure. Why not?", null, new ConversationSentence.OnConsequenceDelegate(ArenaMasterCampaignBehavior.conversation_arena_join_fight_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("2593", "arena_master_post_practice_fight_talk", "arena_master_practice_fight_reject", "{=qsg7pZOs}Thanks. But I will give my bruises some time to heal.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("2594", "arena_master_practice_fight_reject", "close_window", "{=Q7B68CVK}{?PLAYER.GENDER}Splendid{?}Good man{\\?}! That's clever of you.[ib:normal]", null, null, 100, null);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002FD10 File Offset: 0x0002DF10
		private static LocationCharacter CreateTournamentMaster(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject tournamentMaster = culture.TournamentMaster;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(tournamentMaster.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(tournamentMaster, out minValue, out maxValue, "");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(tournamentMaster, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "spawnpoint_tournamentmaster", true, relation, null, true, true, null, false, false, true);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		private bool conversation_arena_master_practice_fights_meet_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && !this._knowTournaments)
			{
				MBTextManager.SetTextVariable("TOWN_NAME", Settlement.CurrentSettlement.Name, false);
				this._knowTournaments = true;
				this._arenaMasterHasMetInSettlements.Add(Settlement.CurrentSettlement);
				return true;
			}
			return false;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		private bool conversation_town_has_tournament_on_condition()
		{
			return Settlement.CurrentSettlement.IsTown && Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town) != null;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002FE28 File Offset: 0x0002E028
		public static bool conversation_tournament_soon_on_condition()
		{
			List<Town> list = (from x in Town.AllTowns
			where Campaign.Current.TournamentManager.GetTournamentGame(x) != null && x != Settlement.CurrentSettlement.Town
			select x).ToList<Town>();
			list = (from x in list
			orderby x.Settlement.Position2D.DistanceSquared(Settlement.CurrentSettlement.Position2D)
			select x).ToList<Town>();
			TextObject text;
			if (list.Count > 1)
			{
				text = new TextObject("{=pinSMuMe}Well, there's one starting up at {CLOSEST_TOURNAMENT}, then another at {NEXT_CLOSEST_TOURNAMENT}. You should probably be able to get to either of those, if you move quickly.[ib:hip]", null);
				MBTextManager.SetTextVariable("CLOSEST_TOURNAMENT", list[0].Settlement.EncyclopediaLinkWithName, false);
				MBTextManager.SetTextVariable("NEXT_CLOSEST_TOURNAMENT", list[1].Settlement.EncyclopediaLinkWithName, false);
			}
			else if (list.Count == 1)
			{
				MBTextManager.SetTextVariable("CLOSEST_TOURNAMENT", list[0].Settlement.EncyclopediaLinkWithName, false);
				text = new TextObject("{=2WnruiBw}I know of one starting up at {CLOSEST_TOURNAMENT}. You should be able to get there if you move quickly enough.", null);
			}
			else
			{
				text = new TextObject("{=tGI135jv}Ah - I don't know of any right now. That's a bit unusual though. Must be the wars.[ib:closed]", null);
			}
			MBTextManager.SetTextVariable("NEARBY_TOURNAMENT_STRING", text, false);
			return true;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002FF2C File Offset: 0x0002E12C
		private bool conversation_arena_join_practice_fight_confirm_on_condition()
		{
			return !Settlement.CurrentSettlement.Town.HasTournament;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002FF40 File Offset: 0x0002E140
		private bool conversation_arena_join_practice_fight_decline_on_condition()
		{
			return Settlement.CurrentSettlement.Town.HasTournament;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002FF54 File Offset: 0x0002E154
		private bool conversation_town_arena_fight_join_check_on_condition(out TextObject explanation)
		{
			if (Hero.MainHero.IsWounded && Campaign.Current.IsMainHeroDisguised)
			{
				explanation = new TextObject("{=DqZtRBXR}You are wounded and in disguise.", null);
				return false;
			}
			if (Hero.MainHero.IsWounded)
			{
				explanation = new TextObject("{=yNMrF2QF}You are wounded", null);
				return false;
			}
			if (Campaign.Current.IsMainHeroDisguised)
			{
				explanation = new TextObject("{=jcEoUPCB}You are in disguise.", null);
				return false;
			}
			explanation = null;
			return true;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		private bool conversation_arena_master_tournament_meet_on_condition()
		{
			if (Settlement.CurrentSettlement == null)
			{
				return false;
			}
			TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town);
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && !this._knowTournaments && tournamentGame != null)
			{
				MBTextManager.SetTextVariable("TOWN_NAME", Settlement.CurrentSettlement.Name, false);
				this._knowTournaments = true;
				this._arenaMasterHasMetInSettlements.Add(Settlement.CurrentSettlement);
				return true;
			}
			return false;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0003003C File Offset: 0x0002E23C
		private bool conversation_arena_master_no_tournament_meet_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && !this._knowTournaments)
			{
				MBTextManager.SetTextVariable("TOWN_NAME", Settlement.CurrentSettlement.Name, false);
				this._knowTournaments = true;
				this._arenaMasterHasMetInSettlements.Add(Settlement.CurrentSettlement);
				return true;
			}
			return false;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00030090 File Offset: 0x0002E290
		private static bool conversation_arena_practice_fight_explain_reward_on_condition()
		{
			MBTextManager.SetTextVariable("OPPONENT_COUNT_1", "3", false);
			MBTextManager.SetTextVariable("PRIZE_1", "5", false);
			MBTextManager.SetTextVariable("OPPONENT_COUNT_2", "6", false);
			MBTextManager.SetTextVariable("PRIZE_2", "10", false);
			MBTextManager.SetTextVariable("OPPONENT_COUNT_3", "10", false);
			MBTextManager.SetTextVariable("PRIZE_3", "25", false);
			MBTextManager.SetTextVariable("OPPONENT_COUNT_4", "20", false);
			MBTextManager.SetTextVariable("PRIZE_4", "60", false);
			MBTextManager.SetTextVariable("PRIZE_5", "250", false);
			MBTextManager.SetTextVariable("ARENA_REWARD", GameTexts.FindText("str_arena_reward", null), false);
			return true;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00030144 File Offset: 0x0002E344
		private static bool conversation_arena_tournament_explain_reward_on_condition()
		{
			MBTextManager.SetTextVariable("TOURNAMENT_REWARD", new TextObject("{=1esi62Zb}Well - we like tournaments to be memorable. So the sponsors pitch together and buy a prize that they'll be talking about in the markets for weeks. A jeweled blade, say, or a fine-bred warhorse. Something a champion would be proud to own.", null), false);
			return true;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00030160 File Offset: 0x0002E360
		private bool conversation_arena_master_meet_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && this._knowTournaments && Settlement.CurrentSettlement.IsTown && !this._arenaMasterHasMetInSettlements.Contains(Settlement.CurrentSettlement))
			{
				MBTextManager.SetTextVariable("TOWN_NAME", Settlement.CurrentSettlement.Name, false);
				this._arenaMasterHasMetInSettlements.Add(Settlement.CurrentSettlement);
				return true;
			}
			return false;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000301C8 File Offset: 0x0002E3C8
		private bool conversation_arena_master_meet_start_on_condition()
		{
			return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && this._knowTournaments && Settlement.CurrentSettlement.IsTown && this._arenaMasterHasMetInSettlements.Contains(Settlement.CurrentSettlement) && !Mission.Current.GetMissionBehavior<ArenaPracticeFightMissionController>().AfterPractice;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003021C File Offset: 0x0002E41C
		private bool conversation_arena_master_player_knows_arenas_on_condition()
		{
			return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && this._knowTournaments && Settlement.CurrentSettlement.IsTown && !this._arenaMasterHasMetInSettlements.Contains(Settlement.CurrentSettlement) && !Mission.Current.GetMissionBehavior<ArenaPracticeFightMissionController>().AfterPractice;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00030270 File Offset: 0x0002E470
		public static void conversation_arena_join_tournament_on_consequence()
		{
			Mission.Current.EndMission();
			Campaign.Current.GameMenuManager.SetNextMenu("menu_town_tournament_join");
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00030290 File Offset: 0x0002E490
		public static void conversation_arena_join_fight_on_consequence()
		{
			Campaign.Current.ConversationManager.ConversationEndOneShot += ArenaMasterCampaignBehavior.StartPlayerPracticeAfterConversationEnd;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000302AD File Offset: 0x0002E4AD
		private static void StartPlayerPracticeAfterConversationEnd()
		{
			Mission.Current.SetMissionMode(MissionMode.Battle, false);
			Mission.Current.GetMissionBehavior<ArenaPracticeFightMissionController>().StartPlayerPractice();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000302CC File Offset: 0x0002E4CC
		private bool conversation_arena_master_post_fight_on_condition()
		{
			Mission mission = Mission.Current;
			ArenaPracticeFightMissionController arenaPracticeFightMissionController = (mission != null) ? mission.GetMissionBehavior<ArenaPracticeFightMissionController>() : null;
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.ArenaMaster && Settlement.CurrentSettlement.IsTown && arenaPracticeFightMissionController != null && arenaPracticeFightMissionController.AfterPractice)
			{
				arenaPracticeFightMissionController.AfterPractice = false;
				int opponentCountBeatenByPlayer = arenaPracticeFightMissionController.OpponentCountBeatenByPlayer;
				bool remainingOpponentCountFromLastPractice = arenaPracticeFightMissionController.RemainingOpponentCountFromLastPractice != 0;
				int num = 0;
				int num2;
				if (!remainingOpponentCountFromLastPractice)
				{
					num2 = 6;
					num = 250;
				}
				else if (opponentCountBeatenByPlayer == 0)
				{
					num2 = 0;
				}
				else if (opponentCountBeatenByPlayer < 3)
				{
					num2 = 1;
				}
				else if (opponentCountBeatenByPlayer < 6)
				{
					num2 = 2;
					num = 5;
				}
				else if (opponentCountBeatenByPlayer < 10)
				{
					num2 = 3;
					num = 10;
				}
				else if (opponentCountBeatenByPlayer < 20)
				{
					num2 = 4;
					num = 25;
				}
				else
				{
					num2 = 5;
					num = 60;
				}
				MBTextManager.SetTextVariable("PRIZE", num);
				MBTextManager.SetTextVariable("OPPONENT_COUNT", opponentCountBeatenByPlayer);
				TextObject text = GameTexts.FindText("str_arena_take_down", num2.ToString());
				MBTextManager.SetTextVariable("FIGHT_DEBRIEF", text, false);
				if (num > 0)
				{
					GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, num, true);
					MBTextManager.SetTextVariable("GOLD_AMOUNT", num);
					InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_quest_gold_reward_msg", null).ToString(), "event:/ui/notification/coins_positive"));
				}
				Mission.Current.SetMissionMode(MissionMode.Conversation, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000303F3 File Offset: 0x0002E5F3
		private void AfterMissionStarted(IMission obj)
		{
			if (this._enteredPracticeFightFromMenu)
			{
				Mission.Current.SetMissionMode(MissionMode.Battle, true);
				Mission.Current.GetMissionBehavior<ArenaPracticeFightMissionController>().StartPlayerPractice();
				this._enteredPracticeFightFromMenu = false;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00030420 File Offset: 0x0002E620
		private void game_menu_enter_practice_fight_on_consequence(MenuCallbackArgs args)
		{
			if (!this._arenaMasterHasMetInSettlements.Contains(Settlement.CurrentSettlement))
			{
				this._arenaMasterHasMetInSettlements.Add(Settlement.CurrentSettlement);
			}
			PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(LocationComplex.Current.GetLocationWithId("arena"), null, null, null);
			this._enteredPracticeFightFromMenu = true;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00030474 File Offset: 0x0002E674
		private bool game_menu_enter_practice_fight_on_condition(MenuCallbackArgs args)
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			args.optionLeaveType = GameMenuOption.LeaveType.PracticeFight;
			if (!this._knowTournaments)
			{
				args.Tooltip = new TextObject("{=Sph9Nliz}You need to learn more about the arena by talking with the arena master.", null);
				args.IsEnabled = false;
				return true;
			}
			if (Hero.MainHero.IsWounded && Campaign.Current.IsMainHeroDisguised)
			{
				args.Tooltip = new TextObject("{=DqZtRBXR}You are wounded and in disguise.", null);
				args.IsEnabled = false;
				return true;
			}
			if (Hero.MainHero.IsWounded)
			{
				args.Tooltip = new TextObject("{=yNMrF2QF}You are wounded", null);
				args.IsEnabled = false;
				return true;
			}
			if (Campaign.Current.IsMainHeroDisguised)
			{
				args.Tooltip = new TextObject("{=jcEoUPCB}You are in disguise.", null);
				args.IsEnabled = false;
				return true;
			}
			if (currentSettlement.Town.HasTournament)
			{
				args.Tooltip = new TextObject("{=NESB0CVc}There is no practice fight because of the Tournament.", null);
				args.IsEnabled = false;
				return true;
			}
			return true;
		}

		// Token: 0x040002D1 RID: 721
		private List<Settlement> _arenaMasterHasMetInSettlements = new List<Settlement>();

		// Token: 0x040002D2 RID: 722
		private bool _knowTournaments;

		// Token: 0x040002D3 RID: 723
		private bool _enteredPracticeFightFromMenu;
	}
}
