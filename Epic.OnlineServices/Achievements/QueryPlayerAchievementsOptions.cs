using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069A RID: 1690
	public struct QueryPlayerAchievementsOptions
	{
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000411BF File Offset: 0x0003F3BF
		// (set) Token: 0x06002B7B RID: 11131 RVA: 0x000411C7 File Offset: 0x0003F3C7
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000411D0 File Offset: 0x0003F3D0
		// (set) Token: 0x06002B7D RID: 11133 RVA: 0x000411D8 File Offset: 0x0003F3D8
		public ProductUserId LocalUserId { get; set; }
	}
}
