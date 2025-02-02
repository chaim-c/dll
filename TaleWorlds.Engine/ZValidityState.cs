using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000098 RID: 152
	[EngineStruct("rglWorld_position::z_validity_state", false)]
	public enum ZValidityState
	{
		// Token: 0x040001EF RID: 495
		Invalid,
		// Token: 0x040001F0 RID: 496
		BatchFormationUnitPosition,
		// Token: 0x040001F1 RID: 497
		ValidAccordingToNavMesh,
		// Token: 0x040001F2 RID: 498
		Valid
	}
}
