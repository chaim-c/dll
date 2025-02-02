using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200054D RID: 1357
	// (Invoke) Token: 0x060022BD RID: 8893
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLoginStatusChangedCallbackInternal(ref LoginStatusChangedCallbackInfoInternal data);
}
