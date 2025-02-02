using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	public interface IExternalSettingsProvider
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000245 RID: 581
		IEnumerable<SettingsDefinition> SettingsDefinitions { get; }

		// Token: 0x06000246 RID: 582
		[return: Nullable(2)]
		BaseSettings GetSettings(string id);

		// Token: 0x06000247 RID: 583
		void SaveSettings(BaseSettings settings);

		// Token: 0x06000248 RID: 584
		void ResetSettings(BaseSettings settings);

		// Token: 0x06000249 RID: 585
		void OverrideSettings(BaseSettings settings);

		// Token: 0x0600024A RID: 586
		IEnumerable<ISettingsPreset> GetPresets(string id);
	}
}
