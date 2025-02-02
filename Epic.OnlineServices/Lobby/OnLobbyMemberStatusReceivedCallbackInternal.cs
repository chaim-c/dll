using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003CA RID: 970
	// (Invoke) Token: 0x06001942 RID: 6466
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyMemberStatusReceivedCallbackInternal(ref LobbyMemberStatusReceivedCallbackInfoInternal data);
}
