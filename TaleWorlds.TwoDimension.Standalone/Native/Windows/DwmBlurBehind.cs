using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.TwoDimension.Standalone.Native.Windows
{
	// Token: 0x02000017 RID: 23
	internal struct DwmBlurBehind
	{
		// Token: 0x04000073 RID: 115
		public BlurBehindConstraints dwFlags;

		// Token: 0x04000074 RID: 116
		[MarshalAs(UnmanagedType.Bool)]
		public bool fEnable;

		// Token: 0x04000075 RID: 117
		public IntPtr hRgnBlur;

		// Token: 0x04000076 RID: 118
		[MarshalAs(UnmanagedType.Bool)]
		public bool fTransitionOnMaximized;
	}
}
