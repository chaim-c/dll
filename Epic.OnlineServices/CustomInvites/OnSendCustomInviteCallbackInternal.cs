using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000506 RID: 1286
	// (Invoke) Token: 0x06002115 RID: 8469
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendCustomInviteCallbackInternal(ref SendCustomInviteCallbackInfoInternal data);
}
