using System;

namespace Mono.Cecil.PE
{
	// Token: 0x0200004B RID: 75
	internal sealed class Section
	{
		// Token: 0x0400035E RID: 862
		public string Name;

		// Token: 0x0400035F RID: 863
		public uint VirtualAddress;

		// Token: 0x04000360 RID: 864
		public uint VirtualSize;

		// Token: 0x04000361 RID: 865
		public uint SizeOfRawData;

		// Token: 0x04000362 RID: 866
		public uint PointerToRawData;

		// Token: 0x04000363 RID: 867
		public byte[] Data;
	}
}
