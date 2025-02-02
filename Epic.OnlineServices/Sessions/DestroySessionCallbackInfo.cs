using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DB RID: 219
	public struct DestroySessionCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000B201 File Offset: 0x00009401
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0000B209 File Offset: 0x00009409
		public Result ResultCode { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0000B212 File Offset: 0x00009412
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0000B21A File Offset: 0x0000941A
		public object ClientData { get; set; }

		// Token: 0x0600075C RID: 1884 RVA: 0x0000B224 File Offset: 0x00009424
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000B241 File Offset: 0x00009441
		internal void Set(ref DestroySessionCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
