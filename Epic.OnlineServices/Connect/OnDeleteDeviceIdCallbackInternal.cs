using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000547 RID: 1351
	// (Invoke) Token: 0x060022A5 RID: 8869
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteDeviceIdCallbackInternal(ref DeleteDeviceIdCallbackInfoInternal data);
}
