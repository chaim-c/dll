using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008D RID: 141
	// (Invoke) Token: 0x06000576 RID: 1398
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryFileCompleteCallbackInternal(ref QueryFileCallbackInfoInternal data);
}
