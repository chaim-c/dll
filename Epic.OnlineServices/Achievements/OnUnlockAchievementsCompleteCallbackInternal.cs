using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000691 RID: 1681
	// (Invoke) Token: 0x06002B1E RID: 11038
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnlockAchievementsCompleteCallbackInternal(ref OnUnlockAchievementsCompleteCallbackInfoInternal data);
}
