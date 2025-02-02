using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003C2 RID: 962
	// (Invoke) Token: 0x06001922 RID: 6434
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLeaveLobbyCallbackInternal(ref LeaveLobbyCallbackInfoInternal data);
}
