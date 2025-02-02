using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000632 RID: 1586
	// (Invoke) Token: 0x0600286A RID: 10346
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerAuthStatusChangedCallbackInternal(ref OnClientAuthStatusChangedCallbackInfoInternal data);
}
