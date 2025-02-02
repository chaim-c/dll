using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.FluentBuilder.Models
{
	// Token: 0x0200007F RID: 127
	public interface ISettingsPropertyFloatingIntegerBuilder : ISettingsPropertyBuilder<ISettingsPropertyFloatingIntegerBuilder>, ISettingsPropertyBuilder
	{
		// Token: 0x060002D4 RID: 724
		[NullableContext(1)]
		ISettingsPropertyBuilder AddValueFormat(string value);
	}
}
