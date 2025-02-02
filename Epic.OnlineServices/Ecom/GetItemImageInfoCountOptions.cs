using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B2 RID: 1202
	public struct GetItemImageInfoCountOptions
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0002E831 File Offset: 0x0002CA31
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0002E839 File Offset: 0x0002CA39
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0002E842 File Offset: 0x0002CA42
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x0002E84A File Offset: 0x0002CA4A
		public Utf8String ItemId { get; set; }
	}
}
