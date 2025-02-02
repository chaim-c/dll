using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000189 RID: 393
	// (Invoke) Token: 0x06000B4A RID: 2890
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDisconnectedCallbackInternal(ref DisconnectedCallbackInfoInternal data);
}
