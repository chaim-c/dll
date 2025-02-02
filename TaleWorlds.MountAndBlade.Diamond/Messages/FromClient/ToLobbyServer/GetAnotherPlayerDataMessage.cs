using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000095 RID: 149
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetAnotherPlayerDataMessage : Message
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00003F1D File Offset: 0x0000211D
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00003F25 File Offset: 0x00002125
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002D2 RID: 722 RVA: 0x00003F2E File Offset: 0x0000212E
		public GetAnotherPlayerDataMessage()
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00003F36 File Offset: 0x00002136
		public GetAnotherPlayerDataMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
