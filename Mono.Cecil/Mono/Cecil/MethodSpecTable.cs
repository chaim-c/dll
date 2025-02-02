using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200008A RID: 138
	internal sealed class MethodSpecTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00012078 File Offset: 0x00010278
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.MethodDefOrRef);
				buffer.WriteBlob(this.rows[i].Col2);
			}
		}
	}
}
