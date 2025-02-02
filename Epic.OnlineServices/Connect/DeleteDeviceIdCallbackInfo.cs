using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000528 RID: 1320
	public struct DeleteDeviceIdCallbackInfo : ICallbackInfo
	{
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x00032A5C File Offset: 0x00030C5C
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x00032A64 File Offset: 0x00030C64
		public Result ResultCode { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00032A6D File Offset: 0x00030C6D
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x00032A75 File Offset: 0x00030C75
		public object ClientData { get; set; }

		// Token: 0x060021E3 RID: 8675 RVA: 0x00032A80 File Offset: 0x00030C80
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00032A9D File Offset: 0x00030C9D
		internal void Set(ref DeleteDeviceIdCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
