using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DA RID: 730
	public static class CustomGameMutedPlayerManager
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x0009A262 File Offset: 0x00098462
		public static List<PlayerId> MutedPlayers
		{
			get
			{
				return CustomGameMutedPlayerManager._mutedPlayers;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0009A269 File Offset: 0x00098469
		public static void MutePlayer(PlayerId playerId)
		{
			CustomGameMutedPlayerManager._mutedPlayers.Add(playerId);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0009A276 File Offset: 0x00098476
		public static void UnmutePlayer(PlayerId playerId)
		{
			CustomGameMutedPlayerManager._mutedPlayers.Remove(playerId);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0009A284 File Offset: 0x00098484
		public static bool IsUserMuted(PlayerId playerId)
		{
			return CustomGameMutedPlayerManager._mutedPlayers.Contains(playerId);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0009A291 File Offset: 0x00098491
		public static void ClearMutedPlayers()
		{
			CustomGameMutedPlayerManager._mutedPlayers.Clear();
		}

		// Token: 0x04000F12 RID: 3858
		private static List<PlayerId> _mutedPlayers = new List<PlayerId>();
	}
}
