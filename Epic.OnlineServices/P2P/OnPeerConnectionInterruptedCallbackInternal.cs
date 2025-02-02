using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002CE RID: 718
	// (Invoke) Token: 0x06001344 RID: 4932
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerConnectionInterruptedCallbackInternal(ref OnPeerConnectionInterruptedInfoInternal data);
}
