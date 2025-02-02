using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000083 RID: 131
	internal sealed class AssemblyTable : OneRowTable<Row<AssemblyHashAlgorithm, ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint>>
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public override void Write(TableHeapBuffer buffer)
		{
			buffer.WriteUInt32((uint)this.row.Col1);
			buffer.WriteUInt16(this.row.Col2);
			buffer.WriteUInt16(this.row.Col3);
			buffer.WriteUInt16(this.row.Col4);
			buffer.WriteUInt16(this.row.Col5);
			buffer.WriteUInt32((uint)this.row.Col6);
			buffer.WriteBlob(this.row.Col7);
			buffer.WriteString(this.row.Col8);
			buffer.WriteString(this.row.Col9);
		}
	}
}
