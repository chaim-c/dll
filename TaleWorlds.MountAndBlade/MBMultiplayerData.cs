using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CC RID: 460
	public class MBMultiplayerData
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0005C7B5 File Offset: 0x0005A9B5
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		public static Guid ServerId { get; set; }

		// Token: 0x06001A48 RID: 6728 RVA: 0x0005C7C4 File Offset: 0x0005A9C4
		[MBCallback]
		public static string GetServerId()
		{
			return MBMultiplayerData.ServerId.ToString();
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
		[MBCallback]
		public static string GetServerName()
		{
			return MBMultiplayerData.ServerName;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0005C7EB File Offset: 0x0005A9EB
		[MBCallback]
		public static string GetGameModule()
		{
			return MBMultiplayerData.GameModule;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0005C7F2 File Offset: 0x0005A9F2
		[MBCallback]
		public static string GetGameType()
		{
			return MBMultiplayerData.GameType;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0005C7F9 File Offset: 0x0005A9F9
		[MBCallback]
		public static string GetMap()
		{
			return MBMultiplayerData.Map;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0005C800 File Offset: 0x0005AA00
		[MBCallback]
		public static int GetCurrentPlayerCount()
		{
			return GameNetwork.NetworkPeerCount;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0005C807 File Offset: 0x0005AA07
		[MBCallback]
		public static int GetPlayerCountLimit()
		{
			return MBMultiplayerData.PlayerCountLimit;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001A4F RID: 6735 RVA: 0x0005C810 File Offset: 0x0005AA10
		// (remove) Token: 0x06001A50 RID: 6736 RVA: 0x0005C844 File Offset: 0x0005AA44
		public static event MBMultiplayerData.GameServerInfoReceivedDelegate GameServerInfoReceived;

		// Token: 0x06001A51 RID: 6737 RVA: 0x0005C878 File Offset: 0x0005AA78
		[MBCallback]
		public static void UpdateGameServerInfo(string id, string gameServer, string gameModule, string gameType, string map, int currentPlayerCount, int maxPlayerCount, string address, int port)
		{
			if (MBMultiplayerData.GameServerInfoReceived != null)
			{
				MBMultiplayerData.GameServerInfoReceived(new CustomBattleId(Guid.Parse(id)), gameServer, gameModule, gameType, map, currentPlayerCount, maxPlayerCount, address, port);
			}
		}

		// Token: 0x04000801 RID: 2049
		public static string ServerName;

		// Token: 0x04000802 RID: 2050
		public static string GameModule;

		// Token: 0x04000803 RID: 2051
		public static string GameType;

		// Token: 0x04000804 RID: 2052
		public static string Map;

		// Token: 0x04000805 RID: 2053
		public static int PlayerCountLimit;

		// Token: 0x020004DA RID: 1242
		// (Invoke) Token: 0x06003786 RID: 14214
		public delegate void GameServerInfoReceivedDelegate(CustomBattleId id, string gameServer, string gameModule, string gameType, string map, int currentPlayerCount, int maxPlayerCount, string address, int port);
	}
}
