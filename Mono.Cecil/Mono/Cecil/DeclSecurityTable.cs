using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000075 RID: 117
	internal sealed class DeclSecurityTable : SortedTable<Row<SecurityAction, uint, uint>>
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x000116AC File Offset: 0x0000F8AC
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.HasDeclSecurity);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00011710 File Offset: 0x0000F910
		public override int Compare(Row<SecurityAction, uint, uint> x, Row<SecurityAction, uint, uint> y)
		{
			return base.Compare(x.Col2, y.Col2);
		}
	}
}
