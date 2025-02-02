using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200059C RID: 1436
	// (Invoke) Token: 0x060024C0 RID: 9408
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryIdTokenCallbackInternal(ref QueryIdTokenCallbackInfoInternal data);
}
