using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A8 RID: 1192
	public struct CopyTransactionByIndexOptions
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0002D88A File Offset: 0x0002BA8A
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0002D892 File Offset: 0x0002BA92
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0002D89B File Offset: 0x0002BA9B
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x0002D8A3 File Offset: 0x0002BAA3
		public uint TransactionIndex { get; set; }
	}
}
