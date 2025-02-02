using System;

namespace Mono.Cecil
{
	// Token: 0x02000059 RID: 89
	[Flags]
	public enum AssemblyAttributes : uint
	{
		// Token: 0x0400038B RID: 907
		PublicKey = 1U,
		// Token: 0x0400038C RID: 908
		SideBySideCompatible = 0U,
		// Token: 0x0400038D RID: 909
		Retargetable = 256U,
		// Token: 0x0400038E RID: 910
		WindowsRuntime = 512U,
		// Token: 0x0400038F RID: 911
		DisableJITCompileOptimizer = 16384U,
		// Token: 0x04000390 RID: 912
		EnableJITCompileTracking = 32768U
	}
}
