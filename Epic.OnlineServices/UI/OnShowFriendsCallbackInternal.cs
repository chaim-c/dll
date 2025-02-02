using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000062 RID: 98
	// (Invoke) Token: 0x06000478 RID: 1144
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnShowFriendsCallbackInternal(ref ShowFriendsCallbackInfoInternal data);
}
