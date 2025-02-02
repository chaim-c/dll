using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200001E RID: 30
	[LibraryInterfaceBase]
	internal interface INativeString
	{
		// Token: 0x06000080 RID: 128
		[EngineMethod("create", false)]
		NativeString Create();

		// Token: 0x06000081 RID: 129
		[EngineMethod("get_string", false)]
		string GetString(NativeString nativeString);

		// Token: 0x06000082 RID: 130
		[EngineMethod("set_string", false)]
		void SetString(NativeString nativeString, string newString);
	}
}
