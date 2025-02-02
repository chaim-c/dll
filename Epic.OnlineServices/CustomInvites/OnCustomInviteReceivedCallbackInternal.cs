using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000500 RID: 1280
	// (Invoke) Token: 0x060020EA RID: 8426
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCustomInviteReceivedCallbackInternal(ref OnCustomInviteReceivedCallbackInfoInternal data);
}
