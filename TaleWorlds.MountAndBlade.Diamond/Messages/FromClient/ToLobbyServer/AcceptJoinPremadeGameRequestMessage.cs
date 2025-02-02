using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000072 RID: 114
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AcceptJoinPremadeGameRequestMessage : Message
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00003957 File Offset: 0x00001B57
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000395F File Offset: 0x00001B5F
		[JsonProperty]
		public Guid PartyId { get; private set; }

		// Token: 0x06000244 RID: 580 RVA: 0x00003968 File Offset: 0x00001B68
		public AcceptJoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00003970 File Offset: 0x00001B70
		public AcceptJoinPremadeGameRequestMessage(Guid partyId)
		{
			this.PartyId = partyId;
		}
	}
}
