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
	// Token: 0x02000030 RID: 48
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class DefaultSettingsPropertyDropdownBuilder : BaseDefaultSettingsPropertyBuilder<ISettingsPropertyDropdownBuilder>, ISettingsPropertyDropdownBuilder, ISettingsPropertyBuilder<ISettingsPropertyDropdownBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionDropdown, IPropertyDefinitionBase
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006382 File Offset: 0x00004582
		public int SelectedIndex { get; }

		// Token: 0x0600013B RID: 315 RVA: 0x0000638A File Offset: 0x0000458A
		[NullableContext(1)]
		internal DefaultSettingsPropertyDropdownBuilder(string id, string name, int selectedIndex, IRef @ref) : base(id, name, @ref)
		{
			base.SettingsPropertyBuilder = this;
			this.SelectedIndex = selectedIndex;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000063A7 File Offset: 0x000045A7
		[NullableContext(1)]
		public override IEnumerable<IPropertyDefinitionBase> GetDefinitions()
		{
			return new IPropertyDefinitionBase[]
			{
				new PropertyDefinitionDropdownWrapper(this),
				new PropertyDefinitionWithIdWrapper(this)
			};
		}
	}
}
