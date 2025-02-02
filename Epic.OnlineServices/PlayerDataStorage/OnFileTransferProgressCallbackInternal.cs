using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200027C RID: 636
	// (Invoke) Token: 0x06001159 RID: 4441
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnFileTransferProgressCallbackInternal(ref FileTransferProgressCallbackInfoInternal data);
}
