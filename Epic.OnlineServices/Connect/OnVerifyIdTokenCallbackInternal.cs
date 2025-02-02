using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000557 RID: 1367
	// (Invoke) Token: 0x060022E5 RID: 8933
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnVerifyIdTokenCallbackInternal(ref VerifyIdTokenCallbackInfoInternal data);
}
