using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000541 RID: 1345
	// (Invoke) Token: 0x0600228D RID: 8845
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAuthExpirationCallbackInternal(ref AuthExpirationCallbackInfoInternal data);
}
