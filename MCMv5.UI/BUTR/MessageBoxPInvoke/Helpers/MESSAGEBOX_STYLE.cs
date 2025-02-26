﻿using System;

namespace BUTR.MessageBoxPInvoke.Helpers
{
	// Token: 0x0200005D RID: 93
	[Flags]
	internal enum MESSAGEBOX_STYLE : uint
	{
		// Token: 0x04000102 RID: 258
		MB_ABORTRETRYIGNORE = 2U,
		// Token: 0x04000103 RID: 259
		MB_CANCELTRYCONTINUE = 6U,
		// Token: 0x04000104 RID: 260
		MB_HELP = 16384U,
		// Token: 0x04000105 RID: 261
		MB_OK = 0U,
		// Token: 0x04000106 RID: 262
		MB_OKCANCEL = 1U,
		// Token: 0x04000107 RID: 263
		MB_RETRYCANCEL = 5U,
		// Token: 0x04000108 RID: 264
		MB_YESNO = 4U,
		// Token: 0x04000109 RID: 265
		MB_YESNOCANCEL = 3U,
		// Token: 0x0400010A RID: 266
		MB_ICONHAND = 16U,
		// Token: 0x0400010B RID: 267
		MB_ICONQUESTION = 32U,
		// Token: 0x0400010C RID: 268
		MB_ICONEXCLAMATION = 48U,
		// Token: 0x0400010D RID: 269
		MB_ICONASTERISK = 64U,
		// Token: 0x0400010E RID: 270
		MB_USERICON = 128U,
		// Token: 0x0400010F RID: 271
		MB_ICONWARNING = 48U,
		// Token: 0x04000110 RID: 272
		MB_ICONERROR = 16U,
		// Token: 0x04000111 RID: 273
		MB_ICONINFORMATION = 64U,
		// Token: 0x04000112 RID: 274
		MB_ICONSTOP = 16U,
		// Token: 0x04000113 RID: 275
		MB_DEFBUTTON1 = 0U,
		// Token: 0x04000114 RID: 276
		MB_DEFBUTTON2 = 256U,
		// Token: 0x04000115 RID: 277
		MB_DEFBUTTON3 = 512U,
		// Token: 0x04000116 RID: 278
		MB_DEFBUTTON4 = 768U,
		// Token: 0x04000117 RID: 279
		MB_APPLMODAL = 0U,
		// Token: 0x04000118 RID: 280
		MB_SYSTEMMODAL = 4096U,
		// Token: 0x04000119 RID: 281
		MB_TASKMODAL = 8192U,
		// Token: 0x0400011A RID: 282
		MB_NOFOCUS = 32768U,
		// Token: 0x0400011B RID: 283
		MB_SETFOREGROUND = 65536U,
		// Token: 0x0400011C RID: 284
		MB_DEFAULT_DESKTOP_ONLY = 131072U,
		// Token: 0x0400011D RID: 285
		MB_TOPMOST = 262144U,
		// Token: 0x0400011E RID: 286
		MB_RIGHT = 524288U,
		// Token: 0x0400011F RID: 287
		MB_RTLREADING = 1048576U,
		// Token: 0x04000120 RID: 288
		MB_SERVICE_NOTIFICATION = 2097152U,
		// Token: 0x04000121 RID: 289
		MB_SERVICE_NOTIFICATION_NT3X = 262144U,
		// Token: 0x04000122 RID: 290
		MB_TYPEMASK = 15U,
		// Token: 0x04000123 RID: 291
		MB_ICONMASK = 240U,
		// Token: 0x04000124 RID: 292
		MB_DEFMASK = 3840U,
		// Token: 0x04000125 RID: 293
		MB_MODEMASK = 12288U,
		// Token: 0x04000126 RID: 294
		MB_MISCMASK = 49152U
	}
}
