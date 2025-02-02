using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000071 RID: 113
	internal sealed class MemberRefTable : MetadataTable<Row<uint, uint, uint>>
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000114CC File Offset: 0x0000F6CC
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.MemberRefParent);
				buffer.WriteString(this.rows[i].Col2);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}
	}
}
