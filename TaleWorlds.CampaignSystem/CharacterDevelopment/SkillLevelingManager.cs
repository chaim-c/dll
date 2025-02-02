using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x02000353 RID: 851
	public static class SkillLevelingManager
	{
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000CDD88 File Offset: 0x000CBF88
		private static ISkillLevelingManager Instance
		{
			get
			{
				return Campaign.Current.SkillLevelingManager;
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000CDD94 File Offset: 0x000CBF94
		public static void OnCombatHit(CharacterObject affectorCharacter, CharacterObject affectedCharacter, CharacterObject captain, Hero commander, float speedBonusFromMovement, float shotDifficulty, WeaponComponentData affectorWeapon, float hitPointRatio, CombatXpModel.MissionTypeEnum missionType, bool isAffectorMounted, bool isTeamKill, bool isAffectorUnderCommand, float damageAmount, bool isFatal, bool isSiegeEngineHit, bool isHorseCharge)
		{
			SkillLevelingManager.Instance.OnCombatHit(affectorCharacter, affectedCharacter, captain, commander, speedBonusFromMovement, shotDifficulty, affectorWeapon, hitPointRatio, missionType, isAffectorMounted, isTeamKill, isAffectorUnderCommand, damageAmount, isFatal, isSiegeEngineHit, isHorseCharge);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000CDDC7 File Offset: 0x000CBFC7
		public static void OnSiegeEngineDestroyed(MobileParty party, SiegeEngineType destroyedSiegeEngine)
		{
			SkillLevelingManager.Instance.OnSiegeEngineDestroyed(party, destroyedSiegeEngine);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000CDDD5 File Offset: 0x000CBFD5
		public static void OnWallBreached(MobileParty party)
		{
			SkillLevelingManager.Instance.OnWallBreached(party);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000CDDE2 File Offset: 0x000CBFE2
		public static void OnSimulationCombatKill(CharacterObject affectorCharacter, CharacterObject affectedCharacter, PartyBase affectorParty, PartyBase commanderParty)
		{
			SkillLevelingManager.Instance.OnSimulationCombatKill(affectorCharacter, affectedCharacter, affectorParty, commanderParty);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000CDDF2 File Offset: 0x000CBFF2
		public static void OnTradeProfitMade(PartyBase party, int tradeProfit)
		{
			SkillLevelingManager.Instance.OnTradeProfitMade(party, tradeProfit);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000CDE00 File Offset: 0x000CC000
		public static void OnTradeProfitMade(Hero hero, int tradeProfit)
		{
			SkillLevelingManager.Instance.OnTradeProfitMade(hero, tradeProfit);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000CDE0E File Offset: 0x000CC00E
		public static void OnSettlementProjectFinished(Settlement settlement)
		{
			SkillLevelingManager.Instance.OnSettlementProjectFinished(settlement);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000CDE1B File Offset: 0x000CC01B
		public static void OnSettlementGoverned(Hero governor, Settlement settlement)
		{
			SkillLevelingManager.Instance.OnSettlementGoverned(governor, settlement);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000CDE29 File Offset: 0x000CC029
		public static void OnInfluenceSpent(Hero hero, float amountSpent)
		{
			SkillLevelingManager.Instance.OnInfluenceSpent(hero, amountSpent);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000CDE37 File Offset: 0x000CC037
		public static void OnGainRelation(Hero hero, Hero gainedRelationWith, float relationChange, ChangeRelationAction.ChangeRelationDetail detail = ChangeRelationAction.ChangeRelationDetail.Default)
		{
			SkillLevelingManager.Instance.OnGainRelation(hero, gainedRelationWith, relationChange, detail);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000CDE47 File Offset: 0x000CC047
		public static void OnTroopRecruited(Hero hero, int amount, int tier)
		{
			SkillLevelingManager.Instance.OnTroopRecruited(hero, amount, tier);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000CDE56 File Offset: 0x000CC056
		public static void OnBribeGiven(int amount)
		{
			SkillLevelingManager.Instance.OnBribeGiven(amount);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000CDE63 File Offset: 0x000CC063
		public static void OnBanditsRecruited(MobileParty mobileParty, CharacterObject bandit, int count)
		{
			SkillLevelingManager.Instance.OnBanditsRecruited(mobileParty, bandit, count);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000CDE72 File Offset: 0x000CC072
		public static void OnMainHeroReleasedFromCaptivity(float captivityTime)
		{
			SkillLevelingManager.Instance.OnMainHeroReleasedFromCaptivity(captivityTime);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000CDE7F File Offset: 0x000CC07F
		public static void OnMainHeroTortured()
		{
			SkillLevelingManager.Instance.OnMainHeroTortured();
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000CDE8B File Offset: 0x000CC08B
		public static void OnMainHeroDisguised(bool isNotCaught)
		{
			SkillLevelingManager.Instance.OnMainHeroDisguised(isNotCaught);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000CDE98 File Offset: 0x000CC098
		public static void OnRaid(MobileParty attackerParty, ItemRoster lootedItems)
		{
			SkillLevelingManager.Instance.OnRaid(attackerParty, lootedItems);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000CDEA6 File Offset: 0x000CC0A6
		public static void OnLoot(MobileParty attackerParty, MobileParty forcedParty, ItemRoster lootedItems, bool attacked)
		{
			SkillLevelingManager.Instance.OnLoot(attackerParty, forcedParty, lootedItems, attacked);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000CDEB6 File Offset: 0x000CC0B6
		public static void OnForceVolunteers(MobileParty attackerParty, PartyBase forcedParty)
		{
			SkillLevelingManager.Instance.OnForceVolunteers(attackerParty, forcedParty);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000CDEC4 File Offset: 0x000CC0C4
		public static void OnForceSupplies(MobileParty attackerParty, ItemRoster lootedItems, bool attacked)
		{
			SkillLevelingManager.Instance.OnForceSupplies(attackerParty, lootedItems, attacked);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000CDED3 File Offset: 0x000CC0D3
		public static void OnPrisonerSell(MobileParty mobileParty, in TroopRoster prisonerRoster)
		{
			SkillLevelingManager.Instance.OnPrisonerSell(mobileParty, prisonerRoster);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000CDEE1 File Offset: 0x000CC0E1
		public static void OnSurgeryApplied(MobileParty party, bool surgerySuccess, int troopTier)
		{
			SkillLevelingManager.Instance.OnSurgeryApplied(party, surgerySuccess, troopTier);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000CDEF0 File Offset: 0x000CC0F0
		public static void OnTacticsUsed(MobileParty party, float xp)
		{
			SkillLevelingManager.Instance.OnTacticsUsed(party, xp);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000CDEFE File Offset: 0x000CC0FE
		public static void OnHideoutSpotted(MobileParty party, PartyBase spottedParty)
		{
			SkillLevelingManager.Instance.OnHideoutSpotted(party, spottedParty);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000CDF0C File Offset: 0x000CC10C
		public static void OnTrackDetected(Track track)
		{
			SkillLevelingManager.Instance.OnTrackDetected(track);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000CDF19 File Offset: 0x000CC119
		public static void OnTravelOnFoot(Hero hero, float speed)
		{
			SkillLevelingManager.Instance.OnTravelOnFoot(hero, speed);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000CDF27 File Offset: 0x000CC127
		public static void OnTravelOnHorse(Hero hero, float speed)
		{
			SkillLevelingManager.Instance.OnTravelOnHorse(hero, speed);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000CDF35 File Offset: 0x000CC135
		public static void OnAIPartiesTravel(Hero hero, bool isCaravanParty, TerrainType currentTerrainType)
		{
			SkillLevelingManager.Instance.OnAIPartiesTravel(hero, isCaravanParty, currentTerrainType);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000CDF44 File Offset: 0x000CC144
		public static void OnTraverseTerrain(MobileParty mobileParty, TerrainType currentTerrainType)
		{
			SkillLevelingManager.Instance.OnTraverseTerrain(mobileParty, currentTerrainType);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000CDF52 File Offset: 0x000CC152
		public static void OnHeroHealedWhileWaiting(Hero hero, int healingAmount)
		{
			SkillLevelingManager.Instance.OnHeroHealedWhileWaiting(hero, healingAmount);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000CDF60 File Offset: 0x000CC160
		public static void OnRegularTroopHealedWhileWaiting(MobileParty mobileParty, int healedTroopCount, float averageTier)
		{
			SkillLevelingManager.Instance.OnRegularTroopHealedWhileWaiting(mobileParty, healedTroopCount, averageTier);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000CDF6F File Offset: 0x000CC16F
		public static void OnLeadingArmy(MobileParty mobileParty)
		{
			SkillLevelingManager.Instance.OnLeadingArmy(mobileParty);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000CDF7C File Offset: 0x000CC17C
		public static void OnSieging(MobileParty mobileParty)
		{
			SkillLevelingManager.Instance.OnSieging(mobileParty);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000CDF89 File Offset: 0x000CC189
		public static void OnSiegeEngineBuilt(MobileParty mobileParty, SiegeEngineType siegeEngine)
		{
			SkillLevelingManager.Instance.OnSiegeEngineBuilt(mobileParty, siegeEngine);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000CDF97 File Offset: 0x000CC197
		public static void OnUpgradeTroops(PartyBase party, CharacterObject troop, CharacterObject upgrade, int numberOfTroops)
		{
			SkillLevelingManager.Instance.OnUpgradeTroops(party, troop, upgrade, numberOfTroops);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000CDFA7 File Offset: 0x000CC1A7
		public static void OnBattleEnd(PartyBase party, FlattenedTroopRoster flattenedTroopRoster)
		{
			SkillLevelingManager.Instance.OnBattleEnd(party, flattenedTroopRoster);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000CDFB5 File Offset: 0x000CC1B5
		public static void OnPersuasionSucceeded(Hero targetHero, SkillObject skill, PersuasionDifficulty difficulty, int argumentDifficultyBonusCoefficient)
		{
			SkillLevelingManager.Instance.OnPersuasionSucceeded(targetHero, skill, difficulty, argumentDifficultyBonusCoefficient);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000CDFC5 File Offset: 0x000CC1C5
		public static void OnPrisonBreakEnd(Hero prisonerHero, bool isSucceeded)
		{
			SkillLevelingManager.Instance.OnPrisonBreakEnd(prisonerHero, isSucceeded);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000CDFD3 File Offset: 0x000CC1D3
		public static void OnFoodConsumed(MobileParty mobileParty, bool wasStarving)
		{
			SkillLevelingManager.Instance.OnFoodConsumed(mobileParty, wasStarving);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000CDFE1 File Offset: 0x000CC1E1
		public static void OnAlleyCleared(Alley alley)
		{
			SkillLevelingManager.Instance.OnAlleyCleared(alley);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000CDFEE File Offset: 0x000CC1EE
		public static void OnDailyAlleyTick(Alley alley, Hero alleyLeader)
		{
			SkillLevelingManager.Instance.OnDailyAlleyTick(alley, alleyLeader);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000CDFFC File Offset: 0x000CC1FC
		public static void OnBoardGameWonAgainstLord(Hero lord, BoardGameHelper.AIDifficulty difficulty, bool extraXpGain)
		{
			SkillLevelingManager.Instance.OnBoardGameWonAgainstLord(lord, difficulty, extraXpGain);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000CE00B File Offset: 0x000CC20B
		public static void OnProductionProducedToWarehouse(EquipmentElement production)
		{
			SkillLevelingManager.Instance.OnWarehouseProduction(production);
		}
	}
}
