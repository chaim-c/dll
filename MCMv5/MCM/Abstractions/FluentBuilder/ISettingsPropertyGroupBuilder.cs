using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.FluentBuilder.Models;
using MCM.Common;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	public interface ISettingsPropertyGroupBuilder
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C9 RID: 713
		Dictionary<string, ISettingsPropertyBuilder> Properties { get; }

		// Token: 0x060002CA RID: 714
		ISettingsPropertyGroupBuilder SetGroupOrder(int value);

		// Token: 0x060002CB RID: 715
		ISettingsPropertyGroupBuilder AddToggle(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyGroupToggleBuilder> builder);

		// Token: 0x060002CC RID: 716
		ISettingsPropertyGroupBuilder AddBool(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyBoolBuilder> builder);

		// Token: 0x060002CD RID: 717
		ISettingsPropertyGroupBuilder AddDropdown(string id, string name, int selectedIndex, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyDropdownBuilder> builder);

		// Token: 0x060002CE RID: 718
		ISettingsPropertyGroupBuilder AddInteger(string id, string name, int minValue, int maxValue, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyIntegerBuilder> builder);

		// Token: 0x060002CF RID: 719
		ISettingsPropertyGroupBuilder AddFloatingInteger(string id, string name, float minValue, float maxValue, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyFloatingIntegerBuilder> builder);

		// Token: 0x060002D0 RID: 720
		ISettingsPropertyGroupBuilder AddText(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyTextBuilder> builder);

		// Token: 0x060002D1 RID: 721
		ISettingsPropertyGroupBuilder AddButton(string id, string name, IRef @ref, string content, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyButtonBuilder> builder);

		// Token: 0x060002D2 RID: 722
		ISettingsPropertyGroupBuilder AddCustom<[Nullable(0)] TSettingsPropertyBuilder>(ISettingsPropertyBuilder<TSettingsPropertyBuilder> builder) where TSettingsPropertyBuilder : ISettingsPropertyBuilder;

		// Token: 0x060002D3 RID: 723
		IPropertyGroupDefinition GetPropertyGroupDefinition();
	}
}
