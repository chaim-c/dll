using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000278 RID: 632
	// (Invoke) Token: 0x06001149 RID: 4425
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteFileCompleteCallbackInternal(ref DeleteFileCallbackInfoInternal data);
}
