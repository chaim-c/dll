using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200008B RID: 139
	internal sealed class GenericParamConstraintTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000120D0 File Offset: 0x000102D0
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.GenericParam);
				buffer.WriteCodedRID(this.rows[i].Col2, CodedIndex.TypeDefOrRef);
			}
		}
	}
}
