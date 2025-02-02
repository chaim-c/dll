using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000477 RID: 1143
	// (Invoke) Token: 0x06001D2B RID: 7467
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendInviteCallbackInternal(ref SendInviteCallbackInfoInternal data);
}
