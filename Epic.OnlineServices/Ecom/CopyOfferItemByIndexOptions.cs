using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A4 RID: 1188
	public struct CopyOfferItemByIndexOptions
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0002D6AF File Offset: 0x0002B8AF
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0002D6B7 File Offset: 0x0002B8B7
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0002D6C0 File Offset: 0x0002B8C0
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		public Utf8String OfferId { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0002D6D1 File Offset: 0x0002B8D1
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0002D6D9 File Offset: 0x0002B8D9
		public uint ItemIndex { get; set; }
	}
}
