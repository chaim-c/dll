using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000280 RID: 640
	// (Invoke) Token: 0x06001169 RID: 4457
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryFileListCompleteCallbackInternal(ref QueryFileListCallbackInfoInternal data);
}
