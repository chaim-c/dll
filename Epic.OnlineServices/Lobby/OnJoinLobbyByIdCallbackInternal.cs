using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003BC RID: 956
	// (Invoke) Token: 0x0600190A RID: 6410
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinLobbyByIdCallbackInternal(ref JoinLobbyByIdCallbackInfoInternal data);
}
