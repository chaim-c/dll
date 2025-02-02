using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000170 RID: 368
	public struct QueryActivePlayerSanctionsOptions
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0000FAF7 File Offset: 0x0000DCF7
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0000FAFF File Offset: 0x0000DCFF
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0000FB08 File Offset: 0x0000DD08
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public ProductUserId LocalUserId { get; set; }
	}
}
