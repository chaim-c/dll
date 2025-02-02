using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003BA RID: 954
	// (Invoke) Token: 0x06001902 RID: 6402
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinLobbyAcceptedCallbackInternal(ref JoinLobbyAcceptedCallbackInfoInternal data);
}
