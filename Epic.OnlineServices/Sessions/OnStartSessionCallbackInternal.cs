using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000108 RID: 264
	// (Invoke) Token: 0x06000826 RID: 2086
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnStartSessionCallbackInternal(ref StartSessionCallbackInfoInternal data);
}
