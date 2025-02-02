using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D5 RID: 1237
	public struct QueryEntitlementsOptions
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0002F1E2 File Offset: 0x0002D3E2
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0002F1EA File Offset: 0x0002D3EA
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0002F1F3 File Offset: 0x0002D3F3
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0002F1FB File Offset: 0x0002D3FB
		public Utf8String[] EntitlementNames { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0002F204 File Offset: 0x0002D404
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0002F20C File Offset: 0x0002D40C
		public bool IncludeRedeemed { get; set; }
	}
}
