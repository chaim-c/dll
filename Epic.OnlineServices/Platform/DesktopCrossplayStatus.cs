using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200064A RID: 1610
	public enum DesktopCrossplayStatus
	{
		// Token: 0x04001288 RID: 4744
		Ok,
		// Token: 0x04001289 RID: 4745
		ApplicationNotBootstrapped,
		// Token: 0x0400128A RID: 4746
		ServiceNotInstalled,
		// Token: 0x0400128B RID: 4747
		ServiceStartFailed,
		// Token: 0x0400128C RID: 4748
		ServiceNotRunning,
		// Token: 0x0400128D RID: 4749
		OverlayDisabled,
		// Token: 0x0400128E RID: 4750
		OverlayNotInstalled,
		// Token: 0x0400128F RID: 4751
		OverlayTrustCheckFailed,
		// Token: 0x04001290 RID: 4752
		OverlayLoadFailed
	}
}
