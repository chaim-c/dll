using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005C5 RID: 1477
	// (Invoke) Token: 0x060025FD RID: 9725
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnClientActionRequiredCallbackInternal(ref OnClientActionRequiredCallbackInfoInternal data);
}
