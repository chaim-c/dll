using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AF RID: 175
	public struct IngestStatOptions
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00009A33 File Offset: 0x00007C33
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00009A3B File Offset: 0x00007C3B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00009A44 File Offset: 0x00007C44
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00009A4C File Offset: 0x00007C4C
		public IngestData[] Stats { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00009A55 File Offset: 0x00007C55
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00009A5D File Offset: 0x00007C5D
		public ProductUserId TargetUserId { get; set; }
	}
}
