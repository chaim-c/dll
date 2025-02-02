using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200006A RID: 106
	internal sealed class ModuleTable : OneRowTable<uint>
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00011186 File Offset: 0x0000F386
		public override void Write(TableHeapBuffer buffer)
		{
			buffer.WriteUInt16(0);
			buffer.WriteString(this.row);
			buffer.WriteUInt16(1);
			buffer.WriteUInt16(0);
			buffer.WriteUInt16(0);
		}
	}
}
