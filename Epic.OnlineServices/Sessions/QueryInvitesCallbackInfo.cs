using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010D RID: 269
	public struct QueryInvitesCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0000BCBC File Offset: 0x00009EBC
		public Result ResultCode { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0000BCCD File Offset: 0x00009ECD
		public object ClientData { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0000BCDE File Offset: 0x00009EDE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600083F RID: 2111 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0000BD05 File Offset: 0x00009F05
		internal void Set(ref QueryInvitesCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
