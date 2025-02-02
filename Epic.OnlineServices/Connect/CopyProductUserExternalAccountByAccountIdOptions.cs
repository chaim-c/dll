using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000516 RID: 1302
	public struct CopyProductUserExternalAccountByAccountIdOptions
	{
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x00032251 File Offset: 0x00030451
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x00032259 File Offset: 0x00030459
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x00032262 File Offset: 0x00030462
		// (set) Token: 0x06002186 RID: 8582 RVA: 0x0003226A File Offset: 0x0003046A
		public Utf8String AccountId { get; set; }
	}
}
