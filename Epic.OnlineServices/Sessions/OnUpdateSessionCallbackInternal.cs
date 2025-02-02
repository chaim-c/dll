using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010C RID: 268
	// (Invoke) Token: 0x06000836 RID: 2102
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateSessionCallbackInternal(ref UpdateSessionCallbackInfoInternal data);
}
