using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000438 RID: 1080
	// (Invoke) Token: 0x06001BC0 RID: 7104
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryAgeGateCallbackInternal(ref QueryAgeGateCallbackInfoInternal data);
}
