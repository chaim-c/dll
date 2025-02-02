using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014C RID: 332
	// (Invoke) Token: 0x0600099C RID: 2460
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SessionSearchOnFindCallbackInternal(ref SessionSearchFindCallbackInfoInternal data);
}
