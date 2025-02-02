using System;
using System.Collections.Generic;
using SandBox.Missions.AgentControllers;
using SandBox.View.Missions.Sound.Components;
using SandBox.View.Missions.Tournaments;
using SandBox.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.View.MissionViews.Sound;

namespace SandBox.View.Missions
{
	// Token: 0x02000020 RID: 32
	[ViewCreatorModule]
	public class SandBoxMissionViews
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000A570 File Offset: 0x00008770
		[ViewMethod("TownCenter")]
		public static MissionView[] OpenTownCenterMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				ViewCreator.CreateMissionLeaveView(),
				ViewCreator.CreatePhotoModeView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView()
			}.ToArray();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A670 File Offset: 0x00008870
		[ViewMethod("TownAmbush")]
		public static MissionView[] OpenTownAmbushMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreatePhotoModeView(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A764 File Offset: 0x00008964
		[ViewMethod("FacialAnimationTest")]
		public static MissionView[] OpenFacialAnimationTest(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A82C File Offset: 0x00008A2C
		[ViewMethod("Indoor")]
		public static MissionView[] OpenTavernMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MusicSilencedMissionView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateMissionLeaveView(),
				SandBoxViewCreator.CreateBoardGameView(),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A92C File Offset: 0x00008B2C
		[ViewMethod("PrisonBreak")]
		public static MissionView[] OpenPrisonBreakMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				new MusicSilencedMissionView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateMissionLeaveView(),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		[ViewMethod("Village")]
		public static MissionView[] OpenVillageMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateMissionLeaveView(),
				new MissionBoundaryWallView(),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		[ViewMethod("Retirement")]
		public static MissionView[] OpenRetirementMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateMissionLeaveView(),
				new MissionBoundaryWallView(),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		[ViewMethod("ArenaPracticeFight")]
		public static MissionView[] OpenArenaStartMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.3f),
				SandBoxViewCreator.CreateMissionArenaPracticeFightView(),
				ViewCreator.CreateMissionLeaveView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				new MusicArenaPracticeMissionView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				new ArenaPreloadView()
			}.ToArray();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		[ViewMethod("ArenaDuelMission")]
		public static MissionView[] OpenArenaDuelMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionLeaveView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				new MissionSingleplayerViewHandler(),
				new MusicSilencedMissionView(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionAudienceHandler(0.4f + MBRandom.RandomFloat * 0.3f),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		[ViewMethod("TownMerchant")]
		public static MissionView[] OpenTownMerchantMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionLeaveView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				SandBoxViewCreator.CreateMissionNameMarkerUIHandler(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000AED0 File Offset: 0x000090D0
		[ViewMethod("Alley")]
		public static MissionView[] OpenAlleyMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateMissionLeaveView(),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionBarterView(),
				new MissionBoundaryWallView(),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000AFD0 File Offset: 0x000091D0
		[ViewMethod("SneakTeam3")]
		public static MissionView[] OpenSneakTeam3Mission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000B08B File Offset: 0x0000928B
		[ViewMethod("SimpleMountedPlayer")]
		public static MissionView[] OpenSimpleMountedPlayerMission(Mission mission)
		{
			return new List<MissionView>().ToArray();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000B098 File Offset: 0x00009298
		[ViewMethod("Battle")]
		public static MissionView[] OpenBattleMission(Mission mission)
		{
			List<MissionView> list = new List<MissionView>();
			list.Add(new MissionCampaignView());
			list.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode));
			list.Add(ViewCreator.CreateMissionAgentLabelUIHandler(mission));
			list.Add(ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)));
			list.Add(ViewCreator.CreateOptionsUIHandler());
			list.Add(ViewCreator.CreateMissionMainAgentEquipDropView(mission));
			MissionView missionView = ViewCreator.CreateMissionOrderUIHandler(null);
			list.Add(missionView);
			list.Add(new OrderTroopPlacer());
			list.Add(new MissionSingleplayerViewHandler());
			list.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
			list.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
			list.Add(ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission));
			list.Add(ViewCreator.CreateMissionAgentLockVisualizerView(mission));
			list.Add(new MusicBattleMissionView(false));
			list.Add(new DeploymentMissionView());
			list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			list.Add(ViewCreator.CreateMissionBoundaryCrossingView());
			list.Add(new MissionBoundaryWallView());
			list.Add(ViewCreator.CreateMissionFormationMarkerUIHandler(mission));
			list.Add(new MissionFormationTargetSelectionHandler());
			list.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
			list.Add(ViewCreator.CreateMissionSpectatorControlView(mission));
			list.Add(new MissionItemContourControllerView());
			list.Add(new MissionAgentContourControllerView());
			list.Add(new MissionPreloadView());
			list.Add(new MissionCampaignBattleSpectatorView());
			list.Add(ViewCreator.CreatePhotoModeView());
			ISiegeDeploymentView @object = missionView as ISiegeDeploymentView;
			list.Add(new MissionEntitySelectionUIHandler(new Action<GameEntity>(@object.OnEntitySelection), new Action<GameEntity>(@object.OnEntityHover)));
			list.Add(ViewCreator.CreateMissionOrderOfBattleUIHandler(mission, new SPOrderOfBattleVM()));
			return list.ToArray();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000B23C File Offset: 0x0000943C
		[ViewMethod("AlleyFight")]
		public static MissionView[] OpenAlleyFightMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000B368 File Offset: 0x00009568
		[ViewMethod("HideoutBattle")]
		public static MissionView[] OpenHideoutBattleMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				new MissionHideoutCinematicView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				new MusicSilencedMissionView(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				new MissionPreloadView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000B4CC File Offset: 0x000096CC
		[ViewMethod("EnteringSettlementBattle")]
		public static MissionView[] OpenBattleMissionWhileEnteringSettlement(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000B60C File Offset: 0x0000980C
		[ViewMethod("CombatWithDialogue")]
		public static MissionView[] OpenCombatMissionWithDialogue(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000B74C File Offset: 0x0000994C
		[ViewMethod("SiegeEngine")]
		public static MissionView[] OpenTestSiegeEngineMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer()
			}.ToArray();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000B77A File Offset: 0x0000997A
		[ViewMethod("CustomCameraMission")]
		public static MissionView[] OpenCustomCameraMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				new MissionCustomCameraView()
			}.ToArray();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000B79C File Offset: 0x0000999C
		[ViewMethod("AmbushBattle")]
		public static MissionView[] OpenAmbushBattleMission(Mission mission)
		{
			throw new NotImplementedException("Ambush battle is not implemented.");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000B7A8 File Offset: 0x000099A8
		[ViewMethod("Ambush")]
		public static MissionView[] OpenAmbushMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionAmbushView(),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000B890 File Offset: 0x00009A90
		[ViewMethod("Camp")]
		public static MissionView[] OpenCampMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000B958 File Offset: 0x00009B58
		[ViewMethod("SiegeMissionWithDeployment")]
		public static MissionView[] OpenSiegeMissionWithDeployment(Mission mission)
		{
			List<MissionView> list = new List<MissionView>();
			mission.GetMissionBehavior<SiegeDeploymentHandler>();
			list.Add(new MissionCampaignView());
			list.Add(new MissionConversationCameraView());
			list.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode));
			list.Add(ViewCreator.CreateOptionsUIHandler());
			list.Add(ViewCreator.CreateMissionMainAgentEquipDropView(mission));
			list.Add(ViewCreator.CreateMissionAgentLabelUIHandler(mission));
			list.Add(ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)));
			MissionView missionView = ViewCreator.CreateMissionOrderUIHandler(null);
			list.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
			list.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
			list.Add(ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission));
			list.Add(ViewCreator.CreateMissionAgentLockVisualizerView(mission));
			list.Add(missionView);
			list.Add(new OrderTroopPlacer());
			list.Add(new MissionSingleplayerViewHandler());
			list.Add(new MusicBattleMissionView(true));
			list.Add(new DeploymentMissionView());
			list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			list.Add(ViewCreator.CreateMissionBoundaryCrossingView());
			list.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
			list.Add(ViewCreator.CreatePhotoModeView());
			list.Add(ViewCreator.CreateMissionFormationMarkerUIHandler(mission));
			list.Add(new MissionFormationTargetSelectionHandler());
			ISiegeDeploymentView @object = missionView as ISiegeDeploymentView;
			list.Add(new MissionEntitySelectionUIHandler(new Action<GameEntity>(@object.OnEntitySelection), new Action<GameEntity>(@object.OnEntityHover)));
			list.Add(ViewCreator.CreateMissionSpectatorControlView(mission));
			list.Add(new MissionItemContourControllerView());
			list.Add(new MissionAgentContourControllerView());
			list.Add(new MissionPreloadView());
			list.Add(new MissionCampaignBattleSpectatorView());
			list.Add(ViewCreator.CreateMissionOrderOfBattleUIHandler(mission, new SPOrderOfBattleVM()));
			list.Add(ViewCreator.CreateMissionSiegeEngineMarkerView(mission));
			return list.ToArray();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000BB10 File Offset: 0x00009D10
		[ViewMethod("SiegeMissionNoDeployment")]
		public static MissionView[] OpenSiegeMissionNoDeployment(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				new MusicBattleMissionView(true),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionPreloadView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreateMissionSiegeEngineMarkerView(mission)
			}.ToArray();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000BC68 File Offset: 0x00009E68
		[ViewMethod("SiegeLordsHallFightMission")]
		public static MissionView[] OpenSiegeLordsHallFightMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionAgentLabelUIHandler(mission),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionPreloadView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000BD90 File Offset: 0x00009F90
		[ViewMethod("Siege")]
		public static MissionView[] OpenSiegeMission(Mission mission)
		{
			List<MissionView> list = new List<MissionView>();
			mission.GetMissionBehavior<SiegeDeploymentHandler>();
			list.Add(new MissionCampaignView());
			list.Add(new MissionConversationCameraView());
			list.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode));
			list.Add(ViewCreator.CreateOptionsUIHandler());
			list.Add(ViewCreator.CreateMissionMainAgentEquipDropView(mission));
			MissionView missionView = ViewCreator.CreateMissionOrderUIHandler(null);
			list.Add(missionView);
			list.Add(new OrderTroopPlacer());
			list.Add(new MissionSingleplayerViewHandler());
			list.Add(new DeploymentMissionView());
			list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			list.Add(ViewCreator.CreateMissionBoundaryCrossingView());
			list.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
			list.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
			list.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
			list.Add(ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission));
			list.Add(ViewCreator.CreateMissionAgentLockVisualizerView(mission));
			list.Add(ViewCreator.CreateMissionSpectatorControlView(mission));
			list.Add(ViewCreator.CreatePhotoModeView());
			ISiegeDeploymentView @object = missionView as ISiegeDeploymentView;
			list.Add(new MissionEntitySelectionUIHandler(new Action<GameEntity>(@object.OnEntitySelection), new Action<GameEntity>(@object.OnEntityHover)));
			list.Add(ViewCreator.CreateMissionFormationMarkerUIHandler(mission));
			list.Add(new MissionFormationTargetSelectionHandler());
			list.Add(new MissionItemContourControllerView());
			list.Add(new MissionAgentContourControllerView());
			list.Add(new MissionCampaignBattleSpectatorView());
			list.Add(ViewCreator.CreateMissionSiegeEngineMarkerView(mission));
			return list.ToArray();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000BF04 File Offset: 0x0000A104
		[ViewMethod("SiegeMissionForTutorial")]
		public static MissionView[] OpenSiegeMissionForTutorial(Mission mission)
		{
			Debug.FailedAssert("Do not use SiegeForTutorial! Use campaign!", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Missions\\SandBoxMissionViews.cs", "OpenSiegeMissionForTutorial", 876);
			List<MissionView> list = new List<MissionView>();
			list.Add(new MissionConversationCameraView());
			list.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode));
			list.Add(ViewCreator.CreateOptionsUIHandler());
			list.Add(ViewCreator.CreateMissionMainAgentEquipDropView(mission));
			MissionView missionView = ViewCreator.CreateMissionOrderUIHandler(null);
			list.Add(missionView);
			list.Add(new OrderTroopPlacer());
			list.Add(new MissionSingleplayerViewHandler());
			list.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
			list.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
			list.Add(ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission));
			list.Add(ViewCreator.CreateMissionAgentLockVisualizerView(mission));
			list.Add(ViewCreator.CreateMissionSpectatorControlView(mission));
			list.Add(ViewCreator.CreatePhotoModeView());
			list.Add(ViewCreator.CreateMissionSiegeEngineMarkerView(mission));
			ISiegeDeploymentView @object = missionView as ISiegeDeploymentView;
			list.Add(new MissionEntitySelectionUIHandler(new Action<GameEntity>(@object.OnEntitySelection), new Action<GameEntity>(@object.OnEntityHover)));
			list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			list.Add(new MissionCampaignBattleSpectatorView());
			return list.ToArray();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000C030 File Offset: 0x0000A230
		[ViewMethod("AmbushBattleForTutorial")]
		public static MissionView[] OpenAmbushMissionForTutorial(Mission mission)
		{
			List<MissionView> list = new List<MissionView>();
			list.Add(new MissionCampaignView());
			list.Add(new MissionConversationCameraView());
			if (mission.GetMissionBehavior<AmbushMissionController>().IsPlayerAmbusher)
			{
				list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			}
			list.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode));
			list.Add(ViewCreator.CreateOptionsUIHandler());
			list.Add(ViewCreator.CreateMissionMainAgentEquipDropView(mission));
			list.Add(ViewCreator.CreateMissionOrderUIHandler(null));
			list.Add(new OrderTroopPlacer());
			list.Add(new MissionSingleplayerViewHandler());
			list.Add(new MissionAmbushView());
			list.Add(ViewCreator.CreatePhotoModeView());
			list.Add(new MissionAmbushIntroView());
			list.Add(new MissionDeploymentBoundaryMarker(new BorderFlagEntityFactory("swallowtail_banner"), 2f));
			list.Add(new MissionCampaignBattleSpectatorView());
			return list.ToArray();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000C115 File Offset: 0x0000A315
		[ViewMethod("FormationTest")]
		public static MissionView[] OpenFormationTestMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer()
			}.ToArray();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C144 File Offset: 0x0000A344
		[ViewMethod("VillageBattle")]
		public static MissionView[] OpenVillageBattleMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				ViewCreator.CreateMissionBattleScoreUIHandler(mission, new SPScoreboardVM(null)),
				ViewCreator.CreateMissionOrderUIHandler(null),
				new OrderTroopPlacer(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionFormationMarkerUIHandler(mission),
				new MissionFormationTargetSelectionHandler(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionItemContourControllerView(),
				new MissionAgentContourControllerView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView(),
				ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler()
			}.ToArray();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000C26C File Offset: 0x0000A46C
		[ViewMethod("SettlementTest")]
		public static MissionView[] OpenSettlementTestMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				ViewCreator.CreateMissionSpectatorControlView(mission),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C328 File Offset: 0x0000A528
		[ViewMethod("EquipmentTest")]
		public static MissionView[] OpenEquipmentTestMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		[ViewMethod("FacialAnimTest")]
		public static MissionView[] OpenFacialAnimTestMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionSingleplayerViewHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				ViewCreator.CreateMissionBoundaryCrossingView(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				SandBoxViewCreator.CreateMissionBarterView(),
				new MissionBoundaryWallView(),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000C470 File Offset: 0x0000A670
		[ViewMethod("EquipItemTool")]
		public static MissionView[] OpenEquipItemToolMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionConversationCameraView(),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionEquipItemToolView(),
				ViewCreator.CreateMissionLeaveView()
			}.ToArray();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		[ViewMethod("Conversation")]
		public static MissionView[] OpenConversationMission(Mission mission)
		{
			return new List<MissionView>
			{
				new MissionCampaignView(),
				new MissionConversationCameraView(),
				new MissionSingleplayerViewHandler(),
				SandBoxViewCreator.CreateMissionConversationView(mission),
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				SandBoxViewCreator.CreateMissionBarterView(),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionCampaignBattleSpectatorView(),
				ViewCreator.CreatePhotoModeView()
			}.ToArray();
		}
	}
}
