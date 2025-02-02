using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004F RID: 79
	public struct HideFriendsCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000064DC File Offset: 0x000046DC
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x000064E4 File Offset: 0x000046E4
		public Result ResultCode { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000064ED File Offset: 0x000046ED
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000064F5 File Offset: 0x000046F5
		public object ClientData { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000064FE File Offset: 0x000046FE
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00006506 File Offset: 0x00004706
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x0600041B RID: 1051 RVA: 0x00006510 File Offset: 0x00004710
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000652D File Offset: 0x0000472D
		internal void Set(ref HideFriendsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
