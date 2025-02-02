using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E3 RID: 1251
	public struct QueryOwnershipTokenCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0002FC33 File Offset: 0x0002DE33
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x0002FC3B File Offset: 0x0002DE3B
		public Result ResultCode { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0002FC44 File Offset: 0x0002DE44
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		public object ClientData { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0002FC55 File Offset: 0x0002DE55
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0002FC5D File Offset: 0x0002DE5D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0002FC66 File Offset: 0x0002DE66
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x0002FC6E File Offset: 0x0002DE6E
		public Utf8String OwnershipToken { get; set; }

		// Token: 0x06002036 RID: 8246 RVA: 0x0002FC78 File Offset: 0x0002DE78
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0002FC95 File Offset: 0x0002DE95
		internal void Set(ref QueryOwnershipTokenCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.OwnershipToken = other.OwnershipToken;
		}
	}
}
