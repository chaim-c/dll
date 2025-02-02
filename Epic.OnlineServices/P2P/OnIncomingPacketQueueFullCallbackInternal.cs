using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C6 RID: 710
	// (Invoke) Token: 0x060012F6 RID: 4854
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIncomingPacketQueueFullCallbackInternal(ref OnIncomingPacketQueueFullInfoInternal data);
}
