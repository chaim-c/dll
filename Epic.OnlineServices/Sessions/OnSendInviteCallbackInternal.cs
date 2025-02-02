using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000102 RID: 258
	// (Invoke) Token: 0x0600080E RID: 2062
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendInviteCallbackInternal(ref SendInviteCallbackInfoInternal data);
}
