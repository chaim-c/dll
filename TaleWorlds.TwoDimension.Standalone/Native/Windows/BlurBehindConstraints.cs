﻿using System;

namespace TaleWorlds.TwoDimension.Standalone.Native.Windows
{
	// Token: 0x02000018 RID: 24
	[Flags]
	public enum BlurBehindConstraints : uint
	{
		// Token: 0x04000078 RID: 120
		Enable = 1U,
		// Token: 0x04000079 RID: 121
		BlurRegion = 2U,
		// Token: 0x0400007A RID: 122
		TransitionOnMaximized = 4U
	}
}
