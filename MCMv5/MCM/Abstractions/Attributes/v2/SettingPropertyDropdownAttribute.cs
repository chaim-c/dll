using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x020000A0 RID: 160
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyDropdownAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionDropdown, IPropertyDefinitionBase
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public int SelectedIndex { get; }

		// Token: 0x06000366 RID: 870 RVA: 0x0000AED1 File Offset: 0x000090D1
		[NullableContext(1)]
		public SettingPropertyDropdownAttribute(string displayName) : base(displayName, -1, true, "")
		{
		}
	}
}
