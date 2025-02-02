using System;

namespace Mono.Cecil
{
	// Token: 0x020000C3 RID: 195
	[Flags]
	public enum EventAttributes : ushort
	{
		// Token: 0x0400049F RID: 1183
		None = 0,
		// Token: 0x040004A0 RID: 1184
		SpecialName = 512,
		// Token: 0x040004A1 RID: 1185
		RTSpecialName = 1024
	}
}
