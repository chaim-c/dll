using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200018D RID: 397
	// (Invoke) Token: 0x06000B5A RID: 2906
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLeaveRoomCallbackInternal(ref LeaveRoomCallbackInfoInternal data);
}
