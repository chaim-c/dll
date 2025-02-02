using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000038 RID: 56
	public struct QueryUserInfoByDisplayNameOptions
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000053CC File Offset: 0x000035CC
		// (set) Token: 0x0600037D RID: 893 RVA: 0x000053D4 File Offset: 0x000035D4
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000053DD File Offset: 0x000035DD
		// (set) Token: 0x0600037F RID: 895 RVA: 0x000053E5 File Offset: 0x000035E5
		public Utf8String DisplayName { get; set; }
	}
}
