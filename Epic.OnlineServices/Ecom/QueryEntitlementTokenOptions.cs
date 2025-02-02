using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D9 RID: 1241
	public struct QueryEntitlementTokenOptions
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0002F543 File Offset: 0x0002D743
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x0002F54B File Offset: 0x0002D74B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0002F554 File Offset: 0x0002D754
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x0002F55C File Offset: 0x0002D75C
		public Utf8String[] EntitlementNames { get; set; }
	}
}
