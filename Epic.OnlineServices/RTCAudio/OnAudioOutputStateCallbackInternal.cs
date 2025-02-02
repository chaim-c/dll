using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001C7 RID: 455
	// (Invoke) Token: 0x06000CC6 RID: 3270
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioOutputStateCallbackInternal(ref AudioOutputStateCallbackInfoInternal data);
}
