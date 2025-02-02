using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D1 RID: 465
	// (Invoke) Token: 0x06000CEE RID: 3310
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateSendingCallbackInternal(ref UpdateSendingCallbackInfoInternal data);
}
