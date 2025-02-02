using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DD RID: 1245
	public struct QueryOffersOptions
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0002F7E6 File Offset: 0x0002D9E6
		// (set) Token: 0x06002003 RID: 8195 RVA: 0x0002F7EE File Offset: 0x0002D9EE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0002F7F7 File Offset: 0x0002D9F7
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x0002F7FF File Offset: 0x0002D9FF
		public Utf8String OverrideCatalogNamespace { get; set; }
	}
}
