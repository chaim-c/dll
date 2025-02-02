using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A6 RID: 1190
	public struct CopyTransactionByIdOptions
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0002D7BB File Offset: 0x0002B9BB
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0002D7CC File Offset: 0x0002B9CC
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		public Utf8String TransactionId { get; set; }
	}
}
