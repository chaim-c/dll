using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000475 RID: 1141
	// (Invoke) Token: 0x06001D23 RID: 7459
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRejectInviteCallbackInternal(ref RejectInviteCallbackInfoInternal data);
}
