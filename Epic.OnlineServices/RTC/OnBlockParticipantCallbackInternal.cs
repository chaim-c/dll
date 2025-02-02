using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000187 RID: 391
	// (Invoke) Token: 0x06000B42 RID: 2882
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnBlockParticipantCallbackInternal(ref BlockParticipantCallbackInfoInternal data);
}
