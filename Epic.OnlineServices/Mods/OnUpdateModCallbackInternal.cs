using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000303 RID: 771
	// (Invoke) Token: 0x060014BF RID: 5311
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateModCallbackInternal(ref UpdateModCallbackInfoInternal data);
}
