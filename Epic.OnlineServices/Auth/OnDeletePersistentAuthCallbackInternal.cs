using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000592 RID: 1426
	// (Invoke) Token: 0x06002498 RID: 9368
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeletePersistentAuthCallbackInternal(ref DeletePersistentAuthCallbackInfoInternal data);
}
