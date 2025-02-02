using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	public interface ISettingsContainerHasUnavailable
	{
		// Token: 0x0600019E RID: 414
		IEnumerable<UnavailableSetting> GetUnavailableSettings();
	}
}
