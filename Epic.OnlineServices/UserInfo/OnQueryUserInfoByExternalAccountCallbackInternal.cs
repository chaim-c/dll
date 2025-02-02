using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000033 RID: 51
	// (Invoke) Token: 0x06000356 RID: 854
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoByExternalAccountCallbackInternal(ref QueryUserInfoByExternalAccountCallbackInfoInternal data);
}
