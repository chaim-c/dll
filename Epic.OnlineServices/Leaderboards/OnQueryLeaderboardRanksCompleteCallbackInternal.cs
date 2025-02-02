using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000416 RID: 1046
	// (Invoke) Token: 0x06001AFA RID: 6906
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardRanksCompleteCallbackInternal(ref OnQueryLeaderboardRanksCompleteCallbackInfoInternal data);
}
