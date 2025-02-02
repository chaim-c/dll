using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002FD RID: 765
	// (Invoke) Token: 0x060014A7 RID: 5287
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnEnumerateModsCallbackInternal(ref EnumerateModsCallbackInfoInternal data);
}
