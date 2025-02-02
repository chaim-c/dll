using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E3 RID: 483
	[Flags]
	[EngineStruct("Blow_flags", false)]
	public enum BlowFlags
	{
		// Token: 0x04000879 RID: 2169
		None = 0,
		// Token: 0x0400087A RID: 2170
		KnockBack = 16,
		// Token: 0x0400087B RID: 2171
		KnockDown = 32,
		// Token: 0x0400087C RID: 2172
		NoSound = 64,
		// Token: 0x0400087D RID: 2173
		CrushThrough = 128,
		// Token: 0x0400087E RID: 2174
		ShrugOff = 256,
		// Token: 0x0400087F RID: 2175
		MakesRear = 512,
		// Token: 0x04000880 RID: 2176
		NonTipThrust = 1024,
		// Token: 0x04000881 RID: 2177
		CanDismount = 2048
	}
}
