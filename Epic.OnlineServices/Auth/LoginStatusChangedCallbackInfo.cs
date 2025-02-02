using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200058B RID: 1419
	public struct LoginStatusChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000363A6 File Offset: 0x000345A6
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x000363AE File Offset: 0x000345AE
		public object ClientData { get; set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000363B7 File Offset: 0x000345B7
		// (set) Token: 0x06002466 RID: 9318 RVA: 0x000363BF File Offset: 0x000345BF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000363C8 File Offset: 0x000345C8
		// (set) Token: 0x06002468 RID: 9320 RVA: 0x000363D0 File Offset: 0x000345D0
		public LoginStatus PrevStatus { get; set; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000363D9 File Offset: 0x000345D9
		// (set) Token: 0x0600246A RID: 9322 RVA: 0x000363E1 File Offset: 0x000345E1
		public LoginStatus CurrentStatus { get; set; }

		// Token: 0x0600246B RID: 9323 RVA: 0x000363EC File Offset: 0x000345EC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00036407 File Offset: 0x00034607
		internal void Set(ref LoginStatusChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PrevStatus = other.PrevStatus;
			this.CurrentStatus = other.CurrentStatus;
		}
	}
}
