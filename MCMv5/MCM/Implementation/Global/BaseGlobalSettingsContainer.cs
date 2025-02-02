using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.Global;

namespace MCM.Implementation.Global
{
	// Token: 0x02000039 RID: 57
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal abstract class BaseGlobalSettingsContainer : BaseSettingsContainer<GlobalSettings>, IGlobalSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000073B0 File Offset: 0x000055B0
		[Nullable(1)]
		protected override GameDirectory RootFolder { [NullableContext(1)] get; }

		// Token: 0x06000183 RID: 387 RVA: 0x000073B8 File Offset: 0x000055B8
		protected BaseGlobalSettingsContainer()
		{
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			this.RootFolder = (((fileSystemProvider != null) ? fileSystemProvider.GetDirectory(base.RootFolder, "Global") : null) ?? base.RootFolder);
		}
	}
}
