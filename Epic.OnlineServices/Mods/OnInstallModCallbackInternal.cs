using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002FF RID: 767
	// (Invoke) Token: 0x060014AF RID: 5295
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnInstallModCallbackInternal(ref InstallModCallbackInfoInternal data);
}
