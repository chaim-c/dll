using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003FF RID: 1023
	public struct CopyLeaderboardUserScoreByIndexOptions
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0002726D File Offset: 0x0002546D
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00027275 File Offset: 0x00025475
		public uint LeaderboardUserScoreIndex { get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0002727E File Offset: 0x0002547E
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00027286 File Offset: 0x00025486
		public Utf8String StatName { get; set; }
	}
}
