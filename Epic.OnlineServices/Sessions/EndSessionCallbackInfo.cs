using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E1 RID: 225
	public struct EndSessionCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0000B455 File Offset: 0x00009655
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0000B45D File Offset: 0x0000965D
		public Result ResultCode { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0000B466 File Offset: 0x00009666
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0000B46E File Offset: 0x0000966E
		public object ClientData { get; set; }

		// Token: 0x06000777 RID: 1911 RVA: 0x0000B478 File Offset: 0x00009678
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000B495 File Offset: 0x00009695
		internal void Set(ref EndSessionCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
