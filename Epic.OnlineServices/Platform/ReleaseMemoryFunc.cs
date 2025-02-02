using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000658 RID: 1624
	// (Invoke) Token: 0x06002995 RID: 10645
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ReleaseMemoryFunc(IntPtr pointer);
}
