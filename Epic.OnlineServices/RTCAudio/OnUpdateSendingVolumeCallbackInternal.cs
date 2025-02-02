using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D3 RID: 467
	// (Invoke) Token: 0x06000CF6 RID: 3318
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateSendingVolumeCallbackInternal(ref UpdateSendingVolumeCallbackInfoInternal data);
}
