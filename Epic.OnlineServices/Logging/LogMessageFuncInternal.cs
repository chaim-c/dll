using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200031D RID: 797
	// (Invoke) Token: 0x0600155C RID: 5468
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void LogMessageFuncInternal(ref LogMessageInternal message);
}
