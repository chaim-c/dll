using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200025B RID: 603
	// (Invoke) Token: 0x06001090 RID: 4240
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SetPresenceCompleteCallbackInternal(ref SetPresenceCallbackInfoInternal data);
}
