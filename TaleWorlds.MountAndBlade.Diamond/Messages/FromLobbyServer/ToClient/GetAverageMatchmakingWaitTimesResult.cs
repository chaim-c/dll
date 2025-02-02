using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class GetAverageMatchmakingWaitTimesResult : FunctionResult
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002662 File Offset: 0x00000862
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000266A File Offset: 0x0000086A
		[JsonProperty]
		public MatchmakingWaitTimeStats MatchmakingWaitTimeStats { get; private set; }

		// Token: 0x06000099 RID: 153 RVA: 0x00002673 File Offset: 0x00000873
		public GetAverageMatchmakingWaitTimesResult()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000267B File Offset: 0x0000087B
		public GetAverageMatchmakingWaitTimesResult(MatchmakingWaitTimeStats matchmakingWaitTimeStats)
		{
			this.MatchmakingWaitTimeStats = matchmakingWaitTimeStats;
		}
	}
}
