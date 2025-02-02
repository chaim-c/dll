using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class BuyCosmeticMessageResult : FunctionResult
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000213B File Offset: 0x0000033B
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002143 File Offset: 0x00000343
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002154 File Offset: 0x00000354
		[JsonProperty]
		public int Gold { get; private set; }

		// Token: 0x0600001C RID: 28 RVA: 0x0000215D File Offset: 0x0000035D
		public BuyCosmeticMessageResult()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002165 File Offset: 0x00000365
		public BuyCosmeticMessageResult(bool successful, int gold)
		{
			this.Successful = successful;
			this.Gold = gold;
		}
	}
}
