using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000106 RID: 262
	// (Invoke) Token: 0x0600081E RID: 2078
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSessionInviteReceivedCallbackInternal(ref SessionInviteReceivedCallbackInfoInternal data);
}
