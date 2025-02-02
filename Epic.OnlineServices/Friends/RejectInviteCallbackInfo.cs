using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200047C RID: 1148
	public struct RejectInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0002B2F9 File Offset: 0x000294F9
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0002B301 File Offset: 0x00029501
		public Result ResultCode { get; set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0002B30A File Offset: 0x0002950A
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0002B312 File Offset: 0x00029512
		public object ClientData { get; set; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0002B31B File Offset: 0x0002951B
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0002B323 File Offset: 0x00029523
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0002B32C File Offset: 0x0002952C
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0002B334 File Offset: 0x00029534
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06001D4F RID: 7503 RVA: 0x0002B340 File Offset: 0x00029540
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0002B35D File Offset: 0x0002955D
		internal void Set(ref RejectInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
