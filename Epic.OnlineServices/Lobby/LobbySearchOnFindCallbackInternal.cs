using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A4 RID: 932
	// (Invoke) Token: 0x0600189A RID: 6298
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void LobbySearchOnFindCallbackInternal(ref LobbySearchFindCallbackInfoInternal data);
}
