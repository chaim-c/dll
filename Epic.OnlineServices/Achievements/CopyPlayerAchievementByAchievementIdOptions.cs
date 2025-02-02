using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200066E RID: 1646
	public struct CopyPlayerAchievementByAchievementIdOptions
	{
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0003F0D5 File Offset: 0x0003D2D5
		// (set) Token: 0x06002A17 RID: 10775 RVA: 0x0003F0DD File Offset: 0x0003D2DD
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x0003F0E6 File Offset: 0x0003D2E6
		// (set) Token: 0x06002A19 RID: 10777 RVA: 0x0003F0EE File Offset: 0x0003D2EE
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x0003F0F7 File Offset: 0x0003D2F7
		// (set) Token: 0x06002A1B RID: 10779 RVA: 0x0003F0FF File Offset: 0x0003D2FF
		public ProductUserId LocalUserId { get; set; }
	}
}
