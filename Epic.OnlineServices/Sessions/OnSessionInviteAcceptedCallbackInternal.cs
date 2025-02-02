using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000104 RID: 260
	// (Invoke) Token: 0x06000816 RID: 2070
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSessionInviteAcceptedCallbackInternal(ref SessionInviteAcceptedCallbackInfoInternal data);
}
