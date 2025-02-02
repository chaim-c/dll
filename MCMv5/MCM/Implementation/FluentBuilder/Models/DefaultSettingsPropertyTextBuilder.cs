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
	// Token: 0x02000034 RID: 52
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyTextBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyTextBuilder>, ISettingsPropertyTextBuilder, ISettingsPropertyBuilder<ISettingsPropertyTextBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionText, IPropertyDefinitionBase
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000065BC File Offset: 0x000047BC
		[NullableContext(1)]
		internal DefaultSettingsPropertyTextBuilder(string id, string name, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000065D1 File Offset: 0x000047D1
		[NullableContext(1)]
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionTextWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
