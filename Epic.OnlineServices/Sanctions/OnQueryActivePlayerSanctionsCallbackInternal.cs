using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200016B RID: 363
	// (Invoke) Token: 0x06000A56 RID: 2646
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryActivePlayerSanctionsCallbackInternal(ref QueryActivePlayerSanctionsCallbackInfoInternal data);
}
