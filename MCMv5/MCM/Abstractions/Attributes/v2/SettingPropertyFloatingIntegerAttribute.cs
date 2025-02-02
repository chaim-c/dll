using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyFloatingIntegerAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithCustomFormatter
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public decimal MinValue { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000AEEB File Offset: 0x000090EB
		public decimal MaxValue { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000AEF3 File Offset: 0x000090F3
		[Nullable(1)]
		public string ValueFormat { [NullableContext(1)] get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000AEFB File Offset: 0x000090FB
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000AF03 File Offset: 0x00009103
		[Nullable(2)]
		public Type CustomFormatter { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x0600036C RID: 876 RVA: 0x0000AF0C File Offset: 0x0000910C
		[NullableContext(1)]
		public SettingPropertyFloatingIntegerAttribute(string displayName, float minValue, float maxValue, string valueFormat = "0.00") : base(displayName, -1, true, "")
		{
			this.MinValue = (decimal)minValue;
			this.MaxValue = (decimal)maxValue;
			this.ValueFormat = valueFormat;
		}
	}
}
