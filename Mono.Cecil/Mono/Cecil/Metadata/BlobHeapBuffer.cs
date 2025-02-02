using System;
using System.Collections.Generic;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000030 RID: 48
	internal sealed class BlobHeapBuffer : HeapBuffer
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007C11 File Offset: 0x00005E11
		public override bool IsEmpty
		{
			get
			{
				return this.length <= 1;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007C1F File Offset: 0x00005E1F
		public BlobHeapBuffer() : base(1)
		{
			base.WriteByte(0);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007C40 File Offset: 0x00005E40
		public uint GetBlobIndex(ByteBuffer blob)
		{
			uint position;
			if (this.blobs.TryGetValue(blob, out position))
			{
				return position;
			}
			position = (uint)this.position;
			this.WriteBlob(blob);
			this.blobs.Add(blob, position);
			return position;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007C7B File Offset: 0x00005E7B
		private void WriteBlob(ByteBuffer blob)
		{
			base.WriteCompressedUInt32((uint)blob.length);
			base.WriteBytes(blob);
		}

		// Token: 0x04000283 RID: 643
		private readonly Dictionary<ByteBuffer, uint> blobs = new Dictionary<ByteBuffer, uint>(new ByteBufferEqualityComparer());
	}
}
