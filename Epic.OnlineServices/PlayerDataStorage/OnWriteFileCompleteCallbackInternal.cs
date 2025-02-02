using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000286 RID: 646
	// (Invoke) Token: 0x06001181 RID: 4481
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnWriteFileCompleteCallbackInternal(ref WriteFileCallbackInfoInternal data);
}
