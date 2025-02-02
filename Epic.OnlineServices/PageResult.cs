using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200001E RID: 30
	public struct PageResult
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000046D0 File Offset: 0x000028D0
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x000046D8 File Offset: 0x000028D8
		public int StartIndex { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000046E1 File Offset: 0x000028E1
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000046E9 File Offset: 0x000028E9
		public int Count { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000046F2 File Offset: 0x000028F2
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000046FA File Offset: 0x000028FA
		public int TotalCount { get; set; }

		// Token: 0x060002E6 RID: 742 RVA: 0x00004703 File Offset: 0x00002903
		internal void Set(ref PageResultInternal other)
		{
			this.StartIndex = other.StartIndex;
			this.Count = other.Count;
			this.TotalCount = other.TotalCount;
		}
	}
}
