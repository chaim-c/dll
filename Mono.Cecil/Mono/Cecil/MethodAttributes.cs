using System;

namespace Mono.Cecil
{
	// Token: 0x020000CB RID: 203
	[Flags]
	public enum MethodAttributes : ushort
	{
		// Token: 0x040004CA RID: 1226
		MemberAccessMask = 7,
		// Token: 0x040004CB RID: 1227
		CompilerControlled = 0,
		// Token: 0x040004CC RID: 1228
		Private = 1,
		// Token: 0x040004CD RID: 1229
		FamANDAssem = 2,
		// Token: 0x040004CE RID: 1230
		Assembly = 3,
		// Token: 0x040004CF RID: 1231
		Family = 4,
		// Token: 0x040004D0 RID: 1232
		FamORAssem = 5,
		// Token: 0x040004D1 RID: 1233
		Public = 6,
		// Token: 0x040004D2 RID: 1234
		Static = 16,
		// Token: 0x040004D3 RID: 1235
		Final = 32,
		// Token: 0x040004D4 RID: 1236
		Virtual = 64,
		// Token: 0x040004D5 RID: 1237
		HideBySig = 128,
		// Token: 0x040004D6 RID: 1238
		VtableLayoutMask = 256,
		// Token: 0x040004D7 RID: 1239
		ReuseSlot = 0,
		// Token: 0x040004D8 RID: 1240
		NewSlot = 256,
		// Token: 0x040004D9 RID: 1241
		CheckAccessOnOverride = 512,
		// Token: 0x040004DA RID: 1242
		Abstract = 1024,
		// Token: 0x040004DB RID: 1243
		SpecialName = 2048,
		// Token: 0x040004DC RID: 1244
		PInvokeImpl = 8192,
		// Token: 0x040004DD RID: 1245
		UnmanagedExport = 8,
		// Token: 0x040004DE RID: 1246
		RTSpecialName = 4096,
		// Token: 0x040004DF RID: 1247
		HasSecurity = 16384,
		// Token: 0x040004E0 RID: 1248
		RequireSecObject = 32768
	}
}
