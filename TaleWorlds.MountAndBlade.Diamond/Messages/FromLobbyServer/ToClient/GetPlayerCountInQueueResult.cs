using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class GetPlayerCountInQueueResult : FunctionResult
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000027F2 File Offset: 0x000009F2
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000027FA File Offset: 0x000009FA
		[JsonProperty]
		public MatchmakingQueueStats MatchmakingQueueStats { get; private set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00002803 File Offset: 0x00000A03
		public GetPlayerCountInQueueResult()
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000280B File Offset: 0x00000A0B
		public GetPlayerCountInQueueResult(MatchmakingQueueStats matchmakingQueueStats)
		{
			this.MatchmakingQueueStats = matchmakingQueueStats;
		}
	}
}
