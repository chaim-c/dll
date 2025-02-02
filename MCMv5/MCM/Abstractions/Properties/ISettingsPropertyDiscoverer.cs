using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions.Properties
{
	// Token: 0x0200006C RID: 108
	[NullableContext(1)]
	public interface ISettingsPropertyDiscoverer
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026E RID: 622
		IEnumerable<string> DiscoveryTypes { get; }

		// Token: 0x0600026F RID: 623
		IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings);
	}
}
