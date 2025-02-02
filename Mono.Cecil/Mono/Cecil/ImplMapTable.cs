using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000081 RID: 129
	internal sealed class ImplMapTable : SortedTable<Row<PInvokeAttributes, uint, uint, uint>>
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00011B38 File Offset: 0x0000FD38
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.MemberForwarded);
				buffer.WriteString(this.rows[i].Col3);
				buffer.WriteRID(this.rows[i].Col4, Table.ModuleRef);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011BB5 File Offset: 0x0000FDB5
		public override int Compare(Row<PInvokeAttributes, uint, uint, uint> x, Row<PInvokeAttributes, uint, uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}
	}
}
