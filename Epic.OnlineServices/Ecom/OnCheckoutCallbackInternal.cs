using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C5 RID: 1221
	// (Invoke) Token: 0x06001F7D RID: 8061
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCheckoutCallbackInternal(ref CheckoutCallbackInfoInternal data);
}
