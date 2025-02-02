using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D7 RID: 1239
	public struct QueryEntitlementTokenCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0002F2F7 File Offset: 0x0002D4F7
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x0002F2FF File Offset: 0x0002D4FF
		public Result ResultCode { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0002F308 File Offset: 0x0002D508
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x0002F310 File Offset: 0x0002D510
		public object ClientData { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0002F319 File Offset: 0x0002D519
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0002F321 File Offset: 0x0002D521
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0002F32A File Offset: 0x0002D52A
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0002F332 File Offset: 0x0002D532
		public Utf8String EntitlementToken { get; set; }

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0002F33C File Offset: 0x0002D53C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0002F359 File Offset: 0x0002D559
		internal void Set(ref QueryEntitlementTokenCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementToken = other.EntitlementToken;
		}
	}
}
