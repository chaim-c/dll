using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000276 RID: 630
	// (Invoke) Token: 0x06001141 RID: 4417
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteCacheCompleteCallbackInternal(ref DeleteCacheCallbackInfoInternal data);
}
