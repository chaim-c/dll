using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003D2 RID: 978
	// (Invoke) Token: 0x06001962 RID: 6498
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryInvitesCallbackInternal(ref QueryInvitesCallbackInfoInternal data);
}
