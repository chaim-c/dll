using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200043C RID: 1084
	// (Invoke) Token: 0x06001BD0 RID: 7120
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRequestPermissionsCallbackInternal(ref RequestPermissionsCallbackInfoInternal data);
}
