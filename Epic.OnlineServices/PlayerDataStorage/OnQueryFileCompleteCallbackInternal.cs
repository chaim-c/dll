using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200027E RID: 638
	// (Invoke) Token: 0x06001161 RID: 4449
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryFileCompleteCallbackInternal(ref QueryFileCallbackInfoInternal data);
}
