using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	public interface ISettingsContainerHasSettingsDefinitions
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019B RID: 411
		IEnumerable<SettingsDefinition> SettingsDefinitions { get; }
	}
}
