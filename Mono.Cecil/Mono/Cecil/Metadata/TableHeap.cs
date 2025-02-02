using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000040 RID: 64
	internal sealed class TableHeap : Heap
	{
		// Token: 0x17000058 RID: 88
		public TableInformation this[Table table]
		{
			get
			{
				return this.Tables[(int)table];
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000080FD File Offset: 0x000062FD
		public TableHeap(Section section, uint start, uint size) : base(section, start, size)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008115 File Offset: 0x00006315
		public bool HasTable(Table table)
		{
			return (this.Valid & 1L << (int)table) != 0L;
		}

		// Token: 0x0400030A RID: 778
		public const int TableCount = 45;

		// Token: 0x0400030B RID: 779
		public long Valid;

		// Token: 0x0400030C RID: 780
		public long Sorted;

		// Token: 0x0400030D RID: 781
		public readonly TableInformation[] Tables = new TableInformation[45];
	}
}
