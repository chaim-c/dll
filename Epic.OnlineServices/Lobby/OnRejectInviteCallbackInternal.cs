using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003D4 RID: 980
	// (Invoke) Token: 0x0600196A RID: 6506
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRejectInviteCallbackInternal(ref RejectInviteCallbackInfoInternal data);
}
