using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000504 RID: 1284
	// (Invoke) Token: 0x0600210D RID: 8461
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCustomInviteRejectedCallbackInternal(ref CustomInviteRejectedCallbackInfoInternal data);
}
