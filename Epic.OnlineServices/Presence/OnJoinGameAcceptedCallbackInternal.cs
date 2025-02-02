using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200023F RID: 575
	// (Invoke) Token: 0x06000FF2 RID: 4082
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinGameAcceptedCallbackInternal(ref JoinGameAcceptedCallbackInfoInternal data);
}
