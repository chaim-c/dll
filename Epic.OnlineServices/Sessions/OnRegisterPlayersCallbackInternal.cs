using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FE RID: 254
	// (Invoke) Token: 0x060007FE RID: 2046
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRegisterPlayersCallbackInternal(ref RegisterPlayersCallbackInfoInternal data);
}
