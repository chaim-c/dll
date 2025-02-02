using System;

namespace HarmonyLib
{
	// Token: 0x0200001D RID: 29
	public enum MethodType
	{
		// Token: 0x0400005D RID: 93
		Normal,
		// Token: 0x0400005E RID: 94
		Getter,
		// Token: 0x0400005F RID: 95
		Setter,
		// Token: 0x04000060 RID: 96
		Constructor,
		// Token: 0x04000061 RID: 97
		StaticConstructor,
		// Token: 0x04000062 RID: 98
		Enumerator,
		// Token: 0x04000063 RID: 99
		Async
	}
}
