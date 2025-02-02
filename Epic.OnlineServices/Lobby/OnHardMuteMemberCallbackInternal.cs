using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B8 RID: 952
	// (Invoke) Token: 0x060018FA RID: 6394
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnHardMuteMemberCallbackInternal(ref HardMuteMemberCallbackInfoInternal data);
}
