using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E1 RID: 993
	public struct QueryInvitesCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00025F67 File Offset: 0x00024167
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x00025F6F File Offset: 0x0002416F
		public Result ResultCode { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00025F78 File Offset: 0x00024178
		// (set) Token: 0x060019AF RID: 6575 RVA: 0x00025F80 File Offset: 0x00024180
		public object ClientData { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00025F89 File Offset: 0x00024189
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x00025F91 File Offset: 0x00024191
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060019B2 RID: 6578 RVA: 0x00025F9C File Offset: 0x0002419C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00025FB9 File Offset: 0x000241B9
		internal void Set(ref QueryInvitesCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
