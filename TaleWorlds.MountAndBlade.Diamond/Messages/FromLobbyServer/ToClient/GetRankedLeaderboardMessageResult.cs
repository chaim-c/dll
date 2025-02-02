using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class GetRankedLeaderboardMessageResult : FunctionResult
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000028E2 File Offset: 0x00000AE2
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000028EA File Offset: 0x00000AEA
		[JsonProperty]
		public PlayerLeaderboardData[] LeaderboardPlayers { get; private set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x000028F3 File Offset: 0x00000AF3
		public GetRankedLeaderboardMessageResult()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000028FB File Offset: 0x00000AFB
		public GetRankedLeaderboardMessageResult(PlayerLeaderboardData[] leaderboardPlayers)
		{
			this.LeaderboardPlayers = leaderboardPlayers;
		}
	}
}
