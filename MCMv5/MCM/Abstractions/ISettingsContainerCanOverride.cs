using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	public interface ISettingsContainerCanOverride
	{
		// Token: 0x06000199 RID: 409
		bool OverrideSettings(BaseSettings settings);
	}
}
