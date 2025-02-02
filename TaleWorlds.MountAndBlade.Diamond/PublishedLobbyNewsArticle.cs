using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200015A RID: 346
	public class PublishedLobbyNewsArticle
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0000E388 File Offset: 0x0000C588
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0000E390 File Offset: 0x0000C590
		public string Title { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0000E399 File Offset: 0x0000C599
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
		public int Type { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0000E3AA File Offset: 0x0000C5AA
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0000E3B2 File Offset: 0x0000C5B2
		public string Description { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0000E3C3 File Offset: 0x0000C5C3
		public string DateStart { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		public string DateEnd { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000E3DD File Offset: 0x0000C5DD
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		public bool Pinned { get; set; }
	}
}
