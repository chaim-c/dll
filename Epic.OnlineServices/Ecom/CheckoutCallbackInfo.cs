using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048A RID: 1162
	public struct CheckoutCallbackInfo : ICallbackInfo
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x0002C9C1 File Offset: 0x0002ABC1
		public Result ResultCode { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0002C9CA File Offset: 0x0002ABCA
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x0002C9D2 File Offset: 0x0002ABD2
		public object ClientData { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0002C9DB File Offset: 0x0002ABDB
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x0002C9E3 File Offset: 0x0002ABE3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		public Utf8String TransactionId { get; set; }

		// Token: 0x06001E26 RID: 7718 RVA: 0x0002CA00 File Offset: 0x0002AC00
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0002CA1D File Offset: 0x0002AC1D
		internal void Set(ref CheckoutCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TransactionId = other.TransactionId;
		}
	}
}
