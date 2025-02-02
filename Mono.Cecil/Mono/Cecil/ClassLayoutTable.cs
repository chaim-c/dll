using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000076 RID: 118
	internal sealed class ClassLayoutTable : SortedTable<Row<ushort, uint, uint>>
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00011730 File Offset: 0x0000F930
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16(this.rows[i].Col1);
				buffer.WriteUInt32(this.rows[i].Col2);
				buffer.WriteRID(this.rows[i].Col3, Table.TypeDef);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011794 File Offset: 0x0000F994
		public override int Compare(Row<ushort, uint, uint> x, Row<ushort, uint, uint> y)
		{
			return base.Compare(x.Col3, y.Col3);
		}
	}
}
