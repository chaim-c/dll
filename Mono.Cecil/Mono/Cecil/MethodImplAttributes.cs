using System;

namespace Mono.Cecil
{
	// Token: 0x020000CE RID: 206
	[Flags]
	public enum MethodImplAttributes : ushort
	{
		// Token: 0x040004F9 RID: 1273
		CodeTypeMask = 3,
		// Token: 0x040004FA RID: 1274
		IL = 0,
		// Token: 0x040004FB RID: 1275
		Native = 1,
		// Token: 0x040004FC RID: 1276
		OPTIL = 2,
		// Token: 0x040004FD RID: 1277
		Runtime = 3,
		// Token: 0x040004FE RID: 1278
		ManagedMask = 4,
		// Token: 0x040004FF RID: 1279
		Unmanaged = 4,
		// Token: 0x04000500 RID: 1280
		Managed = 0,
		// Token: 0x04000501 RID: 1281
		ForwardRef = 16,
		// Token: 0x04000502 RID: 1282
		PreserveSig = 128,
		// Token: 0x04000503 RID: 1283
		InternalCall = 4096,
		// Token: 0x04000504 RID: 1284
		Synchronized = 32,
		// Token: 0x04000505 RID: 1285
		NoOptimization = 64,
		// Token: 0x04000506 RID: 1286
		NoInlining = 8
	}
}
