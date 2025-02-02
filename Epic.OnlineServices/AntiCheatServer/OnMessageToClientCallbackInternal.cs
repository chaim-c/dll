using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005C9 RID: 1481
	// (Invoke) Token: 0x0600260D RID: 9741
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToClientCallbackInternal(ref OnMessageToClientCallbackInfoInternal data);
}
