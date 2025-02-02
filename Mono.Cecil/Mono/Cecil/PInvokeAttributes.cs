using System;

namespace Mono.Cecil
{
	// Token: 0x020000D4 RID: 212
	[Flags]
	public enum PInvokeAttributes : ushort
	{
		// Token: 0x04000526 RID: 1318
		NoMangle = 1,
		// Token: 0x04000527 RID: 1319
		CharSetMask = 6,
		// Token: 0x04000528 RID: 1320
		CharSetNotSpec = 0,
		// Token: 0x04000529 RID: 1321
		CharSetAnsi = 2,
		// Token: 0x0400052A RID: 1322
		CharSetUnicode = 4,
		// Token: 0x0400052B RID: 1323
		CharSetAuto = 6,
		// Token: 0x0400052C RID: 1324
		SupportsLastError = 64,
		// Token: 0x0400052D RID: 1325
		CallConvMask = 1792,
		// Token: 0x0400052E RID: 1326
		CallConvWinapi = 256,
		// Token: 0x0400052F RID: 1327
		CallConvCdecl = 512,
		// Token: 0x04000530 RID: 1328
		CallConvStdCall = 768,
		// Token: 0x04000531 RID: 1329
		CallConvThiscall = 1024,
		// Token: 0x04000532 RID: 1330
		CallConvFastcall = 1280,
		// Token: 0x04000533 RID: 1331
		BestFitMask = 48,
		// Token: 0x04000534 RID: 1332
		BestFitEnabled = 16,
		// Token: 0x04000535 RID: 1333
		BestFitDisabled = 32,
		// Token: 0x04000536 RID: 1334
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04000537 RID: 1335
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04000538 RID: 1336
		ThrowOnUnmappableCharDisabled = 8192
	}
}
