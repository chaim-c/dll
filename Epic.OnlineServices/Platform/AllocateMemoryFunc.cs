using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000646 RID: 1606
	// (Invoke) Token: 0x0600290E RID: 10510
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr AllocateMemoryFunc(UIntPtr sizeInBytes, UIntPtr alignment);
}
