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
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyFloatingIntegerBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyFloatingIntegerBuilder>, ISettingsPropertyFloatingIntegerBuilder, ISettingsPropertyBuilder<ISettingsPropertyFloatingIntegerBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithActionFormat
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000063C1 File Offset: 0x000045C1
		public decimal MinValue { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000063C9 File Offset: 0x000045C9
		public decimal MaxValue { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000063D1 File Offset: 0x000045D1
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000063D9 File Offset: 0x000045D9
		public string ValueFormat { get; private set; } = string.Empty;

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000063E2 File Offset: 0x000045E2
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000063EA File Offset: 0x000045EA
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

		// Token: 0x06000143 RID: 323 RVA: 0x000063F3 File Offset: 0x000045F3
		internal DefaultSettingsPropertyFloatingIntegerBuilder(string id, string name, float minValue, float maxValue, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
			this.MinValue = (decimal)minValue;
			this.MaxValue = (decimal)maxValue;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006430 File Offset: 0x00004630
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

		// Token: 0x06000145 RID: 325 RVA: 0x0000646B File Offset: 0x0000466B
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
