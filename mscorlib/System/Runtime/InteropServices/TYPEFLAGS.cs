﻿using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000992 RID: 2450
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		// Token: 0x04002C0A RID: 11274
		TYPEFLAG_FAPPOBJECT = 1,
		// Token: 0x04002C0B RID: 11275
		TYPEFLAG_FCANCREATE = 2,
		// Token: 0x04002C0C RID: 11276
		TYPEFLAG_FLICENSED = 4,
		// Token: 0x04002C0D RID: 11277
		TYPEFLAG_FPREDECLID = 8,
		// Token: 0x04002C0E RID: 11278
		TYPEFLAG_FHIDDEN = 16,
		// Token: 0x04002C0F RID: 11279
		TYPEFLAG_FCONTROL = 32,
		// Token: 0x04002C10 RID: 11280
		TYPEFLAG_FDUAL = 64,
		// Token: 0x04002C11 RID: 11281
		TYPEFLAG_FNONEXTENSIBLE = 128,
		// Token: 0x04002C12 RID: 11282
		TYPEFLAG_FOLEAUTOMATION = 256,
		// Token: 0x04002C13 RID: 11283
		TYPEFLAG_FRESTRICTED = 512,
		// Token: 0x04002C14 RID: 11284
		TYPEFLAG_FAGGREGATABLE = 1024,
		// Token: 0x04002C15 RID: 11285
		TYPEFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002C16 RID: 11286
		TYPEFLAG_FDISPATCHABLE = 4096,
		// Token: 0x04002C17 RID: 11287
		TYPEFLAG_FREVERSEBIND = 8192,
		// Token: 0x04002C18 RID: 11288
		TYPEFLAG_FPROXY = 16384
	}
}
