using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007C RID: 124
	internal sealed class PropertyTable : MetadataTable<Row<PropertyAttributes, uint, uint>>
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00011970 File Offset: 0x0000FB70
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteString(this.rows[i].Col2);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}
	}
}
