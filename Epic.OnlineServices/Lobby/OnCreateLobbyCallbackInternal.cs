using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B4 RID: 948
	// (Invoke) Token: 0x060018EA RID: 6378
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateLobbyCallbackInternal(ref CreateLobbyCallbackInfoInternal data);
}
