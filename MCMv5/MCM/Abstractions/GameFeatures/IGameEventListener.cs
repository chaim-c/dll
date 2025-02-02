using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	public interface IGameEventListener
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600029E RID: 670
		// (remove) Token: 0x0600029F RID: 671
		event Action GameStarted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060002A0 RID: 672
		// (remove) Token: 0x060002A1 RID: 673
		event Action GameLoaded;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060002A2 RID: 674
		// (remove) Token: 0x060002A3 RID: 675
		event Action GameEnded;
	}
}
