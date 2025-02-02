using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200001B RID: 27
	[LibraryInterfaceBase]
	internal interface INativeArray
	{
		// Token: 0x0600006F RID: 111
		[EngineMethod("get_data_pointer_offset", false)]
		int GetDataPointerOffset();

		// Token: 0x06000070 RID: 112
		[EngineMethod("create", false)]
		NativeArray Create();

		// Token: 0x06000071 RID: 113
		[EngineMethod("get_data_size", false)]
		int GetDataSize(UIntPtr pointer);

		// Token: 0x06000072 RID: 114
		[EngineMethod("get_data_pointer", false)]
		UIntPtr GetDataPointer(UIntPtr pointer);

		// Token: 0x06000073 RID: 115
		[EngineMethod("add_integer_element", false)]
		void AddIntegerElement(UIntPtr pointer, int value);

		// Token: 0x06000074 RID: 116
		[EngineMethod("add_float_element", false)]
		void AddFloatElement(UIntPtr pointer, float value);

		// Token: 0x06000075 RID: 117
		[EngineMethod("add_element", false)]
		void AddElement(UIntPtr pointer, IntPtr element, int elementSize);

		// Token: 0x06000076 RID: 118
		[EngineMethod("clear", false)]
		void Clear(UIntPtr pointer);
	}
}
