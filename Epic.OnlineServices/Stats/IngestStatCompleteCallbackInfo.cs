using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AD RID: 173
	public struct IngestStatCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000097E8 File Offset: 0x000079E8
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000097F0 File Offset: 0x000079F0
		public Result ResultCode { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000097F9 File Offset: 0x000079F9
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00009801 File Offset: 0x00007A01
		public object ClientData { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000980A File Offset: 0x00007A0A
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00009812 File Offset: 0x00007A12
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000981B File Offset: 0x00007A1B
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00009823 File Offset: 0x00007A23
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x06000657 RID: 1623 RVA: 0x0000982C File Offset: 0x00007A2C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00009849 File Offset: 0x00007A49
		internal void Set(ref IngestStatCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
