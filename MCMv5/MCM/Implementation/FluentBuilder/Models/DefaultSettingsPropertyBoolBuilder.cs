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
	// Token: 0x0200002E RID: 46
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyBoolBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyBoolBuilder>, ISettingsPropertyBoolBuilder, ISettingsPropertyBuilder<ISettingsPropertyBoolBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionBool, IPropertyDefinitionBase
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00006314 File Offset: 0x00004514
		[NullableContext(1)]
		internal DefaultSettingsPropertyBoolBuilder(string id, string name, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006329 File Offset: 0x00004529
		[NullableContext(1)]
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionBoolWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
