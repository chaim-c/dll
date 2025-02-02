using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008F RID: 143
	// (Invoke) Token: 0x0600057E RID: 1406
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryFileListCompleteCallbackInternal(ref QueryFileListCallbackInfoInternal data);
}
