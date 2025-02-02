using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Global;

namespace MCM.Implementation.Global
{
	// Token: 0x0200003B RID: 59
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class FluentGlobalSettingsContainer : BaseSettingsContainer<FluentGlobalSettings>, IFluentGlobalSettingsContainer, IGlobalSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00007660 File Offset: 0x00005860
		[NullableContext(1)]
		public void Register(FluentGlobalSettings settings)
		{
			bool flag = settings.GetType() != typeof(FluentGlobalSettings);
			if (!flag)
			{
				this.RegisterSettings(settings);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007694 File Offset: 0x00005894
		[NullableContext(1)]
		public void Unregister(FluentGlobalSettings settings)
		{
			bool flag = settings.GetType() != typeof(FluentGlobalSettings);
			if (!flag)
			{
				bool flag2 = this.LoadedSettings.ContainsKey(settings.Id);
				if (flag2)
				{
					this.LoadedSettings.Remove(settings.Id);
				}
			}
		}
	}
}
