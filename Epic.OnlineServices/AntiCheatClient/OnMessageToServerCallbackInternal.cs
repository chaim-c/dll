using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200062C RID: 1580
	// (Invoke) Token: 0x0600284B RID: 10315
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToServerCallbackInternal(ref OnMessageToServerCallbackInfoInternal data);
}
