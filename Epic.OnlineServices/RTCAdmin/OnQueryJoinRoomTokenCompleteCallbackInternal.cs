using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000203 RID: 515
	// (Invoke) Token: 0x06000E7A RID: 3706
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryJoinRoomTokenCompleteCallbackInternal(ref QueryJoinRoomTokenCompleteCallbackInfoInternal data);
}
