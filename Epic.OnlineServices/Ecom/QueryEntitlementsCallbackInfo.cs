using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D3 RID: 1235
	public struct QueryEntitlementsCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0002F012 File Offset: 0x0002D212
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x0002F01A File Offset: 0x0002D21A
		public Result ResultCode { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0002F023 File Offset: 0x0002D223
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x0002F02B File Offset: 0x0002D22B
		public object ClientData { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0002F034 File Offset: 0x0002D234
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0002F03C File Offset: 0x0002D23C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0002F048 File Offset: 0x0002D248
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0002F065 File Offset: 0x0002D265
		internal void Set(ref QueryEntitlementsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
