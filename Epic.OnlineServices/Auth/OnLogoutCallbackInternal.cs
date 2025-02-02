using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200059A RID: 1434
	// (Invoke) Token: 0x060024B8 RID: 9400
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLogoutCallbackInternal(ref LogoutCallbackInfoInternal data);
}
