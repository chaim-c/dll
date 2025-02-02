using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000224 RID: 548
	// (Invoke) Token: 0x06000F48 RID: 3912
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteSnapshotCallbackInternal(ref DeleteSnapshotCallbackInfoInternal data);
}
