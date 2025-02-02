using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.PerSave;

namespace MCM.Abstractions.PerSave
{
	// Token: 0x020000A5 RID: 165
	public interface IFluentPerSaveSettingsContainer : IPerSaveSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x0600037A RID: 890
		[NullableContext(1)]
		void Register(FluentPerSaveSettings settings);

		// Token: 0x0600037B RID: 891
		[NullableContext(1)]
		void Unregister(FluentPerSaveSettings settings);
	}
}
