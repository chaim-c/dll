using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A9 RID: 1449
	public struct VerifyIdTokenCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x00037280 File Offset: 0x00035480
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x00037288 File Offset: 0x00035488
		public Result ResultCode { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x00037291 File Offset: 0x00035491
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x00037299 File Offset: 0x00035499
		public object ClientData { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000372A2 File Offset: 0x000354A2
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x000372AA File Offset: 0x000354AA
		public Utf8String ApplicationId { get; set; }

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000372B3 File Offset: 0x000354B3
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x000372BB File Offset: 0x000354BB
		public Utf8String ClientId { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000372C4 File Offset: 0x000354C4
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x000372CC File Offset: 0x000354CC
		public Utf8String ProductId { get; set; }

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000372D5 File Offset: 0x000354D5
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x000372DD File Offset: 0x000354DD
		public Utf8String SandboxId { get; set; }

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x000372E6 File Offset: 0x000354E6
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x000372EE File Offset: 0x000354EE
		public Utf8String DeploymentId { get; set; }

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000372F7 File Offset: 0x000354F7
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x000372FF File Offset: 0x000354FF
		public Utf8String DisplayName { get; set; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x00037308 File Offset: 0x00035508
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x00037310 File Offset: 0x00035510
		public bool IsExternalAccountInfoPresent { get; set; }

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x00037319 File Offset: 0x00035519
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x00037321 File Offset: 0x00035521
		public ExternalAccountType ExternalAccountIdType { get; set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x0003732A File Offset: 0x0003552A
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00037332 File Offset: 0x00035532
		public Utf8String ExternalAccountId { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0003733B File Offset: 0x0003553B
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x00037343 File Offset: 0x00035543
		public Utf8String ExternalAccountDisplayName { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x0003734C File Offset: 0x0003554C
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x00037354 File Offset: 0x00035554
		public Utf8String Platform { get; set; }

		// Token: 0x0600254F RID: 9551 RVA: 0x00037360 File Offset: 0x00035560
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00037380 File Offset: 0x00035580
		internal void Set(ref VerifyIdTokenCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.ApplicationId = other.ApplicationId;
			this.ClientId = other.ClientId;
			this.ProductId = other.ProductId;
			this.SandboxId = other.SandboxId;
			this.DeploymentId = other.DeploymentId;
			this.DisplayName = other.DisplayName;
			this.IsExternalAccountInfoPresent = other.IsExternalAccountInfoPresent;
			this.ExternalAccountIdType = other.ExternalAccountIdType;
			this.ExternalAccountId = other.ExternalAccountId;
			this.ExternalAccountDisplayName = other.ExternalAccountDisplayName;
			this.Platform = other.Platform;
		}
	}
}
