using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000035 RID: 53
	// (Invoke) Token: 0x0600035E RID: 862
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoCallbackInternal(ref QueryUserInfoCallbackInfoInternal data);
}
