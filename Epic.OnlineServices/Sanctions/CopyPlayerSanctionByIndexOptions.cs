using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000166 RID: 358
	public struct CopyPlayerSanctionByIndexOptions
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0000F565 File Offset: 0x0000D765
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0000F56D File Offset: 0x0000D76D
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0000F576 File Offset: 0x0000D776
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0000F57E File Offset: 0x0000D77E
		public uint SanctionIndex { get; set; }
	}
}
