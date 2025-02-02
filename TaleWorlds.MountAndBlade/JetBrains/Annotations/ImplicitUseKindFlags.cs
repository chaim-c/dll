using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E8 RID: 232
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x0400021C RID: 540
		Default = 7,
		// Token: 0x0400021D RID: 541
		Access = 1,
		// Token: 0x0400021E RID: 542
		Assign = 2,
		// Token: 0x0400021F RID: 543
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000220 RID: 544
		InstantiatedNoFixedConstructorSignature = 8
	}
}
