using System;

namespace Mono.Cecil
{
	// Token: 0x020000E6 RID: 230
	[Flags]
	public enum ModuleAttributes
	{
		// Token: 0x04000599 RID: 1433
		ILOnly = 1,
		// Token: 0x0400059A RID: 1434
		Required32Bit = 2,
		// Token: 0x0400059B RID: 1435
		StrongNameSigned = 8,
		// Token: 0x0400059C RID: 1436
		Preferred32Bit = 131072
	}
}
