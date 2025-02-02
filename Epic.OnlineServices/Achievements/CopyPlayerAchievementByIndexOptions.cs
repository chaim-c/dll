using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000670 RID: 1648
	public struct CopyPlayerAchievementByIndexOptions
	{
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x0003F1F3 File Offset: 0x0003D3F3
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x0003F1FB File Offset: 0x0003D3FB
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x0003F204 File Offset: 0x0003D404
		// (set) Token: 0x06002A25 RID: 10789 RVA: 0x0003F20C File Offset: 0x0003D40C
		public uint AchievementIndex { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x0003F215 File Offset: 0x0003D415
		// (set) Token: 0x06002A27 RID: 10791 RVA: 0x0003F21D File Offset: 0x0003D41D
		public ProductUserId LocalUserId { get; set; }
	}
}
