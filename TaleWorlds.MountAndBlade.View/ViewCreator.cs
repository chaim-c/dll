using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200001A RID: 26
	public static class ViewCreator
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00006F60 File Offset: 0x00005160
		public static ScreenBase CreateCreditsScreen()
		{
			return ViewCreatorManager.CreateScreenView<CreditsScreen>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006F67 File Offset: 0x00005167
		public static ScreenBase CreateOptionsScreen(bool fromMainMenu)
		{
			return ViewCreatorManager.CreateScreenView<OptionsScreen>(new object[]
			{
				fromMainMenu
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006F7D File Offset: 0x0000517D
		public static ScreenBase CreateMBFaceGeneratorScreen(BasicCharacterObject character, bool openedFromMultiplayer = false, IFaceGeneratorCustomFilter filter = null)
		{
			return ViewCreatorManager.CreateScreenView<FaceGeneratorScreen>(new object[]
			{
				character,
				openedFromMultiplayer,
				filter
			});
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006F9B File Offset: 0x0000519B
		public static MissionView CreateMissionAgentStatusUIHandler(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionAgentStatusUIHandler>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006FAC File Offset: 0x000051AC
		public static MissionView CreateMissionMainAgentEquipDropView(Mission mission)
		{
			return ViewCreatorManager.CreateMissionView<MissionMainAgentEquipDropView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006FBD File Offset: 0x000051BD
		public static MissionView CreateMissionSiegeEngineMarkerView(Mission mission)
		{
			return ViewCreatorManager.CreateMissionView<MissionSiegeEngineMarkerView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006FCE File Offset: 0x000051CE
		public static MissionView CreateMissionMainAgentEquipmentController(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionMainAgentEquipmentControllerView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006FDF File Offset: 0x000051DF
		public static MissionView CreateMissionMainAgentCheerBarkControllerView(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionMainAgentCheerBarkControllerView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006FF0 File Offset: 0x000051F0
		public static MissionView CreateMissionAgentLockVisualizerView(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionAgentLockVisualizerView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007001 File Offset: 0x00005201
		public static MissionView CreateOptionsUIHandler()
		{
			return ViewCreatorManager.CreateMissionView<MissionOptionsUIHandler>(false, null, Array.Empty<object>());
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000700F File Offset: 0x0000520F
		public static MissionView CreateSingleplayerMissionKillNotificationUIHandler()
		{
			return ViewCreatorManager.CreateMissionView<MissionSingleplayerKillNotificationUIHandler>(false, null, Array.Empty<object>());
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000701D File Offset: 0x0000521D
		public static MissionView CreateMissionAgentLabelUIHandler(Mission mission)
		{
			return ViewCreatorManager.CreateMissionView<MissionAgentLabelView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000702E File Offset: 0x0000522E
		public static MissionView CreateMissionOrderUIHandler(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionOrderUIHandler>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000703F File Offset: 0x0000523F
		public static MissionView CreateMissionOrderOfBattleUIHandler(Mission mission, OrderOfBattleVM dataSource)
		{
			return ViewCreatorManager.CreateMissionView<MissionOrderOfBattleUIHandler>(false, mission, new object[]
			{
				dataSource
			});
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007052 File Offset: 0x00005252
		public static MissionView CreateMissionSpectatorControlView(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionSpectatorControlView>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007063 File Offset: 0x00005263
		public static MissionView CreateMissionBattleScoreUIHandler(Mission mission, ScoreboardBaseVM dataSource)
		{
			return ViewCreatorManager.CreateMissionView<MissionBattleScoreUIHandler>(false, mission, new object[]
			{
				dataSource
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007076 File Offset: 0x00005276
		public static MissionView CreateMissionBoundaryCrossingView()
		{
			return ViewCreatorManager.CreateMissionView<MissionBoundaryCrossingView>(false, null, Array.Empty<object>());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007084 File Offset: 0x00005284
		public static MissionView CreateMissionLeaveView()
		{
			return ViewCreatorManager.CreateMissionView<MissionLeaveView>(false, null, Array.Empty<object>());
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007092 File Offset: 0x00005292
		public static MissionView CreatePhotoModeView()
		{
			return ViewCreatorManager.CreateMissionView<PhotoModeView>(false, null, Array.Empty<object>());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000070A0 File Offset: 0x000052A0
		public static MissionView CreateMissionSingleplayerEscapeMenu(bool isIronmanMode)
		{
			return ViewCreatorManager.CreateMissionView<MissionSingleplayerEscapeMenu>(false, null, new object[]
			{
				isIronmanMode
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000070B8 File Offset: 0x000052B8
		public static MissionView CreateOrderTroopPlacerView(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<OrderTroopPlacer>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000070C9 File Offset: 0x000052C9
		public static MissionView CreateMissionFormationMarkerUIHandler(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionFormationMarkerUIHandler>(mission != null, mission, Array.Empty<object>());
		}
	}
}
