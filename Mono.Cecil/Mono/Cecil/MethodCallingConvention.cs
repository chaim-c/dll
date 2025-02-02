using System;

namespace Mono.Cecil
{
	// Token: 0x020000CC RID: 204
	public enum MethodCallingConvention : byte
	{
		// Token: 0x040004E2 RID: 1250
		Default,
		// Token: 0x040004E3 RID: 1251
		C,
		// Token: 0x040004E4 RID: 1252
		StdCall,
		// Token: 0x040004E5 RID: 1253
		ThisCall,
		// Token: 0x040004E6 RID: 1254
		FastCall,
		// Token: 0x040004E7 RID: 1255
		VarArg,
		// Token: 0x040004E8 RID: 1256
		Generic = 16
	}
}
