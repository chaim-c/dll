using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x0200007F RID: 127
	internal sealed class ModuleRefTable : MetadataTable<uint>
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteString(this.rows[i]);
			}
		}
	}
}
