using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003CE RID: 974
	// (Invoke) Token: 0x06001952 RID: 6482
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyUpdateReceivedCallbackInternal(ref LobbyUpdateReceivedCallbackInfoInternal data);
}
