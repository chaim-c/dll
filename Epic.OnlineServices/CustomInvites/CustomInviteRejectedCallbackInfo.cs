using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F6 RID: 1270
	public struct CustomInviteRejectedCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000304D4 File Offset: 0x0002E6D4
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x000304DC File Offset: 0x0002E6DC
		public object ClientData { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000304E5 File Offset: 0x0002E6E5
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x000304ED File Offset: 0x0002E6ED
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x000304F6 File Offset: 0x0002E6F6
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x000304FE File Offset: 0x0002E6FE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x00030507 File Offset: 0x0002E707
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x0003050F File Offset: 0x0002E70F
		public Utf8String CustomInviteId { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00030518 File Offset: 0x0002E718
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x00030520 File Offset: 0x0002E720
		public Utf8String Payload { get; set; }

		// Token: 0x06002093 RID: 8339 RVA: 0x0003052C File Offset: 0x0002E72C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00030548 File Offset: 0x0002E748
		internal void Set(ref CustomInviteRejectedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}
	}
}
