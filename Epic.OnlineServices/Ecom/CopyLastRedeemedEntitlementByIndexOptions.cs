using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049C RID: 1180
	public struct CopyLastRedeemedEntitlementByIndexOptions
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0002D363 File Offset: 0x0002B563
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0002D36B File Offset: 0x0002B56B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0002D374 File Offset: 0x0002B574
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0002D37C File Offset: 0x0002B57C
		public uint RedeemedEntitlementIndex { get; set; }
	}
}
