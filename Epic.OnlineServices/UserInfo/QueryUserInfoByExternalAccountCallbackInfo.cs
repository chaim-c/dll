using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003A RID: 58
	public struct QueryUserInfoByExternalAccountCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000549A File Offset: 0x0000369A
		// (set) Token: 0x06000386 RID: 902 RVA: 0x000054A2 File Offset: 0x000036A2
		public Result ResultCode { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000054AB File Offset: 0x000036AB
		// (set) Token: 0x06000388 RID: 904 RVA: 0x000054B3 File Offset: 0x000036B3
		public object ClientData { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x0600038A RID: 906 RVA: 0x000054C4 File Offset: 0x000036C4
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000054CD File Offset: 0x000036CD
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000054D5 File Offset: 0x000036D5
		public Utf8String ExternalAccountId { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000054DE File Offset: 0x000036DE
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000054E6 File Offset: 0x000036E6
		public ExternalAccountType AccountType { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000054EF File Offset: 0x000036EF
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000054F7 File Offset: 0x000036F7
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06000391 RID: 913 RVA: 0x00005500 File Offset: 0x00003700
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00005520 File Offset: 0x00003720
		internal void Set(ref QueryUserInfoByExternalAccountCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ExternalAccountId = other.ExternalAccountId;
			this.AccountType = other.AccountType;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
