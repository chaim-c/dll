using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace SandBox
{
	// Token: 0x0200001F RID: 31
	public class CampaignMissionManager : CampaignMission.ICampaignMissionManager
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00005BCC File Offset: 0x00003DCC
		IMission CampaignMission.ICampaignMissionManager.OpenSiegeMissionWithDeployment(string scene, float[] wallHitPointsPercentages, bool hasAnySiegeTower, List<MissionSiegeWeapon> siegeWeaponsOfAttackers, List<MissionSiegeWeapon> siegeWeaponsOfDefenders, bool isPlayerAttacker, int upgradeLevel, bool isSallyOut, bool isReliefForceAttack)
		{
			return SandBoxMissions.OpenSiegeMissionWithDeployment(scene, wallHitPointsPercentages, hasAnySiegeTower, siegeWeaponsOfAttackers, siegeWeaponsOfDefenders, isPlayerAttacker, upgradeLevel, isSallyOut, isReliefForceAttack);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005BED File Offset: 0x00003DED
		IMission CampaignMission.ICampaignMissionManager.OpenSiegeMissionNoDeployment(string scene, bool isSallyOut, bool isReliefForceAttack)
		{
			return SandBoxMissions.OpenSiegeMissionNoDeployment(scene, isSallyOut, isReliefForceAttack);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005BF7 File Offset: 0x00003DF7
		IMission CampaignMission.ICampaignMissionManager.OpenSiegeLordsHallFightMission(string scene, FlattenedTroopRoster attackerPriorityList)
		{
			return SandBoxMissions.OpenSiegeLordsHallFightMission(scene, attackerPriorityList);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005C00 File Offset: 0x00003E00
		IMission CampaignMission.ICampaignMissionManager.OpenBattleMission(MissionInitializerRecord rec)
		{
			return SandBoxMissions.OpenBattleMission(rec);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005C08 File Offset: 0x00003E08
		IMission CampaignMission.ICampaignMissionManager.OpenCaravanBattleMission(MissionInitializerRecord rec, bool isCaravan)
		{
			return SandBoxMissions.OpenCaravanBattleMission(rec, isCaravan);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005C11 File Offset: 0x00003E11
		IMission CampaignMission.ICampaignMissionManager.OpenBattleMission(string scene, bool usesTownDecalAtlas)
		{
			return SandBoxMissions.OpenBattleMission(scene, usesTownDecalAtlas);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005C1A File Offset: 0x00003E1A
		IMission CampaignMission.ICampaignMissionManager.OpenAlleyFightMission(string scene, int upgradeLevel, Location location, TroopRoster playerSideTroops, TroopRoster rivalSideTroops)
		{
			return SandBoxMissions.OpenAlleyFightMission(scene, upgradeLevel, location, playerSideTroops, rivalSideTroops);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005C28 File Offset: 0x00003E28
		IMission CampaignMission.ICampaignMissionManager.OpenCombatMissionWithDialogue(string scene, CharacterObject characterToTalkTo, int upgradeLevel)
		{
			return SandBoxMissions.OpenCombatMissionWithDialogue(scene, characterToTalkTo, upgradeLevel);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005C32 File Offset: 0x00003E32
		IMission CampaignMission.ICampaignMissionManager.OpenBattleMissionWhileEnteringSettlement(string scene, int upgradeLevel, int numberOfMaxTroopToBeSpawnedForPlayer, int numberOfMaxTroopToBeSpawnedForOpponent)
		{
			return SandBoxMissions.OpenBattleMissionWhileEnteringSettlement(scene, upgradeLevel, numberOfMaxTroopToBeSpawnedForPlayer, numberOfMaxTroopToBeSpawnedForOpponent);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005C3E File Offset: 0x00003E3E
		IMission CampaignMission.ICampaignMissionManager.OpenHideoutBattleMission(string scene, FlattenedTroopRoster playerTroops)
		{
			return SandBoxMissions.OpenHideoutBattleMission(scene, playerTroops);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005C47 File Offset: 0x00003E47
		IMission CampaignMission.ICampaignMissionManager.OpenTownCenterMission(string scene, int townUpgradeLevel, Location location, CharacterObject talkToChar, string playerSpawnTag)
		{
			return SandBoxMissions.OpenTownCenterMission(scene, townUpgradeLevel, location, talkToChar, playerSpawnTag);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005C55 File Offset: 0x00003E55
		IMission CampaignMission.ICampaignMissionManager.OpenCastleCourtyardMission(string scene, int castleUpgradeLevel, Location location, CharacterObject talkToChar)
		{
			return SandBoxMissions.OpenCastleCourtyardMission(scene, castleUpgradeLevel, location, talkToChar);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005C61 File Offset: 0x00003E61
		IMission CampaignMission.ICampaignMissionManager.OpenVillageMission(string scene, Location location, CharacterObject talkToChar)
		{
			return SandBoxMissions.OpenVillageMission(scene, location, talkToChar, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005C6C File Offset: 0x00003E6C
		IMission CampaignMission.ICampaignMissionManager.OpenIndoorMission(string scene, int upgradeLevel, Location location, CharacterObject talkToChar)
		{
			return SandBoxMissions.OpenIndoorMission(scene, upgradeLevel, location, talkToChar);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005C78 File Offset: 0x00003E78
		IMission CampaignMission.ICampaignMissionManager.OpenPrisonBreakMission(string scene, Location location, CharacterObject prisonerCharacter, CharacterObject companionCharacter)
		{
			return SandBoxMissions.OpenPrisonBreakMission(scene, location, prisonerCharacter, companionCharacter);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005C84 File Offset: 0x00003E84
		IMission CampaignMission.ICampaignMissionManager.OpenArenaStartMission(string scene, Location location, CharacterObject talkToChar)
		{
			return SandBoxMissions.OpenArenaStartMission(scene, location, talkToChar, "");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005C93 File Offset: 0x00003E93
		public IMission OpenArenaDuelMission(string scene, Location location, CharacterObject duelCharacter, bool requireCivilianEquipment, bool spawnBOthSidesWithHorse, Action<CharacterObject> onDuelEndAction, float customAgentHealth)
		{
			return SandBoxMissions.OpenArenaDuelMission(scene, location, duelCharacter, requireCivilianEquipment, spawnBOthSidesWithHorse, onDuelEndAction, customAgentHealth, "");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005CAA File Offset: 0x00003EAA
		IMission CampaignMission.ICampaignMissionManager.OpenConversationMission(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, string specialScene, string sceneLevels)
		{
			return SandBoxMissions.OpenConversationMission(playerCharacterData, conversationPartnerData, specialScene, sceneLevels);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005CB6 File Offset: 0x00003EB6
		IMission CampaignMission.ICampaignMissionManager.OpenMeetingMission(string scene, CharacterObject character)
		{
			return SandBoxMissions.OpenMeetingMission(scene, character);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005CBF File Offset: 0x00003EBF
		IMission CampaignMission.ICampaignMissionManager.OpenRetirementMission(string scene, Location location, CharacterObject talkToChar, string sceneLevels)
		{
			return SandBoxMissions.OpenRetirementMission(scene, location, talkToChar, sceneLevels);
		}
	}
}
