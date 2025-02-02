using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A4 RID: 164
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetPlayerGameTypeRankInfoMessage : Message
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000406D File Offset: 0x0000226D
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00004075 File Offset: 0x00002275
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x0000407E File Offset: 0x0000227E
		public GetPlayerGameTypeRankInfoMessage()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00004086 File Offset: 0x00002286
		public GetPlayerGameTypeRankInfoMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
