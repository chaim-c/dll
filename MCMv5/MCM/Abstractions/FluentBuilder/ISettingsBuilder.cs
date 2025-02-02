using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.Base.PerSave;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	public interface ISettingsBuilder
	{
		// Token: 0x060002B4 RID: 692
		ISettingsBuilder SetFolderName(string value);

		// Token: 0x060002B5 RID: 693
		ISettingsBuilder SetSubFolder(string value);

		// Token: 0x060002B6 RID: 694
		ISettingsBuilder SetFormat(string value);

		// Token: 0x060002B7 RID: 695
		ISettingsBuilder SetUIVersion(int value);

		// Token: 0x060002B8 RID: 696
		ISettingsBuilder SetSubGroupDelimiter(char value);

		// Token: 0x060002B9 RID: 697
		ISettingsBuilder SetOnPropertyChanged(PropertyChangedEventHandler value);

		// Token: 0x060002BA RID: 698
		ISettingsBuilder CreateGroup(string name, Action<ISettingsPropertyGroupBuilder> builder);

		// Token: 0x060002BB RID: 699
		ISettingsBuilder CreatePreset(string id, string name, Action<ISettingsPresetBuilder> builder);

		// Token: 0x060002BC RID: 700
		ISettingsBuilder WithoutDefaultPreset();

		// Token: 0x060002BD RID: 701
		FluentGlobalSettings BuildAsGlobal();

		// Token: 0x060002BE RID: 702
		FluentPerSaveSettings BuildAsPerSave();

		// Token: 0x060002BF RID: 703
		FluentPerCampaignSettings BuildAsPerCampaign();
	}
}
