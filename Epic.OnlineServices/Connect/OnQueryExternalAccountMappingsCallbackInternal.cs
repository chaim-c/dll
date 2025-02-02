using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200054F RID: 1359
	// (Invoke) Token: 0x060022C5 RID: 8901
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryExternalAccountMappingsCallbackInternal(ref QueryExternalAccountMappingsCallbackInfoInternal data);
}
