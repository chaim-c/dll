using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000029 RID: 41
	internal abstract class Heap
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000770D File Offset: 0x0000590D
		protected Heap(Section section, uint offset, uint size)
		{
			this.Section = section;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x04000277 RID: 631
		public int IndexSize;

		// Token: 0x04000278 RID: 632
		public readonly Section Section;

		// Token: 0x04000279 RID: 633
		public readonly uint Offset;

		// Token: 0x0400027A RID: 634
		public readonly uint Size;
	}
}
