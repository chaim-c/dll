using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000291 RID: 657
	public struct QueryFileOptions
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0001A491 File Offset: 0x00018691
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0001A499 File Offset: 0x00018699
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0001A4A2 File Offset: 0x000186A2
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0001A4AA File Offset: 0x000186AA
		public Utf8String Filename { get; set; }
	}
}
