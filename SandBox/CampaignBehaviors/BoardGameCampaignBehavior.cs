﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.BoardGames;
using SandBox.BoardGames.MissionLogics;
using SandBox.Conversation;
using SandBox.Conversation.MissionLogics;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A2 RID: 162
	public class BoardGameCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00030D0B File Offset: 0x0002EF0B
		public IEnumerable<Settlement> WonBoardGamesInOneWeekInSettlement
		{
			get
			{
				foreach (Settlement settlement in this._wonBoardGamesInOneWeekInSettlement.Keys)
				{
					yield return settlement;
				}
				Dictionary<Settlement, CampaignTime>.KeyCollection.Enumerator enumerator = default(Dictionary<Settlement, CampaignTime>.KeyCollection.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00030D1C File Offset: 0x0002EF1C
		public override void RegisterEvents()
		{
			CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
			CampaignEvents.OnMissionEndedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionEnd));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnPlayerBoardGameOverEvent.AddNonSerializedListener(this, new Action<Hero, BoardGameHelper.BoardGameState>(this.OnPlayerBoardGameOver));
			CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, new Action(this.WeeklyTick));
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00030DCA File Offset: 0x0002EFCA
		private void OnMissionEnd(IMission obj)
		{
			this._initializedBoardGameCultureInMission = null;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00030DD3 File Offset: 0x0002EFD3
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Hero, List<CampaignTime>>>("_heroAndBoardGameTimeDictionary", ref this._heroAndBoardGameTimeDictionary);
			dataStore.SyncData<Dictionary<Settlement, CampaignTime>>("_wonBoardGamesInOneWeekInSettlement", ref this._wonBoardGamesInOneWeekInSettlement);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00030DF9 File Offset: 0x0002EFF9
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00030E04 File Offset: 0x0002F004
		private void WeeklyTick()
		{
			this.DeleteOldBoardGamesOfChampion();
			foreach (Hero hero in this._heroAndBoardGameTimeDictionary.Keys.ToList<Hero>())
			{
				this.DeleteOldBoardGamesOfHero(hero);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00030E68 File Offset: 0x0002F068
		private void OnPlayerBoardGameOver(Hero opposingHero, BoardGameHelper.BoardGameState state)
		{
			if (opposingHero != null)
			{
				this.GameEndWithHero(opposingHero);
				if (state == BoardGameHelper.BoardGameState.Win)
				{
					this._opposingHeroExtraXPGained = (this._difficulty != BoardGameHelper.AIDifficulty.Hard && MBRandom.RandomFloat <= 0.5f);
					SkillLevelingManager.OnBoardGameWonAgainstLord(opposingHero, this._difficulty, this._opposingHeroExtraXPGained);
					float num = 0.1f;
					num += ((opposingHero.IsFemale != Hero.MainHero.IsFemale) ? 0.1f : 0f);
					num += (float)Hero.MainHero.GetSkillValue(DefaultSkills.Charm) / 100f;
					num += ((opposingHero.GetTraitLevel(DefaultTraits.Calculating) == 1) ? 0.2f : 0f);
					bool flag = MBRandom.RandomFloat <= num;
					bool flag2 = opposingHero.MapFaction == Hero.MainHero.MapFaction && this._difficulty == BoardGameHelper.AIDifficulty.Hard && MBRandom.RandomFloat <= 0.4f;
					bool flag3 = this._difficulty == BoardGameHelper.AIDifficulty.Hard;
					if (flag)
					{
						ChangeRelationAction.ApplyPlayerRelation(opposingHero, 1, true, true);
						this._relationGained = true;
					}
					else if (flag2)
					{
						GainKingdomInfluenceAction.ApplyForBoardGameWon(opposingHero, 1f);
						this._influenceGained = true;
					}
					else if (flag3)
					{
						GainRenownAction.Apply(Hero.MainHero, 1f, false);
						this._renownGained = true;
					}
					else
					{
						this._gainedNothing = true;
					}
				}
			}
			else if (state == BoardGameHelper.BoardGameState.Win)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._betAmount, false);
				if (this._betAmount > 0)
				{
					this.PlayerWonAgainstTavernChampion();
				}
			}
			else if (state == BoardGameHelper.BoardGameState.Loss)
			{
				GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, this._betAmount, false);
			}
			this.SetBetAmount(0);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		public void InitializeConversationVars()
		{
			ICampaignMission campaignMission = CampaignMission.Current;
			string a;
			if (campaignMission == null)
			{
				a = null;
			}
			else
			{
				Location location = campaignMission.Location;
				a = ((location != null) ? location.StringId : null);
			}
			if (!(a == "lordshall"))
			{
				ICampaignMission campaignMission2 = CampaignMission.Current;
				string a2;
				if (campaignMission2 == null)
				{
					a2 = null;
				}
				else
				{
					Location location2 = campaignMission2.Location;
					a2 = ((location2 != null) ? location2.StringId : null);
				}
				if (!(a2 == "tavern"))
				{
					return;
				}
			}
			CultureObject boardGameCulture = this.GetBoardGameCulture();
			CultureObject.BoardGameType boardGame = boardGameCulture.BoardGame;
			if (boardGame == CultureObject.BoardGameType.None)
			{
				MBDebug.ShowWarning("Boardgame not yet implemented, or not found.");
			}
			if (boardGame != CultureObject.BoardGameType.None)
			{
				MBTextManager.SetTextVariable("GAME_NAME", GameTexts.FindText("str_boardgame_name", boardGame.ToString()), false);
				MBTextManager.SetTextVariable("CULTURE_NAME", boardGameCulture.Name, false);
				MBTextManager.SetTextVariable("DIFFICULTY", GameTexts.FindText("str_boardgame_difficulty", this._difficulty.ToString()), false);
				MBTextManager.SetTextVariable("BET_AMOUNT", this._betAmount.ToString(), false);
				MBTextManager.SetTextVariable("IS_BETTING", (this._betAmount > 0) ? 1 : 0);
				Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetBoardGame(boardGame);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003110C File Offset: 0x0002F30C
		public void OnMissionStarted(IMission mission)
		{
			Mission mission2 = (Mission)mission;
			if (Mission.Current.Scene != null)
			{
				Mission.Current.Scene.FindEntityWithTag("boardgame") != null;
			}
			if (Mission.Current.Scene != null && Mission.Current.Scene.FindEntityWithTag("boardgame_holder") != null && CampaignMission.Current.Location != null && (CampaignMission.Current.Location.StringId == "lordshall" || CampaignMission.Current.Location.StringId == "tavern"))
			{
				mission2.AddMissionBehavior(new MissionBoardGameLogic());
				this.InitializeBoardGamePrefabInMission();
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000311D4 File Offset: 0x0002F3D4
		private CultureObject GetBoardGameCulture()
		{
			if (this._initializedBoardGameCultureInMission != null)
			{
				return this._initializedBoardGameCultureInMission;
			}
			if (CampaignMission.Current.Location.StringId == "lordshall")
			{
				return Settlement.CurrentSettlement.OwnerClan.Culture;
			}
			return Settlement.CurrentSettlement.Culture;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00031228 File Offset: 0x0002F428
		private void InitializeBoardGamePrefabInMission()
		{
			if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
			{
				return;
			}
			CultureObject boardGameCulture = this.GetBoardGameCulture();
			CultureObject.BoardGameType boardGame = boardGameCulture.BoardGame;
			GameEntity gameEntity = Mission.Current.Scene.FindEntityWithTag("boardgame_holder");
			MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
			Mission.Current.Scene.RemoveEntity(gameEntity, 92);
			GameEntity gameEntity2 = GameEntity.Instantiate(Mission.Current.Scene, "BoardGame" + boardGame.ToString() + "_FullSetup", true);
			GameEntity gameEntity3 = gameEntity2;
			MatrixFrame matrixFrame = globalFrame.TransformToParent(gameEntity2.GetFrame());
			gameEntity3.SetGlobalFrame(matrixFrame);
			GameEntity firstChildEntityWithTag = gameEntity2.GetFirstChildEntityWithTag("dice_board");
			if (firstChildEntityWithTag != null && firstChildEntityWithTag.HasScriptOfType<VertexAnimator>())
			{
				firstChildEntityWithTag.GetFirstScriptOfType<VertexAnimator>().StopAndGoToEnd();
			}
			this._initializedBoardGameCultureInMission = boardGameCulture;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000312FA File Offset: 0x0002F4FA
		public void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (this._heroAndBoardGameTimeDictionary.ContainsKey(victim))
			{
				this._heroAndBoardGameTimeDictionary.Remove(victim);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00031318 File Offset: 0x0002F518
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			if (settlement.IsTown && CampaignMission.Current != null)
			{
				Location location = CampaignMission.Current.Location;
				int num;
				if (location != null && location.StringId == "tavern" && unusedUsablePointCount.TryGetValue("spawnpoint_tavernkeeper", out num) && num > 0)
				{
					location.AddLocationCharacters(new CreateLocationCharacterDelegate(BoardGameCampaignBehavior.CreateGameHost), settlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
				}
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003138C File Offset: 0x0002F58C
		private static LocationCharacter CreateGameHost(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject tavernGamehost = culture.TavernGamehost;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(tavernGamehost.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(tavernGamehost, out minValue, out maxValue, "");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(tavernGamehost, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddCompanionBehaviors), "gambler_npc", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_villager"), true, false, null, false, false, true);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0003143C File Offset: 0x0002F63C
		protected void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("talk_common_to_taverngamehost", "start", "close_window", "{GAME_MASTER_INTRO}", () => this.conversation_talk_common_to_taverngamehost_on_condition() && !BoardGameCampaignBehavior.taverngamehost_player_sitting_now_on_condition(), null, 100, null);
			campaignGameStarter.AddDialogLine("talk_common_to_taverngamehost_2", "start", "taverngamehost_talk", "{=LGrzKlET}Let me know how much of a challenge you can stand and we'll get started. I'm ready to offer you a {DIFFICULTY} challenge and {?IS_BETTING}a bet of {BET_AMOUNT}{GOLD_ICON}.{?}friendly game.{\\?}", () => this.conversation_talk_common_to_taverngamehost_on_condition() && BoardGameCampaignBehavior.taverngamehost_player_sitting_now_on_condition(), null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_game", "taverngamehost_talk", "taverngamehost_think_play", "{=BdpW8gUM}That looks good, let's play!", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_change_difficulty", "taverngamehost_talk", "taverngamehost_change_difficulty", "{=MbwG7Gy8}Can I change the difficulty?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_change_bet", "taverngamehost_talk", "taverngamehost_change_bet", "{=PbDK3PIi}Can I change the amount we're betting?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_game_history", "taverngamehost_talk", "taverngamehost_learn_history", "{=YM7etEzu}What exactly is {GAME_NAME}?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_reject", "taverngamehost_talk", "close_window", "{=N7BFbQmT}I'm not interested.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("taverngamehost_start_playing_ask_accept", "taverngamehost_think_play", "taverngamehost_start_play", "{=GrHJYz7O}Very well. Now, what side do you want?", new ConversationSentence.OnConditionDelegate(this.taverngame_host_play_game_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_start_playing_ask_decline", "taverngamehost_think_play", "taverngamehost_talk", "{=bTnmpqU4}I'm afraid I don't have time for another game.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=7tuyySmq}I'll start.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_seega_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=J9fJlz2Y}You can start.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_seega_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first_2", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=HdT5YyAb}I'll be white.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_puluc_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last_2", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=i8HysulS}I'll be black.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_puluc_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first_3", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=HdT5YyAb}I'll be white.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_konane_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last_3", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=i8HysulS}I'll be black.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_konane_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first_4", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=HdT5YyAb}I'll be white.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_mutorere_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last_4", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=i8HysulS}I'll be black.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_mutorere_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first_5", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=EnOOqaqf}I'll be sheep.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_baghchal_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last_5", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=QjtOAyKE}I'll be wolves.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_baghchal_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_first_6", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=qsavxffL}I'll be attackers.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_tablut_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_one_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_last_6", "taverngamehost_start_play", "taverngamehost_confirm_play", "{=WD7vOalb}I'll be defenders.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_tablut_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_set_player_two_starts_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_back", "taverngamehost_start_play", "start", "{=dUSfRYYH}Just a minute..", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_start_playing_now", "taverngamehost_confirm_play", "close_window", "{=aB1EZssb}Great, let's begin!", null, new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_play_game_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("taverngamehost_ask_difficulty", "taverngamehost_change_difficulty", "taverngamehost_changing_difficulty", "{=9VR0VeNT}Yes, how easy should I make things for you?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_change_difficulty_easy", "taverngamehost_changing_difficulty", "start", "{=j9Weia10}Easy", null, new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_difficulty_easy_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_change_difficulty_normal", "taverngamehost_changing_difficulty", "start", "{=8UBfIenN}Normal", null, new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_difficulty_normal_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_change_difficulty_hard", "taverngamehost_changing_difficulty", "start", "{=OnaJowBF}Hard. Don't hold back or you'll regret it.", null, new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_difficulty_hard_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("taverngamehost_ask_betting", "taverngamehost_change_bet", "taverngamehost_changing_bet", "{=T5jd4m69}That will only make this more fun. How much were you thinking?", new ConversationSentence.OnConditionDelegate(this.conversation_taverngamehost_talk_place_bet_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_100_denars", "taverngamehost_changing_bet", "start", "{=T29epQk3}100{GOLD_ICON}", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_can_bet_100_denars_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_100_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_200_denars", "taverngamehost_changing_bet", "start", "{=mHm5SLhb}200{GOLD_ICON}", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_can_bet_200_denars_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_200_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_300_denars", "taverngamehost_changing_bet", "start", "{=LnbzQIz6}300{GOLD_ICON}", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_can_bet_300_denars_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_300_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_400_denars", "taverngamehost_changing_bet", "start", "{=ck36TZFP}400{GOLD_ICON}", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_can_bet_400_denars_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_400_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_500_denars", "taverngamehost_changing_bet", "start", "{=YHTTPKMb}500{GOLD_ICON}", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_can_bet_500_denars_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_500_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_0_denars", "taverngamehost_changing_bet", "start", "{=lVx35dWp}On second thought, let's keep this match friendly.", null, new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_bet_0_denars_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("taverngamehost_deny_betting", "taverngamehost_change_bet", "taverngamehost_changing_difficulty_for_bet", "{=4xtBNkjN}Unfortunately, I only allow betting when I'm playing at my best. You'll have to up the difficulty.", new ConversationSentence.OnConditionDelegate(this.conversation_taverngamehost_talk_not_place_bet_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_changing_difficulty_for_bet_yes", "taverngamehost_changing_difficulty_for_bet", "taverngamehost_change_bet_2", "{=i4xzuOJE}Sure, I'll play at the hardest level.", null, new ConversationSentence.OnConsequenceDelegate(this.conversation_taverngamehost_difficulty_hard_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_changing_difficulty_for_bet_no", "taverngamehost_changing_difficulty_for_bet", "start", "{=2ynnnR4c}I'd prefer to keep the difficulty where it's at.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("taverngamehost_ask_betting_2", "taverngamehost_change_bet_2", "taverngamehost_changing_bet", "{=GfHssUYV}Now, feel free to place a bet.", new ConversationSentence.OnConditionDelegate(this.conversation_taverngamehost_talk_place_bet_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_seega", "taverngamehost_learn_history", "taverngamehost_after_history", "{=9PUvbZzD}{GAME_NAME} is a traditional game within the {CULTURE_NAME}. It is a game of calm strategy. You start by placing your pieces on the board, crafting a trap for your enemy to fall into. Then you battle across the board, capturing and eliminating your opponent.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_seega_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_puluc", "taverngamehost_learn_history", "taverngamehost_after_history", "{=sVcJTu7K}{GAME_NAME} is fast and harsh, as warfare should be. Capture as much as possible to keep your opponent weakened and demoralized. But behind this endless offense, there should always be a strong defense to punish any attempt from your opponent to regain control.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_puluc_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_mutorere", "taverngamehost_learn_history", "taverngamehost_after_history", "{=SV0IEWD2}{GAME_NAME} is a game of anticipation. With no possibility of capturing, all your effort should be on reading your opponent and planning further ahead than him.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_mutorere_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_konane", "taverngamehost_learn_history", "taverngamehost_after_history", "{=tVb0nWxm}War is all about sacrifice. In {GAME_NAME} you must make sure that your opponent sacrifices more than you do. Every move can expose you or your opponent and must be carefully considered.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_konane_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_baghchal", "taverngamehost_learn_history", "taverngamehost_after_history", "{=mo4rbYvm}A couple of powerful wolves against a flock of helpless sheep. {GAME_NAME} is a game of uneven odds and seemingly all-powerful adversaries. But through strategy and sacrifice, even the sheep can dominate the wolves.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_baghchal_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("taverngamehost_tell_history_tablut", "taverngamehost_learn_history", "taverngamehost_after_history", "{=nMzfnOFG}{GAME_NAME} is a game of incredibly uneven odds. A weakened and trapped king must try to escape from a horde of attackers who assault from every direction. Ironic how we, the once all-powerful {CULTURE_NAME}, have now fallen in the same position.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_taverngamehost_talk_is_tablut_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_history_back", "taverngamehost_after_history", "start", "{=QP7L2YLG}Sounds fun.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("taverngamehost_player_history_leave", "taverngamehost_after_history", "close_window", "{=Ng6Rrlr6}I'd rather do something else", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("lord_player_play_game", "hero_main_options", "lord_answer_to_play_boardgame", "{=3hv4P5OO}Would you care to pass the time with a game of {GAME_NAME}?", new ConversationSentence.OnConditionDelegate(this.conversation_lord_talk_game_on_condition), null, 2, null, null);
			campaignGameStarter.AddPlayerLine("lord_player_cancel_boardgame", "hero_main_options", "lord_answer_to_cancel_play_boardgame", "{=ySk7bD8P}Actually, I have other things to do. Maybe later.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_lord_talk_cancel_game_on_condition), null, 2, null, null);
			campaignGameStarter.AddDialogLine("lord_agrees_cancel_play", "lord_answer_to_cancel_play_boardgame", "close_window", "{=dzXaXKaC}Very well.", null, new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_lord_talk_cancel_game_on_consequence), 100, null);
			campaignGameStarter.AddPlayerLine("lord_player_ask_to_play_boardgame_again", "hero_main_options", "lord_answer_to_play_again_boardgame", "{=U342eACh}Would you like to play another round of {GAME_NAME}?", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.conversation_lord_talk_game_again_on_condition), null, 2, null, null);
			campaignGameStarter.AddDialogLine("lord_answer_to_play_boardgame_again_accept", "lord_answer_to_play_again_boardgame", "close_window", "{=aD1BoB3c}Yes. Let's have another round.", new ConversationSentence.OnConditionDelegate(this.conversation_lord_play_game_on_condition), new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_lord_play_game_again_on_consequence), 100, null);
			campaignGameStarter.AddDialogLine("lord_answer_to_play_boardgame_again_decline", "lord_answer_to_play_again_boardgame", "hero_main_options", "{=fqKVojaV}No, not now.", null, new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_lord_dont_play_game_again_on_consequence), 100, null);
			campaignGameStarter.AddDialogLine("lord_after_player_win_boardgame", "start", "close_window", "{=!}{PLAYER_GAME_WON_LORD_STRING}", new ConversationSentence.OnConditionDelegate(this.lord_after_player_win_boardgame_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("lord_after_lord_win_boardgame", "start", "hero_main_options", "{=dC6YhgPP}Ah. A good match, that.", new ConversationSentence.OnConditionDelegate(BoardGameCampaignBehavior.lord_after_lord_win_boardgame_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("lord_agrees_play", "lord_answer_to_play_boardgame", "lord_setup_game", "{=!}{GAME_AGREEMENT_STRING}", new ConversationSentence.OnConditionDelegate(this.conversation_lord_play_game_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_lord_detect_difficulty_consequence), 100, null);
			campaignGameStarter.AddPlayerLine("lord_player_start_game", "lord_setup_game", "close_window", "{=bAy9PdrF}Let's begin, then.", null, new ConversationSentence.OnConsequenceDelegate(BoardGameCampaignBehavior.conversation_lord_play_game_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("lord_player_leave", "lord_setup_game", "close_window", "{=OQgBim7l}Actually, I have other things to do.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("lord_refuses_play", "lord_answer_to_play_boardgame", "close_window", "{=!}{LORD_REJECT_GAME_STRING}", new ConversationSentence.OnConditionDelegate(this.conversation_lord_reject_game_condition), null, 100, null);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00031E78 File Offset: 0x00030078
		private bool conversation_lord_reject_game_condition()
		{
			TextObject text = (Hero.OneToOneConversationHero.GetRelationWithPlayer() > -20f) ? new TextObject("{=aRDcoLX0}Now is not a good time, {PLAYER.NAME}. ", null) : new TextObject("{=GLRrAj61}I do not wish to play games with the likes of you.", null);
			MBTextManager.SetTextVariable("LORD_REJECT_GAME_STRING", text, false);
			return true;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00031EBC File Offset: 0x000300BC
		private bool conversation_talk_common_to_taverngamehost_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation != Occupation.TavernGameHost)
			{
				return false;
			}
			this.InitializeConversationVars();
			MBTextManager.SetTextVariable("GAME_MASTER_INTRO", "{=HDhLMbt7}Greetings, traveler. Do you play {GAME_NAME}? I am reckoned a master of this game, the traditional pastime of the {CULTURE_NAME}. If you are interested in playing, take a seat and we'll start.", false);
			if (Settlement.CurrentSettlement.OwnerClan == Hero.MainHero.Clan || Settlement.CurrentSettlement.MapFaction.Leader == Hero.MainHero)
			{
				MBTextManager.SetTextVariable("GAME_MASTER_INTRO", "{=yN4imaGo}Your {?PLAYER.GENDER}ladyship{?}lordship{\\?}... This is quite the honor. Do you play {GAME_NAME}? It's the traditional pastime of the {CULTURE_NAME}, and I am reckoned a master. If you wish to play a game, please, take a seat and we'll start.", false);
			}
			return true;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00031F2C File Offset: 0x0003012C
		private void conversation_taverngamehost_bet_0_denars_on_consequence()
		{
			this.SetBetAmount(0);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00031F38 File Offset: 0x00030138
		private static bool conversation_taverngamehost_can_bet_100_denars_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			CharacterObject characterObject = (CharacterObject)Agent.Main.Character;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 100;
			bool flag2 = characterObject.HeroObject.Gold >= 100;
			return flag && flag2;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00031F8D File Offset: 0x0003018D
		private void conversation_taverngamehost_bet_100_denars_on_consequence()
		{
			this.SetBetAmount(100);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00031F98 File Offset: 0x00030198
		private static bool conversation_taverngamehost_can_bet_200_denars_on_condition()
		{
			CharacterObject characterObject = (CharacterObject)ConversationMission.OneToOneConversationAgent.Character;
			CharacterObject characterObject2 = (CharacterObject)Agent.Main.Character;
			bool flag = !characterObject.IsHero || characterObject.HeroObject.Gold >= 200;
			bool flag2 = characterObject2.HeroObject.Gold >= 200;
			return flag && flag2;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00031FFD File Offset: 0x000301FD
		private void conversation_taverngamehost_bet_200_denars_on_consequence()
		{
			this.SetBetAmount(200);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003200C File Offset: 0x0003020C
		private static bool conversation_taverngamehost_can_bet_300_denars_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			CharacterObject characterObject = (CharacterObject)Agent.Main.Character;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 300;
			bool flag2 = characterObject.HeroObject.Gold >= 300;
			return flag && flag2;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00032067 File Offset: 0x00030267
		private void conversation_taverngamehost_bet_300_denars_on_consequence()
		{
			this.SetBetAmount(300);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00032074 File Offset: 0x00030274
		private static bool conversation_taverngamehost_can_bet_400_denars_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			CharacterObject characterObject = (CharacterObject)Agent.Main.Character;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 400;
			bool flag2 = characterObject.HeroObject.Gold >= 400;
			return flag && flag2;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000320CF File Offset: 0x000302CF
		private void conversation_taverngamehost_bet_400_denars_on_consequence()
		{
			this.SetBetAmount(400);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000320DC File Offset: 0x000302DC
		private static bool conversation_taverngamehost_can_bet_500_denars_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			CharacterObject characterObject = (CharacterObject)Agent.Main.Character;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 500;
			bool flag2 = characterObject.HeroObject.Gold >= 500;
			return flag && flag2;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00032137 File Offset: 0x00030337
		private bool taverngame_host_play_game_on_condition()
		{
			if (this._betAmount == 0)
			{
				return true;
			}
			this.DeleteOldBoardGamesOfChampion();
			return !this._wonBoardGamesInOneWeekInSettlement.ContainsKey(Settlement.CurrentSettlement);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0003215C File Offset: 0x0003035C
		private void conversation_taverngamehost_bet_500_denars_on_consequence()
		{
			this.SetBetAmount(500);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00032169 File Offset: 0x00030369
		private void conversation_taverngamehost_difficulty_easy_on_consequence()
		{
			this.SetDifficulty(BoardGameHelper.AIDifficulty.Easy);
			this.SetBetAmount(0);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00032179 File Offset: 0x00030379
		private void conversation_taverngamehost_difficulty_normal_on_consequence()
		{
			this.SetDifficulty(BoardGameHelper.AIDifficulty.Normal);
			this.SetBetAmount(0);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00032189 File Offset: 0x00030389
		private void conversation_taverngamehost_difficulty_hard_on_consequence()
		{
			this.SetDifficulty(BoardGameHelper.AIDifficulty.Hard);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00032192 File Offset: 0x00030392
		private static void conversation_lord_play_game_again_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().DetectOpposingAgent();
			Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
			{
				Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().StartBoardGame();
			};
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000321D1 File Offset: 0x000303D1
		private static void conversation_lord_dont_play_game_again_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetGameOver(GameOverEnum.PlayerCanceledTheGame);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000321E4 File Offset: 0x000303E4
		private void conversation_lord_detect_difficulty_consequence()
		{
			int skillValue = ConversationMission.OneToOneConversationCharacter.GetSkillValue(DefaultSkills.Steward);
			if (skillValue >= 0 && skillValue < 50)
			{
				this.SetDifficulty(BoardGameHelper.AIDifficulty.Easy);
				return;
			}
			if (skillValue >= 50 && skillValue < 100)
			{
				this.SetDifficulty(BoardGameHelper.AIDifficulty.Normal);
				return;
			}
			if (skillValue >= 100)
			{
				this.SetDifficulty(BoardGameHelper.AIDifficulty.Hard);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00032230 File Offset: 0x00030430
		private static void conversation_taverngamehost_set_player_one_starts_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetStartingPlayer(true);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00032242 File Offset: 0x00030442
		private static void conversation_taverngamehost_set_player_two_starts_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetStartingPlayer(false);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00032254 File Offset: 0x00030454
		private static void conversation_taverngamehost_play_game_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().DetectOpposingAgent();
			Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
			{
				Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().StartBoardGame();
			};
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00032294 File Offset: 0x00030494
		private bool conversation_taverngamehost_talk_place_bet_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 100;
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return flag && this._difficulty == BoardGameHelper.AIDifficulty.Hard;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000322DC File Offset: 0x000304DC
		private bool conversation_taverngamehost_talk_not_place_bet_on_condition()
		{
			CharacterObject oneToOneConversationCharacter = ConversationMission.OneToOneConversationCharacter;
			bool flag = !oneToOneConversationCharacter.IsHero || oneToOneConversationCharacter.HeroObject.Gold >= 100;
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return flag && this._difficulty != BoardGameHelper.AIDifficulty.Hard;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00032328 File Offset: 0x00030528
		private static bool conversation_taverngamehost_talk_is_seega_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.Seega;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00032350 File Offset: 0x00030550
		private static bool conversation_taverngamehost_talk_is_puluc_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.Puluc;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00032378 File Offset: 0x00030578
		private static bool conversation_taverngamehost_talk_is_mutorere_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.MuTorere;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000323A0 File Offset: 0x000305A0
		private static bool conversation_taverngamehost_talk_is_konane_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.Konane;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000323C8 File Offset: 0x000305C8
		private static bool conversation_taverngamehost_talk_is_baghchal_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.BaghChal;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000323F0 File Offset: 0x000305F0
		private static bool conversation_taverngamehost_talk_is_tablut_on_condition()
		{
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			return missionBehavior != null && missionBehavior.CurrentBoardGame == CultureObject.BoardGameType.Tablut;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00032418 File Offset: 0x00030618
		public static bool taverngamehost_player_sitting_now_on_condition()
		{
			GameEntity gameEntity = Mission.Current.Scene.FindEntityWithTag("gambler_player");
			if (gameEntity != null)
			{
				Chair chair = gameEntity.CollectObjects<Chair>().FirstOrDefault<Chair>();
				return chair != null && Agent.Main != null && chair.IsAgentFullySitting(Agent.Main);
			}
			return false;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00032468 File Offset: 0x00030668
		private bool conversation_lord_talk_game_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Lord)
			{
				ICampaignMission campaignMission = CampaignMission.Current;
				string a;
				if (campaignMission == null)
				{
					a = null;
				}
				else
				{
					Location location = campaignMission.Location;
					a = ((location != null) ? location.StringId : null);
				}
				if (a == "lordshall" && MissionBoardGameLogic.IsBoardGameAvailable())
				{
					this.InitializeConversationVars();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000324BB File Offset: 0x000306BB
		private static bool conversation_lord_talk_game_again_on_condition()
		{
			return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Lord && MissionBoardGameLogic.IsThereActiveBoardGameWithHero(Hero.OneToOneConversationHero) && Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().IsGameInProgress;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000324E8 File Offset: 0x000306E8
		private static bool conversation_lord_talk_cancel_game_on_condition()
		{
			return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Lord && MissionBoardGameLogic.IsThereActiveBoardGameWithHero(Hero.OneToOneConversationHero) && (Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().IsOpposingAgentMovingToPlayingChair || !Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().IsGameInProgress);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00032535 File Offset: 0x00030735
		private static void conversation_lord_talk_cancel_game_on_consequence()
		{
			Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
			{
				Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetGameOver(GameOverEnum.PlayerCanceledTheGame);
			};
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00032568 File Offset: 0x00030768
		private static bool lord_after_lord_win_boardgame_condition()
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = (mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null;
			return missionBoardGameLogic != null && missionBoardGameLogic.BoardGameFinalState != BoardGameHelper.BoardGameState.None && missionBoardGameLogic.BoardGameFinalState != BoardGameHelper.BoardGameState.Win;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000325A0 File Offset: 0x000307A0
		private bool lord_after_player_win_boardgame_condition()
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = (mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null;
			if (missionBoardGameLogic != null && missionBoardGameLogic.BoardGameFinalState == BoardGameHelper.BoardGameState.Win)
			{
				if (this._relationGained)
				{
					MBTextManager.SetTextVariable("PLAYER_GAME_WON_LORD_STRING", "{=QTfliM5b}I enjoyed our game. Let?s play again later.", false);
				}
				else if (this._influenceGained)
				{
					MBTextManager.SetTextVariable("PLAYER_GAME_WON_LORD_STRING", "{=31oG5njl}You are a sharp thinker. Our kingdom would do well to hear your thoughts on matters of importance.", false);
				}
				else if (this._opposingHeroExtraXPGained)
				{
					MBTextManager.SetTextVariable("PLAYER_GAME_WON_LORD_STRING", "{=nxpyHb77}Well, I am still a novice in this game, but I learned a lot from playing with you.", false);
				}
				else if (this._renownGained)
				{
					MBTextManager.SetTextVariable("PLAYER_GAME_WON_LORD_STRING", "{=k1b5crrx}You are an accomplished player. I will take note of that.", false);
				}
				else if (this._gainedNothing)
				{
					MBTextManager.SetTextVariable("PLAYER_GAME_WON_LORD_STRING", "{=HzabMi4t}That was a fun game. Thank you.", false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00032654 File Offset: 0x00030854
		private bool conversation_lord_play_game_on_condition()
		{
			if (this.CanPlayerPlayBoardGameAgainstHero(Hero.OneToOneConversationHero))
			{
				string tagId = "DrinkingInTavernTag";
				if (MissionConversationLogic.Current.ConversationManager.IsTagApplicable(tagId, Hero.OneToOneConversationHero.CharacterObject))
				{
					MBTextManager.SetTextVariable("GAME_AGREEMENT_STRING", "{=LztDzy8W}Why not? I'm not going anywhere right now, and I could use another drink.", false);
				}
				else if (Hero.OneToOneConversationHero.CharacterObject.GetPersona() == DefaultTraits.PersonaCurt)
				{
					MBTextManager.SetTextVariable("GAME_AGREEMENT_STRING", "{=2luygc8o}Mm. I suppose. Takes my mind off all these problems I have to deal with.", false);
				}
				else if (Hero.OneToOneConversationHero.CharacterObject.GetPersona() == DefaultTraits.PersonaEarnest)
				{
					MBTextManager.SetTextVariable("GAME_AGREEMENT_STRING", "{=349mwgWC}Certainly. A good game always keeps the mind active and fresh.", false);
				}
				else if (Hero.OneToOneConversationHero.CharacterObject.GetPersona() == DefaultTraits.PersonaIronic)
				{
					MBTextManager.SetTextVariable("GAME_AGREEMENT_STRING", "{=rGaaVBBT}Ah. Very well. I don't mind testing your mettle.", false);
				}
				else if (Hero.OneToOneConversationHero.CharacterObject.GetPersona() == DefaultTraits.PersonaSoftspoken)
				{
					MBTextManager.SetTextVariable("GAME_AGREEMENT_STRING", "{=idPV1Csj}Yes... Why not? I have nothing too urgent right now.", false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00032749 File Offset: 0x00030949
		private static void conversation_lord_play_game_on_consequence()
		{
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().DetectOpposingAgent();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003275A File Offset: 0x0003095A
		public void PlayerWonAgainstTavernChampion()
		{
			if (!this._wonBoardGamesInOneWeekInSettlement.ContainsKey(Settlement.CurrentSettlement))
			{
				this._wonBoardGamesInOneWeekInSettlement.Add(Settlement.CurrentSettlement, CampaignTime.Now);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00032784 File Offset: 0x00030984
		private void GameEndWithHero(Hero hero)
		{
			if (this._heroAndBoardGameTimeDictionary.ContainsKey(hero))
			{
				this._heroAndBoardGameTimeDictionary[hero].Add(CampaignTime.Now);
				return;
			}
			this._heroAndBoardGameTimeDictionary.Add(hero, new List<CampaignTime>());
			this._heroAndBoardGameTimeDictionary[hero].Add(CampaignTime.Now);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000327E0 File Offset: 0x000309E0
		private bool CanPlayerPlayBoardGameAgainstHero(Hero hero)
		{
			if (hero.GetRelationWithPlayer() < 0f)
			{
				return false;
			}
			this.DeleteOldBoardGamesOfHero(hero);
			if (this._heroAndBoardGameTimeDictionary.ContainsKey(hero))
			{
				List<CampaignTime> list = this._heroAndBoardGameTimeDictionary[hero];
				return 3 > list.Count;
			}
			return true;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0003282C File Offset: 0x00030A2C
		private void DeleteOldBoardGamesOfChampion()
		{
			foreach (Settlement key in Settlement.All)
			{
				if (this._wonBoardGamesInOneWeekInSettlement.ContainsKey(key) && this._wonBoardGamesInOneWeekInSettlement[key].ElapsedWeeksUntilNow >= 1f)
				{
					this._wonBoardGamesInOneWeekInSettlement.Remove(key);
				}
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000328B0 File Offset: 0x00030AB0
		private void DeleteOldBoardGamesOfHero(Hero hero)
		{
			if (this._heroAndBoardGameTimeDictionary.ContainsKey(hero))
			{
				List<CampaignTime> list = this._heroAndBoardGameTimeDictionary[hero];
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i].ElapsedDaysUntilNow > 1f)
					{
						list.RemoveAt(i);
					}
				}
				if (list.IsEmpty<CampaignTime>())
				{
					this._heroAndBoardGameTimeDictionary.Remove(hero);
				}
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0003291D File Offset: 0x00030B1D
		public void SetBetAmount(int bet)
		{
			this._betAmount = bet;
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetBetAmount(bet);
			MBTextManager.SetTextVariable("BET_AMOUNT", bet.ToString(), false);
			MBTextManager.SetTextVariable("IS_BETTING", (bet > 0) ? 1 : 0);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0003295A File Offset: 0x00030B5A
		private void SetDifficulty(BoardGameHelper.AIDifficulty difficulty)
		{
			this._difficulty = difficulty;
			Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().SetCurrentDifficulty(difficulty);
			MBTextManager.SetTextVariable("DIFFICULTY", GameTexts.FindText("str_boardgame_difficulty", difficulty.ToString()), false);
		}

		// Token: 0x040002D7 RID: 727
		private const int NumberOfBoardGamesCanPlayerPlayAgainstHeroPerDay = 3;

		// Token: 0x040002D8 RID: 728
		private Dictionary<Hero, List<CampaignTime>> _heroAndBoardGameTimeDictionary = new Dictionary<Hero, List<CampaignTime>>();

		// Token: 0x040002D9 RID: 729
		private Dictionary<Settlement, CampaignTime> _wonBoardGamesInOneWeekInSettlement = new Dictionary<Settlement, CampaignTime>();

		// Token: 0x040002DA RID: 730
		private BoardGameHelper.AIDifficulty _difficulty;

		// Token: 0x040002DB RID: 731
		private int _betAmount;

		// Token: 0x040002DC RID: 732
		private bool _influenceGained;

		// Token: 0x040002DD RID: 733
		private bool _renownGained;

		// Token: 0x040002DE RID: 734
		private bool _opposingHeroExtraXPGained;

		// Token: 0x040002DF RID: 735
		private bool _relationGained;

		// Token: 0x040002E0 RID: 736
		private bool _gainedNothing;

		// Token: 0x040002E1 RID: 737
		private CultureObject _initializedBoardGameCultureInMission;
	}
}
