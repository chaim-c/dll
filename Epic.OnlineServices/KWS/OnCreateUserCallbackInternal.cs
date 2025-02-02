using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000434 RID: 1076
	// (Invoke) Token: 0x06001BB0 RID: 7088
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateUserCallbackInternal(ref CreateUserCallbackInfoInternal data);
}
