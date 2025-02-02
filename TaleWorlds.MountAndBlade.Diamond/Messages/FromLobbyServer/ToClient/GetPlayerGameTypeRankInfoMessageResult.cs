using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class GetPlayerGameTypeRankInfoMessageResult : FunctionResult
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000281A File Offset: 0x00000A1A
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002822 File Offset: 0x00000A22
		[JsonProperty]
		public GameTypeRankInfo[] GameTypeRankInfo { get; private set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000282B File Offset: 0x00000A2B
		public GetPlayerGameTypeRankInfoMessageResult()
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002833 File Offset: 0x00000A33
		public GetPlayerGameTypeRankInfoMessageResult(GameTypeRankInfo[] gameTypeRankInfo)
		{
			this.GameTypeRankInfo = gameTypeRankInfo;
		}
	}
}
