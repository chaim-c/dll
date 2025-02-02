using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200062A RID: 1578
	// (Invoke) Token: 0x06002843 RID: 10307
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToPeerCallbackInternal(ref OnMessageToClientCallbackInfoInternal data);
}
