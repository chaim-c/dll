using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000070 RID: 112
	internal sealed class InterfaceImplTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00011474 File Offset: 0x0000F674
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.TypeDefOrRef);
			}
		}
	}
}
