using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012F RID: 303
	public struct SessionInviteReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		public object ClientData { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0000D306 File Offset: 0x0000B506
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0000D30E File Offset: 0x0000B50E
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000D317 File Offset: 0x0000B517
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0000D31F File Offset: 0x0000B51F
		public Utf8String InviteId { get; set; }

		// Token: 0x06000920 RID: 2336 RVA: 0x0000D328 File Offset: 0x0000B528
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000D343 File Offset: 0x0000B543
		internal void Set(ref SessionInviteReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.InviteId = other.InviteId;
		}
	}
}
