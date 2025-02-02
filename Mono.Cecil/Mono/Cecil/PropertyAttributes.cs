using System;

namespace Mono.Cecil
{
	// Token: 0x020000D7 RID: 215
	[Flags]
	public enum PropertyAttributes : ushort
	{
		// Token: 0x0400053D RID: 1341
		None = 0,
		// Token: 0x0400053E RID: 1342
		SpecialName = 512,
		// Token: 0x0400053F RID: 1343
		RTSpecialName = 1024,
		// Token: 0x04000540 RID: 1344
		HasDefault = 4096,
		// Token: 0x04000541 RID: 1345
		Unused = 59903
	}
}
