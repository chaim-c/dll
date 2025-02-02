using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AA RID: 170
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetRecentPlayersStatusMessage : Message
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000414D File Offset: 0x0000234D
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00004155 File Offset: 0x00002355
		[JsonProperty]
		public PlayerId[] RecentPlayers { get; private set; }

		// Token: 0x0600030B RID: 779 RVA: 0x0000415E File Offset: 0x0000235E
		public GetRecentPlayersStatusMessage()
		{
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00004166 File Offset: 0x00002366
		public GetRecentPlayersStatusMessage(PlayerId[] recentPlayers)
		{
			this.RecentPlayers = recentPlayers;
		}
	}
}
