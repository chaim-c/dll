using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingPropertyGroupAttribute : Attribute, IPropertyGroupDefinition
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000AE45 File Offset: 0x00009045
		public static IPropertyGroupDefinition Default
		{
			get
			{
				return new SettingPropertyGroupAttribute(SettingsPropertyGroupDefinition.DefaultGroupName);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000AE51 File Offset: 0x00009051
		public string GroupName { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000AE59 File Offset: 0x00009059
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000AE61 File Offset: 0x00009061
		public int GroupOrder { get; set; }

		// Token: 0x0600035E RID: 862 RVA: 0x0000AE6A File Offset: 0x0000906A
		public SettingPropertyGroupAttribute(string groupName)
		{
			this.GroupName = groupName;
		}
	}
}
