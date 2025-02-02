using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200051E RID: 1310
	public struct CreateDeviceIdCallbackInfo : ICallbackInfo
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x00032515 File Offset: 0x00030715
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x0003251D File Offset: 0x0003071D
		public Result ResultCode { get; set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x00032526 File Offset: 0x00030726
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0003252E File Offset: 0x0003072E
		public object ClientData { get; set; }

		// Token: 0x060021A8 RID: 8616 RVA: 0x00032538 File Offset: 0x00030738
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00032555 File Offset: 0x00030755
		internal void Set(ref CreateDeviceIdCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
