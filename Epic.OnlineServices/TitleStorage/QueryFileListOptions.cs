using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000098 RID: 152
	public struct QueryFileListOptions
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000869F File Offset: 0x0000689F
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x000086A7 File Offset: 0x000068A7
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000086B0 File Offset: 0x000068B0
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x000086B8 File Offset: 0x000068B8
		public Utf8String[] ListOfTags { get; set; }
	}
}
