using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200057B RID: 1403
	public struct DeletePersistentAuthCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000357DB File Offset: 0x000339DB
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000357E3 File Offset: 0x000339E3
		public Result ResultCode { get; set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000357EC File Offset: 0x000339EC
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000357F4 File Offset: 0x000339F4
		public object ClientData { get; set; }

		// Token: 0x060023F2 RID: 9202 RVA: 0x00035800 File Offset: 0x00033A00
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0003581D File Offset: 0x00033A1D
		internal void Set(ref DeletePersistentAuthCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
