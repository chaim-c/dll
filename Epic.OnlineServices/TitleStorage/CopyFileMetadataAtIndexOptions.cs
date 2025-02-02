using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007A RID: 122
	public struct CopyFileMetadataAtIndexOptions
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00007977 File Offset: 0x00005B77
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0000797F File Offset: 0x00005B7F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00007988 File Offset: 0x00005B88
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00007990 File Offset: 0x00005B90
		public uint Index { get; set; }
	}
}
