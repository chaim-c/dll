using System;

namespace TaleWorlds.TwoDimension.Standalone.Native.OpenGL
{
	// Token: 0x02000032 RID: 50
	internal enum BlendingSourceFactor : uint
	{
		// Token: 0x04000226 RID: 550
		Zero,
		// Token: 0x04000227 RID: 551
		One,
		// Token: 0x04000228 RID: 552
		SourceColor = 768U,
		// Token: 0x04000229 RID: 553
		OneMinusSourceColor,
		// Token: 0x0400022A RID: 554
		SourceAlpha,
		// Token: 0x0400022B RID: 555
		OneMinusSourceAlpha,
		// Token: 0x0400022C RID: 556
		DestinationAlpha,
		// Token: 0x0400022D RID: 557
		OneMinusDestinationAlpha,
		// Token: 0x0400022E RID: 558
		DestinationColor,
		// Token: 0x0400022F RID: 559
		OneMinusDestinationColor,
		// Token: 0x04000230 RID: 560
		SourceAlphaSaturate
	}
}
