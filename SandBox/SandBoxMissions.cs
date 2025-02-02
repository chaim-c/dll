using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation.MissionLogics;
using SandBox.Missions.AgentBehaviors;
using SandBox.Missions.AgentControllers;
using SandBox.Missions.Handlers;
using SandBox.Missions.MissionLogics;
using SandBox.Missions.MissionLogics.Arena;
using SandBox.Missions.MissionLogics.Towns;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.TroopSuppliers;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic;

namespace SandBox
{
	// Token: 0x02000021 RID: 33
	[MissionManager]
	public static class SandBoxMissions
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public static MissionInitializerRecord CreateSandBoxMissionInitializerRecord(string sceneName, string sceneLevels = "", bool doNotUseLoadingScreen = false, DecalAtlasGroup decalAtlasGroup = DecalAtlasGroup.All)
		{
			return new MissionInitializerRecord(sceneName)
			{
				DamageToPlayerMultiplier = Campaign.Current.Models.DifficultyModel.GetDamageToPlayerMultiplier(),
				DamageToFriendsMultiplier = Campaign.Current.Models.DifficultyModel.GetPlayerTroopsReceivedDamageMultiplier(),
				DamageFromPlayerToFriendsMultiplier = Campaign.Current.Models.DifficultyModel.GetPlayerTroopsReceivedDamageMultiplier(),
				PlayingInCampaignMode = (Campaign.Current.GameMode == CampaignGameMode.Campaign),
				AtmosphereOnCampaign = ((Campaign.Current.GameMode == CampaignGameMode.Campaign) ? Campaign.Current.Models.MapWeatherModel.GetAtmosphereModel(MobileParty.MainParty.GetLogicalPosition()) : AtmosphereInfo.GetInvalidAtmosphereInfo()),
				TerrainType = (int)Campaign.Current.MapSceneWrapper.GetFaceTerrainType(MobileParty.MainParty.CurrentNavigationFace),
				SceneLevels = sceneLevels,
				DoNotUseLoadingScreen = doNotUseLoadingScreen,
				DecalAtlasGroup = (int)decalAtlasGroup
			};
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005DCC File Offset: 0x00003FCC
		public static MissionInitializerRecord CreateSandBoxTrainingMissionInitializerRecord(string sceneName, string sceneLevels = "", bool doNotUseLoadingScreen = false)
		{
			return new MissionInitializerRecord(sceneName)
			{
				DamageToPlayerMultiplier = Campaign.Current.Models.DifficultyModel.GetDamageToPlayerMultiplier(),
				DamageToFriendsMultiplier = Campaign.Current.Models.DifficultyModel.GetPlayerTroopsReceivedDamageMultiplier(),
				DamageFromPlayerToFriendsMultiplier = 1f,
				PlayingInCampaignMode = (Campaign.Current.GameMode == CampaignGameMode.Campaign),
				AtmosphereOnCampaign = ((Campaign.Current.GameMode == CampaignGameMode.Campaign) ? Campaign.Current.Models.MapWeatherModel.GetAtmosphereModel(MobileParty.MainParty.GetLogicalPosition()) : AtmosphereInfo.GetInvalidAtmosphereInfo()),
				TerrainType = (int)Campaign.Current.MapSceneWrapper.GetFaceTerrainType(MobileParty.MainParty.CurrentNavigationFace),
				SceneLevels = sceneLevels,
				DoNotUseLoadingScreen = doNotUseLoadingScreen
			};
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005EA4 File Offset: 0x000040A4
		[MissionMethod]
		public static Mission OpenTownCenterMission(string scene, int townUpgradeLevel, Location location, CharacterObject talkToChar, string playerSpawnTag)
		{
			string civilianUpgradeLevelTag = Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(townUpgradeLevel);
			return SandBoxMissions.OpenTownCenterMission(scene, civilianUpgradeLevelTag, location, talkToChar, playerSpawnTag);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005ED4 File Offset: 0x000040D4
		[MissionMethod]
		public static Mission OpenTownCenterMission(string scene, string sceneLevels, Location location, CharacterObject talkToChar, string playerSpawnTag)
		{
			return MissionState.OpenNew("TownCenter", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new MissionBasicTeamLogic(),
				new MissionSettlementPrepareLogic(),
				new TownCenterMissionController(),
				new MissionAgentLookHandler(),
				new SandBoxMissionHandler(),
				new WorkshopMissionHandler(SandBoxMissions.GetCurrentTown()),
				new BasicLeaveMissionLogic(),
				new LeaveMissionLogic(),
				new BattleAgentLogic(),
				new MountAgentLogic(),
				new NotableSpawnPointHandler(),
				new MissionAgentPanicHandler(),
				new AgentHumanAILogic(),
				new MissionAlleyHandler(),
				new MissionCrimeHandler(),
				new MissionConversationLogic(talkToChar),
				new MissionAgentHandler(location, talkToChar, playerSpawnTag),
				new HeroSkillHandler(),
				new MissionFightHandler(),
				new MissionFacialAnimationHandler(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new VisualTrackerMissionBehavior(),
				new EquipmentControllerLeaveLogic()
			}, true, true);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005F20 File Offset: 0x00004120
		[MissionMethod]
		public static Mission OpenCastleCourtyardMission(string scene, int castleUpgradeLevel, Location location, CharacterObject talkToChar)
		{
			string civilianUpgradeLevelTag = Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(castleUpgradeLevel);
			return SandBoxMissions.OpenCastleCourtyardMission(scene, civilianUpgradeLevelTag, location, talkToChar);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005F4C File Offset: 0x0000414C
		[MissionMethod]
		public static Mission OpenCastleCourtyardMission(string scene, string sceneLevels, Location location, CharacterObject talkToChar)
		{
			return MissionState.OpenNew("TownCenter", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new CampaignMissionComponent());
				list.Add(new MissionBasicTeamLogic());
				list.Add(new MissionSettlementPrepareLogic());
				list.Add(new TownCenterMissionController());
				list.Add(new MissionAgentLookHandler());
				list.Add(new SandBoxMissionHandler());
				list.Add(new BasicLeaveMissionLogic());
				list.Add(new LeaveMissionLogic());
				list.Add(new BattleAgentLogic());
				list.Add(new MountAgentLogic());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				list.Add(new MissionAgentPanicHandler());
				list.Add(new AgentHumanAILogic());
				list.Add(new MissionConversationLogic(talkToChar));
				list.Add(new MissionAgentHandler(location, talkToChar, null));
				list.Add(new HeroSkillHandler());
				list.Add(new MissionFightHandler());
				list.Add(new MissionFacialAnimationHandler());
				list.Add(new MissionHardBorderPlacer());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new EquipmentControllerLeaveLogic());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new VisualTrackerMissionBehavior());
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005F90 File Offset: 0x00004190
		[MissionMethod]
		public static Mission OpenIndoorMission(string scene, int townUpgradeLevel, Location location, CharacterObject talkToChar)
		{
			string civilianUpgradeLevelTag = Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(townUpgradeLevel);
			return SandBoxMissions.OpenIndoorMission(scene, location, talkToChar, civilianUpgradeLevelTag);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005FBC File Offset: 0x000041BC
		[MissionMethod]
		public static Mission OpenIndoorMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = "")
		{
			return MissionState.OpenNew("Indoor", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, true, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new MissionBasicTeamLogic(),
				new BasicLeaveMissionLogic(),
				new LeaveMissionLogic(),
				new SandBoxMissionHandler(),
				new MissionAgentLookHandler(),
				new MissionConversationLogic(talkToChar),
				new MissionAgentHandler(location, talkToChar, null),
				new HeroSkillHandler(),
				new MissionFightHandler(),
				new BattleAgentLogic(),
				new MountAgentLogic(),
				new AgentHumanAILogic(),
				new MissionCrimeHandler(),
				new MissionFacialAnimationHandler(),
				new LocationItemSpawnHandler(),
				new IndoorMissionController(),
				new VisualTrackerMissionBehavior(),
				new EquipmentControllerLeaveLogic(),
				new BattleSurgeonLogic()
			}, true, true);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006000 File Offset: 0x00004200
		[MissionMethod]
		public static Mission OpenPrisonBreakMission(string scene, Location location, CharacterObject prisonerCharacter, CharacterObject companionCharacter = null)
		{
			Mission mission2 = MissionState.OpenNew("PrisonBreak", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "prison_break", true, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new CampaignMissionComponent());
				list.Add(new MissionBasicTeamLogic());
				list.Add(new BasicLeaveMissionLogic());
				list.Add(new LeaveMissionLogic());
				list.Add(new SandBoxMissionHandler());
				list.Add(new MissionAgentLookHandler());
				list.Add(new MissionAgentHandler(location, null, "sp_prison_break"));
				list.Add(new HeroSkillHandler());
				list.Add(new MissionFightHandler());
				list.Add(new BattleAgentLogic());
				list.Add(new AgentHumanAILogic());
				list.Add(new MissionCrimeHandler());
				list.Add(new MissionFacialAnimationHandler());
				list.Add(new LocationItemSpawnHandler());
				list.Add(new PrisonBreakMissionController(prisonerCharacter, companionCharacter));
				list.Add(new VisualTrackerMissionBehavior());
				list.Add(new EquipmentControllerLeaveLogic());
				list.Add(new BattleSurgeonLogic());
				bool isDevelopmentMode = Game.Current.IsDevelopmentMode;
				return list.ToArray();
			}, true, true);
			mission2.ForceNoFriendlyFire = true;
			return mission2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006054 File Offset: 0x00004254
		[MissionMethod]
		public static Mission OpenVillageMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = null)
		{
			return MissionState.OpenNew("Village", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new MissionBasicTeamLogic(),
				new VillageMissionController(),
				new NotableSpawnPointHandler(),
				new BasicLeaveMissionLogic(),
				new LeaveMissionLogic(),
				new MissionAgentLookHandler(),
				new SandBoxMissionHandler(),
				new MissionConversationLogic(talkToChar),
				new MissionFightHandler(),
				new MissionAgentHandler(location, talkToChar, null),
				new MissionAlleyHandler(),
				new HeroSkillHandler(),
				new MissionFacialAnimationHandler(),
				new MissionAgentPanicHandler(),
				new BattleAgentLogic(),
				new MountAgentLogic(),
				new AgentHumanAILogic(),
				new MissionCrimeHandler(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new EquipmentControllerLeaveLogic(),
				new MissionBoundaryCrossingHandler(),
				new VisualTrackerMissionBehavior(),
				new BattleSurgeonLogic()
			}, true, true);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006098 File Offset: 0x00004298
		[MissionMethod]
		public static Mission OpenArenaStartMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = "")
		{
			return MissionState.OpenNew("ArenaPracticeFight", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new EquipmentControllerLeaveLogic(),
				new ArenaPracticeFightMissionController(),
				new BasicLeaveMissionLogic(),
				new MissionConversationLogic(talkToChar),
				new HeroSkillHandler(),
				new MissionFacialAnimationHandler(),
				new MissionAgentPanicHandler(),
				new AgentHumanAILogic(),
				new ArenaAgentStateDeciderLogic(),
				new VisualTrackerMissionBehavior(),
				new CampaignMissionComponent(),
				new MissionAgentHandler(location, talkToChar, null)
			}, true, true);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000060DC File Offset: 0x000042DC
		[MissionMethod]
		public static Mission OpenRetirementMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = null)
		{
			return MissionState.OpenNew("Retirement", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new MissionBasicTeamLogic(),
				new VillageMissionController(),
				new NotableSpawnPointHandler(),
				new BasicLeaveMissionLogic(),
				new MissionAgentLookHandler(),
				new MissionConversationLogic(talkToChar),
				new MissionFightHandler(),
				new MissionAgentHandler(location, talkToChar, null),
				new MissionAlleyHandler(),
				new HeroSkillHandler(),
				new MissionFacialAnimationHandler(),
				new MissionAgentPanicHandler(),
				new MountAgentLogic(),
				new AgentHumanAILogic(),
				new MissionCrimeHandler(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new EquipmentControllerLeaveLogic(),
				new MissionBoundaryCrossingHandler(),
				new VisualTrackerMissionBehavior(),
				new BattleSurgeonLogic(),
				new RetirementMissionLogic(),
				new LeaveMissionLogic()
			}, true, true);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006120 File Offset: 0x00004320
		[MissionMethod]
		public static Mission OpenArenaDuelMission(string scene, Location location, CharacterObject duelCharacter, bool requireCivilianEquipment, bool spawnBOthSidesWithHorse, Action<CharacterObject> onDuelEnd, float customAgentHealth, string sceneLevels = "")
		{
			return MissionState.OpenNew("ArenaDuelMission", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, sceneLevels, false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new ArenaDuelMissionController(duelCharacter, requireCivilianEquipment, spawnBOthSidesWithHorse, onDuelEnd, customAgentHealth),
				new MissionFacialAnimationHandler(),
				new MissionAgentPanicHandler(),
				new AgentHumanAILogic(),
				new ArenaAgentStateDeciderLogic(),
				new VisualTrackerMissionBehavior(),
				new CampaignMissionComponent(),
				new EquipmentControllerLeaveLogic(),
				new MissionAgentHandler(location, null, null)
			}, true, true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006184 File Offset: 0x00004384
		[MissionMethod]
		public static Mission OpenArenaDuelMission(string scene, Location location)
		{
			return MissionState.OpenNew("ArenaDuel", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new ArenaDuelMissionBehavior(),
				new BasicLeaveMissionLogic(),
				new MissionAgentHandler(location, null, null),
				new HeroSkillHandler(),
				new MissionFacialAnimationHandler(),
				new MissionAgentPanicHandler(),
				new AgentHumanAILogic(),
				new EquipmentControllerLeaveLogic(),
				new ArenaAgentStateDeciderLogic()
			}, true, true);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000061C4 File Offset: 0x000043C4
		[MissionMethod]
		public static Mission OpenBattleMission(MissionInitializerRecord rec)
		{
			bool isPlayerSergeant = MobileParty.MainParty.MapEvent.IsPlayerSergeant();
			bool isPlayerInArmy = MobileParty.MainParty.Army != null;
			List<string> heroesOnPlayerSideByPriority = HeroHelper.OrderHeroesOnPlayerSideByPriority();
			bool isPlayerAttacker = !(from p in MobileParty.MainParty.MapEvent.AttackerSide.Parties
			where p.Party == MobileParty.MainParty.Party
			select p).IsEmpty<MapEventParty>();
			return MissionState.OpenNew("Battle", rec, delegate(Mission mission)
			{
				MissionBehavior[] array = new MissionBehavior[29];
				array[0] = SandBoxMissions.CreateCampaignMissionAgentSpawnLogic(Mission.BattleSizeType.Battle, null, null);
				array[1] = new BattlePowerCalculationLogic();
				array[2] = new BattleSpawnLogic("battle_set");
				array[3] = new SandBoxBattleMissionSpawnHandler();
				array[4] = new CampaignMissionComponent();
				array[5] = new BattleAgentLogic();
				array[6] = new MountAgentLogic();
				array[7] = new BannerBearerLogic();
				array[8] = new MissionOptionsComponent();
				array[9] = new BattleEndLogic();
				array[10] = new BattleReinforcementsSpawnController();
				array[11] = new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.FieldBattle, isPlayerSergeant);
				array[12] = new BattleObserverMissionLogic();
				array[13] = new AgentHumanAILogic();
				array[14] = new AgentVictoryLogic();
				array[15] = new BattleSurgeonLogic();
				array[16] = new MissionAgentPanicHandler();
				array[17] = new BattleMissionAgentInteractionLogic();
				array[18] = new AgentMoraleInteractionLogic();
				array[19] = new AssignPlayerRoleInTeamMissionController(!isPlayerSergeant, isPlayerSergeant, isPlayerInArmy, heroesOnPlayerSideByPriority, FormationClass.NumberOfRegularFormations);
				int num = 20;
				Hero leaderHero = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty.LeaderHero;
				TextObject attackerGeneralName = (leaderHero != null) ? leaderHero.Name : null;
				Hero leaderHero2 = MapEvent.PlayerMapEvent.DefenderSide.LeaderParty.LeaderHero;
				array[num] = new SandboxGeneralsAndCaptainsAssignmentLogic(attackerGeneralName, (leaderHero2 != null) ? leaderHero2.Name : null, null, null, true);
				array[21] = new EquipmentControllerLeaveLogic();
				array[22] = new MissionHardBorderPlacer();
				array[23] = new MissionBoundaryPlacer();
				array[24] = new MissionBoundaryCrossingHandler();
				array[25] = new HighlightsController();
				array[26] = new BattleHighlightsController();
				array[27] = new DeploymentMissionController(isPlayerAttacker);
				array[28] = new BattleDeploymentHandler(isPlayerAttacker);
				return array;
			}, true, true);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000626C File Offset: 0x0000446C
		[MissionMethod]
		public static Mission OpenCaravanBattleMission(MissionInitializerRecord rec, bool isCaravan)
		{
			bool isPlayerAttacker = !(from p in MobileParty.MainParty.MapEvent.AttackerSide.Parties
			where p.Party == MobileParty.MainParty.Party
			select p).IsEmpty<MapEventParty>();
			bool isPlayerSergeant = MobileParty.MainParty.MapEvent.IsPlayerSergeant();
			bool isPlayerInArmy = MobileParty.MainParty.Army != null;
			return MissionState.OpenNew("Battle", rec, delegate(Mission mission)
			{
				MissionBehavior[] array = new MissionBehavior[31];
				array[0] = new MissionOptionsComponent();
				array[1] = new CampaignMissionComponent();
				array[2] = new BattleEndLogic();
				array[3] = new BattleReinforcementsSpawnController();
				array[4] = new BannerBearerLogic();
				array[5] = new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.FieldBattle, isPlayerSergeant);
				array[6] = new BattleSpawnLogic("battle_set");
				array[7] = new AgentHumanAILogic();
				array[8] = SandBoxMissions.CreateCampaignMissionAgentSpawnLogic(Mission.BattleSizeType.Battle, null, null);
				array[9] = new BattlePowerCalculationLogic();
				array[10] = new SandBoxBattleMissionSpawnHandler();
				array[11] = new BattleObserverMissionLogic();
				array[12] = new BattleAgentLogic();
				array[13] = new MountAgentLogic();
				array[14] = new AgentVictoryLogic();
				array[15] = new MissionAgentPanicHandler();
				array[16] = new MissionHardBorderPlacer();
				array[17] = new MissionBoundaryPlacer();
				array[18] = new MissionBoundaryCrossingHandler();
				array[19] = new BattleMissionAgentInteractionLogic();
				array[20] = new AgentMoraleInteractionLogic();
				array[21] = new HighlightsController();
				array[22] = new BattleHighlightsController();
				array[23] = new AssignPlayerRoleInTeamMissionController(!isPlayerSergeant, isPlayerSergeant, isPlayerInArmy, null, FormationClass.NumberOfRegularFormations);
				int num = 24;
				Hero leaderHero = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty.LeaderHero;
				TextObject attackerGeneralName = (leaderHero != null) ? leaderHero.Name : null;
				Hero leaderHero2 = MapEvent.PlayerMapEvent.DefenderSide.LeaderParty.LeaderHero;
				array[num] = new SandboxGeneralsAndCaptainsAssignmentLogic(attackerGeneralName, (leaderHero2 != null) ? leaderHero2.Name : null, null, null, true);
				array[25] = new EquipmentControllerLeaveLogic();
				array[26] = new MissionCaravanOrVillagerTacticsHandler();
				array[27] = new CaravanBattleMissionHandler(MathF.Min((from ip in MapEvent.PlayerMapEvent.InvolvedParties
				where ip.Side == BattleSideEnum.Attacker
				select ip).Sum((PartyBase ip) => ip.MobileParty.Party.MemberRoster.TotalManCount - ip.MobileParty.Party.MemberRoster.TotalWounded), (from ip in MapEvent.PlayerMapEvent.InvolvedParties
				where ip.Side == BattleSideEnum.Defender
				select ip).Sum((PartyBase ip) => ip.MobileParty.Party.MemberRoster.TotalManCount - ip.MobileParty.Party.MemberRoster.TotalWounded)), MapEvent.PlayerMapEvent.InvolvedParties.Any((PartyBase ip) => (ip.MobileParty.IsCaravan || ip.MobileParty.IsVillager) && (ip.Culture.StringId == "aserai" || ip.Culture.StringId == "khuzait")), isCaravan);
				array[28] = new BattleDeploymentHandler(isPlayerAttacker);
				array[29] = new DeploymentMissionController(isPlayerAttacker);
				array[30] = new BattleSurgeonLogic();
				return array;
			}, true, true);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006310 File Offset: 0x00004510
		[MissionMethod]
		public static Mission OpenAlleyFightMission(MissionInitializerRecord rec, Location location, TroopRoster playerSideTroops, TroopRoster rivalSideTroops)
		{
			return MissionState.OpenNew("AlleyFight", rec, delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new BattleEndLogic());
				list.Add(new AgentHumanAILogic());
				list.Add(new BattlePowerCalculationLogic());
				list.Add(new CampaignMissionComponent());
				list.Add(new AlleyFightMissionHandler(playerSideTroops, rivalSideTroops));
				list.Add(new BattleObserverMissionLogic());
				list.Add(new AgentVictoryLogic());
				list.Add(new MissionHardBorderPlacer());
				list.Add(new MissionAgentHandler(location, null, null));
				list.Add(new MissionFightHandler());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new BattleMissionAgentInteractionLogic());
				list.Add(new HighlightsController());
				list.Add(new BattleHighlightsController());
				list.Add(new EquipmentControllerLeaveLogic());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006354 File Offset: 0x00004554
		[MissionMethod]
		public static Mission OpenCombatMissionWithDialogue(MissionInitializerRecord rec, CharacterObject characterToTalkTo)
		{
			return MissionState.OpenNew("CombatWithDialogue", rec, delegate(Mission mission)
			{
				IMissionTroopSupplier[] suppliers = new IMissionTroopSupplier[]
				{
					new PartyGroupTroopSupplier(PlayerEncounter.Battle, BattleSideEnum.Defender, null, null),
					new PartyGroupTroopSupplier(PlayerEncounter.Battle, BattleSideEnum.Attacker, null, null)
				};
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new CampaignMissionComponent());
				list.Add(new BattleEndLogic());
				list.Add(new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.NoTeamAI, false));
				list.Add(new BattleSpawnLogic("battle_set"));
				list.Add(new MissionAgentPanicHandler());
				list.Add(new AgentHumanAILogic());
				list.Add(new CombatMissionWithDialogueController(suppliers, characterToTalkTo));
				list.Add(new MissionConversationLogic(null));
				list.Add(new BattleObserverMissionLogic());
				list.Add(new BattleAgentLogic());
				list.Add(new AgentVictoryLogic());
				list.Add(new MissionHardBorderPlacer());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new BattleMissionAgentInteractionLogic());
				list.Add(new HighlightsController());
				list.Add(new BattleHighlightsController());
				list.Add(new EquipmentControllerLeaveLogic());
				list.Add(new BattleSurgeonLogic());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006388 File Offset: 0x00004588
		[MissionMethod]
		public static Mission OpenBattleMissionWhileEnteringSettlement(string scene, int upgradeLevel, int numberOfMaxTroopToBeSpawnedForPlayer, int numberOfMaxTroopToBeSpawnedForOpponent)
		{
			return MissionState.OpenNew("EnteringSettlementBattle", new MissionInitializerRecord(scene)
			{
				PlayingInCampaignMode = (Campaign.Current.GameMode == CampaignGameMode.Campaign),
				AtmosphereOnCampaign = ((Campaign.Current.GameMode == CampaignGameMode.Campaign) ? Campaign.Current.Models.MapWeatherModel.GetAtmosphereModel(MobileParty.MainParty.GetLogicalPosition()) : AtmosphereInfo.GetInvalidAtmosphereInfo()),
				DecalAtlasGroup = 3,
				SceneLevels = Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(upgradeLevel)
			}, delegate(Mission mission)
			{
				IMissionTroopSupplier[] suppliers = new IMissionTroopSupplier[]
				{
					new PartyGroupTroopSupplier(PlayerEncounter.Battle, BattleSideEnum.Defender, null, null),
					new PartyGroupTroopSupplier(PlayerEncounter.Battle, BattleSideEnum.Attacker, null, null)
				};
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new CampaignMissionComponent());
				list.Add(new BattleEndLogic());
				list.Add(new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.NoTeamAI, false));
				list.Add(new BattleSpawnLogic("battle_set"));
				list.Add(new MissionAgentPanicHandler());
				list.Add(new AgentHumanAILogic());
				list.Add(new BattleObserverMissionLogic());
				list.Add(new WhileEnteringSettlementBattleMissionController(suppliers, numberOfMaxTroopToBeSpawnedForPlayer, numberOfMaxTroopToBeSpawnedForOpponent));
				list.Add(new MissionFightHandler());
				list.Add(new BattleAgentLogic());
				list.Add(new MountAgentLogic());
				list.Add(new AgentVictoryLogic());
				list.Add(new MissionHardBorderPlacer());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new BattleMissionAgentInteractionLogic());
				list.Add(new HighlightsController());
				list.Add(new BattleHighlightsController());
				list.Add(new EquipmentControllerLeaveLogic());
				list.Add(new BattleSurgeonLogic());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000643B File Offset: 0x0000463B
		[MissionMethod]
		public static Mission OpenBattleMission(string scene, bool usesTownDecalAtlas)
		{
			return SandBoxMissions.OpenBattleMission(SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, usesTownDecalAtlas ? DecalAtlasGroup.Town : DecalAtlasGroup.Battle));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006455 File Offset: 0x00004655
		[MissionMethod]
		public static Mission OpenAlleyFightMission(string scene, int upgradeLevel, Location location, TroopRoster playerSideTroops, TroopRoster rivalSideTroops)
		{
			return SandBoxMissions.OpenAlleyFightMission(SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(upgradeLevel), false, DecalAtlasGroup.Town), location, playerSideTroops, rivalSideTroops);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000647D File Offset: 0x0000467D
		[MissionMethod]
		public static Mission OpenCombatMissionWithDialogue(string scene, CharacterObject characterToTalkTo, int upgradeLevel)
		{
			return SandBoxMissions.OpenCombatMissionWithDialogue(SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, Campaign.Current.Models.LocationModel.GetCivilianUpgradeLevelTag(upgradeLevel), false, DecalAtlasGroup.Town), characterToTalkTo);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000064A4 File Offset: 0x000046A4
		[MissionMethod]
		public static Mission OpenHideoutBattleMission(string scene, FlattenedTroopRoster playerTroops)
		{
			int firstPhaseEnemySideTroopCount;
			FlattenedTroopRoster banditPriorityList = SandBoxMissions.GetPriorityListForHideoutMission(MapEvent.PlayerMapEvent, BattleSideEnum.Defender, out firstPhaseEnemySideTroopCount);
			FlattenedTroopRoster playerPriorityList = playerTroops ?? MobilePartyHelper.GetStrongestAndPriorTroops(MobileParty.MainParty, Campaign.Current.Models.BanditDensityModel.GetPlayerMaximumTroopCountForHideoutMission(MobileParty.MainParty), true).ToFlattenedRoster();
			int firstPhasePlayerSideTroopCount = playerPriorityList.Count<FlattenedTroopRosterElement>();
			return MissionState.OpenNew("HideoutBattle", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				IMissionTroopSupplier[] suppliers = new IMissionTroopSupplier[]
				{
					new PartyGroupTroopSupplier(MapEvent.PlayerMapEvent, BattleSideEnum.Defender, banditPriorityList, null),
					new PartyGroupTroopSupplier(MapEvent.PlayerMapEvent, BattleSideEnum.Attacker, playerPriorityList, null)
				};
				return new MissionBehavior[]
				{
					new MissionOptionsComponent(),
					new CampaignMissionComponent(),
					new BattleEndLogic(),
					new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.NoTeamAI, false),
					new AgentHumanAILogic(),
					new HideoutCinematicController(),
					new MissionConversationLogic(),
					new HideoutMissionController(suppliers, PartyBase.MainParty.Side, firstPhaseEnemySideTroopCount, firstPhasePlayerSideTroopCount),
					new BattleObserverMissionLogic(),
					new BattleAgentLogic(),
					new MountAgentLogic(),
					new AgentVictoryLogic(),
					new MissionAgentPanicHandler(),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionBoundaryCrossingHandler(),
					new AgentMoraleInteractionLogic(),
					new HighlightsController(),
					new BattleHighlightsController(),
					new EquipmentControllerLeaveLogic(),
					new BattleSurgeonLogic()
				};
			}, true, true);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006538 File Offset: 0x00004738
		private static FlattenedTroopRoster GetPriorityListForHideoutMission(MapEvent playerMapEvent, BattleSideEnum side, out int firstPhaseTroopCount)
		{
			List<MapEventParty> list = playerMapEvent.PartiesOnSide(side).WhereQ((MapEventParty x) => x.Party.IsMobile).ToList<MapEventParty>();
			int num = list.SumQ((MapEventParty x) => x.Party.MemberRoster.TotalHealthyCount);
			firstPhaseTroopCount = MathF.Min(MathF.Floor((float)num * Campaign.Current.Models.BanditDensityModel.SpawnPercentageForFirstFightInHideoutMission), Campaign.Current.Models.BanditDensityModel.NumberOfMaximumTroopCountForFirstFightInHideout);
			int num2 = num - firstPhaseTroopCount;
			FlattenedTroopRoster flattenedTroopRoster = new FlattenedTroopRoster(num);
			foreach (MapEventParty mapEventParty in list)
			{
				flattenedTroopRoster.Add(mapEventParty.Party.MemberRoster.GetTroopRoster());
			}
			flattenedTroopRoster.RemoveIf((FlattenedTroopRosterElement x) => x.IsWounded);
			int count = flattenedTroopRoster.RemoveIf((FlattenedTroopRosterElement x) => x.Troop.IsHero || x.Troop.Culture.BanditBoss == x.Troop).ToList<FlattenedTroopRosterElement>().Count;
			int num3 = 0;
			int num4 = num2 - count;
			if (num4 > 0)
			{
				IEnumerable<FlattenedTroopRosterElement> selectedRegularTroops = (from x in flattenedTroopRoster
				orderby x.Troop.Level descending
				select x).Take(num4);
				flattenedTroopRoster.RemoveIf((FlattenedTroopRosterElement x) => selectedRegularTroops.Contains(x));
				num3 += selectedRegularTroops.Count<FlattenedTroopRosterElement>();
			}
			Debug.Print("Picking bandit troops for hideout mission...", 0, Debug.DebugColor.Yellow, 256UL);
			Debug.Print("- First phase troop count: " + firstPhaseTroopCount, 0, Debug.DebugColor.Yellow, 256UL);
			Debug.Print("- Second phase boss troop count: " + count, 0, Debug.DebugColor.Yellow, 256UL);
			Debug.Print("- Second phase regular troop count: " + num3, 0, Debug.DebugColor.Yellow, 256UL);
			return flattenedTroopRoster;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006764 File Offset: 0x00004964
		[MissionMethod]
		public static Mission OpenAmbushMission(string scene, MissionResult oldResult)
		{
			Debug.FailedAssert("This mission was broken", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\SandBoxMissions.cs", "OpenAmbushMission", 850);
			return MissionState.OpenNew("Ambush", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new BattleEndLogic(),
				new BattleReinforcementsSpawnController(),
				new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.FieldBattle, false),
				new BattleObserverMissionLogic(),
				new BattleAgentLogic(),
				new MountAgentLogic(),
				new AgentVictoryLogic(),
				new AgentHumanAILogic(),
				new MissionAgentPanicHandler(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new AgentMoraleInteractionLogic(),
				new HighlightsController(),
				new BattleHighlightsController(),
				new EquipmentControllerLeaveLogic(),
				new BattleSurgeonLogic()
			}, true, true);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000067C2 File Offset: 0x000049C2
		[MissionMethod]
		public static Mission OpenCampMission(string scene)
		{
			return MissionState.OpenNew("Camp", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new CampaignMissionComponent(),
				new BattleEndLogic(),
				new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.NoTeamAI, false),
				new BasicLeaveMissionLogic(),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new EquipmentControllerLeaveLogic()
			}, true, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000067FC File Offset: 0x000049FC
		[MissionMethod]
		public static Mission OpenSiegeMissionWithDeployment(string scene, float[] wallHitPointPercentages, bool hasAnySiegeTower, List<MissionSiegeWeapon> siegeWeaponsOfAttackers, List<MissionSiegeWeapon> siegeWeaponsOfDefenders, bool isPlayerAttacker, int sceneUpgradeLevel = 0, bool isSallyOut = false, bool isReliefForceAttack = false)
		{
			string text = Campaign.Current.Models.LocationModel.GetUpgradeLevelTag(sceneUpgradeLevel);
			text += " siege";
			bool isPlayerSergeant = MobileParty.MainParty.MapEvent.IsPlayerSergeant();
			bool isPlayerInArmy = MobileParty.MainParty.Army != null;
			List<string> heroesOnPlayerSideByPriority = HeroHelper.OrderHeroesOnPlayerSideByPriority();
			return MissionState.OpenNew("SiegeMissionWithDeployment", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, text, false, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new BattleSpawnLogic(isSallyOut ? "sally_out_set" : (isReliefForceAttack ? "relief_force_attack_set" : "battle_set")));
				list.Add(new MissionOptionsComponent());
				list.Add(new CampaignMissionComponent());
				BattleEndLogic battleEndLogic = new BattleEndLogic();
				if (MobileParty.MainParty.MapEvent.PlayerSide == BattleSideEnum.Attacker)
				{
					battleEndLogic.EnableEnemyDefenderPullBack(Campaign.Current.Models.SiegeLordsHallFightModel.DefenderTroopNumberForSuccessfulPullBack);
				}
				list.Add(battleEndLogic);
				list.Add(new BattleReinforcementsSpawnController());
				list.Add(new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), isSallyOut ? Mission.MissionTeamAITypeEnum.SallyOut : Mission.MissionTeamAITypeEnum.Siege, isPlayerSergeant));
				list.Add(new SiegeMissionPreparationHandler(isSallyOut, isReliefForceAttack, wallHitPointPercentages, hasAnySiegeTower));
				list.Add(new CampaignSiegeStateHandler());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				Mission.BattleSizeType battleSizeType = Mission.BattleSizeType.Siege;
				if (isSallyOut)
				{
					battleSizeType = Mission.BattleSizeType.SallyOut;
					FlattenedTroopRoster priorityTroopsForSallyOutAmbush = Campaign.Current.Models.SiegeEventModel.GetPriorityTroopsForSallyOutAmbush();
					list.Add(new SandBoxSallyOutMissionController());
					list.Add(SandBoxMissions.CreateCampaignMissionAgentSpawnLogic(battleSizeType, priorityTroopsForSallyOutAmbush, null));
				}
				else
				{
					if (isReliefForceAttack)
					{
						list.Add(new SandBoxSallyOutMissionController());
					}
					else
					{
						list.Add(new SandBoxSiegeMissionSpawnHandler());
					}
					list.Add(SandBoxMissions.CreateCampaignMissionAgentSpawnLogic(battleSizeType, null, null));
				}
				list.Add(new BattlePowerCalculationLogic());
				list.Add(new BattleObserverMissionLogic());
				list.Add(new BattleAgentLogic());
				list.Add(new BattleSurgeonLogic());
				list.Add(new MountAgentLogic());
				list.Add(new BannerBearerLogic());
				list.Add(new AgentHumanAILogic());
				list.Add(new AmmoSupplyLogic(new List<BattleSideEnum>
				{
					BattleSideEnum.Defender
				}));
				list.Add(new AgentVictoryLogic());
				list.Add(new AssignPlayerRoleInTeamMissionController(!isPlayerSergeant, isPlayerSergeant, isPlayerInArmy, heroesOnPlayerSideByPriority, FormationClass.NumberOfRegularFormations));
				List<MissionBehavior> list2 = list;
				Hero leaderHero = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty.LeaderHero;
				TextObject attackerGeneralName = (leaderHero != null) ? leaderHero.Name : null;
				Hero leaderHero2 = MapEvent.PlayerMapEvent.DefenderSide.LeaderParty.LeaderHero;
				list2.Add(new SandboxGeneralsAndCaptainsAssignmentLogic(attackerGeneralName, (leaderHero2 != null) ? leaderHero2.Name : null, null, null, false));
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

		// Token: 0x060000EA RID: 234 RVA: 0x000068BC File Offset: 0x00004ABC
		[MissionMethod]
		public static Mission OpenSiegeMissionNoDeployment(string scene, bool isSallyOut = false, bool isReliefForceAttack = false)
		{
			string text = Campaign.Current.Models.LocationModel.GetUpgradeLevelTag(3);
			text += " siege";
			bool isPlayerSergeant = MobileParty.MainParty.MapEvent.IsPlayerSergeant();
			bool isPlayerInArmy = MobileParty.MainParty.Army != null;
			List<string> heroesOnPlayerSideByPriority = HeroHelper.OrderHeroesOnPlayerSideByPriority();
			return MissionState.OpenNew("SiegeMissionNoDeployment", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, text, false, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new BattleSpawnLogic(isSallyOut ? "sally_out_set" : (isReliefForceAttack ? "relief_force_attack_set" : "battle_set")));
				list.Add(new CampaignMissionComponent());
				BattleEndLogic battleEndLogic = new BattleEndLogic();
				if (!isSallyOut && !isReliefForceAttack && MobileParty.MainParty.MapEvent.PlayerSide == BattleSideEnum.Attacker)
				{
					battleEndLogic.EnableEnemyDefenderPullBack(Campaign.Current.Models.SiegeLordsHallFightModel.DefenderTroopNumberForSuccessfulPullBack);
				}
				list.Add(battleEndLogic);
				list.Add(new BattleReinforcementsSpawnController());
				list.Add(new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.FieldBattle, isPlayerSergeant));
				list.Add(new CampaignSiegeStateHandler());
				Mission.BattleSizeType battleSizeType = isSallyOut ? Mission.BattleSizeType.SallyOut : Mission.BattleSizeType.Siege;
				list.Add(SandBoxMissions.CreateCampaignMissionAgentSpawnLogic(battleSizeType, null, null));
				list.Add(new BattlePowerCalculationLogic());
				list.Add(new SandBoxBattleMissionSpawnHandler());
				Settlement currentTown = SandBoxMissions.GetCurrentTown();
				if (currentTown != null)
				{
					list.Add(new WorkshopMissionHandler(currentTown));
				}
				list.Add(new BattleObserverMissionLogic());
				list.Add(new BattleAgentLogic());
				list.Add(new BattleSurgeonLogic());
				list.Add(new MountAgentLogic());
				list.Add(new AgentVictoryLogic());
				list.Add(new AmmoSupplyLogic(new List<BattleSideEnum>
				{
					BattleSideEnum.Defender
				}));
				list.Add(new MissionAgentPanicHandler());
				list.Add(new MissionHardBorderPlacer());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new EquipmentControllerLeaveLogic());
				list.Add(new MissionBoundaryCrossingHandler());
				list.Add(new AgentHumanAILogic());
				list.Add(new AgentMoraleInteractionLogic());
				list.Add(new HighlightsController());
				list.Add(new BattleHighlightsController());
				list.Add(new AssignPlayerRoleInTeamMissionController(!isPlayerSergeant, isPlayerSergeant, isPlayerInArmy, heroesOnPlayerSideByPriority, FormationClass.NumberOfRegularFormations));
				List<MissionBehavior> list2 = list;
				Hero leaderHero = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty.LeaderHero;
				TextObject attackerGeneralName = (leaderHero != null) ? leaderHero.Name : null;
				Hero leaderHero2 = MapEvent.PlayerMapEvent.DefenderSide.LeaderParty.LeaderHero;
				list2.Add(new SandboxGeneralsAndCaptainsAssignmentLogic(attackerGeneralName, (leaderHero2 != null) ? leaderHero2.Name : null, null, null, false));
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006954 File Offset: 0x00004B54
		[MissionMethod]
		public static Mission OpenSiegeLordsHallFightMission(string scene, FlattenedTroopRoster attackerPriorityList)
		{
			int remainingDefenderArcherCount = Campaign.Current.Models.SiegeLordsHallFightModel.MaxDefenderArcherCount;
			FlattenedTroopRoster defenderPriorityList = Campaign.Current.Models.SiegeLordsHallFightModel.GetPriorityListForLordsHallFightMission(MapEvent.PlayerMapEvent, BattleSideEnum.Defender, Campaign.Current.Models.SiegeLordsHallFightModel.MaxDefenderSideTroopCount);
			int attackerSideTroopCountMax = MathF.Min(Campaign.Current.Models.SiegeLordsHallFightModel.MaxAttackerSideTroopCount, attackerPriorityList.Troops.Count<CharacterObject>());
			int defenderSideTroopCountMax = MathF.Min(Campaign.Current.Models.SiegeLordsHallFightModel.MaxDefenderSideTroopCount, defenderPriorityList.Troops.Count<CharacterObject>());
			MissionInitializerRecord rec = SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "siege", false, DecalAtlasGroup.Town);
			Func<UniqueTroopDescriptor, MapEventParty, bool> <>9__1;
			return MissionState.OpenNew("SiegeLordsHallFightMission", rec, delegate(Mission mission)
			{
				IMissionTroopSupplier[] array = new IMissionTroopSupplier[2];
				IMissionTroopSupplier[] array2 = array;
				int num = 0;
				MapEvent playerMapEvent = MapEvent.PlayerMapEvent;
				BattleSideEnum side = BattleSideEnum.Defender;
				FlattenedTroopRoster defenderPriorityList = defenderPriorityList;
				Func<UniqueTroopDescriptor, MapEventParty, bool> customAllocationConditions;
				if ((customAllocationConditions = <>9__1) == null)
				{
					customAllocationConditions = (<>9__1 = delegate(UniqueTroopDescriptor uniqueTroopDescriptor, MapEventParty mapEventParty)
					{
						bool result = true;
						if (mapEventParty.GetTroop(uniqueTroopDescriptor).IsRanged)
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
				}
				array2[num] = new PartyGroupTroopSupplier(playerMapEvent, side, defenderPriorityList, customAllocationConditions);
				array[1] = new PartyGroupTroopSupplier(MapEvent.PlayerMapEvent, BattleSideEnum.Attacker, attackerPriorityList, null);
				return new MissionBehavior[]
				{
					new MissionOptionsComponent(),
					new CampaignMissionComponent(),
					new BattleEndLogic(),
					new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.NoTeamAI, false),
					new CampaignSiegeStateHandler(),
					new AgentHumanAILogic(),
					new LordsHallFightMissionController(array, Campaign.Current.Models.SiegeLordsHallFightModel.AreaLostRatio, Campaign.Current.Models.SiegeLordsHallFightModel.AttackerDefenderTroopCountRatio, attackerSideTroopCountMax, defenderSideTroopCountMax, PartyBase.MainParty.Side),
					new BattleObserverMissionLogic(),
					new BattleAgentLogic(),
					new AgentVictoryLogic(),
					new AmmoSupplyLogic(new List<BattleSideEnum>
					{
						BattleSideEnum.Defender
					}),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionBoundaryCrossingHandler(),
					new EquipmentControllerLeaveLogic(),
					new BattleMissionAgentInteractionLogic(),
					new HighlightsController(),
					new BattleHighlightsController(),
					new BattleSurgeonLogic()
				};
			}, true, true);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006A44 File Offset: 0x00004C44
		[MissionMethod]
		public static Mission OpenAmbushMissionForTutorial(string scene, bool isPlayerAttacker)
		{
			return MissionState.OpenNew("AmbushMissionForTutorial", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission missionController)
			{
				List<MissionBehavior> list = new List<MissionBehavior>();
				list.Add(new MissionOptionsComponent());
				list.Add(new AmbushMissionController(isPlayerAttacker));
				AmbushBattleDeploymentHandler item = new AmbushBattleDeploymentHandler(isPlayerAttacker);
				list.Add(item);
				list.Add(new BasicLeaveMissionLogic());
				list.Add(new MissionAgentPanicHandler());
				list.Add(new MissionBoundaryPlacer());
				list.Add(new AgentMoraleInteractionLogic());
				list.Add(new EquipmentControllerLeaveLogic());
				return list.ToArray();
			}, true, true);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006A84 File Offset: 0x00004C84
		[MissionMethod]
		public static Mission OpenVillageBattleMission(string scene)
		{
			bool isPlayerSergeant = MobileParty.MainParty.MapEvent.IsPlayerSergeant();
			bool isPlayerInArmy = MobileParty.MainParty.Army != null;
			return MissionState.OpenNew("VillageBattle", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission mission)
			{
				MissionBehavior[] array = new MissionBehavior[17];
				array[0] = new MissionOptionsComponent();
				array[1] = new CampaignMissionComponent();
				array[2] = new BattleEndLogic();
				array[3] = new BattleReinforcementsSpawnController();
				array[4] = new MissionCombatantsLogic(MobileParty.MainParty.MapEvent.InvolvedParties, PartyBase.MainParty, MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender), MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker), Mission.MissionTeamAITypeEnum.FieldBattle, isPlayerSergeant);
				array[5] = new AgentHumanAILogic();
				array[6] = new MissionAgentPanicHandler();
				array[7] = new MissionHardBorderPlacer();
				array[8] = new MissionBoundaryPlacer();
				array[9] = new MissionBoundaryCrossingHandler();
				array[10] = new AgentMoraleInteractionLogic();
				array[11] = new HighlightsController();
				array[12] = new BattleHighlightsController();
				array[13] = new EquipmentControllerLeaveLogic();
				array[14] = new AssignPlayerRoleInTeamMissionController(!isPlayerSergeant, isPlayerSergeant, isPlayerInArmy, null, FormationClass.NumberOfRegularFormations);
				int num = 15;
				Hero leaderHero = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty.LeaderHero;
				TextObject attackerGeneralName = (leaderHero != null) ? leaderHero.Name : null;
				Hero leaderHero2 = MapEvent.PlayerMapEvent.DefenderSide.LeaderParty.LeaderHero;
				array[num] = new SandboxGeneralsAndCaptainsAssignmentLogic(attackerGeneralName, (leaderHero2 != null) ? leaderHero2.Name : null, null, null, true);
				array[16] = new BattleSurgeonLogic();
				return array;
			}, true, true);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006AE4 File Offset: 0x00004CE4
		[MissionMethod]
		public static Mission OpenConversationMission(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, string specialScene = "", string sceneLevels = "")
		{
			string sceneName = specialScene.IsEmpty<char>() ? PlayerEncounter.GetConversationSceneForMapPosition(PartyBase.MainParty.Position2D) : specialScene;
			return MissionState.OpenNew("Conversation", SandBoxMissions.CreateSandBoxMissionInitializerRecord(sceneName, sceneLevels, true, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new CampaignMissionComponent(),
				new MissionConversationLogic(),
				new MissionOptionsComponent(),
				new ConversationMissionLogic(playerCharacterData, conversationPartnerData),
				new EquipmentControllerLeaveLogic()
			}, true, false);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006B44 File Offset: 0x00004D44
		[MissionMethod]
		public static Mission OpenMeetingMission(string scene, CharacterObject character)
		{
			Debug.FailedAssert("This mission was broken", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\SandBoxMissions.cs", "OpenMeetingMission", 1268);
			return MissionState.OpenNew("Conversation", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), (Mission mission) => new MissionBehavior[]
			{
				new CampaignMissionComponent(),
				new MissionSettlementPrepareLogic(),
				new MissionOptionsComponent(),
				new MissionConversationLogic(),
				new EquipmentControllerLeaveLogic()
			}, true, false);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006BA4 File Offset: 0x00004DA4
		private static Settlement GetCurrentTown()
		{
			if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement.IsTown)
			{
				return Settlement.CurrentSettlement;
			}
			if (MapEvent.PlayerMapEvent != null && MapEvent.PlayerMapEvent.MapEventSettlement != null && MapEvent.PlayerMapEvent.MapEventSettlement.IsTown)
			{
				return MapEvent.PlayerMapEvent.MapEventSettlement;
			}
			return null;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006BFA File Offset: 0x00004DFA
		private static MissionAgentSpawnLogic CreateCampaignMissionAgentSpawnLogic(Mission.BattleSizeType battleSizeType, FlattenedTroopRoster priorTroopsForDefenders = null, FlattenedTroopRoster priorTroopsForAttackers = null)
		{
			return new MissionAgentSpawnLogic(new IMissionTroopSupplier[]
			{
				new PartyGroupTroopSupplier(MapEvent.PlayerMapEvent, BattleSideEnum.Defender, priorTroopsForDefenders, null),
				new PartyGroupTroopSupplier(MapEvent.PlayerMapEvent, BattleSideEnum.Attacker, priorTroopsForAttackers, null)
			}, PartyBase.MainParty.Side, battleSizeType);
		}
	}
}
