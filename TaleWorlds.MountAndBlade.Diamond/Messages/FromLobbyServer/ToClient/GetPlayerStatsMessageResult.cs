using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class GetPlayerStatsMessageResult : FunctionResult
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002842 File Offset: 0x00000A42
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000284A File Offset: 0x00000A4A
		[JsonProperty]
		public PlayerStatsBase[] PlayerStats { get; private set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00002853 File Offset: 0x00000A53
		public GetPlayerStatsMessageResult()
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000285B File Offset: 0x00000A5B
		public GetPlayerStatsMessageResult(PlayerStatsBase[] playerStats)
		{
			this.PlayerStats = playerStats;
		}
	}
}
