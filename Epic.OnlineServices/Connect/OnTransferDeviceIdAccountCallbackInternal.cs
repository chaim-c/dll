using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000553 RID: 1363
	// (Invoke) Token: 0x060022D5 RID: 8917
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnTransferDeviceIdAccountCallbackInternal(ref TransferDeviceIdAccountCallbackInfoInternal data);
}
