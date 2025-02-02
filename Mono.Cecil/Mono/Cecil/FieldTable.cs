using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200006D RID: 109
	internal sealed class FieldTable : MetadataTable<Row<FieldAttributes, uint, uint>>
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000112E4 File Offset: 0x0000F4E4
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
