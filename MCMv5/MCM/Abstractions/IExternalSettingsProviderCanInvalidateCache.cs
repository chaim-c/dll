using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000065 RID: 101
	[NullableContext(1)]
	public interface IExternalSettingsProviderCanInvalidateCache
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600024B RID: 587
		// (remove) Token: 0x0600024C RID: 588
		event Action<ExternalSettingsProviderInvalidateCacheType> InstanceCacheInvalidated;
	}
}
