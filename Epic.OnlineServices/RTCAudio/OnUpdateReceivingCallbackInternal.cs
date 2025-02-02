using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001CD RID: 461
	// (Invoke) Token: 0x06000CDE RID: 3294
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateReceivingCallbackInternal(ref UpdateReceivingCallbackInfoInternal data);
}
