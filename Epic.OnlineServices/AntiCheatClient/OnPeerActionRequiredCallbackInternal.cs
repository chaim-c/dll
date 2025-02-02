using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000630 RID: 1584
	// (Invoke) Token: 0x06002862 RID: 10338
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerActionRequiredCallbackInternal(ref OnClientActionRequiredCallbackInfoInternal data);
}
