using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000364 RID: 868
	[Flags]
	public enum TargetFlags
	{
		// Token: 0x040013F5 RID: 5109
		None = 0,
		// Token: 0x040013F6 RID: 5110
		IsMoving = 1,
		// Token: 0x040013F7 RID: 5111
		IsFlammable = 2,
		// Token: 0x040013F8 RID: 5112
		IsStructure = 4,
		// Token: 0x040013F9 RID: 5113
		IsSiegeEngine = 8,
		// Token: 0x040013FA RID: 5114
		IsAttacker = 16,
		// Token: 0x040013FB RID: 5115
		IsSmall = 32,
		// Token: 0x040013FC RID: 5116
		NotAThreat = 64,
		// Token: 0x040013FD RID: 5117
		DebugThreat = 128,
		// Token: 0x040013FE RID: 5118
		IsSiegeTower = 256
	}
}
