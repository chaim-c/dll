using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DB RID: 1243
	public struct QueryOffersCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0002F616 File Offset: 0x0002D816
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x0002F61E File Offset: 0x0002D81E
		public Result ResultCode { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0002F627 File Offset: 0x0002D827
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x0002F62F File Offset: 0x0002D82F
		public object ClientData { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0002F638 File Offset: 0x0002D838
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x0002F640 File Offset: 0x0002D840
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0002F64C File Offset: 0x0002D84C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0002F669 File Offset: 0x0002D869
		internal void Set(ref QueryOffersCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
