using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes.v1
{
	// Token: 0x020000A4 RID: 164
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingPropertyAttribute : BaseSettingPropertyAttribute, IPropertyDefinitionWithMinMax
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public decimal MinValue { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public decimal MaxValue { get; }

		// Token: 0x06000376 RID: 886 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public SettingPropertyAttribute(string displayName) : base(displayName, -1, true, "")
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000AFCA File Offset: 0x000091CA
		public SettingPropertyAttribute(string displayName, float minValue, float maxValue) : base(displayName, -1, true, "")
		{
			this.MinValue = (decimal)minValue;
			this.MaxValue = (decimal)maxValue;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public SettingPropertyAttribute(string displayName, int minValue, int maxValue) : base(displayName, -1, true, "")
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B01E File Offset: 0x0000921E
		public SettingPropertyAttribute(string displayName, decimal minValue, decimal maxValue) : base(displayName, -1, true, "")
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}
	}
}
