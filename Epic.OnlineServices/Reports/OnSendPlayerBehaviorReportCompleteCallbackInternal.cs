using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000212 RID: 530
	// (Invoke) Token: 0x06000EEA RID: 3818
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendPlayerBehaviorReportCompleteCallbackInternal(ref SendPlayerBehaviorReportCompleteCallbackInfoInternal data);
}
