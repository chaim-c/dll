using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000494 RID: 1172
	public struct CopyEntitlementByNameAndIndexOptions
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0002CF76 File Offset: 0x0002B176
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x0002CF7E File Offset: 0x0002B17E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0002CF87 File Offset: 0x0002B187
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0002CF8F File Offset: 0x0002B18F
		public Utf8String EntitlementName { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0002CF98 File Offset: 0x0002B198
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0002CFA0 File Offset: 0x0002B1A0
		public uint Index { get; set; }
	}
}
