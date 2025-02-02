using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000421 RID: 1057
	public struct QueryLeaderboardUserScoresOptions
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00028516 File Offset: 0x00026716
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0002851E File Offset: 0x0002671E
		public ProductUserId[] UserIds { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00028527 File Offset: 0x00026727
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0002852F File Offset: 0x0002672F
		public UserScoresQueryStatInfo[] StatInfo { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00028538 File Offset: 0x00026738
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x00028540 File Offset: 0x00026740
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00028549 File Offset: 0x00026749
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x00028551 File Offset: 0x00026751
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0002855A File Offset: 0x0002675A
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x00028562 File Offset: 0x00026762
		public ProductUserId LocalUserId { get; set; }
	}
}
