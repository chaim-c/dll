using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000412 RID: 1042
	// (Invoke) Token: 0x06001AE3 RID: 6883
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardDefinitionsCompleteCallbackInternal(ref OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal data);
}
