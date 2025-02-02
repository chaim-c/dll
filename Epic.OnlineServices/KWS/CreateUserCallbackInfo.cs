using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000429 RID: 1065
	public struct CreateUserCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000288E2 File Offset: 0x00026AE2
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x000288EA File Offset: 0x00026AEA
		public Result ResultCode { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000288F3 File Offset: 0x00026AF3
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x000288FB File Offset: 0x00026AFB
		public object ClientData { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00028904 File Offset: 0x00026B04
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0002890C File Offset: 0x00026B0C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00028915 File Offset: 0x00026B15
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0002891D File Offset: 0x00026B1D
		public Utf8String KWSUserId { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00028926 File Offset: 0x00026B26
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0002892E File Offset: 0x00026B2E
		public bool IsMinor { get; set; }

		// Token: 0x06001B6D RID: 7021 RVA: 0x00028938 File Offset: 0x00026B38
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00028958 File Offset: 0x00026B58
		internal void Set(ref CreateUserCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.KWSUserId = other.KWSUserId;
			this.IsMinor = other.IsMinor;
		}
	}
}
