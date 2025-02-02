using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000217 RID: 535
	public struct SendPlayerBehaviorReportOptions
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00016405 File Offset: 0x00014605
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0001640D File Offset: 0x0001460D
		public ProductUserId ReporterUserId { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00016416 File Offset: 0x00014616
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0001641E File Offset: 0x0001461E
		public ProductUserId ReportedUserId { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00016427 File Offset: 0x00014627
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0001642F File Offset: 0x0001462F
		public PlayerReportsCategory Category { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00016438 File Offset: 0x00014638
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00016440 File Offset: 0x00014640
		public Utf8String Message { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00016449 File Offset: 0x00014649
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00016451 File Offset: 0x00014651
		public Utf8String Context { get; set; }
	}
}
