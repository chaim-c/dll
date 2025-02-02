using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000490 RID: 1168
	public struct CopyEntitlementByIdOptions
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0002CDEB File Offset: 0x0002AFEB
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0002CDF3 File Offset: 0x0002AFF3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0002CDFC File Offset: 0x0002AFFC
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0002CE04 File Offset: 0x0002B004
		public Utf8String EntitlementId { get; set; }
	}
}
