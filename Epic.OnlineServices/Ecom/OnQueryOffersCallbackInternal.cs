using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004CB RID: 1227
	// (Invoke) Token: 0x06001F95 RID: 8085
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryOffersCallbackInternal(ref QueryOffersCallbackInfoInternal data);
}
