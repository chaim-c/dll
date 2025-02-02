using System;
using System.Collections.Generic;

namespace Mono.Cecil.PE
{
	// Token: 0x02000046 RID: 70
	internal sealed class ByteBufferEqualityComparer : IEqualityComparer<ByteBuffer>
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00009800 File Offset: 0x00007A00
		public bool Equals(ByteBuffer x, ByteBuffer y)
		{
			if (x.length != y.length)
			{
				return false;
			}
			byte[] buffer = x.buffer;
			byte[] buffer2 = y.buffer;
			for (int i = 0; i < x.length; i++)
			{
				if (buffer[i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009848 File Offset: 0x00007A48
		public int GetHashCode(ByteBuffer buffer)
		{
			int num = 0;
			byte[] buffer2 = buffer.buffer;
			for (int i = 0; i < buffer.length; i++)
			{
				num = (num * 37 ^ (int)buffer2[i]);
			}
			return num;
		}
	}
}
