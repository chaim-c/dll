using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x0200021D RID: 541
	public struct DeleteSnapshotCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00016741 File Offset: 0x00014941
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00016749 File Offset: 0x00014949
		public Result ResultCode { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00016752 File Offset: 0x00014952
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0001675A File Offset: 0x0001495A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00016763 File Offset: 0x00014963
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0001676B File Offset: 0x0001496B
		public object ClientData { get; set; }

		// Token: 0x06000F2A RID: 3882 RVA: 0x00016774 File Offset: 0x00014974
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00016791 File Offset: 0x00014991
		internal void Set(ref DeleteSnapshotCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
		}
	}
}
