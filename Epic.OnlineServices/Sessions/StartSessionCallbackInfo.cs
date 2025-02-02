using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000158 RID: 344
	public struct StartSessionCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0000ED99 File Offset: 0x0000CF99
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public Result ResultCode { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0000EDAA File Offset: 0x0000CFAA
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0000EDB2 File Offset: 0x0000CFB2
		public object ClientData { get; set; }

		// Token: 0x060009F2 RID: 2546 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		internal void Set(ref StartSessionCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
