using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000100 RID: 256
	// (Invoke) Token: 0x06000806 RID: 2054
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRejectInviteCallbackInternal(ref RejectInviteCallbackInfoInternal data);
}
