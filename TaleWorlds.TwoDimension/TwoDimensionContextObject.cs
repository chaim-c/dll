using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000037 RID: 55
	public class TwoDimensionContextObject
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000098E7 File Offset: 0x00007AE7
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000098EF File Offset: 0x00007AEF
		public TwoDimensionContext Context { get; private set; }

		// Token: 0x0600027B RID: 635 RVA: 0x000098F8 File Offset: 0x00007AF8
		protected TwoDimensionContextObject(TwoDimensionContext context)
		{
			this.Context = context;
		}
	}
}
