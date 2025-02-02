using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x0200009F RID: 159
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyButtonAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionButton, IPropertyDefinitionBase
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000AE9E File Offset: 0x0000909E
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000AEA6 File Offset: 0x000090A6
		public string Content { get; set; } = string.Empty;

		// Token: 0x06000364 RID: 868 RVA: 0x0000AEAF File Offset: 0x000090AF
		public SettingPropertyButtonAttribute(string displayName, int order = -1, bool requireRestart = true, string hintText = "") : base(displayName, order, requireRestart, hintText)
		{
		}
	}
}
