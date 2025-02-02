using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions.Properties
{
	// Token: 0x0200006D RID: 109
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoneSettingsPropertyDiscoverer : ISettingsPropertyDiscoverer
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009A30 File Offset: 0x00007C30
		public IEnumerable<string> DiscoveryTypes { get; } = new string[]
		{
			"none"
		};

		// Token: 0x06000271 RID: 625 RVA: 0x00009A38 File Offset: 0x00007C38
		public IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings)
		{
			return Enumerable.Empty<ISettingsPropertyDefinition>();
		}
	}
}
