using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B4 RID: 1204
	public struct GetItemReleaseCountOptions
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0002E8FE File Offset: 0x0002CAFE
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0002E906 File Offset: 0x0002CB06
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0002E90F File Offset: 0x0002CB0F
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0002E917 File Offset: 0x0002CB17
		public Utf8String ItemId { get; set; }
	}
}
