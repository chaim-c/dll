using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000039 RID: 57
	[ApplicationInterfaceBase]
	internal interface IEngineSizeChecker
	{
		// Token: 0x0600051A RID: 1306
		[EngineMethod("get_engine_struct_size", false)]
		int GetEngineStructSize(string str);

		// Token: 0x0600051B RID: 1307
		[EngineMethod("get_engine_struct_member_offset", false)]
		IntPtr GetEngineStructMemberOffset(string className, string memberName);
	}
}
