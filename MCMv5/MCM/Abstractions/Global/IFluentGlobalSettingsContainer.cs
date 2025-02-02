using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.Global;

namespace MCM.Abstractions.Global
{
	// Token: 0x020000A9 RID: 169
	public interface IFluentGlobalSettingsContainer : IGlobalSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack
	{
		// Token: 0x0600037F RID: 895
		[NullableContext(1)]
		void Register(FluentGlobalSettings settings);

		// Token: 0x06000380 RID: 896
		[NullableContext(1)]
		void Unregister(FluentGlobalSettings settings);
	}
}
