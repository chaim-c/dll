using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000689 RID: 1673
	// (Invoke) Token: 0x06002AEC RID: 10988
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryDefinitionsCompleteCallbackInternal(ref OnQueryDefinitionsCompleteCallbackInfoInternal data);
}
