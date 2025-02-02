using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x02000058 RID: 88
	public interface ISettingsPropertyDefinition : IPropertyDefinitionBase, IPropertyDefinitionBool, IPropertyDefinitionDropdown, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithEditableMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithCustomFormatter, IPropertyDefinitionWithId, IPropertyDefinitionText, IPropertyDefinitionGroupToggle, IPropertyGroupDefinition, IPropertyDefinitionButton
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CF RID: 463
		[Nullable(1)]
		IRef PropertyReference { [NullableContext(1)] get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D0 RID: 464
		SettingType SettingType { get; }

		// Token: 0x060001D1 RID: 465
		[NullableContext(1)]
		SettingsPropertyDefinition Clone(bool keepRefs = true);
	}
}
