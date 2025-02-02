using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033C RID: 828
	public struct CreateLobbyCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00020602 File Offset: 0x0001E802
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x0002060A File Offset: 0x0001E80A
		public Result ResultCode { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00020613 File Offset: 0x0001E813
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0002061B File Offset: 0x0001E81B
		public object ClientData { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00020624 File Offset: 0x0001E824
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0002062C File Offset: 0x0001E82C
		public Utf8String LobbyId { get; set; }

		// Token: 0x060015D1 RID: 5585 RVA: 0x00020638 File Offset: 0x0001E838
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00020655 File Offset: 0x0001E855
		internal void Set(ref CreateLobbyCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
