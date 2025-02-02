using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	public interface ISettingsContainer
	{
		// Token: 0x06000195 RID: 405
		[return: Nullable(2)]
		BaseSettings GetSettings(string id);

		// Token: 0x06000196 RID: 406
		bool SaveSettings(BaseSettings settings);
	}
}
