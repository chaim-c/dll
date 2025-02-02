using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000586 RID: 1414
	public struct LoginCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x00035EEB File Offset: 0x000340EB
		// (set) Token: 0x06002438 RID: 9272 RVA: 0x00035EF3 File Offset: 0x000340F3
		public Result ResultCode { get; set; }

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x00035EFC File Offset: 0x000340FC
		// (set) Token: 0x0600243A RID: 9274 RVA: 0x00035F04 File Offset: 0x00034104
		public object ClientData { get; set; }

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x00035F0D File Offset: 0x0003410D
		// (set) Token: 0x0600243C RID: 9276 RVA: 0x00035F15 File Offset: 0x00034115
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x00035F1E File Offset: 0x0003411E
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x00035F26 File Offset: 0x00034126
		public PinGrantInfo? PinGrantInfo { get; set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x00035F2F File Offset: 0x0003412F
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x00035F37 File Offset: 0x00034137
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00035F40 File Offset: 0x00034140
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x00035F48 File Offset: 0x00034148
		public AccountFeatureRestrictedInfo? AccountFeatureRestrictedInfo { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00035F51 File Offset: 0x00034151
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x00035F59 File Offset: 0x00034159
		public EpicAccountId SelectedAccountId { get; set; }

		// Token: 0x06002445 RID: 9285 RVA: 0x00035F64 File Offset: 0x00034164
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00035F84 File Offset: 0x00034184
		internal void Set(ref LoginCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PinGrantInfo = other.PinGrantInfo;
			this.ContinuanceToken = other.ContinuanceToken;
			this.AccountFeatureRestrictedInfo = other.AccountFeatureRestrictedInfo;
			this.SelectedAccountId = other.SelectedAccountId;
		}
	}
}
