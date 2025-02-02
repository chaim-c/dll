using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001CB RID: 459
	// (Invoke) Token: 0x06000CD6 RID: 3286
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateParticipantVolumeCallbackInternal(ref UpdateParticipantVolumeCallbackInfoInternal data);
}
