using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B6 RID: 950
	// (Invoke) Token: 0x060018F2 RID: 6386
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDestroyLobbyCallbackInternal(ref DestroyLobbyCallbackInfoInternal data);
}
