using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000672 RID: 1650
	public struct CopyUnlockedAchievementByAchievementIdOptions
	{
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		// (set) Token: 0x06002A2F RID: 10799 RVA: 0x0003F307 File Offset: 0x0003D507
		public ProductUserId UserId { get; set; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x0003F310 File Offset: 0x0003D510
		// (set) Token: 0x06002A31 RID: 10801 RVA: 0x0003F318 File Offset: 0x0003D518
		public Utf8String AchievementId { get; set; }
	}
}
