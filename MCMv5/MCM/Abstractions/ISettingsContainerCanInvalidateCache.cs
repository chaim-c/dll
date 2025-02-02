using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	public interface ISettingsContainerCanInvalidateCache
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000197 RID: 407
		// (remove) Token: 0x06000198 RID: 408
		event Action InstanceCacheInvalidated;
	}
}
