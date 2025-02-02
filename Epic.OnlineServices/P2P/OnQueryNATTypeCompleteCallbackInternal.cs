using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D2 RID: 722
	// (Invoke) Token: 0x06001363 RID: 4963
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryNATTypeCompleteCallbackInternal(ref OnQueryNATTypeCompleteInfoInternal data);
}
