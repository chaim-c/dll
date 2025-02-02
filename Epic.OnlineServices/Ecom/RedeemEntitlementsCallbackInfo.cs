using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E7 RID: 1255
	public struct RedeemEntitlementsCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0002FFA3 File Offset: 0x0002E1A3
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x0002FFAB File Offset: 0x0002E1AB
		public Result ResultCode { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		public object ClientData { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0002FFC5 File Offset: 0x0002E1C5
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x0002FFCD File Offset: 0x0002E1CD
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0002FFD6 File Offset: 0x0002E1D6
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0002FFDE File Offset: 0x0002E1DE
		public uint RedeemedEntitlementIdsCount { get; set; }

		// Token: 0x06002059 RID: 8281 RVA: 0x0002FFE8 File Offset: 0x0002E1E8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00030005 File Offset: 0x0002E205
		internal void Set(ref RedeemEntitlementsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RedeemedEntitlementIdsCount = other.RedeemedEntitlementIdsCount;
		}
	}
}
