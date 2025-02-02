using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200006B RID: 107
	internal sealed class TypeRefTable : MetadataTable<Row<uint, uint, uint>>
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteCodedRID(this.rows[i].Col1, CodedIndex.ResolutionScope);
				buffer.WriteString(this.rows[i].Col2);
				buffer.WriteString(this.rows[i].Col3);
			}
		}
	}
}
