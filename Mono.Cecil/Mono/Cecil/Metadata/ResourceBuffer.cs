using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002D RID: 45
	internal sealed class ResourceBuffer : ByteBuffer
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00007B37 File Offset: 0x00005D37
		public ResourceBuffer() : base(0)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007B40 File Offset: 0x00005D40
		public uint AddResource(byte[] resource)
		{
			uint position = (uint)this.position;
			base.WriteInt32(resource.Length);
			base.WriteBytes(resource);
			return position;
		}
	}
}
