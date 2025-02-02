using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Platform;

namespace Epic.OnlineServices
{
	// Token: 0x02000011 RID: 17
	public static class AndroidBindings
	{
		// Token: 0x0600009A RID: 154
		[DllImport("EOSSDK.dll")]
		internal static extern Result EOS_Initialize(ref AndroidInitializeOptionsInternal options);
	}
}
