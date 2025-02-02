using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000267 RID: 615
	public struct DeleteFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00018E19 File Offset: 0x00017019
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00018E21 File Offset: 0x00017021
		public Result ResultCode { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00018E2A File Offset: 0x0001702A
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00018E32 File Offset: 0x00017032
		public object ClientData { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00018E3B File Offset: 0x0001703B
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00018E43 File Offset: 0x00017043
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060010CD RID: 4301 RVA: 0x00018E4C File Offset: 0x0001704C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00018E69 File Offset: 0x00017069
		internal void Set(ref DeleteFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
