using System;
using Steamworks;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.PlatformService.Steam
{
	// Token: 0x02000007 RID: 7
	public static class SteamPlayerIdExtensions
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002D55 File Offset: 0x00000F55
		public static PlayerId ToPlayerId(this CSteamID steamId)
		{
			return new PlayerId(2, 0UL, steamId.m_SteamID);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D65 File Offset: 0x00000F65
		public static CSteamID ToSteamId(this PlayerId playerId)
		{
			if (playerId.IsValidSteamId())
			{
				return new CSteamID(playerId.Part4);
			}
			return new CSteamID(0UL);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D83 File Offset: 0x00000F83
		public static bool IsValidSteamId(this PlayerId playerId)
		{
			return playerId.IsValid && playerId.ProvidedType == PlayerIdProvidedTypes.Steam;
		}
	}
}
