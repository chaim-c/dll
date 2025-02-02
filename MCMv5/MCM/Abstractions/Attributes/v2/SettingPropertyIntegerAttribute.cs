using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v2
{
	// Token: 0x020000A2 RID: 162
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingPropertyIntegerAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithCustomFormatter
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000AF3E File Offset: 0x0000913E
		public decimal MinValue { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000AF46 File Offset: 0x00009146
		public decimal MaxValue { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000AF4E File Offset: 0x0000914E
		[Nullable(1)]
		public string ValueFormat { [NullableContext(1)] get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000AF56 File Offset: 0x00009156
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000AF5E File Offset: 0x0000915E
		[Nullable(2)]
		public Type CustomFormatter { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x06000372 RID: 882 RVA: 0x0000AF67 File Offset: 0x00009167
		[NullableContext(1)]
		public SettingPropertyIntegerAttribute(string displayName, int minValue, int maxValue, string valueFormat = "0") : base(displayName, -1, true, "")
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.ValueFormat = valueFormat;
		}
	}
}
