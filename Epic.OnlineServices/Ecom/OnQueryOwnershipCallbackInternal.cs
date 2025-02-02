using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004CD RID: 1229
	// (Invoke) Token: 0x06001F9D RID: 8093
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryOwnershipCallbackInternal(ref QueryOwnershipCallbackInfoInternal data);
}
