using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000241 RID: 577
	// (Invoke) Token: 0x06000FFA RID: 4090
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPresenceChangedCallbackInternal(ref PresenceChangedCallbackInfoInternal data);
}
