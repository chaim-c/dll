using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000073 RID: 115
	[Flags]
	[EngineStruct("rglPhysics_material::rglPhymat_flags", false)]
	public enum PhysicsMaterialFlags : byte
	{
		// Token: 0x04000154 RID: 340
		None = 0,
		// Token: 0x04000155 RID: 341
		DontStickMissiles = 1,
		// Token: 0x04000156 RID: 342
		Flammable = 2,
		// Token: 0x04000157 RID: 343
		RainSplashesEnabled = 4,
		// Token: 0x04000158 RID: 344
		AttacksCanPassThrough = 8
	}
}
