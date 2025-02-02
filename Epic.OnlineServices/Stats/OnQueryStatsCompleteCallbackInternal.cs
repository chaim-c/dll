using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B4 RID: 180
	// (Invoke) Token: 0x0600067F RID: 1663
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryStatsCompleteCallbackInternal(ref OnQueryStatsCompleteCallbackInfoInternal data);
}
