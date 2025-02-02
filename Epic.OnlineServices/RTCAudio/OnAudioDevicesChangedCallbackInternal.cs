using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001C3 RID: 451
	// (Invoke) Token: 0x06000CB6 RID: 3254
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioDevicesChangedCallbackInternal(ref AudioDevicesChangedCallbackInfoInternal data);
}
