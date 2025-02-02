using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000089 RID: 137
	// (Invoke) Token: 0x06000566 RID: 1382
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteCacheCompleteCallbackInternal(ref DeleteCacheCallbackInfoInternal data);
}
