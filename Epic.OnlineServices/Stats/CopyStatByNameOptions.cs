using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A7 RID: 167
	public struct CopyStatByNameOptions
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00009576 File Offset: 0x00007776
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0000957E File Offset: 0x0000777E
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00009587 File Offset: 0x00007787
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000958F File Offset: 0x0000778F
		public Utf8String Name { get; set; }
	}
}
