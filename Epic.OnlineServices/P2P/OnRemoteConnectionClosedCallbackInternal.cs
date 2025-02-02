using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D6 RID: 726
	// (Invoke) Token: 0x0600137E RID: 4990
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRemoteConnectionClosedCallbackInternal(ref OnRemoteConnectionClosedInfoInternal data);
}
