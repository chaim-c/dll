using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200054B RID: 1355
	// (Invoke) Token: 0x060022B5 RID: 8885
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLoginCallbackInternal(ref LoginCallbackInfoInternal data);
}
