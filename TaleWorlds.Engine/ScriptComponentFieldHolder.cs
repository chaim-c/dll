using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000056 RID: 86
	[EngineStruct("rglScript_component_field_holder", false)]
	internal struct ScriptComponentFieldHolder
	{
		// Token: 0x040000BA RID: 186
		public MatrixFrame matrixFrame;

		// Token: 0x040000BB RID: 187
		public Vec3 color;

		// Token: 0x040000BC RID: 188
		public Vec3 v3;

		// Token: 0x040000BD RID: 189
		public UIntPtr entityPointer;

		// Token: 0x040000BE RID: 190
		public UIntPtr texturePointer;

		// Token: 0x040000BF RID: 191
		public UIntPtr meshPointer;

		// Token: 0x040000C0 RID: 192
		public UIntPtr materialPointer;

		// Token: 0x040000C1 RID: 193
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string s;

		// Token: 0x040000C2 RID: 194
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string enumValue;

		// Token: 0x040000C3 RID: 195
		public double d;

		// Token: 0x040000C4 RID: 196
		public float f;

		// Token: 0x040000C5 RID: 197
		public int b;

		// Token: 0x040000C6 RID: 198
		public int i;
	}
}
