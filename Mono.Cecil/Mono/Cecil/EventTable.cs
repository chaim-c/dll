using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007A RID: 122
	internal sealed class EventTable : MetadataTable<Row<EventAttributes, uint, uint>>
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x000118AC File Offset: 0x0000FAAC
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteString(this.rows[i].Col2);
				buffer.WriteCodedRID(this.rows[i].Col3, CodedIndex.TypeDefOrRef);
			}
		}
	}
}
