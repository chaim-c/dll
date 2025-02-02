using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000058 RID: 88
	// (Invoke) Token: 0x06000436 RID: 1078
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDisplaySettingsUpdatedCallbackInternal(ref OnDisplaySettingsUpdatedCallbackInfoInternal data);
}
