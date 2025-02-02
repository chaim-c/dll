using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000086 RID: 134
	internal sealed class ExportedTypeTable : MetadataTable<Row<TypeAttributes, uint, uint, uint, uint>>
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00011E64 File Offset: 0x00010064
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt32((uint)this.rows[i].Col1);
				buffer.WriteUInt32(this.rows[i].Col2);
				buffer.WriteString(this.rows[i].Col3);
				buffer.WriteString(this.rows[i].Col4);
				buffer.WriteCodedRID(this.rows[i].Col5, CodedIndex.Implementation);
			}
		}
	}
}
