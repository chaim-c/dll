using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000596 RID: 1430
	// (Invoke) Token: 0x060024A8 RID: 9384
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLoginCallbackInternal(ref LoginCallbackInfoInternal data);
}
