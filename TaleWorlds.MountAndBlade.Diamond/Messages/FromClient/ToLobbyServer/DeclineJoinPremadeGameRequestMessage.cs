using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200008C RID: 140
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class DeclineJoinPremadeGameRequestMessage : Message
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00003DDC File Offset: 0x00001FDC
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00003DE4 File Offset: 0x00001FE4
		[JsonProperty]
		public Guid PartyId { get; private set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x00003DED File Offset: 0x00001FED
		public DeclineJoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00003DF5 File Offset: 0x00001FF5
		public DeclineJoinPremadeGameRequestMessage(Guid partyId)
		{
			this.PartyId = partyId;
		}
	}
}
