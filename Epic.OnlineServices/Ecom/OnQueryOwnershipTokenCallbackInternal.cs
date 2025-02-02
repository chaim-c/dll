using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004CF RID: 1231
	// (Invoke) Token: 0x06001FA5 RID: 8101
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryOwnershipTokenCallbackInternal(ref QueryOwnershipTokenCallbackInfoInternal data);
}
