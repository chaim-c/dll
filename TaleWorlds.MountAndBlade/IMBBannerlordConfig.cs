using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BA RID: 442
	[ScriptingInterfaceBase]
	internal interface IMBBannerlordConfig
	{
		// Token: 0x060017DE RID: 6110
		[EngineMethod("validate_options", false)]
		void ValidateOptions();
	}
}
