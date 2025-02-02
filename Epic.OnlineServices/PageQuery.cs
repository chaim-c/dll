using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200001C RID: 28
	public struct PageQuery
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000045C3 File Offset: 0x000027C3
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000045CB File Offset: 0x000027CB
		public int StartIndex { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000045D4 File Offset: 0x000027D4
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x000045DC File Offset: 0x000027DC
		public int MaxCount { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x000045E5 File Offset: 0x000027E5
		internal void Set(ref PageQueryInternal other)
		{
			this.StartIndex = other.StartIndex;
			this.MaxCount = other.MaxCount;
		}
	}
}
