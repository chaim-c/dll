using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class UpdateUsedCosmeticItemsMessageResult : FunctionResult
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003241 File Offset: 0x00001441
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00003249 File Offset: 0x00001449
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060001B6 RID: 438 RVA: 0x00003252 File Offset: 0x00001452
		public UpdateUsedCosmeticItemsMessageResult()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000325A File Offset: 0x0000145A
		public UpdateUsedCosmeticItemsMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
