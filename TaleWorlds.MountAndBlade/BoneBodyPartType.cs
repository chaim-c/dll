using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D4 RID: 468
	[EngineStruct("Bone_body_part_type", false)]
	public enum BoneBodyPartType : sbyte
	{
		// Token: 0x04000848 RID: 2120
		None = -1,
		// Token: 0x04000849 RID: 2121
		Head,
		// Token: 0x0400084A RID: 2122
		Neck,
		// Token: 0x0400084B RID: 2123
		Chest,
		// Token: 0x0400084C RID: 2124
		Abdomen,
		// Token: 0x0400084D RID: 2125
		ShoulderLeft,
		// Token: 0x0400084E RID: 2126
		ShoulderRight,
		// Token: 0x0400084F RID: 2127
		ArmLeft,
		// Token: 0x04000850 RID: 2128
		ArmRight,
		// Token: 0x04000851 RID: 2129
		Legs,
		// Token: 0x04000852 RID: 2130
		NumOfBodyPartTypes,
		// Token: 0x04000853 RID: 2131
		CriticalBodyPartsBegin = 0,
		// Token: 0x04000854 RID: 2132
		CriticalBodyPartsEnd = 6
	}
}
