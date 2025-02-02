using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000079 RID: 121
	internal sealed class EventMapTable : MetadataTable<Row<uint, uint>>
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x00011854 File Offset: 0x0000FA54
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteRID(this.rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(this.rows[i].Col2, Table.Event);
			}
		}
	}
}
