using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000201 RID: 513
	// (Invoke) Token: 0x06000E72 RID: 3698
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnKickCompleteCallbackInternal(ref KickCompleteCallbackInfoInternal data);
}
