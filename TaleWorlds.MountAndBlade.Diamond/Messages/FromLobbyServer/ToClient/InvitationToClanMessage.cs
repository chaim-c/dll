using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000036 RID: 54
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class InvitationToClanMessage : Message
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000029EC File Offset: 0x00000BEC
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000029F4 File Offset: 0x00000BF4
		[JsonProperty]
		public PlayerId InviterId { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000029FD File Offset: 0x00000BFD
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002A05 File Offset: 0x00000C05
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002A0E File Offset: 0x00000C0E
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002A16 File Offset: 0x00000C16
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002A1F File Offset: 0x00000C1F
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002A27 File Offset: 0x00000C27
		[JsonProperty]
		public int ClanPlayerCount { get; private set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x00002A30 File Offset: 0x00000C30
		public InvitationToClanMessage()
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002A38 File Offset: 0x00000C38
		public InvitationToClanMessage(PlayerId inviterId, string clanName, string clanTag, int clanPlayerCount)
		{
			this.InviterId = inviterId;
			this.ClanName = clanName;
			this.ClanTag = clanTag;
			this.ClanPlayerCount = clanPlayerCount;
		}
	}
}
