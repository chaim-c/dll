using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200043E RID: 1086
	// (Invoke) Token: 0x06001BD8 RID: 7128
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateParentEmailCallbackInternal(ref UpdateParentEmailCallbackInfoInternal data);
}
