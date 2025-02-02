using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Platform;

namespace Epic.OnlineServices
{
	// Token: 0x02000022 RID: 34
	public static class WindowsBindings
	{
		// Token: 0x060002FA RID: 762
		[DllImport("EOSSDK.dll")]
		internal static extern IntPtr EOS_Platform_Create(ref WindowsOptionsInternal options);
	}
}
