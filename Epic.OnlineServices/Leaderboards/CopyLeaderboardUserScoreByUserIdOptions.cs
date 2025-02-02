using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000401 RID: 1025
	public struct CopyLeaderboardUserScoreByUserIdOptions
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0002732A File Offset: 0x0002552A
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00027332 File Offset: 0x00025532
		public ProductUserId UserId { get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0002733B File Offset: 0x0002553B
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00027343 File Offset: 0x00025543
		public Utf8String StatName { get; set; }
	}
}
