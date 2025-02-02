using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F2 RID: 242
	// (Invoke) Token: 0x060007D6 RID: 2006
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDestroySessionCallbackInternal(ref DestroySessionCallbackInfoInternal data);
}
