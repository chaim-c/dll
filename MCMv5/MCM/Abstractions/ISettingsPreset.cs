using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x0200005F RID: 95
	[NullableContext(1)]
	public interface ISettingsPreset
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000226 RID: 550
		string SettingsId { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000227 RID: 551
		string Id { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000228 RID: 552
		string Name { get; }

		// Token: 0x06000229 RID: 553
		BaseSettings LoadPreset();

		// Token: 0x0600022A RID: 554
		bool SavePreset(BaseSettings settings);
	}
}
