using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B4 RID: 436
	[ScriptingInterfaceBase]
	internal interface IMBGame
	{
		// Token: 0x060017A8 RID: 6056
		[EngineMethod("start_new", false)]
		void StartNew();

		// Token: 0x060017A9 RID: 6057
		[EngineMethod("load_module_data", false)]
		void LoadModuleData(bool isLoadGame);
	}
}
