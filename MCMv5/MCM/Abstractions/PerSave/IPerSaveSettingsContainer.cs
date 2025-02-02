using System;

namespace MCM.Abstractions.PerSave
{
	// Token: 0x020000A6 RID: 166
	public interface IPerSaveSettingsContainer : ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x0600037C RID: 892
		void LoadSettings();
	}
}
