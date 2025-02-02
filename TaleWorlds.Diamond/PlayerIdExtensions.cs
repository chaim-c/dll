using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000021 RID: 33
	public static class PlayerIdExtensions
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000031A7 File Offset: 0x000013A7
		public static PeerId ConvertToPeerId(this PlayerId playerId)
		{
			return new PeerId(playerId.ToByteArray());
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031B5 File Offset: 0x000013B5
		public static PlayerId ConvertToPlayerId(this PeerId peerId)
		{
			return new PlayerId(peerId.ToByteArray());
		}
	}
}
