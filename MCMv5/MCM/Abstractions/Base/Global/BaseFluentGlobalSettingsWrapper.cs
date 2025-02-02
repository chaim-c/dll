using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000BC RID: 188
	[NullableContext(1)]
	[Nullable(0)]
	[Obsolete("Will be removed from future API", true)]
	public abstract class BaseFluentGlobalSettingsWrapper : FluentGlobalSettings, IWrapper
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000BD30 File Offset: 0x00009F30
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000BD38 File Offset: 0x00009F38
		public object Object { get; protected set; }

		// Token: 0x060003EA RID: 1002 RVA: 0x0000BD44 File Offset: 0x00009F44
		protected BaseFluentGlobalSettingsWrapper(object @object, string id, string displayName, string folderName, string subFolder, string format, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets) : base(id, displayName, folderName, subFolder, format, uiVersion, subGroupDelimiter, onPropertyChanged, settingPropertyGroups, presets)
		{
			this.Object = @object;
		}
	}
}
