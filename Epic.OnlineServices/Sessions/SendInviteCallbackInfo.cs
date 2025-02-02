using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000119 RID: 281
	public struct SendInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0000C45A File Offset: 0x0000A65A
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0000C462 File Offset: 0x0000A662
		public Result ResultCode { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000C46B File Offset: 0x0000A66B
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0000C473 File Offset: 0x0000A673
		public object ClientData { get; set; }

		// Token: 0x0600088E RID: 2190 RVA: 0x0000C47C File Offset: 0x0000A67C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0000C499 File Offset: 0x0000A699
		internal void Set(ref SendInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
