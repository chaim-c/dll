using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A0 RID: 1440
	// (Invoke) Token: 0x060024D0 RID: 9424
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnVerifyUserAuthCallbackInternal(ref VerifyUserAuthCallbackInfoInternal data);
}
