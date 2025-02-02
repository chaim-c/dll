using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	public interface ISettingsContainerPresets
	{
		// Token: 0x0600019F RID: 415
		IEnumerable<ISettingsPreset> GetPresets(string settingsId);

		// Token: 0x060001A0 RID: 416
		bool SavePresets(ISettingsPreset preset);
	}
}
