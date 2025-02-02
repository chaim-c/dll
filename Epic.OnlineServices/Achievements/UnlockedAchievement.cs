using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020006A0 RID: 1696
	public struct UnlockedAchievement
	{
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x0004148E File Offset: 0x0003F68E
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x00041496 File Offset: 0x0003F696
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0004149F File Offset: 0x0003F69F
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000414A7 File Offset: 0x0003F6A7
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x06002B9D RID: 11165 RVA: 0x000414B0 File Offset: 0x0003F6B0
		internal void Set(ref UnlockedAchievementInternal other)
		{
			this.AchievementId = other.AchievementId;
			this.UnlockTime = other.UnlockTime;
		}
	}
}
