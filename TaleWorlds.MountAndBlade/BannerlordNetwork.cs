using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D3 RID: 723
	public static class BannerlordNetwork
	{
		// Token: 0x060027DC RID: 10204 RVA: 0x00099AE4 File Offset: 0x00097CE4
		private static PlayerConnectionInfo CreateServerPeerConnectionInfo()
		{
			LobbyClient gameClient = NetworkMain.GameClient;
			PlayerConnectionInfo playerConnectionInfo = new PlayerConnectionInfo(gameClient.PlayerID);
			PlayerData playerData = gameClient.PlayerData;
			playerConnectionInfo.AddParameter("PlayerData", playerData);
			playerConnectionInfo.AddParameter("UsedCosmetics", gameClient.UsedCosmetics);
			playerConnectionInfo.Name = gameClient.Name;
			return playerConnectionInfo;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x00099B32 File Offset: 0x00097D32
		public static void CreateServerPeer()
		{
			if (MBCommon.CurrentGameType == MBCommon.GameType.MultiClientServer)
			{
				GameNetwork.AddNewPlayerOnServer(BannerlordNetwork.CreateServerPeerConnectionInfo(), true, true);
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00099B49 File Offset: 0x00097D49
		public static void StartMultiplayerLobbyMission(LobbyMissionType lobbyMissionType)
		{
			BannerlordNetwork.LobbyMissionType = lobbyMissionType;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00099B54 File Offset: 0x00097D54
		public static void EndMultiplayerLobbyMission()
		{
			MissionState missionState = Game.Current.GameStateManager.ActiveState as MissionState;
			if (missionState != null && missionState.CurrentMission != null && !missionState.CurrentMission.MissionEnded)
			{
				if (missionState.CurrentMission.CurrentState != Mission.State.Continuing)
				{
					Debug.Print("Remove From Game: Begin delayed disconnect from server.".ToUpper(), 0, Debug.DebugColor.White, 17179869184UL);
					missionState.BeginDelayedDisconnectFromMission();
				}
				else
				{
					Debug.Print("Remove From Game: Begin instant disconnect from server.".ToUpper(), 0, Debug.DebugColor.White, 17179869184UL);
					missionState.CurrentMission.EndMission();
				}
				MBDebug.Print("Starting to clean up the current mission now.", 0, Debug.DebugColor.White, 17179869184UL);
			}
			ChatBox gameHandler = Game.Current.GetGameHandler<ChatBox>();
			if (gameHandler != null)
			{
				gameHandler.ResetMuteList();
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00099C12 File Offset: 0x00097E12
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x00099C19 File Offset: 0x00097E19
		public static LobbyMissionType LobbyMissionType { get; private set; }

		// Token: 0x04000EB9 RID: 3769
		public const int DefaultPort = 9999;
	}
}
