using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E1 RID: 1249
	public struct QueryOwnershipOptions
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0002FB0F File Offset: 0x0002DD0F
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x0002FB17 File Offset: 0x0002DD17
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0002FB20 File Offset: 0x0002DD20
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x0002FB28 File Offset: 0x0002DD28
		public Utf8String[] CatalogItemIds { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0002FB31 File Offset: 0x0002DD31
		// (set) Token: 0x06002027 RID: 8231 RVA: 0x0002FB39 File Offset: 0x0002DD39
		public Utf8String CatalogNamespace { get; set; }
	}
}
