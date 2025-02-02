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
	// Token: 0x02000032 RID: 50
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyGroupToggleBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyGroupToggleBuilder>, ISettingsPropertyGroupToggleBuilder, ISettingsPropertyBuilder<ISettingsPropertyGroupToggleBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionGroupToggle, IPropertyDefinitionBase
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000064A0 File Offset: 0x000046A0
		public bool IsToggle { get; } = 1;

		// Token: 0x06000147 RID: 327 RVA: 0x000064A8 File Offset: 0x000046A8
		[NullableContext(1)]
		internal DefaultSettingsPropertyGroupToggleBuilder(string id, string name, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000064C4 File Offset: 0x000046C4
		[NullableContext(1)]
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionGroupToggleWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
