using System;

namespace Mono.Cecil
{
	// Token: 0x020000D1 RID: 209
	[Flags]
	public enum ParameterAttributes : ushort
	{
		// Token: 0x04000513 RID: 1299
		None = 0,
		// Token: 0x04000514 RID: 1300
		In = 1,
		// Token: 0x04000515 RID: 1301
		Out = 2,
		// Token: 0x04000516 RID: 1302
		Lcid = 4,
		// Token: 0x04000517 RID: 1303
		Retval = 8,
		// Token: 0x04000518 RID: 1304
		Optional = 16,
		// Token: 0x04000519 RID: 1305
		HasDefault = 4096,
		// Token: 0x0400051A RID: 1306
		HasFieldMarshal = 8192,
		// Token: 0x0400051B RID: 1307
		Unused = 53216
	}
}
