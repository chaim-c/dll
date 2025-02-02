using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000ED RID: 237
	public struct JoinSessionCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000BA02 File Offset: 0x00009C02
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0000BA0A File Offset: 0x00009C0A
		public Result ResultCode { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000BA13 File Offset: 0x00009C13
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0000BA1B File Offset: 0x00009C1B
		public object ClientData { get; set; }

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000BA24 File Offset: 0x00009C24
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000BA41 File Offset: 0x00009C41
		internal void Set(ref JoinSessionCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
