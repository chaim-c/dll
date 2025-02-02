using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002A RID: 42
	internal sealed class BlobHeap : Heap
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000772A File Offset: 0x0000592A
		public BlobHeap(Section section, uint start, uint size) : base(section, start, size)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007738 File Offset: 0x00005938
		public byte[] Read(uint index)
		{
			if (index == 0U || index > this.Size - 1U)
			{
				return Empty<byte>.Array;
			}
			byte[] data = this.Section.Data;
			int srcOffset = (int)(index + this.Offset);
			int num = (int)data.ReadCompressedUInt32(ref srcOffset);
			byte[] array = new byte[num];
			Buffer.BlockCopy(data, srcOffset, array, 0, num);
			return array;
		}
	}
}
