using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000657 RID: 1623
	// (Invoke) Token: 0x06002991 RID: 10641
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr ReallocateMemoryFunc(IntPtr pointer, UIntPtr sizeInBytes, UIntPtr alignment);
}
