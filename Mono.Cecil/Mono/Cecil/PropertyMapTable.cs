using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007B RID: 123
	internal sealed class PropertyMapTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00011918 File Offset: 0x0000FB18
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.Property);
			}
		}
	}
}
