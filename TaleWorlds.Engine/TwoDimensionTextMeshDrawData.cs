using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000093 RID: 147
	[EngineStruct("rglTwo_dimension_text_mesh_draw_data", false)]
	public struct TwoDimensionTextMeshDrawData
	{
		// Token: 0x040001D1 RID: 465
		public float DrawX;

		// Token: 0x040001D2 RID: 466
		public float DrawY;

		// Token: 0x040001D3 RID: 467
		public float ScreenWidth;

		// Token: 0x040001D4 RID: 468
		public float ScreenHeight;

		// Token: 0x040001D5 RID: 469
		public uint Color;

		// Token: 0x040001D6 RID: 470
		public float ScaleFactor;

		// Token: 0x040001D7 RID: 471
		public float SmoothingConstant;

		// Token: 0x040001D8 RID: 472
		public float ColorFactor;

		// Token: 0x040001D9 RID: 473
		public float AlphaFactor;

		// Token: 0x040001DA RID: 474
		public float HueFactor;

		// Token: 0x040001DB RID: 475
		public float SaturationFactor;

		// Token: 0x040001DC RID: 476
		public float ValueFactor;

		// Token: 0x040001DD RID: 477
		public Vec2 ClipRectPosition;

		// Token: 0x040001DE RID: 478
		public Vec2 ClipRectSize;

		// Token: 0x040001DF RID: 479
		public uint GlowColor;

		// Token: 0x040001E0 RID: 480
		public Vec3 OutlineColor;

		// Token: 0x040001E1 RID: 481
		public float OutlineAmount;

		// Token: 0x040001E2 RID: 482
		public float GlowRadius;

		// Token: 0x040001E3 RID: 483
		public float Blur;

		// Token: 0x040001E4 RID: 484
		public float ShadowOffset;

		// Token: 0x040001E5 RID: 485
		public float ShadowAngle;

		// Token: 0x040001E6 RID: 486
		public int Layer;

		// Token: 0x040001E7 RID: 487
		public ulong HashCode1;

		// Token: 0x040001E8 RID: 488
		public ulong HashCode2;
	}
}
