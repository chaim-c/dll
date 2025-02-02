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
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyButtonBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyButtonBuilder>, ISettingsPropertyButtonBuilder, ISettingsPropertyBuilder<ISettingsPropertyButtonBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionButton, IPropertyDefinitionBase
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006343 File Offset: 0x00004543
		public string Content { get; }

		// Token: 0x06000138 RID: 312 RVA: 0x0000634B File Offset: 0x0000454B
		internal DefaultSettingsPropertyButtonBuilder(string id, string name, IRef @ref, string content) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
			this.Content = content;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006368 File Offset: 0x00004568
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionButtonWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
