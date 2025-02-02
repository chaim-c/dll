using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010A RID: 266
	// (Invoke) Token: 0x0600082E RID: 2094
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnregisterPlayersCallbackInternal(ref UnregisterPlayersCallbackInfoInternal data);
}
