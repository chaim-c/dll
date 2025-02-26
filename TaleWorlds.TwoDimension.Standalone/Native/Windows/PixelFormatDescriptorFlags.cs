﻿using System;

namespace TaleWorlds.TwoDimension.Standalone.Native.Windows
{
	// Token: 0x0200001D RID: 29
	[Flags]
	internal enum PixelFormatDescriptorFlags : uint
	{
		// Token: 0x0400009C RID: 156
		DoubleBuffer = 1U,
		// Token: 0x0400009D RID: 157
		Stereo = 2U,
		// Token: 0x0400009E RID: 158
		DrawToWindow = 4U,
		// Token: 0x0400009F RID: 159
		DrawToBitmap = 8U,
		// Token: 0x040000A0 RID: 160
		SupportGDI = 16U,
		// Token: 0x040000A1 RID: 161
		SupportOpengl = 32U,
		// Token: 0x040000A2 RID: 162
		GenericFormat = 64U,
		// Token: 0x040000A3 RID: 163
		NeedPalette = 128U,
		// Token: 0x040000A4 RID: 164
		NeedSystemPalette = 256U,
		// Token: 0x040000A5 RID: 165
		SwapExchange = 512U,
		// Token: 0x040000A6 RID: 166
		SwapCopy = 1024U,
		// Token: 0x040000A7 RID: 167
		SwapLayerBuffers = 2048U,
		// Token: 0x040000A8 RID: 168
		GenericAccelerated = 4096U,
		// Token: 0x040000A9 RID: 169
		SupportDirectDraw = 8192U,
		// Token: 0x040000AA RID: 170
		Direct3DAccelerated = 16384U,
		// Token: 0x040000AB RID: 171
		SupportComposition = 32768U
	}
}
