using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000226 RID: 550
	// (Invoke) Token: 0x06000F50 RID: 3920
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSubmitSnapshotCallbackInternal(ref SubmitSnapshotCallbackInfoInternal data);
}
