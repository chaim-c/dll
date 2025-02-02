using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000282 RID: 642
	// (Invoke) Token: 0x06001171 RID: 4465
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnReadFileCompleteCallbackInternal(ref ReadFileCallbackInfoInternal data);
}
