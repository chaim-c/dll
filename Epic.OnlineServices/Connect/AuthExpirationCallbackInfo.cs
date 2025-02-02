using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000511 RID: 1297
	public struct AuthExpirationCallbackInfo : ICallbackInfo
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000316A0 File Offset: 0x0002F8A0
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x000316A8 File Offset: 0x0002F8A8
		public object ClientData { get; set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000316B1 File Offset: 0x0002F8B1
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x000316B9 File Offset: 0x0002F8B9
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600214B RID: 8523 RVA: 0x000316C4 File Offset: 0x0002F8C4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000316DF File Offset: 0x0002F8DF
		internal void Set(ref AuthExpirationCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
