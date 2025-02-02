using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000078 RID: 120
	internal sealed class StandAloneSigTable : MetadataTable<uint>
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00011820 File Offset: 0x0000FA20
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < this.length; i++)
			{
				buffer.WriteBlob(this.rows[i]);
			}
		}
	}
}
