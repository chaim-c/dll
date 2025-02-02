using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000B0 RID: 176
	public class StatisticsCampaignBehavior : CampaignBehaviorBase, IStatisticsCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0003FD0C File Offset: 0x0003DF0C
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<int>("_highestTournamentRank", ref this._highestTournamentRank);
			dataStore.SyncData<int>("_numberOfTournamentWins", ref this._numberOfTournamentWins);
			dataStore.SyncData<int>("_numberOfChildrenBorn", ref this._numberOfChildrenBorn);
			dataStore.SyncData<int>("_numberOfPrisonersRecruited", ref this._numberOfPrisonersRecruited);
			dataStore.SyncData<int>("_numberOfTroopsRecruited", ref this._numberOfTroopsRecruited);
			dataStore.SyncData<int>("_numberOfClansDefected", ref this._numberOfClansDefected);
			dataStore.SyncData<int>("_numberOfIssuesSolved", ref this._numberOfIssuesSolved);
			dataStore.SyncData<int>("_totalInfluenceEarned", ref this._totalInfluenceEarned);
			dataStore.SyncData<int>("_totalCrimeRatingGained", ref this._totalCrimeRatingGained);
			dataStore.SyncData<ulong>("_totalTimePlayedInSeconds", ref this._totalTimePlayedInSeconds);
			dataStore.SyncData<int>("_numberOfbattlesWon", ref this._numberOfbattlesWon);
			dataStore.SyncData<int>("_numberOfbattlesLost", ref this._numberOfbattlesLost);
			dataStore.SyncData<int>("_largestBattleWonAsLeader", ref this._largestBattleWonAsLeader);
			dataStore.SyncData<int>("_largestArmyFormedByPlayer", ref this._largestArmyFormedByPlayer);
			dataStore.SyncData<int>("_numberOfEnemyClansDestroyed", ref this._numberOfEnemyClansDestroyed);
			dataStore.SyncData<int>("_numberOfHeroesKilledInBattle", ref this._numberOfHeroesKilledInBattle);
			dataStore.SyncData<int>("_numberOfTroopsKnockedOrKilledAsParty", ref this._numberOfTroopsKnockedOrKilledAsParty);
			dataStore.SyncData<int>("_numberOfTroopsKnockedOrKilledByPlayer", ref this._numberOfTroopsKnockedOrKilledByPlayer);
			dataStore.SyncData<int>("_numberOfHeroPrisonersTaken", ref this._numberOfHeroPrisonersTaken);
			dataStore.SyncData<int>("_numberOfTroopPrisonersTaken", ref this._numberOfTroopPrisonersTaken);
			dataStore.SyncData<int>("_numberOfTownsCaptured", ref this._numberOfTownsCaptured);
			dataStore.SyncData<int>("_numberOfHideoutsCleared", ref this._numberOfHideoutsCleared);
			dataStore.SyncData<int>("_numberOfCastlesCaptured", ref this._numberOfCastlesCaptured);
			dataStore.SyncData<int>("_numberOfVillagesRaided", ref this._numberOfVillagesRaided);
			dataStore.SyncData<CampaignTime>("_timeSpentAsPrisoner", ref this._timeSpentAsPrisoner);
			dataStore.SyncData<ulong>("_totalDenarsEarned", ref this._totalDenarsEarned);
			dataStore.SyncData<ulong>("_denarsEarnedFromCaravans", ref this._denarsEarnedFromCaravans);
			dataStore.SyncData<ulong>("_denarsEarnedFromWorkshops", ref this._denarsEarnedFromWorkshops);
			dataStore.SyncData<ulong>("_denarsEarnedFromRansoms", ref this._denarsEarnedFromRansoms);
			dataStore.SyncData<ulong>("_denarsEarnedFromTaxes", ref this._denarsEarnedFromTaxes);
			dataStore.SyncData<ulong>("_denarsEarnedFromTributes", ref this._denarsEarnedFromTributes);
			dataStore.SyncData<ulong>("_denarsPaidAsTributes", ref this._denarsPaidAsTributes);
			dataStore.SyncData<int>("_numberOfCraftingPartsUnlocked", ref this._numberOfCraftingPartsUnlocked);
			dataStore.SyncData<int>("_numberOfWeaponsCrafted", ref this._numberOfWeaponsCrafted);
			dataStore.SyncData<int>("_numberOfCraftingOrdersCompleted", ref this._numberOfCraftingOrdersCompleted);
			dataStore.SyncData<ValueTuple<string, int>>("_mostExpensiveItemCrafted", ref this._mostExpensiveItemCrafted);
			dataStore.SyncData<int>("_numberOfCompanionsHired", ref this._numberOfCompanionsHired);
			dataStore.SyncData<Dictionary<Hero, ValueTuple<int, int>>>("_companionData", ref this._companionData);
			dataStore.SyncData<int>("_lastPlayerBattleSize", ref this._lastPlayerBattleSize);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
			CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
			CampaignEvents.OnClanInfluenceChangedEvent.AddNonSerializedListener(this, new Action<Clan, float>(this.OnClanInfluenceChanged));
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.CrimeRatingChanged.AddNonSerializedListener(this, new Action<IFaction, float>(this.OnCrimeRatingChanged));
			CampaignEvents.OnMainPartyPrisonerRecruitedEvent.AddNonSerializedListener(this, new Action<FlattenedTroopRoster>(this.OnMainPartyPrisonerRecruited));
			CampaignEvents.OnUnitRecruitedEvent.AddNonSerializedListener(this, new Action<CharacterObject, int>(this.OnUnitRecruited));
			CampaignEvents.OnBeforeSaveEvent.AddNonSerializedListener(this, new Action(this.OnBeforeSave));
			CampaignEvents.CraftingPartUnlockedEvent.AddNonSerializedListener(this, new Action<CraftingPiece>(this.OnCraftingPartUnlocked));
			CampaignEvents.OnNewItemCraftedEvent.AddNonSerializedListener(this, new Action<ItemObject, ItemModifier, bool>(this.OnNewItemCrafted));
			CampaignEvents.NewCompanionAdded.AddNonSerializedListener(this, new Action<Hero>(this.OnNewCompanionAdded));
			CampaignEvents.HeroOrPartyTradedGold.AddNonSerializedListener(this, new Action<ValueTuple<Hero, PartyBase>, ValueTuple<Hero, PartyBase>, ValueTuple<int, string>, bool>(this.OnHeroOrPartyTradedGold));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnd));
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
			CampaignEvents.PartyAttachedAnotherParty.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyAttachedAnotherParty));
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnHeroPrisonerTaken));
			CampaignEvents.OnPrisonerTakenEvent.AddNonSerializedListener(this, new Action<FlattenedTroopRoster>(this.OnPrisonersTaken));
			CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
			CampaignEvents.OnHideoutBattleCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, HideoutEventComponent>(this.OnHideoutBattleCompleted));
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.HeroPrisonerReleased.AddNonSerializedListener(this, new Action<Hero, PartyBase, IFaction, EndCaptivityDetail>(this.OnHeroPrisonerReleased));
			CampaignEvents.OnPlayerPartyKnockedOrKilledTroopEvent.AddNonSerializedListener(this, new Action<CharacterObject>(this.OnPlayerPartyKnockedOrKilledTroop));
			CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
			CampaignEvents.OnPlayerEarnedGoldFromAssetEvent.AddNonSerializedListener(this, new Action<DefaultClanFinanceModel.AssetIncomeType, int>(this.OnPlayerEarnedGoldFromAsset));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00040252 File Offset: 0x0003E452
		private void OnBeforeSave()
		{
			this.UpdateTotalTimePlayedInSeconds();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0004025A File Offset: 0x0003E45A
		private void OnAfterSessionLaunched(CampaignGameStarter starter)
		{
			this._lastGameplayTimeCheck = DateTime.Now;
			if (this._highestTournamentRank == 0)
			{
				this._highestTournamentRank = Campaign.Current.TournamentManager.GetLeaderBoardRank(Hero.MainHero);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00040289 File Offset: 0x0003E489
		public void OnDefectionPersuasionSucess()
		{
			this._numberOfClansDefected++;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00040299 File Offset: 0x0003E499
		private void OnUnitRecruited(CharacterObject character, int amount)
		{
			this._numberOfTroopsRecruited += amount;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000402A9 File Offset: 0x0003E4A9
		private void OnMainPartyPrisonerRecruited(FlattenedTroopRoster flattenedTroopRoster)
		{
			this._numberOfPrisonersRecruited += flattenedTroopRoster.CountQ<FlattenedTroopRosterElement>();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000402BE File Offset: 0x0003E4BE
		private void OnCrimeRatingChanged(IFaction kingdom, float deltaCrimeAmount)
		{
			if (deltaCrimeAmount > 0f)
			{
				this._totalCrimeRatingGained += (int)deltaCrimeAmount;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000402D7 File Offset: 0x0003E4D7
		private void OnClanInfluenceChanged(Clan clan, float change)
		{
			if (change > 0f && clan == Clan.PlayerClan)
			{
				this._totalInfluenceEarned += (int)change;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000402F8 File Offset: 0x0003E4F8
		private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			if (winner.HeroObject == Hero.MainHero)
			{
				this._numberOfTournamentWins++;
				int leaderBoardRank = Campaign.Current.TournamentManager.GetLeaderBoardRank(Hero.MainHero);
				if (leaderBoardRank < this._highestTournamentRank)
				{
					this._highestTournamentRank = leaderBoardRank;
				}
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00040348 File Offset: 0x0003E548
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver = null)
		{
			if (details == IssueBase.IssueUpdateDetails.IssueFinishedWithSuccess || details == IssueBase.IssueUpdateDetails.SentTroopsFinishedQuest || details == IssueBase.IssueUpdateDetails.IssueFinishedWithBetrayal)
			{
				this._numberOfIssuesSolved++;
				if (issueSolver != null && issueSolver.IsPlayerCompanion)
				{
					if (this._companionData.ContainsKey(issueSolver))
					{
						this._companionData[issueSolver] = new ValueTuple<int, int>(this._companionData[issueSolver].Item1 + 1, this._companionData[issueSolver].Item2);
						return;
					}
					this._companionData.Add(issueSolver, new ValueTuple<int, int>(1, 0));
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000403D1 File Offset: 0x0003E5D1
		private void OnHeroCreated(Hero hero, bool isBornNaturally = false)
		{
			if (hero.Mother == Hero.MainHero || hero.Father == Hero.MainHero)
			{
				this._numberOfChildrenBorn++;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000403FB File Offset: 0x0003E5FB
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (killer != null && killer.PartyBelongedTo == MobileParty.MainParty && detail == KillCharacterAction.KillCharacterActionDetail.DiedInBattle)
			{
				this._numberOfHeroesKilledInBattle++;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00040420 File Offset: 0x0003E620
		private void OnMissionStarted(IMission mission)
		{
			StatisticsCampaignBehavior.StatisticsMissionLogic missionBehavior = new StatisticsCampaignBehavior.StatisticsMissionLogic();
			Mission.Current.AddMissionBehavior(missionBehavior);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00040440 File Offset: 0x0003E640
		private void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent)
		{
			if (affectorAgent != null)
			{
				if (affectorAgent == Agent.Main)
				{
					this._numberOfTroopsKnockedOrKilledByPlayer++;
				}
				else if (affectorAgent.IsPlayerTroop)
				{
					this._numberOfTroopsKnockedOrKilledAsParty++;
				}
				else if (affectorAgent.IsHero)
				{
					Hero heroObject = (affectorAgent.Character as CharacterObject).HeroObject;
					if (heroObject.IsPlayerCompanion)
					{
						if (this._companionData.ContainsKey(heroObject))
						{
							this._companionData[heroObject] = new ValueTuple<int, int>(this._companionData[heroObject].Item1, this._companionData[heroObject].Item2 + 1);
						}
						else
						{
							this._companionData.Add(heroObject, new ValueTuple<int, int>(0, 1));
						}
					}
				}
				if (affectedAgent.IsHero && affectedAgent.State == AgentState.Killed)
				{
					this._numberOfHeroesKilledInBattle++;
				}
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0004051E File Offset: 0x0003E71E
		private void OnPlayerPartyKnockedOrKilledTroop(CharacterObject troop)
		{
			this._numberOfTroopsKnockedOrKilledAsParty++;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0004052E File Offset: 0x0003E72E
		private void OnHeroPrisonerReleased(Hero prisoner, PartyBase party, IFaction capturerFaction, EndCaptivityDetail detail)
		{
			if (prisoner == Hero.MainHero)
			{
				this._timeSpentAsPrisoner += CampaignTime.Now - PlayerCaptivity.CaptivityStartTime;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00040558 File Offset: 0x0003E758
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsPlayerMapEvent)
			{
				this._lastPlayerBattleSize = mapEvent.AttackerSide.TroopCount + mapEvent.DefenderSide.TroopCount;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0004057F File Offset: 0x0003E77F
		private void OnHideoutBattleCompleted(BattleSideEnum winnerSide, HideoutEventComponent hideoutEventComponent)
		{
			if (hideoutEventComponent.MapEvent.PlayerSide == winnerSide)
			{
				this._numberOfHideoutsCleared++;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0004059D File Offset: 0x0003E79D
		private void OnRaidCompleted(BattleSideEnum winnerSide, RaidEventComponent raidEventComponent)
		{
			if (raidEventComponent.MapEvent.PlayerSide == winnerSide)
			{
				this._numberOfVillagesRaided++;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000405BB File Offset: 0x0003E7BB
		private void OnPrisonersTaken(FlattenedTroopRoster troopRoster)
		{
			this._numberOfTroopPrisonersTaken += troopRoster.CountQ<FlattenedTroopRosterElement>();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000405D0 File Offset: 0x0003E7D0
		private void OnHeroPrisonerTaken(PartyBase capturer, Hero prisoner)
		{
			if (capturer == PartyBase.MainParty)
			{
				this._numberOfHeroPrisonersTaken++;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000405E8 File Offset: 0x0003E7E8
		private void OnArmyCreated(Army army)
		{
			if (army.LeaderParty == MobileParty.MainParty && this._largestArmyFormedByPlayer < army.TotalManCount)
			{
				this._largestArmyFormedByPlayer = army.TotalManCount;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00040614 File Offset: 0x0003E814
		private void OnPartyAttachedAnotherParty(MobileParty mobileParty)
		{
			if (mobileParty.Army == MobileParty.MainParty.Army && MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty && this._largestArmyFormedByPlayer < MobileParty.MainParty.Army.TotalManCount)
			{
				this._largestArmyFormedByPlayer = MobileParty.MainParty.Army.TotalManCount;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00040675 File Offset: 0x0003E875
		private void OnClanDestroyed(Clan clan)
		{
			if (clan.IsAtWarWith(Clan.PlayerClan))
			{
				this._numberOfEnemyClansDestroyed++;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00040694 File Offset: 0x0003E894
		private void OnMapEventEnd(MapEvent mapEvent)
		{
			if (mapEvent.IsPlayerMapEvent)
			{
				if (mapEvent.WinningSide == mapEvent.PlayerSide)
				{
					this._numberOfbattlesWon++;
					if (mapEvent.IsSiegeAssault && !mapEvent.IsPlayerSergeant() && mapEvent.MapEventSettlement != null)
					{
						if (mapEvent.MapEventSettlement.IsTown)
						{
							this._numberOfTownsCaptured++;
						}
						else if (mapEvent.MapEventSettlement.IsCastle)
						{
							this._numberOfCastlesCaptured++;
						}
					}
					if (this._largestBattleWonAsLeader < this._lastPlayerBattleSize && !mapEvent.IsPlayerSergeant())
					{
						this._largestBattleWonAsLeader = this._lastPlayerBattleSize;
						return;
					}
				}
				else if (mapEvent.HasWinner)
				{
					this._numberOfbattlesLost++;
				}
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00040754 File Offset: 0x0003E954
		private void OnHeroOrPartyTradedGold(ValueTuple<Hero, PartyBase> giver, ValueTuple<Hero, PartyBase> recipient, ValueTuple<int, string> goldAmount, bool showNotification)
		{
			if (recipient.Item1 == Hero.MainHero || recipient.Item2 == PartyBase.MainParty)
			{
				this._totalDenarsEarned += (ulong)((long)goldAmount.Item1);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00040784 File Offset: 0x0003E984
		public void OnPlayerAcceptedRansomOffer(int ransomPrice)
		{
			this._denarsEarnedFromRansoms += (ulong)((long)ransomPrice);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00040798 File Offset: 0x0003E998
		private void OnPlayerEarnedGoldFromAsset(DefaultClanFinanceModel.AssetIncomeType assetType, int amount)
		{
			switch (assetType)
			{
			case DefaultClanFinanceModel.AssetIncomeType.Workshop:
				this._denarsEarnedFromWorkshops += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.Caravan:
				this._denarsEarnedFromCaravans += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.Taxes:
				this._denarsEarnedFromTaxes += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.TributesEarned:
				this._denarsEarnedFromTributes += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.TributesPaid:
				this._denarsPaidAsTributes += (ulong)((long)amount);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0004080F File Offset: 0x0003EA0F
		private void OnNewCompanionAdded(Hero hero)
		{
			this._numberOfCompanionsHired++;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00040820 File Offset: 0x0003EA20
		private void OnNewItemCrafted(ItemObject itemObject, ItemModifier overriddenItemModifier, bool isCraftingOrderItem)
		{
			this._numberOfWeaponsCrafted++;
			if (isCraftingOrderItem)
			{
				this._numberOfCraftingOrdersCompleted++;
			}
			if (this._mostExpensiveItemCrafted.Item2 == 0 || this._mostExpensiveItemCrafted.Item2 < itemObject.Value)
			{
				this._mostExpensiveItemCrafted.Item1 = itemObject.Name.ToString();
				this._mostExpensiveItemCrafted.Item2 = itemObject.Value;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00040893 File Offset: 0x0003EA93
		private void OnCraftingPartUnlocked(CraftingPiece craftingPiece)
		{
			this._numberOfCraftingPartsUnlocked++;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000408A4 File Offset: 0x0003EAA4
		[return: TupleElementNames(new string[]
		{
			"name",
			"value"
		})]
		public ValueTuple<string, int> GetCompanionWithMostKills()
		{
			if (this._companionData.IsEmpty<KeyValuePair<Hero, ValueTuple<int, int>>>())
			{
				return new ValueTuple<string, int>(null, 0);
			}
			KeyValuePair<Hero, ValueTuple<int, int>> keyValuePair = this._companionData.MaxBy((KeyValuePair<Hero, ValueTuple<int, int>> kvp) => kvp.Value.Item2);
			return new ValueTuple<string, int>(keyValuePair.Key.Name.ToString(), keyValuePair.Value.Item2);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00040914 File Offset: 0x0003EB14
		[return: TupleElementNames(new string[]
		{
			"name",
			"value"
		})]
		public ValueTuple<string, int> GetCompanionWithMostIssuesSolved()
		{
			if (this._companionData.IsEmpty<KeyValuePair<Hero, ValueTuple<int, int>>>())
			{
				return new ValueTuple<string, int>(null, 0);
			}
			KeyValuePair<Hero, ValueTuple<int, int>> keyValuePair = this._companionData.MaxBy((KeyValuePair<Hero, ValueTuple<int, int>> kvp) => kvp.Value.Item1);
			return new ValueTuple<string, int>(keyValuePair.Key.Name.ToString(), keyValuePair.Value.Item1);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00040983 File Offset: 0x0003EB83
		public int GetHighestTournamentRank()
		{
			return this._highestTournamentRank;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0004098B File Offset: 0x0003EB8B
		public int GetNumberOfTournamentWins()
		{
			return this._numberOfTournamentWins;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00040993 File Offset: 0x0003EB93
		public int GetNumberOfChildrenBorn()
		{
			return this._numberOfChildrenBorn;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0004099B File Offset: 0x0003EB9B
		public int GetNumberOfPrisonersRecruited()
		{
			return this._numberOfPrisonersRecruited;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000409A3 File Offset: 0x0003EBA3
		public int GetNumberOfTroopsRecruited()
		{
			return this._numberOfTroopsRecruited;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000409AB File Offset: 0x0003EBAB
		public int GetNumberOfClansDefected()
		{
			return this._numberOfClansDefected;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000409B3 File Offset: 0x0003EBB3
		public int GetNumberOfIssuesSolved()
		{
			return this._numberOfIssuesSolved;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000409BB File Offset: 0x0003EBBB
		public int GetTotalInfluenceEarned()
		{
			return this._totalInfluenceEarned;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000409C3 File Offset: 0x0003EBC3
		public int GetTotalCrimeRatingGained()
		{
			return this._totalCrimeRatingGained;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000409CB File Offset: 0x0003EBCB
		public int GetNumberOfBattlesWon()
		{
			return this._numberOfbattlesWon;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000409D3 File Offset: 0x0003EBD3
		public int GetNumberOfBattlesLost()
		{
			return this._numberOfbattlesLost;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000409DB File Offset: 0x0003EBDB
		public int GetLargestBattleWonAsLeader()
		{
			return this._largestBattleWonAsLeader;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000409E3 File Offset: 0x0003EBE3
		public int GetLargestArmyFormedByPlayer()
		{
			return this._largestArmyFormedByPlayer;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000409EB File Offset: 0x0003EBEB
		public int GetNumberOfEnemyClansDestroyed()
		{
			return this._numberOfEnemyClansDestroyed;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000409F3 File Offset: 0x0003EBF3
		public int GetNumberOfHeroesKilledInBattle()
		{
			return this._numberOfHeroesKilledInBattle;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000409FB File Offset: 0x0003EBFB
		public int GetNumberOfTroopsKnockedOrKilledAsParty()
		{
			return this._numberOfTroopsKnockedOrKilledAsParty;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00040A03 File Offset: 0x0003EC03
		public int GetNumberOfTroopsKnockedOrKilledByPlayer()
		{
			return this._numberOfTroopsKnockedOrKilledByPlayer;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00040A0B File Offset: 0x0003EC0B
		public int GetNumberOfHeroPrisonersTaken()
		{
			return this._numberOfHeroPrisonersTaken;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00040A13 File Offset: 0x0003EC13
		public int GetNumberOfTroopPrisonersTaken()
		{
			return this._numberOfTroopPrisonersTaken;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00040A1B File Offset: 0x0003EC1B
		public int GetNumberOfTownsCaptured()
		{
			return this._numberOfTownsCaptured;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00040A23 File Offset: 0x0003EC23
		public int GetNumberOfHideoutsCleared()
		{
			return this._numberOfHideoutsCleared;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00040A2B File Offset: 0x0003EC2B
		public int GetNumberOfCastlesCaptured()
		{
			return this._numberOfCastlesCaptured;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00040A33 File Offset: 0x0003EC33
		public int GetNumberOfVillagesRaided()
		{
			return this._numberOfVillagesRaided;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00040A3B File Offset: 0x0003EC3B
		public int GetNumberOfCraftingPartsUnlocked()
		{
			return this._numberOfCraftingPartsUnlocked;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00040A43 File Offset: 0x0003EC43
		public int GetNumberOfWeaponsCrafted()
		{
			return this._numberOfWeaponsCrafted;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00040A4B File Offset: 0x0003EC4B
		public int GetNumberOfCraftingOrdersCompleted()
		{
			return this._numberOfCraftingOrdersCompleted;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00040A53 File Offset: 0x0003EC53
		public int GetNumberOfCompanionsHired()
		{
			return this._numberOfCompanionsHired;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00040A5B File Offset: 0x0003EC5B
		public CampaignTime GetTimeSpentAsPrisoner()
		{
			return this._timeSpentAsPrisoner;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00040A63 File Offset: 0x0003EC63
		public ulong GetTotalTimePlayedInSeconds()
		{
			this.UpdateTotalTimePlayedInSeconds();
			return this._totalTimePlayedInSeconds;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00040A71 File Offset: 0x0003EC71
		public ulong GetTotalDenarsEarned()
		{
			return this._totalDenarsEarned;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00040A79 File Offset: 0x0003EC79
		public ulong GetDenarsEarnedFromCaravans()
		{
			return this._denarsEarnedFromCaravans;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00040A81 File Offset: 0x0003EC81
		public ulong GetDenarsEarnedFromWorkshops()
		{
			return this._denarsEarnedFromWorkshops;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00040A89 File Offset: 0x0003EC89
		public ulong GetDenarsEarnedFromRansoms()
		{
			return this._denarsEarnedFromRansoms;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00040A91 File Offset: 0x0003EC91
		public ulong GetDenarsEarnedFromTaxes()
		{
			return this._denarsEarnedFromTaxes;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00040A99 File Offset: 0x0003EC99
		public ulong GetDenarsEarnedFromTributes()
		{
			return this._denarsEarnedFromTributes;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00040AA1 File Offset: 0x0003ECA1
		public ulong GetDenarsPaidAsTributes()
		{
			return this._denarsPaidAsTributes;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00040AA9 File Offset: 0x0003ECA9
		public CampaignTime GetTotalTimePlayed()
		{
			return CampaignTime.Now - Campaign.Current.CampaignStartTime;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public ValueTuple<string, int> GetMostExpensiveItemCrafted()
		{
			return this._mostExpensiveItemCrafted;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00040AC8 File Offset: 0x0003ECC8
		private void UpdateTotalTimePlayedInSeconds()
		{
			int seconds = (DateTime.Now - this._lastGameplayTimeCheck).Seconds;
			if (seconds > 0)
			{
				this._totalTimePlayedInSeconds += (ulong)((long)seconds);
				this._lastGameplayTimeCheck = DateTime.Now;
			}
		}

		// Token: 0x04000315 RID: 789
		private int _highestTournamentRank;

		// Token: 0x04000316 RID: 790
		private int _numberOfTournamentWins;

		// Token: 0x04000317 RID: 791
		private int _numberOfChildrenBorn;

		// Token: 0x04000318 RID: 792
		private int _numberOfPrisonersRecruited;

		// Token: 0x04000319 RID: 793
		private int _numberOfTroopsRecruited;

		// Token: 0x0400031A RID: 794
		private int _numberOfClansDefected;

		// Token: 0x0400031B RID: 795
		private int _numberOfIssuesSolved;

		// Token: 0x0400031C RID: 796
		private int _totalInfluenceEarned;

		// Token: 0x0400031D RID: 797
		private int _totalCrimeRatingGained;

		// Token: 0x0400031E RID: 798
		private ulong _totalTimePlayedInSeconds;

		// Token: 0x0400031F RID: 799
		private int _numberOfbattlesWon;

		// Token: 0x04000320 RID: 800
		private int _numberOfbattlesLost;

		// Token: 0x04000321 RID: 801
		private int _largestBattleWonAsLeader;

		// Token: 0x04000322 RID: 802
		private int _largestArmyFormedByPlayer;

		// Token: 0x04000323 RID: 803
		private int _numberOfEnemyClansDestroyed;

		// Token: 0x04000324 RID: 804
		private int _numberOfHeroesKilledInBattle;

		// Token: 0x04000325 RID: 805
		private int _numberOfTroopsKnockedOrKilledAsParty;

		// Token: 0x04000326 RID: 806
		private int _numberOfTroopsKnockedOrKilledByPlayer;

		// Token: 0x04000327 RID: 807
		private int _numberOfHeroPrisonersTaken;

		// Token: 0x04000328 RID: 808
		private int _numberOfTroopPrisonersTaken;

		// Token: 0x04000329 RID: 809
		private int _numberOfTownsCaptured;

		// Token: 0x0400032A RID: 810
		private int _numberOfHideoutsCleared;

		// Token: 0x0400032B RID: 811
		private int _numberOfCastlesCaptured;

		// Token: 0x0400032C RID: 812
		private int _numberOfVillagesRaided;

		// Token: 0x0400032D RID: 813
		private CampaignTime _timeSpentAsPrisoner = CampaignTime.Zero;

		// Token: 0x0400032E RID: 814
		private ulong _totalDenarsEarned;

		// Token: 0x0400032F RID: 815
		private ulong _denarsEarnedFromCaravans;

		// Token: 0x04000330 RID: 816
		private ulong _denarsEarnedFromWorkshops;

		// Token: 0x04000331 RID: 817
		private ulong _denarsEarnedFromRansoms;

		// Token: 0x04000332 RID: 818
		private ulong _denarsEarnedFromTaxes;

		// Token: 0x04000333 RID: 819
		private ulong _denarsEarnedFromTributes;

		// Token: 0x04000334 RID: 820
		private ulong _denarsPaidAsTributes;

		// Token: 0x04000335 RID: 821
		private int _numberOfCraftingPartsUnlocked;

		// Token: 0x04000336 RID: 822
		private int _numberOfWeaponsCrafted;

		// Token: 0x04000337 RID: 823
		private int _numberOfCraftingOrdersCompleted;

		// Token: 0x04000338 RID: 824
		private ValueTuple<string, int> _mostExpensiveItemCrafted = new ValueTuple<string, int>(null, 0);

		// Token: 0x04000339 RID: 825
		private int _numberOfCompanionsHired;

		// Token: 0x0400033A RID: 826
		private Dictionary<Hero, ValueTuple<int, int>> _companionData = new Dictionary<Hero, ValueTuple<int, int>>();

		// Token: 0x0400033B RID: 827
		private int _lastPlayerBattleSize;

		// Token: 0x0400033C RID: 828
		private DateTime _lastGameplayTimeCheck;

		// Token: 0x0200019F RID: 415
		private class StatisticsMissionLogic : MissionLogic
		{
			// Token: 0x060010C6 RID: 4294 RVA: 0x000642AF File Offset: 0x000624AF
			public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
			{
				if (this.behavior != null)
				{
					this.behavior.OnAgentRemoved(affectedAgent, affectorAgent);
				}
			}

			// Token: 0x0400070D RID: 1805
			private readonly StatisticsCampaignBehavior behavior = Campaign.Current.CampaignBehaviorManager.GetBehavior<StatisticsCampaignBehavior>();
		}
	}
}
