using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000555 RID: 1365
	// (Invoke) Token: 0x060022DD RID: 8925
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnlinkAccountCallbackInternal(ref UnlinkAccountCallbackInfoInternal data);
}
