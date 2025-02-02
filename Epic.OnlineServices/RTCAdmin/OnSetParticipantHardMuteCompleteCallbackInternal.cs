using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000205 RID: 517
	// (Invoke) Token: 0x06000E82 RID: 3714
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSetParticipantHardMuteCompleteCallbackInternal(ref SetParticipantHardMuteCompleteCallbackInfoInternal data);
}
