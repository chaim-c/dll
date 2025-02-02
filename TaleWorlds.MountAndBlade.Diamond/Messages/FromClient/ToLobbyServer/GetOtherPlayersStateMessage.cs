using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200009F RID: 159
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetOtherPlayersStateMessage : Message
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003FCD File Offset: 0x000021CD
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00003FD5 File Offset: 0x000021D5
		[JsonProperty]
		public List<PlayerId> Players { get; private set; }

		// Token: 0x060002E5 RID: 741 RVA: 0x00003FDE File Offset: 0x000021DE
		public GetOtherPlayersStateMessage()
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00003FE6 File Offset: 0x000021E6
		public GetOtherPlayersStateMessage(List<PlayerId> players)
		{
			this.Players = players;
		}
	}
}
