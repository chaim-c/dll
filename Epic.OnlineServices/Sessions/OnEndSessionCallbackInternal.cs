using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F4 RID: 244
	// (Invoke) Token: 0x060007DE RID: 2014
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnEndSessionCallbackInternal(ref EndSessionCallbackInfoInternal data);
}
