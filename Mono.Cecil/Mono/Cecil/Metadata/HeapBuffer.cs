using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002B RID: 43
	internal abstract class HeapBuffer : ByteBuffer
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007789 File Offset: 0x00005989
		public bool IsLarge
		{
			get
			{
				return this.length > 65535;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000181 RID: 385
		public abstract bool IsEmpty { get; }

		// Token: 0x06000182 RID: 386 RVA: 0x00007798 File Offset: 0x00005998
		protected HeapBuffer(int length) : base(length)
		{
		}
	}
}
