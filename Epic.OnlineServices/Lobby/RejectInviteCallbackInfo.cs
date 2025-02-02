using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E5 RID: 997
	public struct RejectInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x000261B5 File Offset: 0x000243B5
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x000261BD File Offset: 0x000243BD
		public Result ResultCode { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x000261C6 File Offset: 0x000243C6
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x000261CE File Offset: 0x000243CE
		public object ClientData { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x000261D7 File Offset: 0x000243D7
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x000261DF File Offset: 0x000243DF
		public Utf8String InviteId { get; set; }

		// Token: 0x060019CB RID: 6603 RVA: 0x000261E8 File Offset: 0x000243E8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00026205 File Offset: 0x00024405
		internal void Set(ref RejectInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
		}
	}
}
