using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.MissionSpawnHandlers;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000254 RID: 596
	[MissionManager]
	public static class BannerlordMissions
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000717F0 File Offset: 0x0006F9F0
		private static Type GetSiegeWeaponType(SiegeEngineType siegeWeaponType)
		{
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ladder)
			{
				return typeof(SiegeLadder);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ballista)
			{
				return typeof(Ballista);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.FireBallista)
			{
				return typeof(FireBallista);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ram || siegeWeaponType == DefaultSiegeEngineTypes.ImprovedRam)
			{
				return typeof(BatteringRam);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.SiegeTower || siegeWeaponType == DefaultSiegeEngineTypes.HeavySiegeTower)
			{
				return typeof(SiegeTower);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Onager || siegeWeaponType == DefaultSiegeEngineTypes.Catapult)
			{
				return typeof(Mangonel);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.FireOnager || siegeWeaponType == DefaultSiegeEngineTypes.FireCatapult)
			{
				return typeof(FireMangonel);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Trebuchet || siegeWeaponType == DefaultSiegeEngineTypes.Bricole)
			{
				return typeof(Trebuchet);
			}
			return null;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000718C0 File Offset: 0x0006FAC0
		private static Dictionary<Type, int> GetSiegeWeaponTypes(Dictionary<SiegeEngineType, int> values)
		{
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
			foreach (KeyValuePair<SiegeEngineType, int> keyValuePair in values)
			{
				dictionary.Add(BannerlordMissions.GetSiegeWeaponType(keyValuePair.Key), keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00071928 File Offset: 0x0006FB28
		private static AtmosphereInfo CreateAtmosphereInfoForMission(string seasonId, int timeOfDay)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary.Add("spring", 0);
			dictionary.Add("summer", 1);
			dictionary.Add("fall", 2);
			dictionary.Add("winter", 3);
			int season = 0;
			dictionary.TryGetValue(seasonId, out season);
			Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
			dictionary2.Add(6, "TOD_06_00_SemiCloudy");
			dictionary2.Add(12, "TOD_12_00_SemiCloudy");
			dictionary2.Add(15, "TOD_04_00_SemiCloudy");
			dictionary2.Add(18, "TOD_03_00_SemiCloudy");
			dictionary2.Add(22, "TOD_01_00_SemiCloudy");
			string atmosphereName = "field_battle";
			dictionary2.TryGetValue(timeOfDay, out atmosphereName);
			return new AtmosphereInfo
			{
				AtmosphereName = atmosphereName,
				TimeInfo = new TimeInformation
				{
					Season = season
				}
			};
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000719F4 File Offset: 0x0006FBF4
		[MissionMethod]
		public static Mission OpenCustomBattleMission(string scene, BasicCharacterObject playerCharacter, CustomBattleCombatant playerParty, CustomBattleCombatant enemyParty, bool isPlayerGeneral, BasicCharacterObject playerSideGeneralCharacter, string sceneLevels = "", string seasonString = "", float timeOfDay = 6f)
		{
			BattleSideEnum playerSide = playerParty.Side;
			bool isPlayerAttacker = playerSide == BattleSideEnum.Attacker;
			IMissionTroopSupplier[] troopSuppliers = new IMissionTroopSupplier[2];
			CustomBattleTroopSupplier customBattleTroopSupplier = new CustomBattleTroopSupplier(playerParty, true, isPlayerGeneral, false, null);
			troopSuppliers[(int)playerParty.Side] = customBattleTroopSupplier;
			CustomBattleTroopSupplier customBattleTroopSupplier2 = new CustomBattleTroopSupplier(enemyParty, false, false, false, null);
			troopSuppliers[(int)enemyParty.Side] = customBattleTroopSupplier2;
			bool isPlayerSergeant = !isPlayerGeneral;
			return MissionState.OpenNew("CustomBattle", new MissionInitializerRecord(scene)
			{
				DoNotUseLoadingScreen = false,
				PlayingInCampaignMode = false,
				AtmosphereOnCampaign = BannerlordMissions.CreateAtmosphereInfoForMission(seasonString, (int)timeOfDay),
				SceneLevels = sceneLevels,
				TimeOfDay = timeOfDay,
				DecalAtlasGroup = 2
			}, (Mission missionController) => new MissionBehavior[]
			{
				new MissionAgentSpawnLogic(troopSuppliers, playerSide, Mission.BattleSizeType.Battle),
				new BattlePowerCalculationLogic(),
				new CustomBattleAgentLogic(),
				new BannerBearerLogic(),
				new CustomBattleMissionSpawnHandler((!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty),
				new MissionOptionsComponent(),
				new BattleEndLogic(),
				new BattleReinforcementsSpawnController(),
				new MissionCombatantsLogic(null, playerParty, (!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty, Mission.MissionTeamAITypeEnum.FieldBattle, isPlayerSergeant),
				new BattleObserverMissionLogic(),
				new AgentHumanAILogic(),
				new AgentVictoryLogic(),
				new MissionAgentPanicHandler(),
				new BattleMissionAgentInteractionLogic(),
				new AgentMoraleInteractionLogic(),
				new AssignPlayerRoleInTeamMissionController(isPlayerGeneral, isPlayerSergeant, false, isPlayerSergeant ? Enumerable.Repeat<string>(playerCharacter.StringId, 1).ToList<string>() : new List<string>(), FormationClass.NumberOfRegularFormations),
				new GeneralsAndCaptainsAssignmentLogic((isPlayerAttacker & isPlayerGeneral) ? playerCharacter.GetName() : ((isPlayerAttacker & isPlayerSergeant) ? playerSideGeneralCharacter.GetName() : null), (!isPlayerAttacker & isPlayerGeneral) ? playerCharacter.GetName() : ((!isPlayerAttacker & isPlayerSergeant) ? playerSideGeneralCharacter.GetName() : null), null, null, true),
				new EquipmentControllerLeaveLogic(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new HighlightsController(),
				new BattleHighlightsController(),
				new DeploymentMissionController(isPlayerAttacker),
				new BattleDeploymentHandler(isPlayerAttacker)
			}, true, true);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00071B10 File Offset: 0x0006FD10
		[MissionMethod]
		public static Mission OpenSiegeMissionWithDeployment(string scene, BasicCharacterObject playerCharacter, CustomBattleCombatant playerParty, CustomBattleCombatant enemyParty, bool isPlayerGeneral, float[] wallHitPointPercentages, bool hasAnySiegeTower, List<MissionSiegeWeapon> siegeWeaponsOfAttackers, List<MissionSiegeWeapon> siegeWeaponsOfDefenders, bool isPlayerAttacker, int sceneUpgradeLevel = 0, string seasonString = "", bool isSallyOut = false, bool isReliefForceAttack = false, float timeOfDay = 6f)
		{
			string text = (sceneUpgradeLevel == 1) ? "level_1" : ((sceneUpgradeLevel == 2) ? "level_2" : "level_3");
			text += " siege";
			BattleSideEnum playerSide = playerParty.Side;
			IMissionTroopSupplier[] troopSuppliers = new IMissionTroopSupplier[2];
			CustomBattleTroopSupplier customBattleTroopSupplier = new CustomBattleTroopSupplier(playerParty, true, isPlayerGeneral, isSallyOut, null);
			troopSuppliers[(int)playerParty.Side] = customBattleTroopSupplier;
			CustomBattleTroopSupplier customBattleTroopSupplier2 = new CustomBattleTroopSupplier(enemyParty, false, false, isSallyOut, null);
			troopSuppliers[(int)enemyParty.Side] = customBattleTroopSupplier2;
			bool isPlayerSergeant = !isPlayerGeneral;
			return MissionState.OpenNew("CustomSiegeBattle", new MissionInitializerRecord(scene)
			{
				PlayingInCampaignMode = false,
				AtmosphereOnCampaign = BannerlordMissions.CreateAtmosphereInfoForMission(seasonString, (int)timeOfDay),
				SceneLevels = text,
				TimeOfDay = timeOfDay
			}, delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new BattleSpawnLogic(isSallyOut ? "sally_out_set" : (isReliefForceAttack ? "relief_force_attack_set" : "battle_set")));
				list.Add(new MissionOptionsComponent());
				list.Add(new BattleEndLogic());
				list.Add(new BattleReinforcementsSpawnController());
				list.Add(new MissionCombatantsLogic(null, playerParty, (!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty, (!isSallyOut) ? Mission.MissionTeamAITypeEnum.Siege : Mission.MissionTeamAITypeEnum.SallyOut, isPlayerSergeant));
				list.Add(new SiegeMissionPreparationHandler(isSallyOut, isReliefForceAttack, wallHitPointPercentages, hasAnySiegeTower));
				Mission.BattleSizeType battleSizeType = isSallyOut ? Mission.BattleSizeType.SallyOut : Mission.BattleSizeType.Siege;
				list.Add(new MissionAgentSpawnLogic(troopSuppliers, playerSide, battleSizeType));
				list.Add(new BattlePowerCalculationLogic());
				if (isSallyOut)
				{
					list.Add(new CustomSallyOutMissionController((!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty));
				}
				else if (isReliefForceAttack)
				{
					list.Add(new CustomSallyOutMissionController((!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty));
				}
				else
				{
					list.Add(new CustomSiegeMissionSpawnHandler((!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty, false));
				}
				list.Add(new BattleObserverMissionLogic());
				list.Add(new CustomBattleAgentLogic());
				list.Add(new BannerBearerLogic());
				list.Add(new AgentHumanAILogic());
				if (!isSallyOut)
				{
					list.Add(new AmmoSupplyLogic(new List<BattleSideEnum>
					{
						BattleSideEnum.Defender
					}));
				}
				list.Add(new AgentVictoryLogic());
				list.Add(new AssignPlayerRoleInTeamMissionController(isPlayerGeneral, isPlayerSergeant, false, null, FormationClass.NumberOfRegularFormations));
				list.Add(new GeneralsAndCaptainsAssignmentLogic((isPlayerAttacker & isPlayerGeneral) ? playerCharacter.GetName() : null, null, null, null, false));
				list.Add(new MissionAgentPanicHandler());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new AgentMoraleInteractionLogic());
				list.Add(new HighlightsController());
				list.Add(new BattleHighlightsController());
				list.Add(new EquipmentControllerLeaveLogic());
				if (isSallyOut)
				{
					list.Add(new MissionSiegeEnginesLogic(new List<MissionSiegeWeapon>(), siegeWeaponsOfAttackers));
				}
				else
				{
					list.Add(new MissionSiegeEnginesLogic(siegeWeaponsOfDefenders, siegeWeaponsOfAttackers));
				}
				list.Add(new SiegeDeploymentHandler(isPlayerAttacker));
				list.Add(new SiegeDeploymentMissionController(isPlayerAttacker));
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00071C70 File Offset: 0x0006FE70
		[MissionMethod]
		public static Mission OpenCustomBattleLordsHallMission(string scene, BasicCharacterObject playerCharacter, CustomBattleCombatant playerParty, CustomBattleCombatant enemyParty, BasicCharacterObject playerSideGeneralCharacter, string sceneLevels = "", int sceneUpgradeLevel = 0, string seasonString = "", float timeOfDay = 6f)
		{
			int remainingDefenderArcherCount = MathF.Round(18.9f);
			BattleSideEnum playerSide = BattleSideEnum.Attacker;
			bool isPlayerAttacker = playerSide == BattleSideEnum.Attacker;
			IMissionTroopSupplier[] troopSuppliers = new IMissionTroopSupplier[2];
			CustomBattleTroopSupplier customBattleTroopSupplier = new CustomBattleTroopSupplier(playerParty, true, playerCharacter == playerSideGeneralCharacter, false, null);
			troopSuppliers[(int)playerParty.Side] = customBattleTroopSupplier;
			CustomBattleTroopSupplier customBattleTroopSupplier2 = new CustomBattleTroopSupplier(enemyParty, false, false, false, delegate(BasicCharacterObject basicCharacterObject)
			{
				bool result = true;
				if (basicCharacterObject.IsRanged)
				{
					int remainingDefenderArcherCount;
					if (remainingDefenderArcherCount > 0)
					{
						remainingDefenderArcherCount = remainingDefenderArcherCount;
						remainingDefenderArcherCount--;
					}
					else
					{
						result = false;
					}
				}
				return result;
			});
			troopSuppliers[(int)enemyParty.Side] = customBattleTroopSupplier2;
			return MissionState.OpenNew("CustomBattleLordsHall", new MissionInitializerRecord(scene)
			{
				DoNotUseLoadingScreen = false,
				PlayingInCampaignMode = false,
				SceneLevels = "siege",
				TimeOfDay = timeOfDay
			}, (Mission missionController) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new BattleEndLogic(),
				new MissionCombatantsLogic(null, playerParty, (!isPlayerAttacker) ? playerParty : enemyParty, isPlayerAttacker ? playerParty : enemyParty, Mission.MissionTeamAITypeEnum.NoTeamAI, false),
				new BattleMissionStarterLogic(),
				new AgentHumanAILogic(),
				new LordsHallFightMissionController(troopSuppliers, 3f, 0.7f, 19, 27, playerSide),
				new BattleObserverMissionLogic(),
				new CustomBattleAgentLogic(),
				new AgentVictoryLogic(),
				new AmmoSupplyLogic(new List<BattleSideEnum>
				{
					BattleSideEnum.Defender
				}),
				new EquipmentControllerLeaveLogic(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new BattleMissionAgentInteractionLogic(),
				new HighlightsController(),
				new BattleHighlightsController()
			}, true, true);
		}

		// Token: 0x04000BEB RID: 3051
		private const string Level1Tag = "level_1";

		// Token: 0x04000BEC RID: 3052
		private const string Level2Tag = "level_2";

		// Token: 0x04000BED RID: 3053
		private const string Level3Tag = "level_3";

		// Token: 0x04000BEE RID: 3054
		private const string SiegeTag = "siege";

		// Token: 0x04000BEF RID: 3055
		private const string SallyOutTag = "sally";

		// Token: 0x0200050C RID: 1292
		private enum CustomBattleGameTypes
		{
			// Token: 0x04001BEE RID: 7150
			AttackerGeneral,
			// Token: 0x04001BEF RID: 7151
			DefenderGeneral,
			// Token: 0x04001BF0 RID: 7152
			AttackerSergeant,
			// Token: 0x04001BF1 RID: 7153
			DefenderSergeant
		}
	}
}
