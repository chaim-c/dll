using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000301 RID: 769
	// (Invoke) Token: 0x060014B7 RID: 5303
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUninstallModCallbackInternal(ref UninstallModCallbackInfoInternal data);
}
