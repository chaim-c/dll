using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000549 RID: 1353
	// (Invoke) Token: 0x060022AD RID: 8877
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLinkAccountCallbackInternal(ref LinkAccountCallbackInfoInternal data);
}
