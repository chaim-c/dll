using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069E RID: 1694
	public struct UnlockAchievementsOptions
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000413B8 File Offset: 0x0003F5B8
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000413C0 File Offset: 0x0003F5C0
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000413C9 File Offset: 0x0003F5C9
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x000413D1 File Offset: 0x0003F5D1
		public Utf8String[] AchievementIds { get; set; }
	}
}
