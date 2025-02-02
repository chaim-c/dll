using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000096 RID: 150
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetAnotherPlayerStateMessage : Message
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00003F45 File Offset: 0x00002145
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00003F4D File Offset: 0x0000214D
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002D6 RID: 726 RVA: 0x00003F56 File Offset: 0x00002156
		public GetAnotherPlayerStateMessage()
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00003F5E File Offset: 0x0000215E
		public GetAnotherPlayerStateMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
