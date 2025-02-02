using System;

namespace Mono.Cecil
{
	// Token: 0x020000D0 RID: 208
	[Flags]
	public enum MethodSemanticsAttributes : ushort
	{
		// Token: 0x0400050B RID: 1291
		None = 0,
		// Token: 0x0400050C RID: 1292
		Setter = 1,
		// Token: 0x0400050D RID: 1293
		Getter = 2,
		// Token: 0x0400050E RID: 1294
		Other = 4,
		// Token: 0x0400050F RID: 1295
		AddOn = 8,
		// Token: 0x04000510 RID: 1296
		RemoveOn = 16,
		// Token: 0x04000511 RID: 1297
		Fire = 32
	}
}
