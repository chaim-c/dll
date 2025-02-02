using System;

namespace Mono.Cecil
{
	// Token: 0x020000E7 RID: 231
	[Flags]
	public enum ModuleCharacteristics
	{
		// Token: 0x0400059E RID: 1438
		HighEntropyVA = 32,
		// Token: 0x0400059F RID: 1439
		DynamicBase = 64,
		// Token: 0x040005A0 RID: 1440
		NoSEH = 1024,
		// Token: 0x040005A1 RID: 1441
		NXCompat = 256,
		// Token: 0x040005A2 RID: 1442
		AppContainer = 4096,
		// Token: 0x040005A3 RID: 1443
		TerminalServerAware = 32768
	}
}
