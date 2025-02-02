using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.FluentBuilder.Models;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.Implementation.FluentBuilder.Models
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyIntegerBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyIntegerBuilder>, ISettingsPropertyIntegerBuilder, ISettingsPropertyBuilder<ISettingsPropertyIntegerBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithActionFormat
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000064DE File Offset: 0x000046DE
		public decimal MinValue { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000064E6 File Offset: 0x000046E6
		public decimal MaxValue { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000064EE File Offset: 0x000046EE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000064F6 File Offset: 0x000046F6
		public string ValueFormat { get; private set; } = string.Empty;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000064FF File Offset: 0x000046FF
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006507 File Offset: 0x00004707
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<object, string> ValueFormatFunc { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] private set; }

		// Token: 0x0600014F RID: 335 RVA: 0x00006510 File Offset: 0x00004710
		internal DefaultSettingsPropertyIntegerBuilder(string id, string name, int minValue, int maxValue, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000654C File Offset: 0x0000474C
		public ISettingsPropertyBuilder AddValueFormat(string value)
		{
			bool flag = this.ValueFormatFunc != null;
			if (flag)
			{
				throw new InvalidOperationException("AddActionValueFormat was already called!");
			}
			this.ValueFormat = value;
			return base.SettingsPropertyBuilder;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006587 File Offset: 0x00004787
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionWithMinMaxWrapper(this),
				new PropertyDefinitionWithFormatWrapper(this),
				new PropertyDefinitionWithActionFormatWrapper(this),
				new PropertyDefinitionWithCustomFormatterWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
