using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009A RID: 154
	public struct QueryFileOptions
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00008772 File Offset: 0x00006972
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000877A File Offset: 0x0000697A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00008783 File Offset: 0x00006983
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000878B File Offset: 0x0000698B
		public Utf8String Filename { get; set; }
	}
}
