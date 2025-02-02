using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200028B RID: 651
	public struct QueryFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0001A011 File Offset: 0x00018211
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0001A019 File Offset: 0x00018219
		public Result ResultCode { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0001A022 File Offset: 0x00018222
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x0001A02A File Offset: 0x0001822A
		public object ClientData { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0001A033 File Offset: 0x00018233
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x0001A03B File Offset: 0x0001823B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060011AE RID: 4526 RVA: 0x0001A044 File Offset: 0x00018244
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0001A061 File Offset: 0x00018261
		internal void Set(ref QueryFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
