using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002CA RID: 714
	// (Invoke) Token: 0x0600131D RID: 4893
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerConnectionEstablishedCallbackInternal(ref OnPeerConnectionEstablishedInfoInternal data);
}
