using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200008E RID: 142
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class DeclinePartyJoinRequestMessage : Message
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00003E0C File Offset: 0x0000200C
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00003E14 File Offset: 0x00002014
		[JsonProperty]
		public PlayerId RequesterPlayerId { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00003E1D File Offset: 0x0000201D
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00003E25 File Offset: 0x00002025
		[JsonProperty]
		public PartyJoinDeclineReason Reason { get; private set; }

		// Token: 0x060002BA RID: 698 RVA: 0x00003E2E File Offset: 0x0000202E
		public DeclinePartyJoinRequestMessage()
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00003E36 File Offset: 0x00002036
		public DeclinePartyJoinRequestMessage(PlayerId requesterPlayerId, PartyJoinDeclineReason reason)
		{
			this.RequesterPlayerId = requesterPlayerId;
			this.Reason = reason;
		}
	}
}
