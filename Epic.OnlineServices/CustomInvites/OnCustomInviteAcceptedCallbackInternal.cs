using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004FC RID: 1276
	// (Invoke) Token: 0x060020C7 RID: 8391
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCustomInviteAcceptedCallbackInternal(ref OnCustomInviteAcceptedCallbackInfoInternal data);
}
