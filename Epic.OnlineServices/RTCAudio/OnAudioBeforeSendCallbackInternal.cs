using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001C1 RID: 449
	// (Invoke) Token: 0x06000CAE RID: 3246
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioBeforeSendCallbackInternal(ref AudioBeforeSendCallbackInfoInternal data);
}
