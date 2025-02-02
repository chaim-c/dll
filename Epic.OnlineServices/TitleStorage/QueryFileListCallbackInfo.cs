using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000096 RID: 150
	public struct QueryFileListCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000846A File Offset: 0x0000666A
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00008472 File Offset: 0x00006672
		public Result ResultCode { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000847B File Offset: 0x0000667B
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00008483 File Offset: 0x00006683
		public object ClientData { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000848C File Offset: 0x0000668C
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00008494 File Offset: 0x00006694
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000849D File Offset: 0x0000669D
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x000084A5 File Offset: 0x000066A5
		public uint FileCount { get; set; }

		// Token: 0x060005AC RID: 1452 RVA: 0x000084B0 File Offset: 0x000066B0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000084CD File Offset: 0x000066CD
		internal void Set(ref QueryFileListCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.FileCount = other.FileCount;
		}
	}
}
