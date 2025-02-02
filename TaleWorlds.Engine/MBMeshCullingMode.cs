using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000061 RID: 97
	[EngineStruct("rglCull_mode", false)]
	public enum MBMeshCullingMode : byte
	{
		// Token: 0x0400010E RID: 270
		None,
		// Token: 0x0400010F RID: 271
		Backfaces,
		// Token: 0x04000110 RID: 272
		Frontfaces,
		// Token: 0x04000111 RID: 273
		Count
	}
}
