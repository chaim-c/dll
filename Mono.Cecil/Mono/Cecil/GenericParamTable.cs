using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000089 RID: 137
	internal sealed class GenericParamTable : MetadataTable<Row<ushort, GenericParameterAttributes, uint, uint>>
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00011FF4 File Offset: 0x000101F4
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16(this.rows[i].Col1);
				buffer.WriteUInt16((ushort)this.rows[i].Col2);
				buffer.WriteCodedRID(this.rows[i].Col3, CodedIndex.TypeOrMethodDef);
				buffer.WriteString(this.rows[i].Col4);
			}
		}
	}
}
