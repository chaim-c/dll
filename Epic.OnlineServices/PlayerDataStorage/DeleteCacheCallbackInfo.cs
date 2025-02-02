using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000263 RID: 611
	public struct DeleteCacheCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x00018BCA File Offset: 0x00016DCA
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x00018BD2 File Offset: 0x00016DD2
		public Result ResultCode { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00018BDB File Offset: 0x00016DDB
		// (set) Token: 0x060010B1 RID: 4273 RVA: 0x00018BE3 File Offset: 0x00016DE3
		public object ClientData { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00018BEC File Offset: 0x00016DEC
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060010B4 RID: 4276 RVA: 0x00018C00 File Offset: 0x00016E00
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00018C1D File Offset: 0x00016E1D
		internal void Set(ref DeleteCacheCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
