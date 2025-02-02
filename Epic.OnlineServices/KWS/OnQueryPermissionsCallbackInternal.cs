using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200043A RID: 1082
	// (Invoke) Token: 0x06001BC8 RID: 7112
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPermissionsCallbackInternal(ref QueryPermissionsCallbackInfoInternal data);
}
