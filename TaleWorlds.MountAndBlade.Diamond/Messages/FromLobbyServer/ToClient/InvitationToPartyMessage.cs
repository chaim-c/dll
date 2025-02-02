using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000037 RID: 55
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class InvitationToPartyMessage : Message
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002A5D File Offset: 0x00000C5D
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002A65 File Offset: 0x00000C65
		[JsonProperty]
		public string InviterPlayerName { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002A76 File Offset: 0x00000C76
		[JsonProperty]
		public PlayerId InviterPlayerId { get; private set; }

		// Token: 0x060000FD RID: 253 RVA: 0x00002A7F File Offset: 0x00000C7F
		public InvitationToPartyMessage()
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002A87 File Offset: 0x00000C87
		public InvitationToPartyMessage(string inviterPlayerName, PlayerId inviterPlayerId)
		{
			this.InviterPlayerName = inviterPlayerName;
			this.InviterPlayerId = inviterPlayerId;
		}
	}
}
