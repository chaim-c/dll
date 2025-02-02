using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005E RID: 94
	// (Invoke) Token: 0x06000459 RID: 1113
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnShowBlockPlayerCallbackInternal(ref OnShowBlockPlayerCallbackInfoInternal data);
}
