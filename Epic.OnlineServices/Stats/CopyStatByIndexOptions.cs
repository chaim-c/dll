using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A5 RID: 165
	public struct CopyStatByIndexOptions
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000094B9 File Offset: 0x000076B9
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x000094C1 File Offset: 0x000076C1
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000094CA File Offset: 0x000076CA
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x000094D2 File Offset: 0x000076D2
		public uint StatIndex { get; set; }
	}
}
