using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000284 RID: 644
	// (Invoke) Token: 0x06001179 RID: 4473
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate ReadResult OnReadFileDataCallbackInternal(ref ReadFileDataCallbackInfoInternal data);
}
