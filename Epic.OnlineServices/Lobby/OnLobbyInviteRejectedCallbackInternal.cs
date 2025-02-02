using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003C8 RID: 968
	// (Invoke) Token: 0x0600193A RID: 6458
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyInviteRejectedCallbackInternal(ref LobbyInviteRejectedCallbackInfoInternal data);
}
