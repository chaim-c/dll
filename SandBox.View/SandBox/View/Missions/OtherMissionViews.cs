using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;

namespace SandBox.View.Missions
{
	// Token: 0x0200001F RID: 31
	[ViewCreatorModule]
	public class OtherMissionViews
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000A4E4 File Offset: 0x000086E4
		[ViewMethod("BattleChallenge")]
		public static MissionView[] OpenBattleChallengeMission(Mission mission)
		{
			return new List<MissionView>
			{
				ViewCreator.CreateMissionSingleplayerEscapeMenu(CampaignOptions.IsIronmanMode),
				ViewCreator.CreateOptionsUIHandler(),
				ViewCreator.CreateMissionMainAgentEquipDropView(mission),
				new MissionMessageUIHandler(),
				ViewCreator.CreateMissionAgentStatusUIHandler(mission),
				ViewCreator.CreateMissionMainAgentEquipmentController(mission),
				ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
				ViewCreator.CreateMissionAgentLockVisualizerView(mission),
				new MissionSingleplayerViewHandler()
			}.ToArray();
		}
	}
}
