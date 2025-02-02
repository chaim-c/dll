using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C2 RID: 706
	// (Invoke) Token: 0x060012D7 RID: 4823
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIncomingConnectionRequestCallbackInternal(ref OnIncomingConnectionRequestInfoInternal data);
}
