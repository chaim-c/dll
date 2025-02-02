using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A5 RID: 933
	public struct LobbySearchRemoveParameterOptions
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00025630 File Offset: 0x00023830
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x00025638 File Offset: 0x00023838
		public Utf8String Key { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00025641 File Offset: 0x00023841
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x00025649 File Offset: 0x00023849
		public ComparisonOp ComparisonOp { get; set; }
	}
}
