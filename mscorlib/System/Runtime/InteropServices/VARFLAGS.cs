﻿using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A3 RID: 2467
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum VARFLAGS : short
	{
		// Token: 0x04002C8A RID: 11402
		VARFLAG_FREADONLY = 1,
		// Token: 0x04002C8B RID: 11403
		VARFLAG_FSOURCE = 2,
		// Token: 0x04002C8C RID: 11404
		VARFLAG_FBINDABLE = 4,
		// Token: 0x04002C8D RID: 11405
		VARFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002C8E RID: 11406
		VARFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002C8F RID: 11407
		VARFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002C90 RID: 11408
		VARFLAG_FHIDDEN = 64,
		// Token: 0x04002C91 RID: 11409
		VARFLAG_FRESTRICTED = 128,
		// Token: 0x04002C92 RID: 11410
		VARFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002C93 RID: 11411
		VARFLAG_FUIDEFAULT = 512,
		// Token: 0x04002C94 RID: 11412
		VARFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002C95 RID: 11413
		VARFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002C96 RID: 11414
		VARFLAG_FIMMEDIATEBIND = 4096
	}
}
