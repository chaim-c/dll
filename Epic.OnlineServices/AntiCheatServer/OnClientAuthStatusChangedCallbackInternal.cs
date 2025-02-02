using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005C7 RID: 1479
	// (Invoke) Token: 0x06002605 RID: 9733
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnClientAuthStatusChangedCallbackInternal(ref OnClientAuthStatusChangedCallbackInfoInternal data);
}
