using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000094 RID: 148
	public struct QueryFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000829D File Offset: 0x0000649D
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x000082A5 File Offset: 0x000064A5
		public Result ResultCode { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000082AE File Offset: 0x000064AE
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000082B6 File Offset: 0x000064B6
		public object ClientData { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000082BF File Offset: 0x000064BF
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000082C7 File Offset: 0x000064C7
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06000597 RID: 1431 RVA: 0x000082D0 File Offset: 0x000064D0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000082ED File Offset: 0x000064ED
		internal void Set(ref QueryFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
