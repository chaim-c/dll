using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200008F RID: 143
	public sealed class PropertyDefinitionDropdownWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionDropdown, IPropertyDefinitionBase
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000A23A File Offset: 0x0000843A
		public int SelectedIndex { get; }

		// Token: 0x0600031F RID: 799 RVA: 0x0000A244 File Offset: 0x00008444
		[NullableContext(1)]
		public PropertyDefinitionDropdownWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "SelectedIndex", true);
			this.SelectedIndex = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as int?).GetValueOrDefault();
		}
	}
}
