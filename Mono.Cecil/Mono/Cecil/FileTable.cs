using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000085 RID: 133
	internal sealed class FileTable : MetadataTable<Row<FileAttributes, uint, uint>>
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32((uint)this.rows[i].Col1);
				buffer.WriteString(this.rows[i].Col2);
				buffer.WriteBlob(this.rows[i].Col3);
			}
		}
	}
}
