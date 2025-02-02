using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D1 RID: 1233
	// (Invoke) Token: 0x06001FAD RID: 8109
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRedeemEntitlementsCallbackInternal(ref RedeemEntitlementsCallbackInfoInternal data);
}
