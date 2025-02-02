using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000074 RID: 116
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AcceptPartyJoinRequestMessage : Message
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00003987 File Offset: 0x00001B87
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000398F File Offset: 0x00001B8F
		[JsonProperty]
		public PlayerId RequesterPlayerId { get; private set; }

		// Token: 0x06000249 RID: 585 RVA: 0x00003998 File Offset: 0x00001B98
		public AcceptPartyJoinRequestMessage()
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000039A0 File Offset: 0x00001BA0
		public AcceptPartyJoinRequestMessage(PlayerId requesterPlayerId)
		{
			this.RequesterPlayerId = requesterPlayerId;
		}
	}
}
