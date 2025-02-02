using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200006F RID: 111
	internal sealed class ParamTable : MetadataTable<Row<ParameterAttributes, ushort, uint>>
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00011408 File Offset: 0x0000F608
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteUInt16((ushort)this.rows[i].Col1);
				buffer.WriteUInt16(this.rows[i].Col2);
				buffer.WriteString(this.rows[i].Col3);
			}
		}
	}
}
