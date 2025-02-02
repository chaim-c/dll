using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200046D RID: 1133
	// (Invoke) Token: 0x06001CF0 RID: 7408
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAcceptInviteCallbackInternal(ref AcceptInviteCallbackInfoInternal data);
}
