using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005C RID: 92
	// (Invoke) Token: 0x06000451 RID: 1105
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnHideFriendsCallbackInternal(ref HideFriendsCallbackInfoInternal data);
}
