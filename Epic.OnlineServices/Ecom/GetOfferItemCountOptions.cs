using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004BC RID: 1212
	public struct GetOfferItemCountOptions
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0002EB96 File Offset: 0x0002CD96
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0002EB9E File Offset: 0x0002CD9E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0002EBA7 File Offset: 0x0002CDA7
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0002EBAF File Offset: 0x0002CDAF
		public Utf8String OfferId { get; set; }
	}
}
