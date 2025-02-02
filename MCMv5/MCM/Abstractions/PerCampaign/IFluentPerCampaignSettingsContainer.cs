using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.PerCampaign;

namespace MCM.Abstractions.PerCampaign
{
	// Token: 0x020000A7 RID: 167
	public interface IFluentPerCampaignSettingsContainer : IPerCampaignSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x0600037D RID: 893
		[NullableContext(1)]
		void Register(FluentPerCampaignSettings settings);

		// Token: 0x0600037E RID: 894
		[NullableContext(1)]
		void Unregister(FluentPerCampaignSettings settings);
	}
}
