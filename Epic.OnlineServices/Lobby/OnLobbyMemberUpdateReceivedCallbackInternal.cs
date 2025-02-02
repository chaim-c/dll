using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003CC RID: 972
	// (Invoke) Token: 0x0600194A RID: 6474
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyMemberUpdateReceivedCallbackInternal(ref LobbyMemberUpdateReceivedCallbackInfoInternal data);
}
