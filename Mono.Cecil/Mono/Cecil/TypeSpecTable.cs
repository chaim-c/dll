using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000080 RID: 128
	internal sealed class TypeSpecTable : MetadataTable<uint>
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00011B04 File Offset: 0x0000FD04
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
