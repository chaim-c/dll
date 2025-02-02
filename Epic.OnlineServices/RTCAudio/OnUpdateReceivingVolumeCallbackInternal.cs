using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001CF RID: 463
	// (Invoke) Token: 0x06000CE6 RID: 3302
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateReceivingVolumeCallbackInternal(ref UpdateReceivingVolumeCallbackInfoInternal data);
}
