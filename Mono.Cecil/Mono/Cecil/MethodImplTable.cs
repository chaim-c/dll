using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007E RID: 126
	internal sealed class MethodImplTable : MetadataTable<Row<uint, uint, uint>>
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00011A60 File Offset: 0x0000FC60
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.MethodDefOrRef);
				buffer.WriteCodedRID(this.rows[i].Col3, CodedIndex.MethodDefOrRef);
			}
		}
	}
}
