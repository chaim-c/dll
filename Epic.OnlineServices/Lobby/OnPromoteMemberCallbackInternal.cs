using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003D0 RID: 976
	// (Invoke) Token: 0x0600195A RID: 6490
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPromoteMemberCallbackInternal(ref PromoteMemberCallbackInfoInternal data);
}
