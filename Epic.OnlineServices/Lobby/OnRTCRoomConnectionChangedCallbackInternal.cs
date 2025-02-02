using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003D6 RID: 982
	// (Invoke) Token: 0x06001972 RID: 6514
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRTCRoomConnectionChangedCallbackInternal(ref RTCRoomConnectionChangedCallbackInfoInternal data);
}
