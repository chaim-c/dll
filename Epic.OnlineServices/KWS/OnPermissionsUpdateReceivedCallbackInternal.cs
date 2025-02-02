using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000436 RID: 1078
	// (Invoke) Token: 0x06001BB8 RID: 7096
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPermissionsUpdateReceivedCallbackInternal(ref PermissionsUpdateReceivedCallbackInfoInternal data);
}
