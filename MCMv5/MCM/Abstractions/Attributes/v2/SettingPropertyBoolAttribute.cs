using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x0200009E RID: 158
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyBoolAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionBool, IPropertyDefinitionBase, IPropertyDefinitionGroupToggle
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000AE7B File Offset: 0x0000907B
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000AE83 File Offset: 0x00009083
		public bool IsToggle { get; set; }

		// Token: 0x06000361 RID: 865 RVA: 0x0000AE8C File Offset: 0x0000908C
		[NullableContext(1)]
		public SettingPropertyBoolAttribute(string displayName) : base(displayName, -1, true, "")
		{
		}
	}
}
