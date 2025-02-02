using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000598 RID: 1432
	// (Invoke) Token: 0x060024B0 RID: 9392
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLoginStatusChangedCallbackInternal(ref LoginStatusChangedCallbackInfoInternal data);
}
