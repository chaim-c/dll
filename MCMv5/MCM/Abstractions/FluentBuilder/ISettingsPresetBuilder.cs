using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	public interface ISettingsPresetBuilder
	{
		// Token: 0x060002C1 RID: 705
		ISettingsPresetBuilder SetPropertyValue(string propertyName, [Nullable(2)] object value);

		// Token: 0x060002C2 RID: 706
		ISettingsPreset Build(BaseSettings settings);
	}
}
