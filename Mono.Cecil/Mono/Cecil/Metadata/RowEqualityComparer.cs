using System;
using System.Collections.Generic;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200003C RID: 60
	internal sealed class RowEqualityComparer : IEqualityComparer<Row<string, string>>, IEqualityComparer<Row<uint, uint>>, IEqualityComparer<Row<uint, uint, uint>>
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00007F4A File Offset: 0x0000614A
		public bool Equals(Row<string, string> x, Row<string, string> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007F78 File Offset: 0x00006178
		public int GetHashCode(Row<string, string> obj)
		{
			string col = obj.Col1;
			string col2 = obj.Col2;
			return ((col != null) ? col.GetHashCode() : 0) ^ ((col2 != null) ? col2.GetHashCode() : 0);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007FAE File Offset: 0x000061AE
		public bool Equals(Row<uint, uint> x, Row<uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007FD2 File Offset: 0x000061D2
		public int GetHashCode(Row<uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007FE3 File Offset: 0x000061E3
		public bool Equals(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2 && x.Col3 == y.Col3;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008017 File Offset: 0x00006217
		public int GetHashCode(Row<uint, uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2 ^ obj.Col3);
		}
	}
}
