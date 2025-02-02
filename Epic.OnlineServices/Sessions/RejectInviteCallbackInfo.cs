using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000115 RID: 277
	public struct RejectInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0000C23A File Offset: 0x0000A43A
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0000C242 File Offset: 0x0000A442
		public Result ResultCode { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000C24B File Offset: 0x0000A44B
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0000C253 File Offset: 0x0000A453
		public object ClientData { get; set; }

		// Token: 0x06000876 RID: 2166 RVA: 0x0000C25C File Offset: 0x0000A45C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000C279 File Offset: 0x0000A479
		internal void Set(ref RejectInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
