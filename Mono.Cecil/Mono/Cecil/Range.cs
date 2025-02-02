using System;

namespace Mono.Cecil
{
	// Token: 0x020000BF RID: 191
	internal struct Range
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00018FF3 File Offset: 0x000171F3
		public Range(uint index, uint length)
		{
			this.Start = index;
			this.Length = length;
		}

		// Token: 0x0400047E RID: 1150
		public uint Start;

		// Token: 0x0400047F RID: 1151
		public uint Length;
	}
}
