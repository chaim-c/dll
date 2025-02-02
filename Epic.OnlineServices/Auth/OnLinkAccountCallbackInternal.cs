using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000594 RID: 1428
	// (Invoke) Token: 0x060024A0 RID: 9376
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLinkAccountCallbackInternal(ref LinkAccountCallbackInfoInternal data);
}
