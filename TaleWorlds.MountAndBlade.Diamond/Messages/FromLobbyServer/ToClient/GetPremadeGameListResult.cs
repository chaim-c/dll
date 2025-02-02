using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class GetPremadeGameListResult : FunctionResult
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000286A File Offset: 0x00000A6A
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002872 File Offset: 0x00000A72
		[JsonProperty]
		public PremadeGameList GameList { get; private set; }

		// Token: 0x060000CD RID: 205 RVA: 0x0000287B File Offset: 0x00000A7B
		public GetPremadeGameListResult()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002883 File Offset: 0x00000A83
		public GetPremadeGameListResult(PremadeGameList gameList)
		{
			this.GameList = gameList;
		}
	}
}
