using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000676 RID: 1654
	public struct Definition
	{
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x0003F48A File Offset: 0x0003D68A
		// (set) Token: 0x06002A41 RID: 10817 RVA: 0x0003F492 File Offset: 0x0003D692
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x0003F49B File Offset: 0x0003D69B
		// (set) Token: 0x06002A43 RID: 10819 RVA: 0x0003F4A3 File Offset: 0x0003D6A3
		public Utf8String DisplayName { get; set; }

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0003F4AC File Offset: 0x0003D6AC
		// (set) Token: 0x06002A45 RID: 10821 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		public Utf8String Description { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x0003F4BD File Offset: 0x0003D6BD
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x0003F4C5 File Offset: 0x0003D6C5
		public Utf8String LockedDisplayName { get; set; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x0003F4CE File Offset: 0x0003D6CE
		// (set) Token: 0x06002A49 RID: 10825 RVA: 0x0003F4D6 File Offset: 0x0003D6D6
		public Utf8String LockedDescription { get; set; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x0003F4DF File Offset: 0x0003D6DF
		// (set) Token: 0x06002A4B RID: 10827 RVA: 0x0003F4E7 File Offset: 0x0003D6E7
		public Utf8String HiddenDescription { get; set; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x0003F4F0 File Offset: 0x0003D6F0
		// (set) Token: 0x06002A4D RID: 10829 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public Utf8String CompletionDescription { get; set; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0003F501 File Offset: 0x0003D701
		// (set) Token: 0x06002A4F RID: 10831 RVA: 0x0003F509 File Offset: 0x0003D709
		public Utf8String UnlockedIconId { get; set; }

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x0003F512 File Offset: 0x0003D712
		// (set) Token: 0x06002A51 RID: 10833 RVA: 0x0003F51A File Offset: 0x0003D71A
		public Utf8String LockedIconId { get; set; }

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x0003F523 File Offset: 0x0003D723
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x0003F52B File Offset: 0x0003D72B
		public bool IsHidden { get; set; }

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x0003F534 File Offset: 0x0003D734
		// (set) Token: 0x06002A55 RID: 10837 RVA: 0x0003F53C File Offset: 0x0003D73C
		public StatThresholds[] StatThresholds { get; set; }

		// Token: 0x06002A56 RID: 10838 RVA: 0x0003F548 File Offset: 0x0003D748
		internal void Set(ref DefinitionInternal other)
		{
			this.AchievementId = other.AchievementId;
			this.DisplayName = other.DisplayName;
			this.Description = other.Description;
			this.LockedDisplayName = other.LockedDisplayName;
			this.LockedDescription = other.LockedDescription;
			this.HiddenDescription = other.HiddenDescription;
			this.CompletionDescription = other.CompletionDescription;
			this.UnlockedIconId = other.UnlockedIconId;
			this.LockedIconId = other.LockedIconId;
			this.IsHidden = other.IsHidden;
			this.StatThresholds = other.StatThresholds;
		}
	}
}
