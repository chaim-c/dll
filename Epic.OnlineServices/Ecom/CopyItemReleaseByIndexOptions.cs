using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049A RID: 1178
	public struct CopyItemReleaseByIndexOptions
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0002D257 File Offset: 0x0002B457
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0002D25F File Offset: 0x0002B45F
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0002D268 File Offset: 0x0002B468
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x0002D270 File Offset: 0x0002B470
		public Utf8String ItemId { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0002D279 File Offset: 0x0002B479
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0002D281 File Offset: 0x0002B481
		public uint ReleaseIndex { get; set; }
	}
}
