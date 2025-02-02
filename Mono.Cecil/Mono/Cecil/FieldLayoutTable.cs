using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000077 RID: 119
	internal sealed class FieldLayoutTable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32(this.rows[i].Col1);
				buffer.WriteRID(this.rows[i].Col2, Table.Field);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011801 File Offset: 0x0000FA01
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}
	}
}
