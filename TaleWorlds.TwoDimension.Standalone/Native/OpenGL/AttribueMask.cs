using System;

namespace TaleWorlds.TwoDimension.Standalone.Native.OpenGL
{
	// Token: 0x02000036 RID: 54
	internal enum AttribueMask : uint
	{
		// Token: 0x04000245 RID: 581
		CurrentBit = 1U,
		// Token: 0x04000246 RID: 582
		PointBit,
		// Token: 0x04000247 RID: 583
		LineBit = 4U,
		// Token: 0x04000248 RID: 584
		PolygonBit = 8U,
		// Token: 0x04000249 RID: 585
		PolygonStippleBit = 16U,
		// Token: 0x0400024A RID: 586
		PixelModeBit = 32U,
		// Token: 0x0400024B RID: 587
		LightingBit = 64U,
		// Token: 0x0400024C RID: 588
		FogBit = 128U,
		// Token: 0x0400024D RID: 589
		DepthBufferBit = 256U,
		// Token: 0x0400024E RID: 590
		AccumBufferBit = 512U,
		// Token: 0x0400024F RID: 591
		StencilBufferBit = 1024U,
		// Token: 0x04000250 RID: 592
		ViewportBit = 2048U,
		// Token: 0x04000251 RID: 593
		TransformBit = 4096U,
		// Token: 0x04000252 RID: 594
		EnableBit = 8192U,
		// Token: 0x04000253 RID: 595
		ColorBufferBit = 16384U,
		// Token: 0x04000254 RID: 596
		HintBit = 32768U,
		// Token: 0x04000255 RID: 597
		EvalBit = 65536U,
		// Token: 0x04000256 RID: 598
		ListBit = 131072U,
		// Token: 0x04000257 RID: 599
		TextureBit = 262144U,
		// Token: 0x04000258 RID: 600
		ScissorBit = 524288U,
		// Token: 0x04000259 RID: 601
		AllAttribBits = 1048575U
	}
}
