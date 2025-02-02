using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000B3 RID: 179
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PerSaveSettings : BaseSettings
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000B709 File Offset: 0x00009909
		internal static void InvalidateCache()
		{
			PerSaveSettings.Cache.Clear();
			PerSaveSettings.CacheInstance.Clear();
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000B722 File Offset: 0x00009922
		public sealed override string FormatType { get; } = "json2";

		// Token: 0x04000138 RID: 312
		protected static readonly ConcurrentDictionary<Type, string> Cache = new ConcurrentDictionary<Type, string>();

		// Token: 0x04000139 RID: 313
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		protected static readonly ConcurrentDictionary<string, BaseSettings> CacheInstance = new ConcurrentDictionary<string, BaseSettings>();
	}
}
