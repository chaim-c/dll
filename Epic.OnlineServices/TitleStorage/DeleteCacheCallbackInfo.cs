using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007E RID: 126
	public struct DeleteCacheCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00007AFE File Offset: 0x00005CFE
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00007B06 File Offset: 0x00005D06
		public Result ResultCode { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00007B0F File Offset: 0x00005D0F
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00007B17 File Offset: 0x00005D17
		public object ClientData { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00007B20 File Offset: 0x00005D20
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00007B28 File Offset: 0x00005D28
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06000518 RID: 1304 RVA: 0x00007B34 File Offset: 0x00005D34
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00007B51 File Offset: 0x00005D51
		internal void Set(ref DeleteCacheCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
