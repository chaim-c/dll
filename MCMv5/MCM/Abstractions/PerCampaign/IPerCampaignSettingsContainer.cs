using System;

namespace MCM.Abstractions.PerCampaign
{
	// Token: 0x020000A8 RID: 168
	public interface IPerCampaignSettingsContainer : ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
	}
}
