using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F6 RID: 246
	// (Invoke) Token: 0x060007E6 RID: 2022
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinSessionAcceptedCallbackInternal(ref JoinSessionAcceptedCallbackInfoInternal data);
}
