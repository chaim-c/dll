using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A5 RID: 165
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetPlayerStatsMessage : Message
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00004095 File Offset: 0x00002295
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000409D File Offset: 0x0000229D
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002F9 RID: 761 RVA: 0x000040A6 File Offset: 0x000022A6
		public GetPlayerStatsMessage()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000040AE File Offset: 0x000022AE
		public GetPlayerStatsMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
