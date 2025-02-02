using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E5 RID: 1253
	public struct QueryOwnershipTokenOptions
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0002FE7F File Offset: 0x0002E07F
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x0002FE87 File Offset: 0x0002E087
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0002FE90 File Offset: 0x0002E090
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x0002FE98 File Offset: 0x0002E098
		public Utf8String[] CatalogItemIds { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0002FEA1 File Offset: 0x0002E0A1
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x0002FEA9 File Offset: 0x0002E0A9
		public Utf8String CatalogNamespace { get; set; }
	}
}
