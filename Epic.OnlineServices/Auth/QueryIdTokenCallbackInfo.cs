using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A3 RID: 1443
	public struct QueryIdTokenCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00036A4E File Offset: 0x00034C4E
		// (set) Token: 0x060024E9 RID: 9449 RVA: 0x00036A56 File Offset: 0x00034C56
		public Result ResultCode { get; set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x00036A5F File Offset: 0x00034C5F
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x00036A67 File Offset: 0x00034C67
		public object ClientData { get; set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x00036A70 File Offset: 0x00034C70
		// (set) Token: 0x060024ED RID: 9453 RVA: 0x00036A78 File Offset: 0x00034C78
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x00036A81 File Offset: 0x00034C81
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x00036A89 File Offset: 0x00034C89
		public EpicAccountId TargetAccountId { get; set; }

		// Token: 0x060024F0 RID: 9456 RVA: 0x00036A94 File Offset: 0x00034C94
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00036AB1 File Offset: 0x00034CB1
		internal void Set(ref QueryIdTokenCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetAccountId = other.TargetAccountId;
		}
	}
}
