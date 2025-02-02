using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200001D RID: 29
	[LibraryInterfaceBase]
	internal interface INativeStringHelper
	{
		// Token: 0x0600007C RID: 124
		[EngineMethod("create_rglVarString", false)]
		UIntPtr CreateRglVarString(string text);

		// Token: 0x0600007D RID: 125
		[EngineMethod("get_thread_local_cached_rglVarString", false)]
		UIntPtr GetThreadLocalCachedRglVarString();

		// Token: 0x0600007E RID: 126
		[EngineMethod("set_rglVarString", false)]
		void SetRglVarString(UIntPtr pointer, string text);

		// Token: 0x0600007F RID: 127
		[EngineMethod("delete_rglVarString", false)]
		void DeleteRglVarString(UIntPtr pointer);
	}
}
