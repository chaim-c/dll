using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000047 RID: 71
	[Flags]
	public enum EntityVisibilityFlags
	{
		// Token: 0x04000074 RID: 116
		None = 0,
		// Token: 0x04000075 RID: 117
		VisibleOnlyWhenEditing = 2,
		// Token: 0x04000076 RID: 118
		NoShadow = 4,
		// Token: 0x04000077 RID: 119
		VisibleOnlyForEnvmap = 8,
		// Token: 0x04000078 RID: 120
		NotVisibleForEnvmap = 16
	}
}
