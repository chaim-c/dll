using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001BF RID: 447
	// (Invoke) Token: 0x06000CA6 RID: 3238
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioBeforeRenderCallbackInternal(ref AudioBeforeRenderCallbackInfoInternal data);
}
