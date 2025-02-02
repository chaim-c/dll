using System;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x02000009 RID: 9
	public class GOGAchievement
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030AC File Offset: 0x000012AC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030B4 File Offset: 0x000012B4
		public string AchievementName { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030BD File Offset: 0x000012BD
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000030C5 File Offset: 0x000012C5
		public string Name { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000030CE File Offset: 0x000012CE
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000030D6 File Offset: 0x000012D6
		public string Description { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030DF File Offset: 0x000012DF
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000030E7 File Offset: 0x000012E7
		public bool Achieved { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000030F0 File Offset: 0x000012F0
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000030F8 File Offset: 0x000012F8
		public int Progress { get; set; }

		// Token: 0x04000019 RID: 25
		public int AchievementID;
	}
}
