using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000082 RID: 130
	internal sealed class FieldRVATable : SortedTable<Row<uint, uint>>
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public override void Write(TableHeapBuffer buffer)
		{
			this.position = buffer.position;
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32(this.rows[i].Col1);
				buffer.WriteRID(this.rows[i].Col2, Table.Field);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011C2D File Offset: 0x0000FE2D
		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}

		// Token: 0x040003B0 RID: 944
		internal int position;
	}
}
