using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C9 RID: 1225
	// (Invoke) Token: 0x06001F8D RID: 8077
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryEntitlementTokenCallbackInternal(ref QueryEntitlementTokenCallbackInfoInternal data);
}
