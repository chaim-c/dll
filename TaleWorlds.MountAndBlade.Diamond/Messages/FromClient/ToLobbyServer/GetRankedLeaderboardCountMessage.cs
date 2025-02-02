using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A8 RID: 168
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetRankedLeaderboardCountMessage : Message
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000040CD File Offset: 0x000022CD
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000040D5 File Offset: 0x000022D5
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060002FF RID: 767 RVA: 0x000040DE File Offset: 0x000022DE
		public GetRankedLeaderboardCountMessage()
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000040E6 File Offset: 0x000022E6
		public GetRankedLeaderboardCountMessage(string gameType)
		{
			this.GameType = gameType;
		}
	}
}
