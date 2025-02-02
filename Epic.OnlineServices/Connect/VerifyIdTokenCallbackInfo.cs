using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200056A RID: 1386
	public struct VerifyIdTokenCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x00034617 File Offset: 0x00032817
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x0003461F File Offset: 0x0003281F
		public Result ResultCode { get; set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x00034628 File Offset: 0x00032828
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x00034630 File Offset: 0x00032830
		public object ClientData { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00034639 File Offset: 0x00032839
		// (set) Token: 0x0600236C RID: 9068 RVA: 0x00034641 File Offset: 0x00032841
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0003464A File Offset: 0x0003284A
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x00034652 File Offset: 0x00032852
		public bool IsAccountInfoPresent { get; set; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0003465B File Offset: 0x0003285B
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x00034663 File Offset: 0x00032863
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x0003466C File Offset: 0x0003286C
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x00034674 File Offset: 0x00032874
		public Utf8String AccountId { get; set; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0003467D File Offset: 0x0003287D
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x00034685 File Offset: 0x00032885
		public Utf8String Platform { get; set; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0003468E File Offset: 0x0003288E
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x00034696 File Offset: 0x00032896
		public Utf8String DeviceType { get; set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x0003469F File Offset: 0x0003289F
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x000346A7 File Offset: 0x000328A7
		public Utf8String ClientId { get; set; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000346B0 File Offset: 0x000328B0
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x000346B8 File Offset: 0x000328B8
		public Utf8String ProductId { get; set; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000346C1 File Offset: 0x000328C1
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x000346C9 File Offset: 0x000328C9
		public Utf8String SandboxId { get; set; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000346D2 File Offset: 0x000328D2
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x000346DA File Offset: 0x000328DA
		public Utf8String DeploymentId { get; set; }

		// Token: 0x0600237F RID: 9087 RVA: 0x000346E4 File Offset: 0x000328E4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00034704 File Offset: 0x00032904
		internal void Set(ref VerifyIdTokenCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.ProductUserId = other.ProductUserId;
			this.IsAccountInfoPresent = other.IsAccountInfoPresent;
			this.AccountIdType = other.AccountIdType;
			this.AccountId = other.AccountId;
			this.Platform = other.Platform;
			this.DeviceType = other.DeviceType;
			this.ClientId = other.ClientId;
			this.ProductId = other.ProductId;
			this.SandboxId = other.SandboxId;
			this.DeploymentId = other.DeploymentId;
		}
	}
}
