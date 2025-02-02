using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200027A RID: 634
	// (Invoke) Token: 0x06001151 RID: 4433
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDuplicateFileCompleteCallbackInternal(ref DuplicateFileCallbackInfoInternal data);
}
