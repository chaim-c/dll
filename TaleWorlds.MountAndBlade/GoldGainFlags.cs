using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000241 RID: 577
	[Flags]
	public enum GoldGainFlags : ushort
	{
		// Token: 0x04000B7F RID: 2943
		FirstRangedKill = 1,
		// Token: 0x04000B80 RID: 2944
		FirstMeleeKill = 2,
		// Token: 0x04000B81 RID: 2945
		FirstAssist = 4,
		// Token: 0x04000B82 RID: 2946
		SecondAssist = 8,
		// Token: 0x04000B83 RID: 2947
		ThirdAssist = 16,
		// Token: 0x04000B84 RID: 2948
		FifthKill = 32,
		// Token: 0x04000B85 RID: 2949
		TenthKill = 64,
		// Token: 0x04000B86 RID: 2950
		DefaultKill = 128,
		// Token: 0x04000B87 RID: 2951
		DefaultAssist = 256,
		// Token: 0x04000B88 RID: 2952
		ObjectiveCompleted = 512,
		// Token: 0x04000B89 RID: 2953
		ObjectiveDestroyed = 1024,
		// Token: 0x04000B8A RID: 2954
		PerkBonus = 2048
	}
}
