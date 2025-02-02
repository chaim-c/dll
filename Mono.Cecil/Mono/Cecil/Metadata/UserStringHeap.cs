using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000042 RID: 66
	internal sealed class UserStringHeap : StringHeap
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000812C File Offset: 0x0000632C
		public UserStringHeap(Section section, uint start, uint size) : base(section, start, size)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008138 File Offset: 0x00006338
		protected override string ReadStringAt(uint index)
		{
			byte[] data = this.Section.Data;
			int num = (int)(index + this.Offset);
			uint num2 = (uint)((ulong)data.ReadCompressedUInt32(ref num) & 18446744073709551614UL);
			if (num2 < 1U)
			{
				return string.Empty;
			}
			char[] array = new char[num2 / 2U];
			int num3 = num;
			int num4 = 0;
			while ((long)num3 < (long)num + (long)((ulong)num2))
			{
				array[num4++] = (char)((int)data[num3] | (int)data[num3 + 1] << 8);
				num3 += 2;
			}
			return new string(array);
		}
	}
}
