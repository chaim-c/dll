using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseSettingsProvider
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008D1A File Offset: 0x00006F1A
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00008D21 File Offset: 0x00006F21
		[Nullable(2)]
		public static BaseSettingsProvider Instance { [NullableContext(2)] get; [NullableContext(2)] internal set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023B RID: 571
		public abstract IEnumerable<SettingsDefinition> SettingsDefinitions { get; }

		// Token: 0x0600023C RID: 572
		[return: Nullable(2)]
		public abstract BaseSettings GetSettings(string id);

		// Token: 0x0600023D RID: 573
		public abstract void SaveSettings(BaseSettings settings);

		// Token: 0x0600023E RID: 574
		public abstract void ResetSettings(BaseSettings settings);

		// Token: 0x0600023F RID: 575
		public abstract void OverrideSettings(BaseSettings settings);

		// Token: 0x06000240 RID: 576
		public abstract IEnumerable<ISettingsPreset> GetPresets(string id);

		// Token: 0x06000241 RID: 577
		public abstract IEnumerable<UnavailableSetting> GetUnavailableSettings();

		// Token: 0x06000242 RID: 578
		public abstract IEnumerable<SettingSnapshot> SaveAvailableSnapshots();

		// Token: 0x06000243 RID: 579
		public abstract IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots);
	}
}
