using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000482 RID: 1154
	public struct SendInviteOptions
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0002B863 File Offset: 0x00029A63
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0002B86B File Offset: 0x00029A6B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0002B874 File Offset: 0x00029A74
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x0002B87C File Offset: 0x00029A7C
		public EpicAccountId TargetUserId { get; set; }
	}
}
