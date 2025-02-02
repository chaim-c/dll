using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B2 RID: 178
	// (Invoke) Token: 0x06000677 RID: 1655
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIngestStatCompleteCallbackInternal(ref IngestStatCompleteCallbackInfoInternal data);
}
