using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048E RID: 1166
	public struct CheckoutOptions
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0002CCC7 File Offset: 0x0002AEC7
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0002CCCF File Offset: 0x0002AECF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public Utf8String OverrideCatalogNamespace { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0002CCE9 File Offset: 0x0002AEE9
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0002CCF1 File Offset: 0x0002AEF1
		public CheckoutEntry[] Entries { get; set; }
	}
}
