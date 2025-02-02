using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000C2 RID: 194
	public abstract class GlobalSettings : BaseSettings
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		internal static void InvalidateCache()
		{
			GlobalSettings.Cache.Clear();
			GlobalSettings.CacheInstance.Clear();
		}

		// Token: 0x0400015A RID: 346
		[Nullable(1)]
		protected static readonly ConcurrentDictionary<Type, string> Cache = new ConcurrentDictionary<Type, string>();

		// Token: 0x0400015B RID: 347
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		protected static readonly ConcurrentDictionary<string, BaseSettings> CacheInstance = new ConcurrentDictionary<string, BaseSettings>();
	}
}
