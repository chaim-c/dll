using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007D RID: 125
	internal sealed class MethodSemanticsTable : SortedTable<Row<MethodSemanticsAttributes, uint, uint>>
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x000119DC File Offset: 0x0000FBDC
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteRID(this.rows[i].Col2, Table.Method);
				buffer.WriteCodedRID(this.rows[i].Col3, CodedIndex.HasSemantics);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011A41 File Offset: 0x0000FC41
		public override int Compare(Row<MethodSemanticsAttributes, uint, uint> x, Row<MethodSemanticsAttributes, uint, uint> y)
		{
			return base.Compare(x.Col3, y.Col3);
		}
	}
}
