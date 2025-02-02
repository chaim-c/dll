using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	public interface ISettingsContainerCanReset
	{
		// Token: 0x0600019A RID: 410
		bool ResetSettings(BaseSettings settings);
	}
}
