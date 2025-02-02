using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004BA RID: 1210
	public struct GetOfferImageInfoCountOptions
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0002EAC9 File Offset: 0x0002CCC9
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0002EAD1 File Offset: 0x0002CCD1
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0002EADA File Offset: 0x0002CCDA
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0002EAE2 File Offset: 0x0002CCE2
		public Utf8String OfferId { get; set; }
	}
}
