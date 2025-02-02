using System;
using Galaxy.Api;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000F RID: 15
	public static class SteamPlayerIdExtensions
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003367 File Offset: 0x00001567
		public static PlayerId ToPlayerId(this GalaxyID galaxyID)
		{
			return new PlayerId(5, 0UL, galaxyID.ToUint64());
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003377 File Offset: 0x00001577
		public static GalaxyID ToGOGID(this PlayerId playerId)
		{
			if (playerId.IsValidGOGId())
			{
				return new GalaxyID(playerId.Part4);
			}
			return new GalaxyID(0UL);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003395 File Offset: 0x00001595
		public static bool IsValidGOGId(this PlayerId playerId)
		{
			return playerId.IsValid && playerId.ProvidedType == PlayerIdProvidedTypes.GOG;
		}
	}
}
