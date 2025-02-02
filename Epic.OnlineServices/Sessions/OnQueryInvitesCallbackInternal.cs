using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FC RID: 252
	// (Invoke) Token: 0x060007F6 RID: 2038
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryInvitesCallbackInternal(ref QueryInvitesCallbackInfoInternal data);
}
