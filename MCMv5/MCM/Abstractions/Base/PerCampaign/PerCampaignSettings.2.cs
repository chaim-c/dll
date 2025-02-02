using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000BA RID: 186
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PerCampaignSettings : BaseSettings
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000BCD5 File Offset: 0x00009ED5
		internal static void InvalidateCache()
		{
			PerCampaignSettings.Cache.Clear();
			PerCampaignSettings.CacheInstance.Clear();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000BCEE File Offset: 0x00009EEE
		public sealed override string FormatType { get; } = "json2";

		// Token: 0x0400014B RID: 331
		protected static readonly ConcurrentDictionary<Type, string> Cache = new ConcurrentDictionary<Type, string>();

		// Token: 0x0400014C RID: 332
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		protected static readonly ConcurrentDictionary<string, BaseSettings> CacheInstance = new ConcurrentDictionary<string, BaseSettings>();
	}
}
