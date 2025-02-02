using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DF RID: 1247
	public struct QueryOwnershipCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x0002F8B2 File Offset: 0x0002DAB2
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x0002F8BA File Offset: 0x0002DABA
		public Result ResultCode { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x0002F8C3 File Offset: 0x0002DAC3
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x0002F8CB File Offset: 0x0002DACB
		public object ClientData { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x0002F8DC File Offset: 0x0002DADC
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x0002F8E5 File Offset: 0x0002DAE5
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x0002F8ED File Offset: 0x0002DAED
		public ItemOwnership[] ItemOwnership { get; set; }

		// Token: 0x06002013 RID: 8211 RVA: 0x0002F8F8 File Offset: 0x0002DAF8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0002F915 File Offset: 0x0002DB15
		internal void Set(ref QueryOwnershipCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ItemOwnership = other.ItemOwnership;
		}
	}
}
