using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001C5 RID: 453
	// (Invoke) Token: 0x06000CBE RID: 3262
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioInputStateCallbackInternal(ref AudioInputStateCallbackInfoInternal data);
}
