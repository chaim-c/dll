using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000694 RID: 1684
	public struct PlayerAchievement
	{
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00040ABF File Offset: 0x0003ECBF
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x00040AC7 File Offset: 0x0003ECC7
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00040AD0 File Offset: 0x0003ECD0
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x00040AD8 File Offset: 0x0003ECD8
		public double Progress { get; set; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x00040AE1 File Offset: 0x0003ECE1
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x00040AE9 File Offset: 0x0003ECE9
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x00040AF2 File Offset: 0x0003ECF2
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x00040AFA File Offset: 0x0003ECFA
		public PlayerStatInfo[] StatInfo { get; set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x00040B03 File Offset: 0x0003ED03
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x00040B0B File Offset: 0x0003ED0B
		public Utf8String DisplayName { get; set; }

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x00040B14 File Offset: 0x0003ED14
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x00040B1C File Offset: 0x0003ED1C
		public Utf8String Description { get; set; }

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x00040B25 File Offset: 0x0003ED25
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x00040B2D File Offset: 0x0003ED2D
		public Utf8String IconURL { get; set; }

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x00040B36 File Offset: 0x0003ED36
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x00040B3E File Offset: 0x0003ED3E
		public Utf8String FlavorText { get; set; }

		// Token: 0x06002B48 RID: 11080 RVA: 0x00040B48 File Offset: 0x0003ED48
		internal void Set(ref PlayerAchievementInternal other)
		{
			this.AchievementId = other.AchievementId;
			this.Progress = other.Progress;
			this.UnlockTime = other.UnlockTime;
			this.StatInfo = other.StatInfo;
			this.DisplayName = other.DisplayName;
			this.Description = other.Description;
			this.IconURL = other.IconURL;
			this.FlavorText = other.FlavorText;
		}
	}
}
