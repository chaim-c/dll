using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003D8 RID: 984
	// (Invoke) Token: 0x0600197A RID: 6522
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendInviteCallbackInternal(ref SendInviteCallbackInfoInternal data);
}
