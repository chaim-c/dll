using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000063 RID: 99
	[Flags]
	public enum VisibilityMaskFlags : uint
	{
		// Token: 0x04000117 RID: 279
		Final = 1U,
		// Token: 0x04000118 RID: 280
		ShadowStatic = 16U,
		// Token: 0x04000119 RID: 281
		ShadowDynamic = 32U,
		// Token: 0x0400011A RID: 282
		Contour = 64U,
		// Token: 0x0400011B RID: 283
		EditModeAtmosphere = 268435456U,
		// Token: 0x0400011C RID: 284
		EditModeLight = 536870912U,
		// Token: 0x0400011D RID: 285
		EditModeParticleSystem = 1073741824U,
		// Token: 0x0400011E RID: 286
		EditModeHelpers = 2147483648U,
		// Token: 0x0400011F RID: 287
		EditModeTerrain = 16777216U,
		// Token: 0x04000120 RID: 288
		EditModeGameEntity = 33554432U,
		// Token: 0x04000121 RID: 289
		EditModeFloraEntity = 67108864U,
		// Token: 0x04000122 RID: 290
		EditModeLayerFlora = 134217728U,
		// Token: 0x04000123 RID: 291
		EditModeShadows = 1048576U,
		// Token: 0x04000124 RID: 292
		EditModeBorders = 2097152U,
		// Token: 0x04000125 RID: 293
		EditModeEditingEntity = 4194304U,
		// Token: 0x04000126 RID: 294
		EditModeAnimations = 8388608U,
		// Token: 0x04000127 RID: 295
		EditModeAny = 4293918720U,
		// Token: 0x04000128 RID: 296
		Default = 1U,
		// Token: 0x04000129 RID: 297
		DefaultStatic = 49U,
		// Token: 0x0400012A RID: 298
		DefaultDynamic = 33U,
		// Token: 0x0400012B RID: 299
		DefaultStaticWithoutDynamic = 17U
	}
}
