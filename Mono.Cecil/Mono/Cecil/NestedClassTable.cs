using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000088 RID: 136
	internal sealed class NestedClassTable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00011F88 File Offset: 0x00010188
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.TypeDef);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00011FD6 File Offset: 0x000101D6
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col1, y.Col1);
		}
	}
}
