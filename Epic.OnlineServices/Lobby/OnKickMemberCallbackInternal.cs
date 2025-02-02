using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003C0 RID: 960
	// (Invoke) Token: 0x0600191A RID: 6426
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnKickMemberCallbackInternal(ref KickMemberCallbackInfoInternal data);
}
