using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003DA RID: 986
	// (Invoke) Token: 0x06001982 RID: 6530
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendLobbyNativeInviteRequestedCallbackInternal(ref SendLobbyNativeInviteRequestedCallbackInfoInternal data);
}
