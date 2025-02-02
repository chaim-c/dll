using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000543 RID: 1347
	// (Invoke) Token: 0x06002295 RID: 8853
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateDeviceIdCallbackInternal(ref CreateDeviceIdCallbackInfoInternal data);
}
