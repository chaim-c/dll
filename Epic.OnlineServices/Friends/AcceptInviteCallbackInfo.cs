using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200045E RID: 1118
	public struct AcceptInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0002A501 File Offset: 0x00028701
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x0002A509 File Offset: 0x00028709
		public Result ResultCode { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0002A512 File Offset: 0x00028712
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x0002A51A File Offset: 0x0002871A
		public object ClientData { get; set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0002A523 File Offset: 0x00028723
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0002A52B File Offset: 0x0002872B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0002A534 File Offset: 0x00028734
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0002A53C File Offset: 0x0002873C
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0002A548 File Offset: 0x00028748
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0002A565 File Offset: 0x00028765
		internal void Set(ref AcceptInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
