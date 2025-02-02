using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000073 RID: 115
	internal sealed class CustomAttributeTable : SortedTable<Row<uint, uint, uint>>
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x000115BC File Offset: 0x0000F7BC
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.HasCustomAttribute);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.CustomAttributeType);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011622 File Offset: 0x0000F822
		public override int Compare(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return base.Compare(x.Col1, y.Col1);
		}
	}
}
