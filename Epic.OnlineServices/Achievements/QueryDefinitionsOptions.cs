using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000698 RID: 1688
	public struct QueryDefinitionsOptions
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x0004109D File Offset: 0x0003F29D
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000410A5 File Offset: 0x0003F2A5
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000410AE File Offset: 0x0003F2AE
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000410B6 File Offset: 0x0003F2B6
		public EpicAccountId EpicUserId_DEPRECATED { get; set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000410BF File Offset: 0x0003F2BF
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x000410C7 File Offset: 0x0003F2C7
		public Utf8String[] HiddenAchievementIds_DEPRECATED { get; set; }
	}
}
