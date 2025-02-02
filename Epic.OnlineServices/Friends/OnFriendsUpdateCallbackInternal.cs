using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200046F RID: 1135
	// (Invoke) Token: 0x06001CF8 RID: 7416
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnFriendsUpdateCallbackInternal(ref OnFriendsUpdateInfoInternal data);
}
