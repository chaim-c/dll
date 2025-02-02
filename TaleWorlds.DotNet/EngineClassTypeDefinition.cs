using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000011 RID: 17
	[EngineStruct("ftlObject_type_definition", false)]
	internal struct EngineClassTypeDefinition
	{
		// Token: 0x04000029 RID: 41
		public int TypeId;

		// Token: 0x0400002A RID: 42
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string TypeName;
	}
}
