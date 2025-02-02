using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000243 RID: 579
	// (Invoke) Token: 0x06001002 RID: 4098
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPresenceCompleteCallbackInternal(ref QueryPresenceCallbackInfoInternal data);
}
