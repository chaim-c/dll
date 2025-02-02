using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	public interface ISettingsPropertyBuilder<[Nullable(0)] out TSettingsPropertyBuilder> : ISettingsPropertyBuilder where TSettingsPropertyBuilder : ISettingsPropertyBuilder
	{
		// Token: 0x060002C6 RID: 710
		TSettingsPropertyBuilder SetOrder(int value);

		// Token: 0x060002C7 RID: 711
		TSettingsPropertyBuilder SetRequireRestart(bool value);

		// Token: 0x060002C8 RID: 712
		TSettingsPropertyBuilder SetHintText(string value);
	}
}
