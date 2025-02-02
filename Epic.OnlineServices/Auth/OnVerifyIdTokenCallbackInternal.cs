using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200059E RID: 1438
	// (Invoke) Token: 0x060024C8 RID: 9416
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnVerifyIdTokenCallbackInternal(ref VerifyIdTokenCallbackInfoInternal data);
}
