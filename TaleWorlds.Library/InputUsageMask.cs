using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200003E RID: 62
	[Flags]
	public enum InputUsageMask
	{
		// Token: 0x040000BD RID: 189
		Invalid = 0,
		// Token: 0x040000BE RID: 190
		MouseButtons = 1,
		// Token: 0x040000BF RID: 191
		MouseWheels = 2,
		// Token: 0x040000C0 RID: 192
		Keyboardkeys = 4,
		// Token: 0x040000C1 RID: 193
		BlockEverythingWithoutHitTest = 8,
		// Token: 0x040000C2 RID: 194
		Mouse = 3,
		// Token: 0x040000C3 RID: 195
		All = 7
	}
}
