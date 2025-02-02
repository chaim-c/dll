using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000045 RID: 69
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PlayerAssignedPartyLeaderMessage : Message
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00002D78 File Offset: 0x00000F78
		[JsonProperty]
		public PlayerId PartyLeaderId { get; private set; }

		// Token: 0x06000144 RID: 324 RVA: 0x00002D81 File Offset: 0x00000F81
		public PlayerAssignedPartyLeaderMessage()
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002D89 File Offset: 0x00000F89
		public PlayerAssignedPartyLeaderMessage(PlayerId partyLeaderId)
		{
			this.PartyLeaderId = partyLeaderId;
		}
	}
}
