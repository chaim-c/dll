using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200018F RID: 399
	[Flags]
	[EngineStruct("Combat_hit_result_flags", false)]
	public enum CombatHitResultFlags : byte
	{
		// Token: 0x0400060C RID: 1548
		NormalHit = 0,
		// Token: 0x0400060D RID: 1549
		HitWithStartOfTheAnimation = 1,
		// Token: 0x0400060E RID: 1550
		HitWithArm = 2,
		// Token: 0x0400060F RID: 1551
		HitWithBackOfTheWeapon = 4
	}
}
