using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000581 RID: 1409
	public struct LinkAccountCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x00035AF4 File Offset: 0x00033CF4
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x00035AFC File Offset: 0x00033CFC
		public Result ResultCode { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x00035B05 File Offset: 0x00033D05
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x00035B0D File Offset: 0x00033D0D
		public object ClientData { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x00035B16 File Offset: 0x00033D16
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x00035B1E File Offset: 0x00033D1E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x00035B27 File Offset: 0x00033D27
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x00035B2F File Offset: 0x00033D2F
		public PinGrantInfo? PinGrantInfo { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00035B38 File Offset: 0x00033D38
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x00035B40 File Offset: 0x00033D40
		public EpicAccountId SelectedAccountId { get; set; }

		// Token: 0x0600241A RID: 9242 RVA: 0x00035B4C File Offset: 0x00033D4C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00035B6C File Offset: 0x00033D6C
		internal void Set(ref LinkAccountCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PinGrantInfo = other.PinGrantInfo;
			this.SelectedAccountId = other.SelectedAccountId;
		}
	}
}
