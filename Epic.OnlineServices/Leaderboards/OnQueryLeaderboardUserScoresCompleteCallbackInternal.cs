using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041A RID: 1050
	// (Invoke) Token: 0x06001B11 RID: 6929
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardUserScoresCompleteCallbackInternal(ref OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal data);
}
