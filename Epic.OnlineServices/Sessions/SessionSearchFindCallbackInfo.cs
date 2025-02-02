using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000145 RID: 325
	public struct SessionSearchFindCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0000DE15 File Offset: 0x0000C015
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0000DE1D File Offset: 0x0000C01D
		public Result ResultCode { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0000DE26 File Offset: 0x0000C026
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0000DE2E File Offset: 0x0000C02E
		public object ClientData { get; set; }

		// Token: 0x06000983 RID: 2435 RVA: 0x0000DE38 File Offset: 0x0000C038
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000DE55 File Offset: 0x0000C055
		internal void Set(ref SessionSearchFindCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
