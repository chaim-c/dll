using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000071 RID: 113
	public struct ShowBlockPlayerOptions
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00007079 File Offset: 0x00005279
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00007081 File Offset: 0x00005281
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000708A File Offset: 0x0000528A
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00007092 File Offset: 0x00005292
		public EpicAccountId TargetUserId { get; set; }
	}
}
