using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C7 RID: 1223
	// (Invoke) Token: 0x06001F85 RID: 8069
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryEntitlementsCallbackInternal(ref QueryEntitlementsCallbackInfoInternal data);
}
