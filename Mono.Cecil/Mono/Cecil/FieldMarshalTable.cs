using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000074 RID: 116
	internal sealed class FieldMarshalTable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00011640 File Offset: 0x0000F840
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.HasFieldMarshal);
				buffer.WriteBlob(this.rows[i].Col2);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001168D File Offset: 0x0000F88D
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col1, y.Col1);
		}
	}
}
