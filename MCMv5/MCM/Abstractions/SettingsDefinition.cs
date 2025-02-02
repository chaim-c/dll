using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(0)]
	public class SettingsDefinition
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007E7B File Offset: 0x0000607B
		public string SettingsId { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007E83 File Offset: 0x00006083
		public string DisplayName { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007E8B File Offset: 0x0000608B
		public List<SettingsPropertyGroupDefinition> SettingPropertyGroups { get; }

		// Token: 0x060001D5 RID: 469 RVA: 0x00007E94 File Offset: 0x00006094
		public SettingsDefinition(string id)
		{
			this.SettingsId = id;
			BaseSettingsProvider instance = BaseSettingsProvider.Instance;
			BaseSettings settings = (instance != null) ? instance.GetSettings(id) : null;
			this.DisplayName = (((settings != null) ? settings.DisplayName : null) ?? "ERROR");
			this.SettingPropertyGroups = (((settings != null) ? settings.GetSettingPropertyGroups() : null) ?? new List<SettingsPropertyGroupDefinition>());
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007EF9 File Offset: 0x000060F9
		public SettingsDefinition(string id, string displayName, List<SettingsPropertyGroupDefinition> settingsPropertyGroups)
		{
			this.SettingsId = id;
			this.DisplayName = displayName;
			this.SettingPropertyGroups = settingsPropertyGroups;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007F18 File Offset: 0x00006118
		public SettingsDefinition(string id, string displayName, IEnumerable<SettingsPropertyDefinition> settingsProperties)
		{
			this.SettingsId = id;
			this.DisplayName = displayName;
			this.SettingPropertyGroups = SettingsUtils.GetSettingsPropertyGroups('/', settingsProperties);
		}
	}
}
