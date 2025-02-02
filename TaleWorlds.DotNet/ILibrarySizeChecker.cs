using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200001A RID: 26
	[LibraryInterfaceBase]
	internal interface ILibrarySizeChecker
	{
		// Token: 0x0600006D RID: 109
		[EngineMethod("get_engine_struct_size", false)]
		int GetEngineStructSize(string str);

		// Token: 0x0600006E RID: 110
		[EngineMethod("get_engine_struct_member_offset", false)]
		IntPtr GetEngineStructMemberOffset(string className, string memberName);
	}
}
