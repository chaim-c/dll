using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000072 RID: 114
	internal sealed class ConstantTable : SortedTable<Row<ElementType, uint, uint>>
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00011538 File Offset: 0x0000F738
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.HasConstant);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001159C File Offset: 0x0000F79C
		public override int Compare(Row<ElementType, uint, uint> x, Row<ElementType, uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}
	}
}
