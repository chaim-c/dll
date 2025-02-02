using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B8 RID: 440
	[ScriptingInterfaceBase]
	internal interface IMBBannerlordChecker
	{
		// Token: 0x060017D9 RID: 6105
		[EngineMethod("get_engine_struct_size", false)]
		int GetEngineStructSize(string str);

		// Token: 0x060017DA RID: 6106
		[EngineMethod("get_engine_struct_member_offset", false)]
		IntPtr GetEngineStructMemberOffset(string className, string memberName);
	}
}
