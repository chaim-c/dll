using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000678 RID: 1656
	public struct DefinitionV2
	{
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x0003FA78 File Offset: 0x0003DC78
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x0003FA80 File Offset: 0x0003DC80
		public Utf8String AchievementId { get; set; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x0003FA89 File Offset: 0x0003DC89
		// (set) Token: 0x06002A74 RID: 10868 RVA: 0x0003FA91 File Offset: 0x0003DC91
		public Utf8String UnlockedDisplayName { get; set; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x0003FA9A File Offset: 0x0003DC9A
		// (set) Token: 0x06002A76 RID: 10870 RVA: 0x0003FAA2 File Offset: 0x0003DCA2
		public Utf8String UnlockedDescription { get; set; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x0003FAAB File Offset: 0x0003DCAB
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x0003FAB3 File Offset: 0x0003DCB3
		public Utf8String LockedDisplayName { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x0003FABC File Offset: 0x0003DCBC
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x0003FAC4 File Offset: 0x0003DCC4
		public Utf8String LockedDescription { get; set; }

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x0003FACD File Offset: 0x0003DCCD
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x0003FAD5 File Offset: 0x0003DCD5
		public Utf8String FlavorText { get; set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x0003FADE File Offset: 0x0003DCDE
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x0003FAE6 File Offset: 0x0003DCE6
		public Utf8String UnlockedIconURL { get; set; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x0003FAEF File Offset: 0x0003DCEF
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x0003FAF7 File Offset: 0x0003DCF7
		public Utf8String LockedIconURL { get; set; }

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0003FB00 File Offset: 0x0003DD00
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0003FB08 File Offset: 0x0003DD08
		public bool IsHidden { get; set; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0003FB11 File Offset: 0x0003DD11
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0003FB19 File Offset: 0x0003DD19
		public StatThresholds[] StatThresholds { get; set; }

		// Token: 0x06002A85 RID: 10885 RVA: 0x0003FB24 File Offset: 0x0003DD24
		internal void Set(ref DefinitionV2Internal other)
		{
			this.AchievementId = other.AchievementId;
			this.UnlockedDisplayName = other.UnlockedDisplayName;
			this.UnlockedDescription = other.UnlockedDescription;
			this.LockedDisplayName = other.LockedDisplayName;
			this.LockedDescription = other.LockedDescription;
			this.FlavorText = other.FlavorText;
			this.UnlockedIconURL = other.UnlockedIconURL;
			this.LockedIconURL = other.LockedIconURL;
			this.IsHidden = other.IsHidden;
			this.StatThresholds = other.StatThresholds;
		}
	}
}
