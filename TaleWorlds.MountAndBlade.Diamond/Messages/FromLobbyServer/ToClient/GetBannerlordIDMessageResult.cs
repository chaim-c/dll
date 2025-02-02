using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class GetBannerlordIDMessageResult : FunctionResult
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002692 File Offset: 0x00000892
		[JsonProperty]
		public string BannerlordID { get; private set; }

		// Token: 0x0600009D RID: 157 RVA: 0x0000269B File Offset: 0x0000089B
		public GetBannerlordIDMessageResult()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000026A3 File Offset: 0x000008A3
		public GetBannerlordIDMessageResult(string bannerlordID)
		{
			this.BannerlordID = bannerlordID;
		}
	}
}
