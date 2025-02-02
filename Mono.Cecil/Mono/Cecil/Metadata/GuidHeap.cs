using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000034 RID: 52
	internal sealed class GuidHeap : Heap
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00007D0F File Offset: 0x00005F0F
		public GuidHeap(Section section, uint start, uint size) : base(section, start, size)
		{
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007D1C File Offset: 0x00005F1C
		public Guid Read(uint index)
		{
			if (index == 0U)
			{
				return default(Guid);
			}
			byte[] array = new byte[16];
			index -= 1U;
			Buffer.BlockCopy(this.Section.Data, (int)(this.Offset + index), array, 0, 16);
			return new Guid(array);
		}
	}
}
