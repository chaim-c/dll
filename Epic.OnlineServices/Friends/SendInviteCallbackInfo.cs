using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000480 RID: 1152
	public struct SendInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0002B616 File Offset: 0x00029816
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0002B61E File Offset: 0x0002981E
		public Result ResultCode { get; set; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0002B627 File Offset: 0x00029827
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0002B62F File Offset: 0x0002982F
		public object ClientData { get; set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0002B638 File Offset: 0x00029838
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0002B640 File Offset: 0x00029840
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0002B649 File Offset: 0x00029849
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0002B651 File Offset: 0x00029851
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06001D6F RID: 7535 RVA: 0x0002B65C File Offset: 0x0002985C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0002B679 File Offset: 0x00029879
		internal void Set(ref SendInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
