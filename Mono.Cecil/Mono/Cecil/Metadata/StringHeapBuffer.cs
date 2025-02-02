using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002F RID: 47
	internal class StringHeapBuffer : HeapBuffer
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007B8C File Offset: 0x00005D8C
		public sealed override bool IsEmpty
		{
			get
			{
				return this.length <= 1;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007B9A File Offset: 0x00005D9A
		public StringHeapBuffer() : base(1)
		{
			base.WriteByte(0);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007BBC File Offset: 0x00005DBC
		public uint GetStringIndex(string @string)
		{
			uint position;
			if (this.strings.TryGetValue(@string, out position))
			{
				return position;
			}
			position = (uint)this.position;
			this.WriteString(@string);
			this.strings.Add(@string, position);
			return position;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007BF7 File Offset: 0x00005DF7
		protected virtual void WriteString(string @string)
		{
			base.WriteBytes(Encoding.UTF8.GetBytes(@string));
			base.WriteByte(0);
		}

		// Token: 0x04000282 RID: 642
		private readonly Dictionary<string, uint> strings = new Dictionary<string, uint>(StringComparer.Ordinal);
	}
}
