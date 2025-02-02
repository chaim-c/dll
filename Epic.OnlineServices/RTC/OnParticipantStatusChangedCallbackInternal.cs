using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200018F RID: 399
	// (Invoke) Token: 0x06000B62 RID: 2914
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnParticipantStatusChangedCallbackInternal(ref ParticipantStatusChangedCallbackInfoInternal data);
}
