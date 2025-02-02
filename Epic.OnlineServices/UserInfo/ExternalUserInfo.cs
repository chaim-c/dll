using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002C RID: 44
	public struct ExternalUserInfo
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00004DEE File Offset: 0x00002FEE
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00004DF6 File Offset: 0x00002FF6
		public ExternalAccountType AccountType { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00004DFF File Offset: 0x00002FFF
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00004E07 File Offset: 0x00003007
		public Utf8String AccountId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00004E10 File Offset: 0x00003010
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00004E18 File Offset: 0x00003018
		public Utf8String DisplayName { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00004E21 File Offset: 0x00003021
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00004E29 File Offset: 0x00003029
		public Utf8String DisplayNameSanitized { get; set; }

		// Token: 0x06000333 RID: 819 RVA: 0x00004E32 File Offset: 0x00003032
		internal void Set(ref ExternalUserInfoInternal other)
		{
			this.AccountType = other.AccountType;
			this.AccountId = other.AccountId;
			this.DisplayName = other.DisplayName;
			this.DisplayNameSanitized = other.DisplayNameSanitized;
		}
	}
}
