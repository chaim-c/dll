using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000026 RID: 38
	public struct CopyExternalUserInfoByAccountTypeOptions
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00004B07 File Offset: 0x00002D07
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00004B0F File Offset: 0x00002D0F
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00004B18 File Offset: 0x00002D18
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00004B20 File Offset: 0x00002D20
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00004B29 File Offset: 0x00002D29
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00004B31 File Offset: 0x00002D31
		public ExternalAccountType AccountType { get; set; }
	}
}
