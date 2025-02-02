using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049E RID: 1182
	public struct CopyOfferByIdOptions
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0002D41E File Offset: 0x0002B61E
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0002D426 File Offset: 0x0002B626
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0002D42F File Offset: 0x0002B62F
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0002D437 File Offset: 0x0002B637
		public Utf8String OfferId { get; set; }
	}
}
