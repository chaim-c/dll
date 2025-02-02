using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000674 RID: 1652
	public struct CopyUnlockedAchievementByIndexOptions
	{
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x0003F3CE File Offset: 0x0003D5CE
		// (set) Token: 0x06002A38 RID: 10808 RVA: 0x0003F3D6 File Offset: 0x0003D5D6
		public ProductUserId UserId { get; set; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x0003F3DF File Offset: 0x0003D5DF
		// (set) Token: 0x06002A3A RID: 10810 RVA: 0x0003F3E7 File Offset: 0x0003D5E7
		public uint AchievementIndex { get; set; }
	}
}
