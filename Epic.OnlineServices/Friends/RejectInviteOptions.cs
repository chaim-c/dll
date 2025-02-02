using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200047E RID: 1150
	public struct RejectInviteOptions
	{
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0002B547 File Offset: 0x00029747
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0002B54F File Offset: 0x0002974F
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0002B558 File Offset: 0x00029758
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0002B560 File Offset: 0x00029760
		public EpicAccountId TargetUserId { get; set; }
	}
}
