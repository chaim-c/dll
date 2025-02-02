using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base
{
	// Token: 0x020000AC RID: 172
	[NullableContext(1)]
	[Obsolete("Will be internal in the future")]
	public interface IFluentSettings
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000391 RID: 913
		List<SettingsPropertyGroupDefinition> SettingPropertyGroups { get; }
	}
}
