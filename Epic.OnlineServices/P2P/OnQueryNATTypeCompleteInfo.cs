using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D3 RID: 723
	public struct OnQueryNATTypeCompleteInfo : ICallbackInfo
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0001C8A7 File Offset: 0x0001AAA7
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0001C8AF File Offset: 0x0001AAAF
		public Result ResultCode { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public object ClientData { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0001C8C9 File Offset: 0x0001AAC9
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0001C8D1 File Offset: 0x0001AAD1
		public NATType NATType { get; set; }

		// Token: 0x0600136C RID: 4972 RVA: 0x0001C8DC File Offset: 0x0001AADC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0001C8F9 File Offset: 0x0001AAF9
		internal void Set(ref OnQueryNATTypeCompleteInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.NATType = other.NATType;
		}
	}
}
