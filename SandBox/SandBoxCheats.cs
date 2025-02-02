using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SandBox.BoardGames.MissionLogics;
using SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000023 RID: 35
	public static class SandBoxCheats
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00006C44 File Offset: 0x00004E44
		[CommandLineFunctionality.CommandLineArgumentFunction("spawn_new_alley_attack", "campaign")]
		public static string SpawnNewAlleyAttack(List<string> strings)
		{
			AlleyCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<AlleyCampaignBehavior>();
			if (campaignBehavior == null)
			{
				return "Alley Campaign Behavior not found";
			}
			foreach (AlleyCampaignBehavior.PlayerAlleyData playerAlleyData in ((List<AlleyCampaignBehavior.PlayerAlleyData>)typeof(AlleyCampaignBehavior).GetField("_playerOwnedCommonAreaData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(campaignBehavior)))
			{
				if (!playerAlleyData.IsUnderAttack)
				{
					if (playerAlleyData.Alley.Settlement.Alleys.Any((Alley x) => x.State == Alley.AreaState.OccupiedByGangLeader))
					{
						typeof(AlleyCampaignBehavior).GetMethod("StartNewAlleyAttack", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(campaignBehavior, new object[]
						{
							playerAlleyData
						});
						return "Success";
					}
				}
			}
			return "There is no suitable alley for spawning an alley attack.";
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006D34 File Offset: 0x00004F34
		[CommandLineFunctionality.CommandLineArgumentFunction("win_board_game", "campaign")]
		public static string WinCurrentGame(List<string> strings)
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = (mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null;
			if (missionBoardGameLogic == null)
			{
				return "There is no board game.";
			}
			missionBoardGameLogic.PlayerOneWon("str_boardgame_victory_message");
			return "Success";
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006D6C File Offset: 0x00004F6C
		[CommandLineFunctionality.CommandLineArgumentFunction("refresh_battle_scene_index_map", "campaign")]
		public static string RefreshBattleSceneIndexMap(List<string> strings)
		{
			MapScene obj = Campaign.Current.MapSceneWrapper as MapScene;
			Type typeFromHandle = typeof(MapScene);
			FieldInfo field = typeFromHandle.GetField("_scene", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo field2 = typeFromHandle.GetField("_battleTerrainIndexMap", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo field3 = typeFromHandle.GetField("_battleTerrainIndexMapWidth", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo field4 = typeFromHandle.GetField("_battleTerrainIndexMapHeight", BindingFlags.Instance | BindingFlags.NonPublic);
			byte[] value = null;
			int num = 0;
			int num2 = 0;
			Scene scene = (Scene)field.GetValue(obj);
			MBMapScene.GetBattleSceneIndexMap(scene, ref value, ref num, ref num2);
			field.SetValue(obj, scene);
			field2.SetValue(obj, value);
			field3.SetValue(obj, num);
			field4.SetValue(obj, num2);
			return "Success";
		}
	}
}
