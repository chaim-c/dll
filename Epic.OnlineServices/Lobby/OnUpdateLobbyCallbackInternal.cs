using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003DC RID: 988
	// (Invoke) Token: 0x0600198A RID: 6538
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateLobbyCallbackInternal(ref UpdateLobbyCallbackInfoInternal data);
}
