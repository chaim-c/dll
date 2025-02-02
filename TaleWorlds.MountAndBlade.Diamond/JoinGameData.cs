using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public class JoinGameData
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		public GameServerProperties GameServerProperties { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public int PeerIndex { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		public int SessionKey { get; set; }

		// Token: 0x06000819 RID: 2073 RVA: 0x0000C2F2 File Offset: 0x0000A4F2
		public JoinGameData()
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000C2FA File Offset: 0x0000A4FA
		public JoinGameData(GameServerProperties gameServerProperties, int peerIndex, int sessionKey)
		{
			this.GameServerProperties = gameServerProperties;
			this.PeerIndex = peerIndex;
			this.SessionKey = sessionKey;
		}
	}
}
