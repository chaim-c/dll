using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000656 RID: 1622
	[Flags]
	public enum PlatformFlags : ulong
	{
		// Token: 0x040012D5 RID: 4821
		None = 0UL,
		// Token: 0x040012D6 RID: 4822
		LoadingInEditor = 1UL,
		// Token: 0x040012D7 RID: 4823
		DisableOverlay = 2UL,
		// Token: 0x040012D8 RID: 4824
		DisableSocialOverlay = 4UL,
		// Token: 0x040012D9 RID: 4825
		Reserved1 = 8UL,
		// Token: 0x040012DA RID: 4826
		WindowsEnableOverlayD3D9 = 16UL,
		// Token: 0x040012DB RID: 4827
		WindowsEnableOverlayD3D10 = 32UL,
		// Token: 0x040012DC RID: 4828
		WindowsEnableOverlayOpengl = 64UL
	}
}
