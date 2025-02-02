using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class GetRankedLeaderboardCountMessageResult : FunctionResult
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000028BA File Offset: 0x00000ABA
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000028C2 File Offset: 0x00000AC2
		[JsonProperty]
		public int Count { get; private set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x000028CB File Offset: 0x00000ACB
		public GetRankedLeaderboardCountMessageResult()
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000028D3 File Offset: 0x00000AD3
		public GetRankedLeaderboardCountMessageResult(int count)
		{
			this.Count = count;
		}
	}
}
