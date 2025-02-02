using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000681 RID: 1665
	// (Invoke) Token: 0x06002AB2 RID: 10930
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAchievementsUnlockedCallbackInternal(ref OnAchievementsUnlockedCallbackInfoInternal data);
}
