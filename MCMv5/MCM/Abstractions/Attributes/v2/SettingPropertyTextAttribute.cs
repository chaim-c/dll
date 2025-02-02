using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x020000A3 RID: 163
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyTextAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionText, IPropertyDefinitionBase
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000AF99 File Offset: 0x00009199
		[NullableContext(1)]
		public SettingPropertyTextAttribute(string displayName, int order = -1, bool requireRestart = true, string hintText = "") : base(displayName, order, requireRestart, hintText)
		{
		}
	}
}
