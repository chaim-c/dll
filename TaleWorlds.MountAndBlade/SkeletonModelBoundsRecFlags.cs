using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D5 RID: 469
	[EngineStruct("Skeleton_model_bounds_rec_flags", false)]
	public enum SkeletonModelBoundsRecFlags : sbyte
	{
		// Token: 0x04000856 RID: 2134
		None,
		// Token: 0x04000857 RID: 2135
		UseSmallerRadiusMultWhileHoldingShield,
		// Token: 0x04000858 RID: 2136
		Sweep,
		// Token: 0x04000859 RID: 2137
		DoNotScaleAccordingToAgentScale = 4
	}
}
