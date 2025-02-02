using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003E RID: 62
	public struct QueryUserInfoCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000058F3 File Offset: 0x00003AF3
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x000058FB File Offset: 0x00003AFB
		public Result ResultCode { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00005904 File Offset: 0x00003B04
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000590C File Offset: 0x00003B0C
		public object ClientData { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00005915 File Offset: 0x00003B15
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000591D File Offset: 0x00003B1D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00005926 File Offset: 0x00003B26
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000592E File Offset: 0x00003B2E
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x060003B8 RID: 952 RVA: 0x00005938 File Offset: 0x00003B38
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00005955 File Offset: 0x00003B55
		internal void Set(ref QueryUserInfoCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
