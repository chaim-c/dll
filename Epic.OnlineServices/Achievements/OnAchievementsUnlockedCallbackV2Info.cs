using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000686 RID: 1670
	public struct OnAchievementsUnlockedCallbackV2Info : ICallbackInfo
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0004030A File Offset: 0x0003E50A
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x00040312 File Offset: 0x0003E512
		public object ClientData { get; set; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0004031B File Offset: 0x0003E51B
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x00040323 File Offset: 0x0003E523
		public ProductUserId UserId { get; set; }

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0004032C File Offset: 0x0003E52C
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x00040334 File Offset: 0x0003E534
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0004033D File Offset: 0x0003E53D
		// (set) Token: 0x06002AD7 RID: 10967 RVA: 0x00040345 File Offset: 0x0003E545
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00040350 File Offset: 0x0003E550
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0004036B File Offset: 0x0003E56B
		internal void Set(ref OnAchievementsUnlockedCallbackV2InfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementId = other.AchievementId;
			this.UnlockTime = other.UnlockTime;
		}
	}
}
