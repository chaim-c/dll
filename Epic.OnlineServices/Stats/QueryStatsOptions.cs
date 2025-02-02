using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B7 RID: 183
	public struct QueryStatsOptions
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00009DA3 File Offset: 0x00007FA3
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00009DAB File Offset: 0x00007FAB
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00009DB4 File Offset: 0x00007FB4
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00009DBC File Offset: 0x00007FBC
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00009DC5 File Offset: 0x00007FC5
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00009DCD File Offset: 0x00007FCD
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00009DD6 File Offset: 0x00007FD6
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00009DDE File Offset: 0x00007FDE
		public Utf8String[] StatNames { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00009DE7 File Offset: 0x00007FE7
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00009DEF File Offset: 0x00007FEF
		public ProductUserId TargetUserId { get; set; }
	}
}
