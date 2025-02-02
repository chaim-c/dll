using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000087 RID: 135
	[EngineStruct("rglRagdoll::Ragdoll_state", false)]
	public enum RagdollState : ushort
	{
		// Token: 0x040001A7 RID: 423
		Disabled,
		// Token: 0x040001A8 RID: 424
		NeedsActivation,
		// Token: 0x040001A9 RID: 425
		ActiveFirstTick,
		// Token: 0x040001AA RID: 426
		Active,
		// Token: 0x040001AB RID: 427
		NeedsDeactivation
	}
}
