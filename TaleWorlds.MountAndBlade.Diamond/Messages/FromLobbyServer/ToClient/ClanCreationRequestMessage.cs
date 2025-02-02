using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200000F RID: 15
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ClanCreationRequestMessage : Message
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000022DB File Offset: 0x000004DB
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000022E3 File Offset: 0x000004E3
		[JsonProperty]
		public string CreatorPlayerName { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000022EC File Offset: 0x000004EC
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000022F4 File Offset: 0x000004F4
		[JsonProperty]
		public PlayerId CreatorPlayerId { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000022FD File Offset: 0x000004FD
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002305 File Offset: 0x00000505
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000230E File Offset: 0x0000050E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002316 File Offset: 0x00000516
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x06000048 RID: 72 RVA: 0x0000231F File Offset: 0x0000051F
		public ClanCreationRequestMessage()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002327 File Offset: 0x00000527
		public ClanCreationRequestMessage(PlayerId creatorPlayerId, string creatorPlayerName, string clanName, string clanTag)
		{
			this.CreatorPlayerId = creatorPlayerId;
			this.CreatorPlayerName = creatorPlayerName;
			this.ClanName = clanName;
			this.ClanTag = clanTag;
		}
	}
}
