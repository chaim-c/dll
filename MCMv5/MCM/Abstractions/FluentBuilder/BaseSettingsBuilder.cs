using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.Base.PerSave;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x02000075 RID: 117
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseSettingsBuilder : ISettingsBuilder
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00009E1F File Offset: 0x0000801F
		[return: Nullable(2)]
		public static ISettingsBuilder Create(string id, string displayName)
		{
			ISettingsBuilderFactory service = GenericServiceProvider.GetService<ISettingsBuilderFactory>();
			return (service != null) ? service.Create(id, displayName) : null;
		}

		// Token: 0x060002A7 RID: 679
		public abstract ISettingsBuilder SetFolderName(string value);

		// Token: 0x060002A8 RID: 680
		public abstract ISettingsBuilder SetSubFolder(string value);

		// Token: 0x060002A9 RID: 681
		public abstract ISettingsBuilder SetFormat(string value);

		// Token: 0x060002AA RID: 682
		public abstract ISettingsBuilder SetUIVersion(int value);

		// Token: 0x060002AB RID: 683
		public abstract ISettingsBuilder SetSubGroupDelimiter(char value);

		// Token: 0x060002AC RID: 684
		public abstract ISettingsBuilder SetOnPropertyChanged(PropertyChangedEventHandler value);

		// Token: 0x060002AD RID: 685
		public abstract ISettingsBuilder CreateGroup(string name, Action<ISettingsPropertyGroupBuilder> builder);

		// Token: 0x060002AE RID: 686
		public abstract ISettingsBuilder CreatePreset(string id, string name, Action<ISettingsPresetBuilder> builder);

		// Token: 0x060002AF RID: 687
		public abstract ISettingsBuilder WithoutDefaultPreset();

		// Token: 0x060002B0 RID: 688
		public abstract FluentGlobalSettings BuildAsGlobal();

		// Token: 0x060002B1 RID: 689
		public abstract FluentPerSaveSettings BuildAsPerSave();

		// Token: 0x060002B2 RID: 690
		public abstract FluentPerCampaignSettings BuildAsPerCampaign();
	}
}
