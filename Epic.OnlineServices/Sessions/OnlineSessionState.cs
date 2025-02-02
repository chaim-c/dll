using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FA RID: 250
	public enum OnlineSessionState
	{
		// Token: 0x040003BB RID: 955
		NoSession,
		// Token: 0x040003BC RID: 956
		Creating,
		// Token: 0x040003BD RID: 957
		Pending,
		// Token: 0x040003BE RID: 958
		Starting,
		// Token: 0x040003BF RID: 959
		InProgress,
		// Token: 0x040003C0 RID: 960
		Ending,
		// Token: 0x040003C1 RID: 961
		Ended,
		// Token: 0x040003C2 RID: 962
		Destroying
	}
}
