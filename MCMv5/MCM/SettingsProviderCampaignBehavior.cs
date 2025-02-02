using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using TaleWorlds.CampaignSystem;

namespace MCM
{
	// Token: 0x0200000A RID: 10
	public class SettingsProviderCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000024C3 File Offset: 0x000006C3
		[NullableContext(2)]
		public SettingsProviderCampaignBehavior(BaseSettingsProvider baseSettingsProvider)
		{
			this._baseSettingsProvider = baseSettingsProvider;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024E0 File Offset: 0x000006E0
		[return: Nullable(2)]
		public TSettings Get<TSettings>() where TSettings : BaseSettings, new()
		{
			bool flag = !this._cache.ContainsKey(typeof(TSettings));
			if (flag)
			{
				this._cache.TryAdd(typeof(TSettings), Activator.CreateInstance<TSettings>().Id);
			}
			BaseSettingsProvider baseSettingsProvider = this._baseSettingsProvider;
			return ((baseSettingsProvider != null) ? baseSettingsProvider.GetSettings(this._cache[typeof(TSettings)]) : null) as TSettings;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002564 File Offset: 0x00000764
		public override void RegisterEvents()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002567 File Offset: 0x00000767
		[NullableContext(1)]
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0400000C RID: 12
		[Nullable(2)]
		private readonly BaseSettingsProvider _baseSettingsProvider;

		// Token: 0x0400000D RID: 13
		[Nullable(1)]
		private readonly ConcurrentDictionary<Type, string> _cache = new ConcurrentDictionary<Type, string>();
	}
}
