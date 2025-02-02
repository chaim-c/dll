using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000064 RID: 100
	// (Invoke) Token: 0x06000480 RID: 1152
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnShowReportPlayerCallbackInternal(ref OnShowReportPlayerCallbackInfoInternal data);
}
