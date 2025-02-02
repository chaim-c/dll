using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002E RID: 46
	internal sealed class DataBuffer : ByteBuffer
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00007B65 File Offset: 0x00005D65
		public DataBuffer() : base(0)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007B70 File Offset: 0x00005D70
		public uint AddData(byte[] data)
		{
			uint position = (uint)this.position;
			base.WriteBytes(data);
			return position;
		}
	}
}
