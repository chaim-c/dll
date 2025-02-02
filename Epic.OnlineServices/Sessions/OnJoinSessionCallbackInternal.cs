using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F8 RID: 248
	// (Invoke) Token: 0x060007EE RID: 2030
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinSessionCallbackInternal(ref JoinSessionCallbackInfoInternal data);
}
