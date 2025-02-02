using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Global;

namespace MCM.Implementation.Global
{
	// Token: 0x0200003C RID: 60
	internal sealed class GlobalSettingsContainer : BaseGlobalSettingsContainer, IGlobalSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000076F0 File Offset: 0x000058F0
		[NullableContext(1)]
		public GlobalSettingsContainer(IBUTRLogger<GlobalSettingsContainer> logger)
		{
			GlobalSettingsContainer.<>c__DisplayClass0_0 CS$<>8__locals1 = new GlobalSettingsContainer.<>c__DisplayClass0_0();
			CS$<>8__locals1.logger = logger;
			base..ctor();
			foreach (GlobalSettings setting in CS$<>8__locals1.<.ctor>g__GetGlobalSettings|0())
			{
				CS$<>8__locals1.logger.LogTrace(string.Format("Registering settings {0}.", setting.GetType()), Array.Empty<object>());
				this.RegisterSettings(setting);
			}
		}
	}
}
