using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000551 RID: 1361
	// (Invoke) Token: 0x060022CD RID: 8909
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryProductUserIdMappingsCallbackInternal(ref QueryProductUserIdMappingsCallbackInfoInternal data);
}
