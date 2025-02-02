using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.FluentBuilder.Models
{
	// Token: 0x02000080 RID: 128
	public interface ISettingsPropertyIntegerBuilder : ISettingsPropertyBuilder<ISettingsPropertyIntegerBuilder>, ISettingsPropertyBuilder
	{
		// Token: 0x060002D5 RID: 725
		[NullableContext(1)]
		ISettingsPropertyBuilder AddValueFormat(string value);
	}
}
