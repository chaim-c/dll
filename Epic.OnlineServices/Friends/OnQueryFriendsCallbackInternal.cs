using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000473 RID: 1139
	// (Invoke) Token: 0x06001D1B RID: 7451
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryFriendsCallbackInternal(ref QueryFriendsCallbackInfoInternal data);
}
