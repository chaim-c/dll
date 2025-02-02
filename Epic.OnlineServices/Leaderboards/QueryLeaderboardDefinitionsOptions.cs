using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041D RID: 1053
	public struct QueryLeaderboardDefinitionsOptions
	{
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00028341 File Offset: 0x00026541
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x00028349 File Offset: 0x00026549
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00028352 File Offset: 0x00026552
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0002835A File Offset: 0x0002655A
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00028363 File Offset: 0x00026563
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0002836B File Offset: 0x0002656B
		public ProductUserId LocalUserId { get; set; }
	}
}
