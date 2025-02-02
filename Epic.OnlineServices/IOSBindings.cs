using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Auth;

namespace Epic.OnlineServices
{
	// Token: 0x0200001A RID: 26
	public static class IOSBindings
	{
		// Token: 0x060002D2 RID: 722
		[DllImport("EOSSDK.dll")]
		internal static extern void EOS_Auth_Login(IntPtr handle, ref IOSLoginOptionsInternal options, IntPtr clientData, OnLoginCallbackInternal completionDelegate);
	}
}
