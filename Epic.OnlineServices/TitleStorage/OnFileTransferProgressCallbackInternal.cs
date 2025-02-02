using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008B RID: 139
	// (Invoke) Token: 0x0600056E RID: 1390
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnFileTransferProgressCallbackInternal(ref FileTransferProgressCallbackInfoInternal data);
}
