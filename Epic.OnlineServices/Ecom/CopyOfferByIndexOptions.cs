using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A0 RID: 1184
	public struct CopyOfferByIndexOptions
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0002D4EA File Offset: 0x0002B6EA
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0002D4F2 File Offset: 0x0002B6F2
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0002D4FB File Offset: 0x0002B6FB
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0002D503 File Offset: 0x0002B703
		public uint OfferIndex { get; set; }
	}
}
