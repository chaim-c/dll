using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000031 RID: 49
	// (Invoke) Token: 0x0600034E RID: 846
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoByDisplayNameCallbackInternal(ref QueryUserInfoByDisplayNameCallbackInfoInternal data);
}
