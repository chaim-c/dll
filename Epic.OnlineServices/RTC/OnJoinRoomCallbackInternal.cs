using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200018B RID: 395
	// (Invoke) Token: 0x06000B52 RID: 2898
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinRoomCallbackInternal(ref JoinRoomCallbackInfoInternal data);
}
