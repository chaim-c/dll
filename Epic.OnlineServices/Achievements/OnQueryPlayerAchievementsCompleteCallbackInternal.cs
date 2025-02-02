using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200068D RID: 1677
	// (Invoke) Token: 0x06002B03 RID: 11011
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPlayerAchievementsCompleteCallbackInternal(ref OnQueryPlayerAchievementsCompleteCallbackInfoInternal data);
}
