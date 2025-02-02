using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000626 RID: 1574
	// (Invoke) Token: 0x06002828 RID: 10280
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnClientIntegrityViolatedCallbackInternal(ref OnClientIntegrityViolatedCallbackInfoInternal data);
}
