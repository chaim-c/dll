using System;
using System.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000069 RID: 105
	internal abstract class SortedTable<TRow> : MetadataTable<TRow>, IComparer<TRow> where TRow : struct
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0001115A File Offset: 0x0000F35A
		public sealed override void Sort()
		{
			Array.Sort<TRow>(this.rows, 0, this.length, this);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001116F File Offset: 0x0000F36F
		protected int Compare(uint x, uint y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x <= y)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000414 RID: 1044
		public abstract int Compare(TRow x, TRow y);
	}
}
