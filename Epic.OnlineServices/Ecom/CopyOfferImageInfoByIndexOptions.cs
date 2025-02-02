using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A2 RID: 1186
	public struct CopyOfferImageInfoByIndexOptions
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0002D5A6 File Offset: 0x0002B7A6
		// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x0002D5AE File Offset: 0x0002B7AE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0002D5B7 File Offset: 0x0002B7B7
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x0002D5BF File Offset: 0x0002B7BF
		public Utf8String OfferId { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0002D5D0 File Offset: 0x0002B7D0
		public uint ImageInfoIndex { get; set; }
	}
}
