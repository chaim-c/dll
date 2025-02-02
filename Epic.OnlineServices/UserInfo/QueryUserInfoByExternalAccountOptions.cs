using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003C RID: 60
	public struct QueryUserInfoByExternalAccountOptions
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000057E8 File Offset: 0x000039E8
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x000057F0 File Offset: 0x000039F0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000057F9 File Offset: 0x000039F9
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00005801 File Offset: 0x00003A01
		public Utf8String ExternalAccountId { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000580A File Offset: 0x00003A0A
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00005812 File Offset: 0x00003A12
		public ExternalAccountType AccountType { get; set; }
	}
}
