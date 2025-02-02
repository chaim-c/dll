using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000036 RID: 54
	public struct QueryUserInfoByDisplayNameCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000050E2 File Offset: 0x000032E2
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000050EA File Offset: 0x000032EA
		public Result ResultCode { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000050F3 File Offset: 0x000032F3
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000050FB File Offset: 0x000032FB
		public object ClientData { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00005104 File Offset: 0x00003304
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000510C File Offset: 0x0000330C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00005115 File Offset: 0x00003315
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000511D File Offset: 0x0000331D
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00005126 File Offset: 0x00003326
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000512E File Offset: 0x0000332E
		public Utf8String DisplayName { get; set; }

		// Token: 0x0600036B RID: 875 RVA: 0x00005138 File Offset: 0x00003338
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00005158 File Offset: 0x00003358
		internal void Set(ref QueryUserInfoByDisplayNameCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.DisplayName = other.DisplayName;
		}
	}
}
