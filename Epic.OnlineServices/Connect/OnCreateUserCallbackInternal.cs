using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000545 RID: 1349
	// (Invoke) Token: 0x0600229D RID: 8861
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateUserCallbackInternal(ref CreateUserCallbackInfoInternal data);
}
