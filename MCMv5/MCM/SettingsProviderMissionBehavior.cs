using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using TaleWorlds.MountAndBlade;

namespace MCM
{
	// Token: 0x0200000B RID: 11
	public class SettingsProviderMissionBehavior : MissionBehavior
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000256A File Offset: 0x0000076A
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Other;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000256D File Offset: 0x0000076D
		[NullableContext(2)]
		public SettingsProviderMissionBehavior(BaseSettingsProvider baseSettingsProvider)
		{
			this._baseSettingsProvider = baseSettingsProvider;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000258C File Offset: 0x0000078C
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

		// Token: 0x0400000E RID: 14
		[Nullable(2)]
		private readonly BaseSettingsProvider _baseSettingsProvider;

		// Token: 0x0400000F RID: 15
		[Nullable(1)]
		private readonly ConcurrentDictionary<Type, string> _cache = new ConcurrentDictionary<Type, string>();
	}
}
