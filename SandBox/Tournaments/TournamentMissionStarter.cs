﻿using System;
using System.Collections.Generic;
using SandBox.Missions.MissionLogics;
using SandBox.Missions.MissionLogics.Arena;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace SandBox.Tournaments
{
	// Token: 0x0200002C RID: 44
	[MissionManager]
	public static class TournamentMissionStarter
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007E9C File Offset: 0x0000609C
		[MissionMethod]
		public static Mission OpenTournamentArcheryMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return MissionState.OpenNew("TournamentArchery", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission missionController)
			{
				TournamentArcheryMissionController tournamentArcheryMissionController = new TournamentArcheryMissionController(culture);
				return new MissionBehavior[]
				{
					new CampaignMissionComponent(),
					new EquipmentControllerLeaveLogic(),
					tournamentArcheryMissionController,
					new TournamentBehavior(tournamentGame, settlement, tournamentArcheryMissionController, isPlayerParticipating),
					new AgentVictoryLogic(),
					new MissionAgentPanicHandler(),
					new AgentHumanAILogic(),
					new ArenaAgentStateDeciderLogic(),
					new BasicLeaveMissionLogic(true),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionOptionsComponent()
				};
			}, true, true);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007EF4 File Offset: 0x000060F4
		[MissionMethod]
		public static Mission OpenTournamentFightMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return MissionState.OpenNew("TournamentFight", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission missionController)
			{
				TournamentFightMissionController tournamentFightMissionController = new TournamentFightMissionController(culture);
				return new MissionBehavior[]
				{
					new CampaignMissionComponent(),
					new EquipmentControllerLeaveLogic(),
					tournamentFightMissionController,
					new TournamentBehavior(tournamentGame, settlement, tournamentFightMissionController, isPlayerParticipating),
					new AgentVictoryLogic(),
					new MissionAgentPanicHandler(),
					new AgentHumanAILogic(),
					new ArenaAgentStateDeciderLogic(),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionOptionsComponent(),
					new HighlightsController(),
					new SandboxHighlightsController()
				};
			}, true, true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007F4C File Offset: 0x0000614C
		[MissionMethod]
		public static Mission OpenTournamentHorseRaceMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return MissionState.OpenNew("TournamentHorseRace", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission missionController)
			{
				TownHorseRaceMissionController townHorseRaceMissionController = new TownHorseRaceMissionController(culture);
				return new MissionBehavior[]
				{
					new CampaignMissionComponent(),
					new EquipmentControllerLeaveLogic(),
					townHorseRaceMissionController,
					new TournamentBehavior(tournamentGame, settlement, townHorseRaceMissionController, isPlayerParticipating),
					new AgentVictoryLogic(),
					new MissionAgentPanicHandler(),
					new AgentHumanAILogic(),
					new ArenaAgentStateDeciderLogic(),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionOptionsComponent()
				};
			}, true, true);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007FA4 File Offset: 0x000061A4
		[MissionMethod]
		public static Mission OpenTournamentJoustingMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return MissionState.OpenNew("TournamentJousting", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false, DecalAtlasGroup.Town), delegate(Mission missionController)
			{
				TournamentJoustingMissionController tournamentJoustingMissionController = new TournamentJoustingMissionController(culture);
				return new MissionBehavior[]
				{
					new CampaignMissionComponent(),
					new EquipmentControllerLeaveLogic(),
					tournamentJoustingMissionController,
					new TournamentBehavior(tournamentGame, settlement, tournamentJoustingMissionController, isPlayerParticipating),
					new AgentVictoryLogic(),
					new MissionAgentPanicHandler(),
					new AgentHumanAILogic(),
					new ArenaAgentStateDeciderLogic(),
					new MissionHardBorderPlacer(),
					new MissionBoundaryPlacer(),
					new MissionBoundaryCrossingHandler(),
					new MissionOptionsComponent()
				};
			}, true, true);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007FF9 File Offset: 0x000061F9
		[MissionMethod]
		public static Mission OpenBattleChallengeMission(string scene, IList<Hero> priorityCharsAttacker, IList<Hero> priorityCharsDefender)
		{
			return null;
		}
	}
}
