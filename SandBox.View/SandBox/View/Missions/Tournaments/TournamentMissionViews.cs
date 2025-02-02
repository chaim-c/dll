using System;
using System.Collections.Generic;
using SandBox.View.Missions.Sound.Components;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;

namespace SandBox.View.Missions.Tournaments
{
	// Token: 0x02000024 RID: 36
	[ViewCreatorModule]
	public class TournamentMissionViews
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000CB04 File Offset: 0x0000AD04
		[ViewMethod("TournamentArchery")]
		public static MissionView[] OpenTournamentArcheryMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				SandBoxViewCreator.CreateMissionTournamentView(),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.6f),
				new MissionSingleplayerViewHandler(),
				new MissionMessageUIHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				new MissionItemContourControllerView(),
				ViewCreator.CreatePhotoModeView(),
				new ArenaPreloadView()
			}.ToArray();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		[ViewMethod("TournamentFight")]
		public static MissionView[] OpenTournamentFightMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				SandBoxViewCreator.CreateMissionTournamentView(),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.6f),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MusicTournamentMissionView(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				new ArenaPreloadView()
			}.ToArray();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		[ViewMethod("TournamentHorseRace")]
		public static MissionView[] OpenTournamentHorseRaceMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionTournamentView(),
				new MissionSingleplayerViewHandler(),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.6f),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				new ArenaPreloadView()
			}.ToArray();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000CE24 File Offset: 0x0000B024
		[ViewMethod("TournamentJousting")]
		public static MissionView[] OpenTournamentJoustingMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				SandBoxViewCreator.CreateMissionTournamentView(),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.6f),
				new MissionSingleplayerViewHandler(),
				new MissionMessageUIHandler(),
				new MissionScoreUIHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				new MissionTournamentJoustingView(),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				new ArenaPreloadView()
			}.ToArray();
		}
	}
}
