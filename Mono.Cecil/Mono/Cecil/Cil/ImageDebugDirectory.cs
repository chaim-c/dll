using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200001D RID: 29
	public struct ImageDebugDirectory
	{
		// Token: 0x0400025D RID: 605
		public int Characteristics;

		// Token: 0x0400025E RID: 606
		public int TimeDateStamp;

		// Token: 0x0400025F RID: 607
		public short MajorVersion;

		// Token: 0x04000260 RID: 608
		public short MinorVersion;

		// Token: 0x04000261 RID: 609
		public int Type;

		// Token: 0x04000262 RID: 610
		public int SizeOfData;

		// Token: 0x04000263 RID: 611
		public int AddressOfRawData;

		// Token: 0x04000264 RID: 612
		public int PointerToRawData;
	}
}
