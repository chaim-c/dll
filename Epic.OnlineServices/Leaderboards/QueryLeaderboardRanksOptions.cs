using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041F RID: 1055
	public struct QueryLeaderboardRanksOptions
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00028447 File Offset: 0x00026647
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x0002844F File Offset: 0x0002664F
		public Utf8String LeaderboardId { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00028458 File Offset: 0x00026658
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x00028460 File Offset: 0x00026660
		public ProductUserId LocalUserId { get; set; }
	}
}
