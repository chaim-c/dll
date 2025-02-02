using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200018E RID: 398
	[EngineStruct("Melee_collision_reaction", false)]
	public enum MeleeCollisionReaction
	{
		// Token: 0x04000605 RID: 1541
		Invalid = -1,
		// Token: 0x04000606 RID: 1542
		SlicedThrough,
		// Token: 0x04000607 RID: 1543
		ContinueChecking,
		// Token: 0x04000608 RID: 1544
		Stuck,
		// Token: 0x04000609 RID: 1545
		Bounced,
		// Token: 0x0400060A RID: 1546
		Staggered
	}
}
