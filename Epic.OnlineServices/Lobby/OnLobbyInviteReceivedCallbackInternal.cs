using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003C6 RID: 966
	// (Invoke) Token: 0x06001932 RID: 6450
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyInviteReceivedCallbackInternal(ref LobbyInviteReceivedCallbackInfoInternal data);
}
