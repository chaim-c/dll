using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014D RID: 333
	public struct SessionSearchRemoveParameterOptions
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0000E018 File Offset: 0x0000C218
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0000E020 File Offset: 0x0000C220
		public Utf8String Key { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0000E029 File Offset: 0x0000C229
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0000E031 File Offset: 0x0000C231
		public ComparisonOp ComparisonOp { get; set; }
	}
}
