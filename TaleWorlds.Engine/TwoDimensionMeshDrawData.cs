using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000092 RID: 146
	[EngineStruct("rglTwo_dimension_mesh_draw_data", false)]
	public struct TwoDimensionMeshDrawData
	{
		// Token: 0x040001B4 RID: 436
		public float DrawX;

		// Token: 0x040001B5 RID: 437
		public float DrawY;

		// Token: 0x040001B6 RID: 438
		public float ScreenWidth;

		// Token: 0x040001B7 RID: 439
		public float ScreenHeight;

		// Token: 0x040001B8 RID: 440
		public Vec2 ClipCircleCenter;

		// Token: 0x040001B9 RID: 441
		public float ClipCircleRadius;

		// Token: 0x040001BA RID: 442
		public float ClipCircleSmoothingRadius;

		// Token: 0x040001BB RID: 443
		public uint Color;

		// Token: 0x040001BC RID: 444
		public float ColorFactor;

		// Token: 0x040001BD RID: 445
		public float AlphaFactor;

		// Token: 0x040001BE RID: 446
		public float HueFactor;

		// Token: 0x040001BF RID: 447
		public float SaturationFactor;

		// Token: 0x040001C0 RID: 448
		public float ValueFactor;

		// Token: 0x040001C1 RID: 449
		public float OverlayTextureWidth;

		// Token: 0x040001C2 RID: 450
		public float OverlayTextureHeight;

		// Token: 0x040001C3 RID: 451
		public Vec2 ClipRectPosition;

		// Token: 0x040001C4 RID: 452
		public Vec2 ClipRectSize;

		// Token: 0x040001C5 RID: 453
		public Vec2 StartCoordinate;

		// Token: 0x040001C6 RID: 454
		public Vec2 Size;

		// Token: 0x040001C7 RID: 455
		public int Layer;

		// Token: 0x040001C8 RID: 456
		public float OverlayXOffset;

		// Token: 0x040001C9 RID: 457
		public float OverlayYOffset;

		// Token: 0x040001CA RID: 458
		public float Width;

		// Token: 0x040001CB RID: 459
		public float Height;

		// Token: 0x040001CC RID: 460
		public float MinU;

		// Token: 0x040001CD RID: 461
		public float MinV;

		// Token: 0x040001CE RID: 462
		public float MaxU;

		// Token: 0x040001CF RID: 463
		public float MaxV;

		// Token: 0x040001D0 RID: 464
		public uint Type;
	}
}
