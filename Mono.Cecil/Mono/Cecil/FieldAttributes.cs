using System;

namespace Mono.Cecil
{
	// Token: 0x020000C7 RID: 199
	[Flags]
	public enum FieldAttributes : ushort
	{
		// Token: 0x040004AB RID: 1195
		FieldAccessMask = 7,
		// Token: 0x040004AC RID: 1196
		CompilerControlled = 0,
		// Token: 0x040004AD RID: 1197
		Private = 1,
		// Token: 0x040004AE RID: 1198
		FamANDAssem = 2,
		// Token: 0x040004AF RID: 1199
		Assembly = 3,
		// Token: 0x040004B0 RID: 1200
		Family = 4,
		// Token: 0x040004B1 RID: 1201
		FamORAssem = 5,
		// Token: 0x040004B2 RID: 1202
		Public = 6,
		// Token: 0x040004B3 RID: 1203
		Static = 16,
		// Token: 0x040004B4 RID: 1204
		InitOnly = 32,
		// Token: 0x040004B5 RID: 1205
		Literal = 64,
		// Token: 0x040004B6 RID: 1206
		NotSerialized = 128,
		// Token: 0x040004B7 RID: 1207
		SpecialName = 512,
		// Token: 0x040004B8 RID: 1208
		PInvokeImpl = 8192,
		// Token: 0x040004B9 RID: 1209
		RTSpecialName = 1024,
		// Token: 0x040004BA RID: 1210
		HasFieldMarshal = 4096,
		// Token: 0x040004BB RID: 1211
		HasDefault = 32768,
		// Token: 0x040004BC RID: 1212
		HasFieldRVA = 256
	}
}
