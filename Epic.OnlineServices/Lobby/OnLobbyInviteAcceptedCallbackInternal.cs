using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003C4 RID: 964
	// (Invoke) Token: 0x0600192A RID: 6442
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyInviteAcceptedCallbackInternal(ref LobbyInviteAcceptedCallbackInfoInternal data);
}
