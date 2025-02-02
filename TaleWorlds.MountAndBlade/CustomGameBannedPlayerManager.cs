using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D9 RID: 729
	public static class CustomGameBannedPlayerManager
	{
		// Token: 0x060027F1 RID: 10225 RVA: 0x0009A1EC File Offset: 0x000983EC
		public static void AddBannedPlayer(PlayerId playerId, int banDueTime)
		{
			CustomGameBannedPlayerManager._bannedPlayers[playerId] = new CustomGameBannedPlayerManager.BannedPlayer
			{
				PlayerId = playerId,
				BanDueTime = banDueTime
			};
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0009A220 File Offset: 0x00098420
		public static bool IsUserBanned(PlayerId playerId)
		{
			return CustomGameBannedPlayerManager._bannedPlayers.ContainsKey(playerId) && CustomGameBannedPlayerManager._bannedPlayers[playerId].BanDueTime > Environment.TickCount;
		}

		// Token: 0x04000F11 RID: 3857
		private static Dictionary<PlayerId, CustomGameBannedPlayerManager.BannedPlayer> _bannedPlayers = new Dictionary<PlayerId, CustomGameBannedPlayerManager.BannedPlayer>();

		// Token: 0x02000598 RID: 1432
		private struct BannedPlayer
		{
			// Token: 0x170009B2 RID: 2482
			// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000E6A01 File Offset: 0x000E4C01
			// (set) Token: 0x06003A59 RID: 14937 RVA: 0x000E6A09 File Offset: 0x000E4C09
			public PlayerId PlayerId { get; set; }

			// Token: 0x170009B3 RID: 2483
			// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000E6A12 File Offset: 0x000E4C12
			// (set) Token: 0x06003A5B RID: 14939 RVA: 0x000E6A1A File Offset: 0x000E4C1A
			public int BanDueTime { get; set; }
		}
	}
}
