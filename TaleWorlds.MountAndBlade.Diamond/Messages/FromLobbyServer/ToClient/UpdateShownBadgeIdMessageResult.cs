using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class UpdateShownBadgeIdMessageResult : FunctionResult
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003219 File Offset: 0x00001419
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00003221 File Offset: 0x00001421
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060001B2 RID: 434 RVA: 0x0000322A File Offset: 0x0000142A
		public UpdateShownBadgeIdMessageResult()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00003232 File Offset: 0x00001432
		public UpdateShownBadgeIdMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
