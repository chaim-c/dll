using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200001C RID: 28
	[LibraryInterfaceBase]
	internal interface INativeObjectArray
	{
		// Token: 0x06000077 RID: 119
		[EngineMethod("create", false)]
		NativeObjectArray Create();

		// Token: 0x06000078 RID: 120
		[EngineMethod("get_count", false)]
		int GetCount(UIntPtr pointer);

		// Token: 0x06000079 RID: 121
		[EngineMethod("add_element", false)]
		void AddElement(UIntPtr pointer, UIntPtr nativeObject);

		// Token: 0x0600007A RID: 122
		[EngineMethod("get_element_at_index", false)]
		NativeObject GetElementAtIndex(UIntPtr pointer, int index);

		// Token: 0x0600007B RID: 123
		[EngineMethod("clear", false)]
		void Clear(UIntPtr pointer);
	}
}
