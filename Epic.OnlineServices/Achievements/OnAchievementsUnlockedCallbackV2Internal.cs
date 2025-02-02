using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000685 RID: 1669
	// (Invoke) Token: 0x06002ACD RID: 10957
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAchievementsUnlockedCallbackV2Internal(ref OnAchievementsUnlockedCallbackV2InfoInternal data);
}
