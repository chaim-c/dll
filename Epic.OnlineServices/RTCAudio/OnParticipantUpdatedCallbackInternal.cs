using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000CCE RID: 3278
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnParticipantUpdatedCallbackInternal(ref ParticipantUpdatedCallbackInfoInternal data);
}
