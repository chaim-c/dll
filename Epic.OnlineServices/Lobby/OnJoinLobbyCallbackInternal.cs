using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003BE RID: 958
	// (Invoke) Token: 0x06001912 RID: 6418
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinLobbyCallbackInternal(ref JoinLobbyCallbackInfoInternal data);
}
