using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A9 RID: 169
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetRankedLeaderboardMessage : Message
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000040F5 File Offset: 0x000022F5
		// (set) Token: 0x06000302 RID: 770 RVA: 0x000040FD File Offset: 0x000022FD
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00004106 File Offset: 0x00002306
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000410E File Offset: 0x0000230E
		[JsonProperty]
		public int StartIndex { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00004117 File Offset: 0x00002317
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000411F File Offset: 0x0000231F
		[JsonProperty]
		public int Count { get; private set; }

		// Token: 0x06000307 RID: 775 RVA: 0x00004128 File Offset: 0x00002328
		public GetRankedLeaderboardMessage()
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00004130 File Offset: 0x00002330
		public GetRankedLeaderboardMessage(string gameType, int startIndex, int count)
		{
			this.GameType = gameType;
			this.StartIndex = startIndex;
			this.Count = count;
		}
	}
}
